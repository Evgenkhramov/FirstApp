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
        int MarkerCount;
        List<MapMarkerModel> _markerList;
        TaskModel taskModel;
        public int _taskId;
        //private IUserDialogService _userDialogService;
        private IDBMapMarkerService _dBMapMarkerService;

        public MapViewModel(IMvxNavigationService navigationService, IDBMapMarkerService dBMapMarkerService/*, IUserDialogService userDialogService*/) : base(navigationService)
        {
            _markerList = new List<MapMarkerModel>();
            //_userDialogService = userDialogService;
            _dBMapMarkerService = dBMapMarkerService;
            MarkerCount = 0;
            SaveButton = false;
            HaveGone = false;
        }

        public override void Prepare(TaskModel task)
        {
            taskModel = task;
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
                        var answ =  Mvx.IoCProvider.Resolve<IUserDialogs>().ConfirmAsync("Do you want to save your markers?", "Save Markers?", "Yes", "No").Result;
                
                        //var answ = await _userDialogService.ShowAlertForUserWithSomeLogic("Save Markers?", "Do you want to save your markers?", "Yes", "No");
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
                    else
                    {
                        await _navigationService.Close(this);
                    }
                });
            }
        }

        public MvxAsyncCommand SaveMapMarker
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
                    else
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
            List<MapMarkerModel> markers = _dBMapMarkerService.GetMapMarkerFromDB(_taskId);
            return markers;
        }
    }
}
