using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskDetailsViewModel : BaseViewModel<TaskModel>, IFileListHandler
    {
        private TaskModel _thisTaskModel;
        private int _taskId;
        private readonly IDBFileNameService _dBFileNameService;
        private readonly IDBMapMarkerService _dBMapMarkerService;
        private readonly IDBTaskService _dBTaskService;

        public TaskDetailsViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService, IDBMapMarkerService dBMapMarkerService,
             IDBFileNameService dBFileNameService) : base(navigationService)
        {
            _dBMapMarkerService = dBMapMarkerService;
            _dBTaskService = dBTaskService;
            _dBFileNameService = dBFileNameService;

            _thisTaskModel = new TaskModel();

            DeleteFileItemCommand = new MvxCommand<int>(RemoveCollectionItem);

            SaveButton = true;
            HaveGone = false;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            if (_taskId == 0)
            {
                TaskName = null;
                TaskDescription = null;
                MapMarkers = null;
                _dBTaskService.AddTaskToTable(_thisTaskModel);
                _taskId = _thisTaskModel.Id;
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
                MapMarkers = _dBMapMarkerService.GetMapMarkerListFromDB(_taskId).Count.ToString();
                AddFileName();

                return;
            }
            TaskName = null;
            TaskDescription = null;
            MapMarkers = null;
            _dBTaskService.AddTaskToTable(_thisTaskModel);
            _taskId = _thisTaskModel.Id;
        }

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
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterTaskName, Constants.EmptyTaskName, Constants.Ok);
                        return;
                    }
                    if (string.IsNullOrEmpty(TaskDescription))
                    {
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterTaskDescription, Constants.EmptyTaskDescription, Constants.Ok);

                        return;
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName))
                    {
                        _dBTaskService.AddTaskToTable(_thisTaskModel);

                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }

        public MvxAsyncCommand SaveTaskForiOS
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {

                    if (string.IsNullOrEmpty(TaskName))
                    {
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterTaskName, Constants.EmptyTaskName, Constants.Ok);

                        return;
                    }
                    if (string.IsNullOrEmpty(TaskDescription))
                    {
                        Mvx.IoCProvider.Resolve<IUserDialogs>().Alert(Constants.EnterTaskDescription, Constants.EmptyTaskDescription, Constants.Ok);

                        return;
                    }
                    if (!string.IsNullOrEmpty(TaskDescription) && !string.IsNullOrEmpty(TaskName))
                    {
                        _dBTaskService.AddTaskToTable(_thisTaskModel);

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
                    bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync(Constants.WantSaveMarkers, Constants.SaveMarkers, Constants.Yes, Constants.No);

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

        public MvxAsyncCommand BackCommandiOS
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    bool answ = await Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync(Constants.WantSaveMarkers, Constants.SaveMarkers, Constants.Yes, Constants.No);

                    if (answ)
                    {
                        SaveTaskForiOS.Execute();
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
                    bool answ = Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync(Constants.WantDeleteTask, Constants.DeleteTask, Constants.Yes, Constants.No).Result;

                    if (answ)
                    {
                        _dBTaskService.DeleteTaskFromTable(_taskId);
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                    if (!answ)
                    {
                        await _navigationService.Navigate<TaskListViewModel>();
                    }
                });
            }
        }
        public List<FileListModel> GetFileNameListFromDB(int taskId)
        {
            List<FileListModel> list = _dBFileNameService.GetFileNameListFromDB(taskId);

            return list;
        }

        public void SaveFileName(string name)
        {
            var item = new FileListModel();
            item.TaskId = _taskId;
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
            MapMarkers = _dBMapMarkerService.GetMapMarkerListFromDB(_taskId).Count.ToString();
        }
    }
}
