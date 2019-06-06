using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using FirstApp.Core;
using FirstApp.Core.Entities;
using FirstApp.Core.ViewModels;
using FirstApp.Droid.Interfaces;
using MvvmCross.Platforms.Android.Presenters.Attributes;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, IBackButtonListener
    {
        #region Variables

        private MapMarkerModel _marcerRow;
        private MapView _mapView;
        private GoogleMap _map;
        protected override int FragmentId => Resource.Layout.MapFragment;

        #endregion Variables

        #region Methods

        public void OnMapReady(GoogleMap googleMap)
        {
            _map = googleMap;
            _map.UiSettings.CompassEnabled = false;
            _map.UiSettings.MyLocationButtonEnabled = true;
            _map.UiSettings.MapToolbarEnabled = true;
            _map.MyLocationEnabled = true;

            var myLocation = new MapMarkerModel();

            Position getPosition = GetCurrentPosition().Result;

            var builder = new LatLngBounds.Builder();

            myLocation.Latitude = getPosition.Latitude;
            myLocation.Longitude = getPosition.Longitude;
            builder.Include(new LatLng(myLocation.Latitude, myLocation.Longitude));

            _map.AddMarker(new MarkerOptions().SetPosition(new LatLng(myLocation.Latitude, myLocation.Longitude)).SetTitle($"{ViewModel._taskId}")
                .SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)));

            if (ViewModel._markerList != null && ViewModel._markerList.Count > 0)
            {
                foreach (MapMarkerModel coord in ViewModel._markerList)
                {
                    _map.AddMarker(new MarkerOptions().SetPosition(new LatLng(coord.Latitude, coord.Longitude))
                        .SetTitle($"{ViewModel._taskId}"));

                    builder.Include(new LatLng(coord.Latitude, coord.Longitude));
                }
            }

            LatLngBounds bound = builder.Build();

            _map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bound, Constants.MapPadding));

            _map.MapClick += ClickOnMap;
        }

        private void ClickOnMap(object sender, GoogleMap.MapClickEventArgs eventArgs)
        {
            using (var markerOption = new MarkerOptions())
            {
                markerOption.SetPosition(eventArgs.Point);
                _marcerRow = new MapMarkerModel();
                _marcerRow.Latitude = markerOption.Position.Latitude;
                _marcerRow.Longitude = markerOption.Position.Longitude;
                ViewModel.SaveMarkerInList(_marcerRow);
                string title = $"{ViewModel._taskId}";

                markerOption.SetTitle(title);

                Marker marker = _map.AddMarker(markerOption);
            }
        }

        public static async Task<Position> GetCurrentPosition()
        {
            Position _position = null;

            IGeolocator locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = Constants.MapDesiredAccuracy;

            _position = await locator.GetLastKnownLocationAsync();

            if (_position != null)
            {
                return _position;
            }

            if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
            {
                return null;
            }

            _position = await locator.GetPositionAsync(TimeSpan.FromSeconds(Constants.TimeOutSmall), null, true);

            if (_position == null)
            {
                return null;
            }

            return _position;
        }

        #endregion Methods

        #region Overrides

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            _mapView = (MapView)view.FindViewById(Resource.Id.mapView);

            _mapView.OnCreate(savedInstanceState);

            _mapView.GetMapAsync(this);

            _marcerRow = new MapMarkerModel();

            return view;
        }

        public override void OnDestroyView()
        {
            _map.MapClick -= ClickOnMap;
            base.OnDestroyView();
        }

        public void OnBackPressed()
        {
            ViewModel.BackViewCommand.Execute();
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

        #endregion Overrides
    }
}
