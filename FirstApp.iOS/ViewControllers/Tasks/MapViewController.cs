using CoreGraphics;
using CoreLocation;
using FirstApp.Core;
using FirstApp.Core.Models;
using FirstApp.Core.ViewModels;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System.Collections.Generic;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class MapViewController : MvxViewController<MapViewModel>
    {
        #region Variables

        private IUICoordinateSpace _coordinateSpace;
        private CLLocationManager _locationManager;
        public List<MapMarkerModel> MarkerListFromDB;
        private MapMarkerModel _marcerRow;
        private MKMapView _map;

        #endregion Variables

        #region Constructors

        public MapViewController() : base(nameof(MapViewController), null)
        {
            _map = new MKMapView(UIScreen.MainScreen.Bounds);
            _coordinateSpace = _map.CoordinateSpace;
            _map.ZoomEnabled = true;
            _map.ScrollEnabled = true;
            MarkerListFromDB = new List<MapMarkerModel>();
            _marcerRow = new MapMarkerModel();
        }

        #endregion Constructors

        #region Methods

        private void SetupNavigationBar()
        {
            var _backButton = new UIButton(UIButtonType.Custom);
            _backButton.Frame = new CGRect(0, 0, 40, 40);
            _backButton.SetImage(UIImage.FromBundle("backButton"), UIControlState.Normal);

            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_backButton) }, false);

            var _saveButton = new UIButton(UIButtonType.Custom);
            _saveButton.Frame = new CGRect(0, 0, 40, 40);
            _saveButton.SetImage(UIImage.FromBundle("saveButton"), UIControlState.Normal);

            NavigationItem.SetRightBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_saveButton) }, false);

            MvxFluentBindingDescriptionSet<MapViewController, MapViewModel> set = this.CreateBindingSet<MapViewController, MapViewModel>();

            set.Bind(_backButton).To(vm => vm.BackCommand);
            set.Bind(_saveButton).To(vm => vm.SaveMapMarkerCommand);

            set.Apply();
        }

        public void LongPress(UILongPressGestureRecognizer touches)
        {
            if (touches.State == UIGestureRecognizerState.Ended)
            {
                CGPoint location = touches.LocationInView(_map);
                CLLocationCoordinate2D coordinate = _map.ConvertPoint(location, _map);

                _marcerRow = new MapMarkerModel();
                _marcerRow.Latitude = coordinate.Latitude;
                _marcerRow.Longitude = coordinate.Longitude;
                ViewModel.SaveMarkerInList(_marcerRow);

                _map.AddAnnotations(new MKPointAnnotation()
                {
                    Title = Constants.MapMarker,
                    Coordinate = new CLLocationCoordinate2D(coordinate.Latitude, coordinate.Longitude)
                });
            }
        }

        #endregion Methods

        #region Overrides

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupNavigationBar();

            //EdgesForExtendedLayout = UIRectEdge.None;

            Title = Constants.MapMarkers;

            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();

            _map.ShowsUserLocation = true;

            MarkerListFromDB = ViewModel.GetMarkerList();

            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    _map.AddAnnotations(new MKPointAnnotation()
                    {
                        Title = Constants.MapMarker,
                        Coordinate = new CLLocationCoordinate2D(coord.Latitude, coord.Longitude)
                    });

                }
            }

            UILongPressGestureRecognizer longp = new UILongPressGestureRecognizer(LongPress);

            _map.AddGestureRecognizer(longp);

            _map.ShowAnnotations(_map.Annotations, false);

            View = _map;
        }

        #endregion Overrides
    }
}