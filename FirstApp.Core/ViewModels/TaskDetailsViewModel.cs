using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskRequestModel>, IFileListHandler
    {
        #region Variables
        private MvxSubscriptionToken _mapToken;
        private CurrentPlatformType _platform;
        private TaskModel _thisTaskModel;
        private List<MapMarkerModel> MapMarkerList;
        private MarkersData _dataForMap;
        private int _taskId;
        private int _userId;
        private readonly IDBFileNameService _dBFileNameService;
        private readonly IDBMapMarkerService _dBMapMarkerService;
        private readonly IDBTaskService _dBTaskService;
        private readonly ICurrentPlatformService _getCurrentPlatformService;
        private readonly IMvxMessenger _mvxMessenger;

        #endregion Variables

        #region Constructors

        public TaskDetailsViewModel(ICurrentPlatformService getCurrentPlatformService, IMvxNavigationService navigationService,
            IDBTaskService dBTaskService, IDBMapMarkerService dBMapMarkerService, IDBFileNameService dBFileNameService,
            IUserDialogs userDialogs, IMvxMessenger mvxMessenger) : base(navigationService, userDialogs)
        {
            _mvxMessenger = mvxMessenger;
            _userId = int.Parse(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            _getCurrentPlatformService = getCurrentPlatformService;
            _platform = _getCurrentPlatformService.GetCurrentPlatform();
            _dBMapMarkerService = dBMapMarkerService;
            _dBTaskService = dBTaskService;
            _dBFileNameService = dBFileNameService;

            _dataForMap = new MarkersData();

            _thisTaskModel = new TaskModel();

            _thisTaskModel.UserId = _userId;

            MapMarkerList = new List<MapMarkerModel>();

            DeleteFileItemCommand = new MvxCommand<int>(RemoveFileCollectionItem);

            _dataForMap.TaskId = _taskId;

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

        public MvxObservableCollection<FileRequestModel> _fileNameList;
        public MvxObservableCollection<FileRequestModel> FileNameList
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
                    _mapToken = _mvxMessenger.Subscribe<MarkersMessage>(GetMarkersFromMessage);
                    var result = await _navigationService.Navigate<MapViewModel, MarkersData>(_dataForMap);
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
                        SaveDataToDB(_thisTaskModel, MapMarkerList, FileNameList);

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

        public MvxAsyncCommand BackViewCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    bool answ = await _userDialogs.ConfirmAsync(Constants.WantSaveTask, Constants.SaveTask, Strings.Yes, Strings.No);

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
            FileNameList = new MvxObservableCollection<FileRequestModel>() { };

            List<FileListModel> list = GetFileNameListFromDB(_taskId);

            foreach (var item in list)
            {
                FileRequestModel element = new FileRequestModel();
                element.Id = item.Id;
                element.TaskId = item.TaskId;
                element.FileName = item.FileName;

                element.VmHandler = this;

                FileNameList.Add(element);
            }
        }

        private void SaveDataToDB(TaskModel task, List<MapMarkerModel> mapMarkerList, MvxObservableCollection<FileRequestModel> fileNameList)
        {
            _dBTaskService.AddTaskToTable(task);

            _taskId = task.Id;

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

            List<FileListModel> fileList = new List<FileListModel>();

            fileList = GetFileListForDB(fileNameList);

            if (fileList.Count > 0)
            {
                foreach (FileListModel item in fileList)
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
            var item = new FileRequestModel
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

            FileRequestModel _itemForDelete = null;

            foreach (FileRequestModel item in FileNameList)
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

        private List<FileListModel> GetFileListForDB(MvxObservableCollection<FileRequestModel> requestList)
        {
            List<FileListModel> list = new List<FileListModel>();

            foreach (FileRequestModel item in requestList)
            {
                FileListModel element = new FileListModel();
                element.Id = item.Id;
                element.TaskId = item.TaskId;
                element.FileName = item.FileName;
                list.Add(element);
            }
            return list;
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

        public override void Prepare(TaskRequestModel parametr)
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

        private void GetMarkersFromMessage(MarkersMessage markersList)
        {
            MapMarkerList.Clear();
            foreach (MapMarkerModel item in markersList.MarkerMessegeList)
            {
                MapMarkerList.Add(item);
            }

            MarkersCounter = MapMarkerList.Count.ToString();

            _mvxMessenger.Unsubscribe<MarkersMessage>(_mapToken);
        }

        public override void ViewAppeared()
        {
            base.ViewAppeared();
           
            _dataForMap.Markers = MapMarkerList;
        }

        #endregion Overrides
    }
}
