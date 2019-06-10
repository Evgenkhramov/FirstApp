using Acr.UserDialogs;
using FirstApp.Core.Entities;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using System.Collections.Generic;
using System.Linq;

namespace FirstApp.Core.ViewModels
{
    public class MapViewModel : BaseViewModel<MarkersData>
    {
        #region Variables

        public int TaskId;
        public List<MapMarkerEntity> MarkerList;

        private readonly IMvxMessenger _messenger;

        #endregion Variables

        #region Constructors

        public MapViewModel(IMvxNavigationService navigationService, IMvxMessenger messenger,
            IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _messenger = messenger;

            MarkerList = new List<MapMarkerEntity>();

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
                    bool userAnswer = await _userDialogs.ConfirmAsync(Constants.WantSaveMarkers, Constants.SaveMarkers, Strings.Yes, Strings.No);
                    if (userAnswer)
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
                    if (!MarkerList.Any())
                    {
                        await _navigationService.Close(this);

                        return;
                    }
                    
                    await _navigationService.Close(this);

                    SendMarkersMessege(MarkerList);

                    MarkerList.Clear();
                });
            }
        }

        private void SendMarkersMessege(List<MapMarkerEntity> markerList)
        {
            var message = new MarkersMessage(this, markerList);

            _messenger.Publish(message);
        }


        #endregion Commands

        #region Methods

        public void SaveMarkerInList(MapMarkerEntity marker)
        {
            marker.TaskId = TaskId;

            MarkerList.Add(marker);
        }

        #endregion Methods

        #region Overrides

        public override void Prepare(MarkersData data)
        {
            foreach (MapMarkerEntity item in data.Markers)
            {
                MarkerList.Add(item);
            }

            TaskId = data.TaskId;
        }

        #endregion Overrides
    }
}
