using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, false)]
    [Register("firstApp.Droid.Views.LoginFragment")]
    public class LoginFragment : BaseFragment<LoginFragmentViewModel>
    {
        protected override int FragmentId => Resource.Layout.LoginFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }

}