using Android.OS;
using Android.Runtime;
using Android.Views;
using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Android.Presenters.Attributes;

namespace FirstApp.Droid.Views
{
    [MvxFragmentPresentation(typeof(StartViewModel), Resource.Id.content_frame_new, true)]
    [Register("firstApp.Droid.Views.RegistrationFragment")]
    public class RegistrationFragment : BaseFragment<RegistrationViewModel>
    {
        protected override int FragmentId => Resource.Layout.RegistrationFragment;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = base.OnCreateView(inflater, container, savedInstanceState);

            return view;
        }
    }
}