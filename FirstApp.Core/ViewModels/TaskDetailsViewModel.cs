using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskModel>
    {
        public TaskModel taskModel = new TaskModel();
        public int TaskId;
        private readonly IDBMapMarkerService _dBMapMarkerService;
        private readonly IDBTaskService _dBTaskService;
        private readonly IUserDialogService _userDialogService;
        public TaskDetailsViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService, IDBMapMarkerService dBMapMarkerService,
            IUserDialogService userDialogService) : base(navigationService)
        {
            _dBMapMarkerService = dBMapMarkerService;
            _userDialogService = userDialogService;
            _dBTaskService = dBTaskService;
           
            SaveButton = true;
            HaveGone = false;

        }

        public override async Task Initialize()
        {
            await base.Initialize();
            if (TaskId == 0)
            {
                TaskName = null;
                TaskDescription = null;
                MapMarkers = null;
                _dBTaskService.AddTaskToTable(taskModel);
                TaskId = taskModel.Id;
            }
        }

        public override void Prepare(TaskModel parametr)
        {
            if (parametr != null)
            {
                taskModel.Id = parametr.Id;
                TaskId = parametr.Id;
                TaskName = parametr.TaskName;
                TaskDescription = parametr.TaskDescription;
                FileName = parametr.FileName;
                MapMarkers = _dBMapMarkerService.GetMapMarkerFromDB(TaskId).Count.ToString();
                return;
            }
            TaskName = null;
            TaskDescription = null;
            MapMarkers = null;
            _dBTaskService.AddTaskToTable(taskModel);
            TaskId = taskModel.Id;
        }

        private bool _saveButton;
        public bool SaveButton
        {
            get => _saveButton;
            set
            {
                _saveButton = value;
            }
        }

        private bool _haveGone;
        public bool HaveGone
        {
            get => _haveGone;
            set
            {
                _haveGone = value;
            }
        }

        private string _taskName;
        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                taskModel.TaskName = _taskName;
                RaisePropertyChanged(() => TaskName);
            }
        }

        private string _taskDescription;
        public string TaskDescription
        {
            get => _taskDescription;
            set
            {
                _taskDescription = value;
                taskModel.TaskDescription = _taskDescription;
                RaisePropertyChanged(() => TaskDescription);
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                taskModel.FileName = _fileName;
                RaisePropertyChanged(() => FileName);
            }
        }

        private string _mapMarkers;
        public string MapMarkers
        {
            get => _mapMarkers;
            set
            {
                _mapMarkers = value;
                RaisePropertyChanged(() => MapMarkers);
            }
        }

        public MvxAsyncCommand AddFile
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                });
            }
        }
        public MvxAsyncCommand AddMarker
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    
                    var result = await _navigationService.Navigate<MapViewModel, TaskModel>(taskModel);
                });
            }
        }

        public MvxAsyncCommand SaveTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {

                    if (string.IsNullOrEmpty(TaskName))
                    {
                        _userDialogService.ShowAlertForUser("Empty task name", "Please, enter task name", "Ok");
                        return;
                    }
                    if (string.IsNullOrEmpty(TaskDescription))
                    {
                        _userDialogService.ShowAlertForUser("Empty task description", "Please, enter task name", "Ok");
                        return;
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName))
                    {
                        //taskModel.TaskName = TaskName;
                        //taskModel.TaskDescription = TaskDescription;
                        _dBTaskService.AddTaskToTable(taskModel);
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public MvxAsyncCommand BackCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {

                    var answ = await _userDialogService.ShowAlertForUserWithSomeLogic("Save Markers?", "Do you want to save your markers?", "Yes", "No");
                    //await _navigationService.Close(this);
                    if (answ)
                    {
                        SaveTask.Execute();
                        await _navigationService.Close(this);
                    }
                    if (!answ)
                    {
                        await _navigationService.Close(this);
                    }

                });
            }
        }

        public MvxAsyncCommand DeleteTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    var answ = await _userDialogService.ShowAlertForUserWithSomeLogic("Delete Task?", "Do you want to delete this task?", "Yes", "No");

                    if (answ)
                    {
                        _dBTaskService.DeleteTaskFromTable(TaskId);
                        await _navigationService.Navigate<TaskListViewModel>();

                        //await _navigationService.Close(this);
                    }
                    if (!answ)
                    {
                        // await _navigationService.Close(this);
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public void SaveFileNameInModel(string name)
        {
            taskModel.FileName += name;
        }

        public override void ViewAppearing()
        {
            MapMarkers = _dBMapMarkerService.GetMapMarkerFromDB(TaskId).Count.ToString();
        }
    }
}
