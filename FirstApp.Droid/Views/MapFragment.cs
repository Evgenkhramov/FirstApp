using System;
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

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.MapFragment")]
    public class MapFragment : BaseFragment<MapViewModel>, IOnMapReadyCallback, IBackButtonListener
    {
        public List<MapCoord> MarkerListFromDB;
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

            MarkerListFromDB = new List<MapCoord>();
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

            map = googleMap;
            MarkerListFromDB = ViewModel.GetMarkerList();
            if (MarkerListFromDB != null && MarkerListFromDB.Count > 0)
            {
                foreach (MapCoord coord in MarkerListFromDB)
                {
                    map.AddMarker(new MarkerOptions().SetPosition(new LatLng(coord.Lat, coord.Lng)).SetTitle($"Marker Task {ViewModel._taskId}"));
                }
            }
            map.AddMarker(new MarkerOptions().SetPosition(new LatLng(0, 0)).SetTitle("Marker"));
            googleMap.MapClick += (object sender, GoogleMap.MapClickEventArgs e) =>
            {
                using (var markerOption = new MarkerOptions())
                {
                    markerOption.SetPosition(e.Point);
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
