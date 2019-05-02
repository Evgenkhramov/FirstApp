using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.ViewModels;
using System;

namespace FirstApp.iOS
{
    public partial class MapController : MvxViewController<MapViewModel>
    {
        public MapController() : base(nameof(MapController),null)
        {
        }
    }
}