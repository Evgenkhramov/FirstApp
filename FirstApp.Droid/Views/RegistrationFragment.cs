using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationFragmentViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}