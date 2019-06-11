using CoreGraphics;
using CoreLocation;
using FirstApp.Core;
using FirstApp.Core.Entities;
using FirstApp.Core.ViewModels;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System.Collections.Generic;
using System.Linq;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class MapViewController : MvxViewController<MapViewModel>
    {
        #region Variables

        public List<MapMarkerEntity> MarkerListFromRepository;

        private IUICoordinateSpace _coordinateSpace;
        private CLLocationManager _locationManager;
        private MapMarkerEntity _marcerRow;
        private readonly MKMapView _map;

        #endregion Variables

        #region Constructors

        public MapViewController() : base(nameof(MapViewController), null)
        {
            _map = new MKMapView(UIScreen.MainScreen.Bounds);
            _coordinateSpace = _map.CoordinateSpace;
            _map.ZoomEnabled = true;
            _map.ScrollEnabled = true;

            MarkerListFromRepository = new List<MapMarkerEntity>();

            _marcerRow = new MapMarkerEntity();
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

            set.Bind(_backButton).To(vm => vm.BackViewCommand);
            set.Bind(_saveButton).To(vm => vm.SaveMapMarkerCommand);

            set.Apply();
        }

        public void LongPress(UILongPressGestureRecognizer touches)
        {
            if (touches.State == UIGestureRecognizerState.Ended)
            {
                CGPoint location = touches.LocationInView(_map);
                CLLocationCoordinate2D coordinate = _map.ConvertPoint(location, _map);

                _marcerRow = new MapMarkerEntity();
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

            Title = Constants.MapMarkers;

            _locationManager = new CLLocationManager();
            _locationManager.RequestWhenInUseAuthorization();

            _map.ShowsUserLocation = true;

            MarkerListFromRepository = ViewModel.MarkerList;

            if (MarkerListFromRepository.Any())
            {
                foreach (MapMarkerEntity coord in MarkerListFromRepository)
                {
                    _map.AddAnnotations(new MKPointAnnotation()
                    {
                        Title = Constants.MapMarker,
                        Coordinate = new CLLocationCoordinate2D(coord.Latitude, coord.Longitude)
                    });

                }
            }

            var longPress = new UILongPressGestureRecognizer(LongPress);

            _map.AddGestureRecognizer(longPress);

            _map.ShowAnnotations(_map.Annotations, false);

            View = _map;
        }

        #endregion Overrides
    }
}