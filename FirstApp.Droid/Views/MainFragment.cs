using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.Gms.Maps;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(MainViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.MainFragment")]
    public class MainFragment : BaseFragment<MainFragmentViewModel>, IOnMapReadyCallback
    {
        private MapView mapView;
        private GoogleMap map;
        public Button menuButton;
        protected override int FragmentId => Resource.Layout.MainFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);


            mapView = (MapView)view.FindViewById(Resource.Id.mapView);
           
            mapView.OnCreate(savedInstanceState);

            mapView.OnResume();
            mapView.GetMapAsync(this);


            menuButton = view.FindViewById<Button>(Resource.Id.menu_icon);
            menuButton.Click += (object sender, EventArgs e) =>
            {
                OpenMenu();
            };

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
            //map.AddMarker(new MarkerOptions().SetPosition(new LatLng(0, 0)).SetTitle("Marker"));
            googleMap.MapClick += (object sender, GoogleMap.MapClickEventArgs e) =>
            {
                using (var markerOption = new MarkerOptions())
                {
                    markerOption.SetPosition(e.Point);
                    markerOption.SetTitle("StackOverflow");
                    // save the "marker" variable returned if you need move, delete, update it, etc...
                    var marker = googleMap.AddMarker(markerOption);
                }
            };
            
        }

        public void OnStart()
        {          
            this.OnStart();
            mapView.OnStart();
        }
        public void OnResume()
        {
            mapView.OnResume();
            this.OnResume();
        }

        public void OnPause()
        {
            this.OnPause();
            mapView.OnPause();
        }

        public void OnDestroy()
        {
            this.OnDestroy();
            mapView.OnDestroy();
        }

        public void onLowMemory()
        {
            this.onLowMemory();
            mapView.OnLowMemory();
        }

    }
}