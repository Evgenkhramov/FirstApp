using CoreGraphics;
using CoreLocation;
using FirstApp.Core;
using FirstApp.Core.Models;
using FirstApp.Core.ViewModels;
using Foundation;
using MapKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class MapController : MvxViewController<MapViewModel>
    {
        private MKMapRect _zoomRect;
        private IUICoordinateSpace _coordinateSpace;
        CLLocationManager locationManager;
        public List<MapMarkerModel> MarkerListFromDB;
        public MapMarkerModel MarcerRow;
        private MKMapView _map;
        private bool _isTouch;

        public MapController() : base(nameof(MapController), null)
        {
            _map = new MKMapView(UIScreen.MainScreen.Bounds);
            _coordinateSpace = _map.CoordinateSpace;
            _map.ZoomEnabled = true;
            _map.ScrollEnabled = true;
            MarkerListFromDB = new List<MapMarkerModel>();
            MarcerRow = new MapMarkerModel();
            _isTouch = false;
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

            MKCoordinateRegion region;
            MKCoordinateSpan span;


            MarkerListFromDB = ViewModel.GetMarkerList();

            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    _map.AddAnnotations(new MKPointAnnotation()
                    {
                        Title = $"Marker Task {ViewModel._taskId}",
                        Coordinate = new CLLocationCoordinate2D(coord.Lat, coord.Lng)
                    });

                }
            }

            _zoomRect = new MKMapRect();


            foreach (MapMarkerModel element in MarkerListFromDB)
            {
                MKMapPoint annotationPoint = new MKMapPoint(element.Lat, element.Lng);
                MKMapRect pointRect = new MKMapRect(annotationPoint.X, annotationPoint.Y, 0.1, 0.1);

                if (_zoomRect.IsEmpty)
                {
                    _zoomRect = pointRect;                  
                }
                if (!_zoomRect.IsEmpty)
                {
                    _zoomRect = MKMapRect.Union(_zoomRect, pointRect);
                }
            }
               

            UILongPressGestureRecognizer longp = new UILongPressGestureRecognizer(LongPress);
            _map.AddGestureRecognizer(longp);

            //region.Center = _zoomRect.;
            //span.LatitudeDelta = 0.005;
            //span.LongitudeDelta = 0.005;
            //region.Span = span;
            //_map.SetRegion(region, true);

            //_map.SetVisibleMapRect(_zoomRect, new UIEdgeInsets(20, 20, 20, 20), true);

            region.Center = new CLLocationCoordinate2D(_zoomRect.Origin.X, _zoomRect.Origin.Y);
            span.LatitudeDelta = _zoomRect.Size.Width;
            span.LongitudeDelta = _zoomRect.Size.Height;
            region.Span = span;
            _map.SetRegion(region, true);
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

            var set = this.CreateBindingSet<MapController, MapViewModel>();
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
                MarcerRow.Lat = coordinate.Latitude;
                MarcerRow.Lng = coordinate.Longitude;
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