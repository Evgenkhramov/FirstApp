using Acr.UserDialogs;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;

namespace FirstApp.Core.ViewModels
{
    public class MapViewModel : BaseViewModel<TaskModel>
    {
        #region Variables

        List<MapMarkerModel> _markerList;
        private TaskModel _taskModel;
        public int _taskId;
        private readonly IUserDialogs _userDialogs;
        private readonly IMvxMessenger _messenger;

        #endregion Variables

        #region Constructors

        public MapViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs, IMvxMessenger messenger) : base(navigationService)
        {
            _messenger = messenger;
            _userDialogs = userDialogs;
            _markerList = new List<MapMarkerModel>();
            SaveButton = false;
            HaveGone = false;
        }

        #endregion Constructors

        #region Properties

        public bool SaveButton { get; set; }

        public bool HaveGone { get; set; }

        #endregion Properties

        #region Commands  

        public MvxAsyncCommand BackViewCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    bool answ = await _userDialogs.ConfirmAsync(Constants.WantSaveMarkers, Constants.SaveMarkers, Strings.Yes, Strings.No);
                    if (answ)
                    {
                        SaveMapMarkerCommand.Execute();
                        return;
                    }
                    await _navigationService.Close(this);
                });
            }
        }

        public MvxAsyncCommand SaveMapMarkerCommand
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    if (_markerList.Count <= 0)
                    {
                        await _navigationService.Close(this);
                        return;
                    }

                    SendMarkersMessege(_markerList);
                    //foreach (MapMarkerModel item in _markerList)
                    //{
                    //    TaskDetailsViewModel.MapMarkerList.Add(item);
                    //}

                    _markerList.Clear();
                    await _navigationService.Close(this);
                });
            }
        }

        private void SendMarkersMessege(List<MapMarkerModel> markerList)
        {
            var message = new MarkersMessage(this, markerList);

            _messenger.Publish(message);
        }


        #endregion Commands

        #region Methods

        public void SaveMarkerInList(MapMarkerModel marker)
        {
            marker.TaskId = _taskId;
            _markerList.Add(marker);
        }

        public List<MapMarkerModel> GetMarkerList()
        {
            List<MapMarkerModel> markers = TaskDetailsViewModel.MapMarkerList;
            return markers;
        }

        #endregion Methods

        #region Overrides

        public override void Prepare(TaskModel task)
        {
            _taskModel = task;
            _taskId = task.Id;
        }

        #endregion Overrides
    }
}
