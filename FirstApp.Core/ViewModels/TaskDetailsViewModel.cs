//using Android.Widget;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskModel>, IFileListHandler
    {
        // public List<FileListModel> _fileNameList = new List<FileListModel> { };
        public TaskModel taskModel = new TaskModel();
        public int TaskId;
        private readonly IDBFileNameService _dBFileNameService;
        private readonly IDBMapMarkerService _dBMapMarkerService;
        private readonly IDBTaskService _dBTaskService;
        private readonly IUserDialogService _userDialogService;
        public TaskDetailsViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService, IDBMapMarkerService dBMapMarkerService,
            IUserDialogService userDialogService, IDBFileNameService dBFileNameService) : base(navigationService)
        {
            _dBMapMarkerService = dBMapMarkerService;
            _userDialogService = userDialogService;
            _dBTaskService = dBTaskService;
            _dBFileNameService = dBFileNameService;

            DeleteFileItemCommand = new MvxCommand<int>(RemoveCollectionItem);

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
                AddFileName();
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
                MapMarkers = _dBMapMarkerService.GetMapMarkerFromDB(TaskId).Count.ToString();
                AddFileName();
                return;
            }
            TaskName = null;
            TaskDescription = null;
            MapMarkers = null;
            _dBTaskService.AddTaskToTable(taskModel);
            TaskId = taskModel.Id;
        }

        public void AddFileName()
        {

            FileNameList = new MvxObservableCollection<FileListModel>() { };
            List<FileListModel> list = GetFileNameListFromDB(TaskId);
            foreach (var item in list)
            {
                item.VmHandler = this;
            }
            FileNameList.AddRange(list);
        }

        public MvxObservableCollection<FileListModel> _fileNameList;
        public MvxObservableCollection<FileListModel> FileNameList
        {
            get => _fileNameList;
            set
            {
                _fileNameList = value;
                RaisePropertyChanged(() => FileNameList);
            }
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

        // ListView listView = new ListView(){ };

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
                });
            }
        }

        public MvxAsyncCommand AddMarkerCommand
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
                    if (answ)
                    {
                        SaveTask.Execute();
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
        public List<FileListModel> GetFileNameListFromDB(int taskId)
        {

            List<FileListModel> list = _dBFileNameService.GetFileNameFromDB(taskId);
            return list;
        }

        public void SaveFileName(string name)
        {
            FileListModel item = new FileListModel();
            item.TaskId = TaskId;
            item.FileName = name;
            item.VmHandler = this;
            _fileNameList.Add(item);
            _dBFileNameService.AddFileNameToTable(item);
        }

        public void RemoveCollectionItem(int itemId)
        {
            _dBFileNameService.DeleteFileName(itemId);

            FileListModel _itemForDelete = null;

            foreach (FileListModel item in FileNameList)
            {
                if (item.Id == itemId)
                {
                    _itemForDelete = item;
                    break;
                }
            }
            FileNameList.Remove(item: _itemForDelete);
        }
        public IMvxCommand<int> DeleteFileItemCommand { get; set; }

        public override void ViewAppearing()
        {
            MapMarkers = _dBMapMarkerService.GetMapMarkerFromDB(TaskId).Count.ToString();
        }
    }
}
