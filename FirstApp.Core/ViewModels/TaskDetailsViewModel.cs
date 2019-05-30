using Acr.UserDialogs;
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
        #region Variables

        private readonly IUserDialogs _userDialogs;
        private CurrentPlatformType _platform;
        private TaskModel _thisTaskModel;
        public static List<MapMarkerModel> MapMarkerList;
        private int _taskId;
        private readonly IDBFileNameService _dBFileNameService;
        private readonly IDBMapMarkerService _dBMapMarkerService;
        private readonly IDBTaskService _dBTaskService;
        private readonly ICurrentPlatformService _getCurrentPlatformService;

        #endregion Variables

        #region Constructors

        public TaskDetailsViewModel(ICurrentPlatformService getCurrentPlatformService, IMvxNavigationService navigationService,
            IDBTaskService dBTaskService, IDBMapMarkerService dBMapMarkerService, IDBFileNameService dBFileNameService,
            IUserDialogs userDialogs) : base(navigationService)
        {
            _userDialogs = userDialogs;
            _getCurrentPlatformService = getCurrentPlatformService;
            _platform = _getCurrentPlatformService.GetCurrentPlatform();
            _dBMapMarkerService = dBMapMarkerService;
            _dBTaskService = dBTaskService;
            _dBFileNameService = dBFileNameService;

            _thisTaskModel = new TaskModel();

            MapMarkerList = new List<MapMarkerModel>();

            DeleteFileItemCommand = new MvxCommand<int>(RemoveFileCollectionItem);

            SaveButton = true;
            HaveGone = false;
        }

        #endregion Constructors

        #region Properties

        public bool SaveButton { get; set; }

        public bool HaveGone { get; set; }

        private string _taskName;
        public string TaskName
        {
            get => _taskName;
            set
            {
                _taskName = value;
                _thisTaskModel.TaskName = _taskName;
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
                _thisTaskModel.TaskDescription = _taskDescription;
                RaisePropertyChanged(() => TaskDescription);
            }
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

        private string _mapMarkers;
        public string MarkersCounter
        {
            get => _mapMarkers;
            set
            {
                _mapMarkers = value;
                RaisePropertyChanged(() => MarkersCounter);
            }
        }

        #endregion Properties

        #region Commands  

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
                    var result = await _navigationService.Navigate<MapViewModel, TaskModel>(_thisTaskModel);
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
                        _userDialogs.Alert(Constants.EnterTaskName, Constants.EmptyTaskName, Strings.Ok);
                        return;
                    }
                    if (string.IsNullOrEmpty(TaskDescription))
                    {
                        _userDialogs.Alert(Constants.EnterTaskDescription, Constants.EmptyTaskDescription, Strings.Ok);

                        return;
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName) && _platform == CurrentPlatformType.Android)
                    {
                        _dBTaskService.AddTaskToTable(_thisTaskModel);

                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName) && _platform == CurrentPlatformType.iOS)
                    {
                        SaveDataToDB(_thisTaskModel, MapMarkerList, FileNameList);

                        await _navigationService.Close(this);
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
                    bool answ = await _userDialogs.ConfirmAsync(Constants.WantSaveTask, Constants.SaveTask, Strings.Yes , Strings.No);

                    if (answ)
                    {
                        SaveTask.Execute();
                        return;
                    }
                    await _navigationService.Close(this);
                });
            }
        }

        public MvxAsyncCommand DeleteTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    bool answ = await _userDialogs.ConfirmAsync(Constants.WantDeleteTask, Constants.DeleteTask, Strings.Yes, Strings.No);

                    if (answ && _platform == CurrentPlatformType.iOS)
                    {
                        _dBFileNameService.DeleteFiles(_taskId);
                        _dBMapMarkerService.DeleteMarkers(_taskId);
                        _dBTaskService.DeleteTaskFromTable(_taskId);
                        await _navigationService.Close(this);
                    }
                    if (answ && _platform == CurrentPlatformType.Android)
                    {
                        _dBFileNameService.DeleteFiles(_taskId);
                        _dBMapMarkerService.DeleteMarkers(_taskId);
                        _dBTaskService.DeleteTaskFromTable(_taskId);
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public IMvxCommand<int> DeleteFileItemCommand { get; set; }

        #endregion Commands

        #region Methods

        public void AddFileName()
        {
            FileNameList = new MvxObservableCollection<FileListModel>() { };

            List<FileListModel> list = GetFileNameListFromDB(_taskId);

            foreach (var item in list)
            {
                item.VmHandler = this;
            }

            FileNameList.AddRange(list);
        }

        private void SaveDataToDB(TaskModel task, List<MapMarkerModel> mapMarkerList, MvxObservableCollection<FileListModel> fileNameList)
        {
            _dBTaskService.AddTaskToTable(_thisTaskModel);

            _taskId = _thisTaskModel.Id;

            if (mapMarkerList.Count > 0)
            {
                foreach (MapMarkerModel item in mapMarkerList)
                {
                    item.TaskId = _taskId;
                    if (item.Id == 0)
                    {
                        _dBMapMarkerService.AddMarkerToTable(item);
                    }
                }

                mapMarkerList.Clear();
            }

            if (fileNameList.Count > 0)
            {
                foreach (FileListModel item in fileNameList)
                {
                    item.TaskId = _taskId;
                    if (item.Id == 0)
                    {
                        _dBFileNameService.AddFileNameToTable(item);
                    }
                }

                fileNameList.Clear();
            }
        }

        public List<FileListModel> GetFileNameListFromDB(int taskId)
        {
            List<FileListModel> list = _dBFileNameService.GetFileNameListFromDB(taskId);

            return list;
        }

        public void SaveFileName(string name)
        {
            var item = new FileListModel
            {
                TaskId = _taskId,
                FileName = name,
                VmHandler = this
            };

            _fileNameList.Add(item);
        }

        public void RemoveFileCollectionItem(int itemId)
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


        public void UpdateMarkersCounter()
        {
            MarkersCounter = MapMarkerList.Count.ToString();
        }

        #endregion Methods

        #region Overrides

        public override async Task Initialize()
        {
            await base.Initialize();
            if (_taskId == 0)
            {
                TaskName = null;
                TaskDescription = null;
                MarkersCounter = null;
                AddFileName();
            }
        }

        public override void Prepare(TaskModel parametr)
        {
            if (parametr != null)
            {
                _thisTaskModel.Id = parametr.Id;
                _taskId = parametr.Id;
                TaskName = parametr.TaskName;
                TaskDescription = parametr.TaskDescription;
                MapMarkerList = _dBMapMarkerService.GetMapMarkerListFromDB(_taskId);
                MarkersCounter = MapMarkerList.Count.ToString();
                AddFileName();

                return;
            }
            TaskName = null;
            TaskDescription = null;
            MarkersCounter = null;
        }

        public override void ViewAppearing()
        {
            MarkersCounter = _dBMapMarkerService.GetMapMarkerListFromDB(_taskId).Count.ToString();
        }

        #endregion Overrides
    }
}
