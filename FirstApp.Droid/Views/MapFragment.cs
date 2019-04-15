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

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, IBackButtonListener
    {
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




            myLocation.Lat = 0;//this.map.MyLocation.Latitude;
            myLocation.Lng = 0;// this.map.MyLocation.Longitude;

            map = googleMap;
            map.AddMarker(new MarkerOptions().SetPosition(new LatLng(myLocation.Lat, myLocation.Lng)).SetTitle($"Marker Task {ViewModel._taskId}").SetIcon(BitmapDescriptorFactory.DefaultMarker(BitmapDescriptorFactory.HueGreen)));
            MarkerListFromDB = ViewModel.GetMarkerList();
            if (MarkerListFromDB!= null && MarkerListFromDB.Count > 0)
            {
                foreach (MapMarkerModel coord in MarkerListFromDB)
                {
                    map.AddMarker(new MarkerOptions().SetPosition(new LatLng(coord.Lat, coord.Lng)).SetTitle($"Marker Task {ViewModel._taskId}"));
                }
            }
           
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
