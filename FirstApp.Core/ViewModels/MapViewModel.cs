using System.Collections.Generic;
using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace FirstApp.Core.ViewModels
{
    public class MapViewModel : BaseViewModel<TaskModel>
    {
        private int _markerCount;
        List<MapMarkerModel> _markerList;
        private TaskModel _taskModel;
        public int _taskId;

        private IDBMapMarkerService _dBMapMarkerService;

        public MapViewModel(IMvxNavigationService navigationService, IDBMapMarkerService dBMapMarkerService) : base(navigationService)
        {
            _markerList = new List<MapMarkerModel>();
            _dBMapMarkerService = dBMapMarkerService;
            _markerCount = 0;
            SaveButton = false;
            HaveGone = false;
        }

        public override void Prepare(TaskModel task)
        {
            _taskModel = task;
            _taskId = task.Id;
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

        public MvxAsyncCommand BackCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if (_markerList.Count > 0)
                    {
                        var answ = Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync("Do you want to save your markers?", "Save Markers?", "Yes", "No").Result;

                        if (answ)
                        {
                            foreach (MapMarkerModel item in _markerList)
                            {
                                _dBMapMarkerService.AddMarkerToTable(item);
                            }
                            _markerList.Clear();
                            await _navigationService.Close(this);
                        }
                        if (!answ)
                        {
                            await _navigationService.Close(this);
                        }
                    }
                    if (_markerList.Count <= 0)
                    {
                        await _navigationService.Close(this);
                    }
                });
            }
        }

        public MvxAsyncCommand SaveMapMarkerCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if (_markerList.Count > 0)
                    {
                        foreach (MapMarkerModel item in _markerList)
                        {
                            _dBMapMarkerService.AddMarkerToTable(item);
                        }
                        _markerList.Clear();
                        await _navigationService.Close(this);
                    }

                    if (_markerList.Count <= 0)
                    {
                        await _navigationService.Close(this);
                    }
                });
            }
        }

        public void SaveMarkerInList(MapMarkerModel marker)
        {
            marker.TaskId = _taskId;
            _markerList.Add(marker);
        }

        public List<MapMarkerModel> GetMarkerList()
        {
            List<MapMarkerModel> markers = _dBMapMarkerService.GetMapMarkerListFromDB(_taskId);
            return markers;
        }
    }
}
