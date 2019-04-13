using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace FirstApp.Core.ViewModels
{
    public class MapViewModel : BaseViewModel<TaskModel>
    {
        int MarkerCount;
        List<MapMarkerModel> _markerList;
        TaskModel taskModel;
        public int _taskId;
        private IUserDialogService _userDialogService;
        private IDBMapMarkerService _dBMapMarkerService;

        public MapViewModel(IMvxNavigationService navigationService, IDBMapMarkerService dBMapMarkerService, IUserDialogService userDialogService) : base(navigationService)
        {
            _markerList = new List<MapMarkerModel>();
            _userDialogService = userDialogService;
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
                        //TaskCompletionSource<TResult>
                        //string dialogResponse = AsyncHelpers.RunSync<string>(() => DisplayCustomDialog("Confirm delete", "Are you sure you want to delete all rows?", "YES", "NO"));
                        var answ = await _userDialogService.ShowAlertForUserWithSomeLogic("Save Markers?", "Do you want to save your markers?", "Yes","No");
                        //await _navigationService.Close(this);
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
                });
            }
        }

        public void SaveMarkerInList(MapMarkerModel marker)
        {
            marker.TaskId = _taskId;
            _markerList.Add(marker);
        }

        public List<MapCoord> GetMarkerList()
        {
            List<MapCoord> markers = _dBMapMarkerService.GetMapMarkerFromDB(_taskId);
            return markers;
        }
    }
}
