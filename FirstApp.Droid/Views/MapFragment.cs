using System.Collections.Generic;
using Android.Gms.Common.Apis;
using Android.Gms.Maps;
using Android.Gms.Location;
using Android.Gms.Common;
using Android.Gms.Maps.Model;
using Android.Locations;
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

using Android;
using Android.App;
using Android.Content.PM;

using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Plugin.Geolocator.Abstractions;
using Plugin.Geolocator;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, IBackButtonListener
    {
        LocationManager locationManager;
        GoogleApiClient apiClient;
        public List<MapMarkerModel> MarkerListFromDB;
        public MapMarkerModel marcerRow;
        private MapView mapView;
        private GoogleMap map;
        public Button menuButton;
        protected override int FragmentId => Resource.Layout.MapFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            mapView = (MapView)view.FindViewById(Resource.Id.mapView);

            mapView.OnCreate(savedInstanceState);

            mapView.GetMapAsync(this);

            MarkerListFromDB = new List<MapMarkerModel>();
            marcerRow = new MapMarkerModel();

            //locationManager = (LocationManager)Android.Content.ContextWrapper.GetSystemService(Android.Content.Context.LocationService);
            //var criteria = new Criteria { PowerRequirement = Power.Medium };
            //var bestProvider = locationManager.GetBestProvider(criteria, true);
            //var location = locationManager.GetLastKnownLocation(bestProvider);


            //menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            ////menuButton.Click += (object sender, EventArgs e) =>
            ////{
            ////    OpenMenu();
            ////};
            return view;
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            this.map = googleMap;
            //Setup and customize your Google Map

            this.map.UiSettings.CompassEnabled = false;
            this.map.UiSettings.MyLocationButtonEnabled = true;
            this.map.UiSettings.MapToolbarEnabled = true;
            this.map.MyLocationEnabled = true;

            MapMarkerModel myLocation = new MapMarkerModel();

            var getPosition = GetCurrentPosition().Result;

            LatLngBounds.Builder builder = new LatLngBounds.Builder();
            myLocation.Lat = getPosition.Latitude;
            myLocation.Lng = getPosition.Longitude;
            builder.Include(new LatLng(myLocation.Lat, myLocation.Lng));
            //map = googleMap;
            map.AddMarker(new MarkerOptions().SetPosition(new LatLng(myLocation.Lat, myLocation.Lng)).SetTitle($"Marker Task {ViewModel._taskId}").SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)));
            MarkerListFromDB = ViewModel.GetMarkerList();
            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    map.AddMarker(new MarkerOptions().SetPosition(new LatLng(coord.Lat, coord.Lng)).SetTitle($"Marker Task {ViewModel._taskId}"));
                    builder.Include(new LatLng(coord.Lat, coord.Lng));
                }
            }
            LatLngBounds bound = builder.Build();

            map.MoveCamera(CameraUpdateFactory.NewLatLngBounds(bound, 100));


            googleMap.MapClick += (object sender, GoogleMap.MapClickEventArgs e) =>
            {
                using (var markerOption = new MarkerOptions())
                {
                    markerOption.SetPosition(e.Point);
                    marcerRow = new MapMarkerModel();
                    marcerRow.Lat = markerOption.Position.Latitude;
                    marcerRow.Lng = markerOption.Position.Longitude;
                    ViewModel.SaveMarkerInList(marcerRow);
                    var title = $"Marker Task {ViewModel._taskId}";
                    markerOption.SetTitle(title);
                    // save the "marker" variable returned if you need move, delete, update it, etc...
                    Marker marker = googleMap.AddMarker(markerOption);
                }
            };
        }

        public static async Task<Position> GetCurrentPosition()
        {
            Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();

                if (position != null)
                {
                    //got a cahched position, so let's use it.
                    return position;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return null;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);

            }
            catch (Exception ex)
            {

            }

            if (position == null)
                return null;

            var output = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
                    position.Timestamp, position.Latitude, position.Longitude,
                    position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

            return position;
        }



        public void OnBackPressed()
        {
            ViewModel.BackCommand.Execute();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            mapView.OnDestroy();
        }

        public override void OnStop()
        {
            base.OnStop();
            mapView.OnStop();
        }

        public override void OnResume()
        {
            base.OnResume();
            mapView.OnResume();
        }

        public override void OnStart()
        {
            base.OnStart();
            mapView.OnStart();
        }

        public override void OnPause()
        {
            base.OnPause();
            mapView.OnPause();
        }


    }
}
