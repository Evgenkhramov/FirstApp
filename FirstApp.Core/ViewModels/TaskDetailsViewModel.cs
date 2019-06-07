using Acr.UserDialogs;
using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskRequestModel>, IFileListHandler
    {
        #region Variables
        private MvxSubscriptionToken _mapToken;
        private readonly CurrentPlatformType _platform;
        private TaskEntity _thisTaskEntity;
        private MvxObservableCollection<MapMarkerEntity> MapMarkerList;
        private MarkersData _dataForMap;
        private int _taskId;
        private readonly int _userId;
        private readonly IFileNameService _dBFileNameService;
        private readonly IMapMarkerService _dBMapMarkerService;
        private readonly ITaskService _dBTaskService;
        private readonly ICurrentPlatformService _getCurrentPlatformService;
        private readonly IMvxMessenger _mvxMessenger;

        #endregion Variables

        #region Constructors

        public TaskDetailsViewModel(ICurrentPlatformService getCurrentPlatformService, IMvxNavigationService navigationService,
            ITaskService dBTaskService, IMapMarkerService dBMapMarkerService, IFileNameService dBFileNameService,
            IUserDialogs userDialogs, IMvxMessenger mvxMessenger) : base(navigationService, userDialogs)
        {
            _mvxMessenger = mvxMessenger;
            _dBMapMarkerService = dBMapMarkerService;
            _dBTaskService = dBTaskService;
            _dBFileNameService = dBFileNameService;
            _getCurrentPlatformService = getCurrentPlatformService;

            _userId = int.Parse(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));

            _platform = _getCurrentPlatformService.GetCurrentPlatform();

            _dataForMap = new MarkersData();

            _thisTaskEntity = new TaskEntity();

            _thisTaskEntity.UserId = _userId;

            MapMarkerList = new MvxObservableCollection<MapMarkerEntity>();

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
                _thisTaskEntity.TaskName = _taskName;
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
                _thisTaskEntity.TaskDescription = _taskDescription;
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

                    await _navigationService.Navigate<MapViewModel, MarkersData>(_dataForMap);
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
                        SaveDataToDB(_thisTaskEntity, MapMarkerList, FileNameList);

                        await _navigationService.Navigate<TaskListViewModel>();

                        return;
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName) && _platform == CurrentPlatformType.iOS)
                    {
                        SaveDataToDB(_thisTaskEntity, MapMarkerList, FileNameList);

                        await _navigationService.Close(this);

                        return;
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
                    bool isUserAccept = await _userDialogs.ConfirmAsync(Constants.WantSaveTask, Constants.SaveTask, Strings.Yes, Strings.No);

                    if (isUserAccept)
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
                    bool userAnswer = await _userDialogs.ConfirmAsync(Constants.WantDeleteTask, Constants.DeleteTask, Strings.Yes, Strings.No);

                    if (!userAnswer)
                    {
                        return;
                    }

                    _dBFileNameService.DeleteFiles(_taskId);
                    _dBMapMarkerService.DeleteMarkers(_taskId);
                    _dBTaskService.DeleteTaskFromDB(_taskId);

                    await _navigationService.Close(this);

                    if (_platform == CurrentPlatformType.Android)
                    {
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

            List<FileListEntity> list = GetFileNameListFromDB(_taskId);

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

        private void SaveDataToDB(TaskEntity task, MvxObservableCollection<MapMarkerEntity> mapMarkerList, MvxObservableCollection<FileRequestModel> fileNameList)
        {
            _dBTaskService.AddTaskToTable(task);

            _taskId = task.Id;

            if (mapMarkerList.Any())
            {
                foreach (MapMarkerEntity item in mapMarkerList)
                {
                    item.TaskId = _taskId;

                    _dBMapMarkerService.AddMarkerToTable(item);
                }
                mapMarkerList.Clear();
            }

            List<FileListEntity> fileList = new List<FileListEntity>();

            fileList = PrepareFileListForDB(fileNameList);

            if (fileList.Any())
            {
                foreach (FileListEntity item in fileList)
                {
                    item.TaskId = _taskId;

                    _dBFileNameService.AddFileNameToTable(item);
                }
                fileList.Clear();
                fileNameList.Clear();
            }
        }

        public List<FileListEntity> GetFileNameListFromDB(int taskId)
        {
            List<FileListEntity> list = _dBFileNameService.GetFileNameListFromDB(taskId);

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

            FileRequestModel _itemForDelete = FileNameList.FirstOrDefault(x=>x.Id == itemId);

            FileNameList.Remove(item: _itemForDelete);
        }

        public void UpdateMarkersCounter()
        {
            MarkersCounter = MapMarkerList.Count.ToString();
        }

        private List<FileListEntity> PrepareFileListForDB(MvxObservableCollection<FileRequestModel> requestList)
        {
            List<FileListEntity> list = new List<FileListEntity>();

            foreach (FileRequestModel item in requestList)
            {
                FileListEntity element = new FileListEntity
                {
                    Id = item.Id,
                    TaskId = item.TaskId,
                    FileName = item.FileName
                };

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
            if (parametr == null)
            {
                TaskName = null;
                TaskDescription = null;
                MarkersCounter = null;

                return;
            }
            _thisTaskEntity.Id = parametr.Id;
            _taskId = parametr.Id;
            TaskName = parametr.TaskName;
            TaskDescription = parametr.TaskDescription;
            MapMarkerList.AddRange(_dBMapMarkerService.GetMapMarkerListFromDB(_taskId));

            UpdateMarkersCounter();

            AddFileName();
        }

        private void GetMarkersFromMessage(MarkersMessage markersList)
        {
            MapMarkerList.Clear();

            foreach (MapMarkerEntity item in markersList.MarkerMessegeList)
            {
                MapMarkerList.Add(item);
            }

            UpdateMarkersCounter();

            _mvxMessenger.Unsubscribe<MarkersMessage>(_mapToken);
        }

        public override void ViewDisappeared()
        {
            MapMarkerList.Clear();
            base.ViewDisappeared();
        }
        public override void ViewAppeared()
        {
            base.ViewAppeared();

            _dataForMap.Markers.AddRange(MapMarkerList);
        }

        #endregion Overrides
    }
}
