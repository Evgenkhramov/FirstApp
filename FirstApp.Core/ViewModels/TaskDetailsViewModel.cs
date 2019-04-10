using Android.Content;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskModel>
    {
        public TaskModel taskModel = new TaskModel();

        private readonly IDBTaskService _dBTaskService;
        private readonly IUserDialogService _userDialogService;
        public TaskDetailsViewModel(IMvxNavigationService navigationService,IDBTaskService dBTaskService, IUserDialogService userDialogService) : base(navigationService)
        {

            _userDialogService = userDialogService;
            _dBTaskService = dBTaskService;
        }

        public override async Task Initialize()
        {
            await base.Initialize();

        }

        public override void Prepare(TaskModel parametr)
        {
            if (parametr != null)
            {
                TaskName = parametr.TaskName;
                TaskDescription = parametr.TaskDescription;
                return;
                //FileName = GetStringFromMassive(parametr.FileName);
            }
            TaskName = null;
            TaskDescription = null;
            MapMarkers = null;
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
            }
        }

        public MvxAsyncCommand AddMarker
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {                
                   
                });
            }
        }

        public MvxAsyncCommand SaveTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    var taskModel = new TaskModel { };
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

        public MvxAsyncCommand CancelTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    //_dBTaskService.DeleteTaskFromTable();
                });
            }
        }

        public string GetStringFromMassive(string[] element)
        {
            string result = null;
            foreach (string elem in element)
            {
                result += elem + "\n";
            }
            return result;
        }

        public void SaveFileNameInModel(string name)
        {
            taskModel.FileName += name;
        }
                        
    }
}
