using System.Collections.Generic;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.Models;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using System;
using System.Threading.Tasks;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, IBackButtonListener
    {
        public List<MapMarkerModel> MarkerListFromDB;
        public MapMarkerModel MarcerRow;
        private MapView _mapView;
        private GoogleMap _map;
        public Button MenuButton;
        protected override int FragmentId => Resource.Layout.MapFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _mapView = (MapView)view.FindViewById(Resource.Id.mapView);

            _mapView.OnCreate(savedInstanceState);

            _mapView.GetMapAsync(this);

            MarkerListFromDB = new List<MapMarkerModel>();
            MarcerRow = new MapMarkerModel();

            return view;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this._map = googleMap;
            this._map.UiSettings.CompassEnabled = false;
            this._map.UiSettings.MyLocationButtonEnabled = true;
            this._map.UiSettings.MapToolbarEnabled = true;
            this._map.MyLocationEnabled = true;

            var myLocation = new MapMarkerModel();

            var getPosition = GetCurrentPosition().Result;

            var builder = new LatLngBounds.Builder();

            myLocation.Latitude = getPosition.Latitude;
            myLocation.Longitude = getPosition.Longitude;
            builder.Include(new LatLng(myLocation.Latitude, myLocation.Longitude));

            _map.AddMarker(new MarkerOptions().SetPosition(new LatLng(myLocation.Latitude, myLocation.Longitude)).SetTitle($"Marker Task {ViewModel._taskId}").SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)));
            MarkerListFromDB = ViewModel.GetMarkerList();
            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    _map.AddMarker(new MarkerOptions().SetPosition(new LatLng(coord.Latitude, coord.Longitude)).SetTitle($"Marker Task {ViewModel._taskId}"));
                    builder.Include(new LatLng(coord.Latitude, coord.Longitude));
                }
            }

            LatLngBounds bound = builder.Build();

            _map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bound, 100));

            googleMap.MapClick += (object sender, GoogleMap.MapClickEventArgs e) =>
            {
                using (var markerOption = new MarkerOptions())
                {
                    markerOption.SetPosition(e.Point);
                    MarcerRow = new MapMarkerModel();
                    MarcerRow.Latitude = markerOption.Position.Latitude;
                    MarcerRow.Longitude = markerOption.Position.Longitude;
                    ViewModel.SaveMarkerInList(MarcerRow);
                    var title = $"Marker Task {ViewModel._taskId}";
                    markerOption.SetTitle(title);

                    Marker marker = googleMap.AddMarker(markerOption);
                }
            };
        }

        public static async Task<Position> GetCurrentPosition()
        {
            Position _position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                _position = await locator.GetLastKnownLocationAsync();

                if (_position != null)
                {
                    return _position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    return null;
                }

                _position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {

            }

            if (_position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    _position.Timestamp, _position.Latitude, _position.Longitude,
                    _position.Altitude, _position.AltitudeAccuracy, _position.Accuracy, _position.Heading, _position.Speed);

            return _position;
        }

        public void OnBackPressed()
        {
            ViewModel.BackCommand.Execute();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _mapView.OnDestroy();
        }

        public override void OnStop()
        {
            base.OnStop();
            _mapView.OnStop();
        }

        public override void OnResume()
        {
            base.OnResume();
            _mapView.OnResume();
        }

        public override void OnStart()
        {
            base.OnStart();
            _mapView.OnStart();
        }

        public override void OnPause()
        {
            base.OnPause();
            _mapView.OnPause();
        }
    }
}
