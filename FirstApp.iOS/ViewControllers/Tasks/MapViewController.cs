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
        private IUICoordinateSpace _coordinateSpace;
        CLLocationManager locationManager;
        public List<MapMarkerModel> MarkerListFromDB;
        public MapMarkerModel MarcerRow;
        private MKMapView _map;

        public MapViewController() : base(nameof(MapViewController), null)
        {
            _map = new MKMapView(UIScreen.MainScreen.Bounds);
            _coordinateSpace = _map.CoordinateSpace;
            _map.ZoomEnabled = true;
            _map.ScrollEnabled = true;
            MarkerListFromDB = new List<MapMarkerModel>();
            MarcerRow = new MapMarkerModel();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            SetupNavigationBar();

            EdgesForExtendedLayout = UIRectEdge.None;

            Title = Constants.MapMarkers;

            locationManager = new CLLocationManager();
            locationManager.RequestWhenInUseAuthorization();

            _map.ShowsUserLocation = true;

            MarkerListFromDB = ViewModel.GetMarkerList();

            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    _map.AddAnnotations(new MKPointAnnotation()
                    {
                        Title = $"Marker Task {ViewModel._taskId}",
                        Coordinate = new CLLocationCoordinate2D(coord.Latitude, coord.Longitude)
                    });

                }
            }          



            UILongPressGestureRecognizer longp = new UILongPressGestureRecognizer(LongPress);
            _map.AddGestureRecognizer(longp);

            _map.ShowAnnotations(_map.Annotations, false);

            View = _map;
        }

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

            var set = this.CreateBindingSet<MapViewController, MapViewModel>();
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

                MarcerRow = new MapMarkerModel();
                MarcerRow.Latitude = coordinate.Latitude;
                MarcerRow.Longitude = coordinate.Longitude;
                ViewModel.SaveMarkerInList(MarcerRow);

                _map.AddAnnotations(new MKPointAnnotation()
                {
                    Title = $"Marker Task {ViewModel._taskId}",
                    Coordinate = new CLLocationCoordinate2D(coordinate.Latitude, coordinate.Longitude)
                });
            }
        }
    }
}