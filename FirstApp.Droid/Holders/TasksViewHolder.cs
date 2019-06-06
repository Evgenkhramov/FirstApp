using Android.Content.Res;
using Android.Views;
using Android.Widget;
using FirstApp.Core;
using FirstApp.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace FirstApp.Droid.Holders
{
    public class TasksViewHolder : MvxRecyclerViewHolder
    {
        #region Variables

        private static int _screenWidth;

        #endregion Variables

        #region Constructors

        public TasksViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            _screenWidth = Resources.System.DisplayMetrics.WidthPixels;

            NameTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskName);
            NameTaskHolder.LayoutParameters.Width = _screenWidth - Constants.AndroidTaskItemSwipeWidth;
            DescriptionTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskShortDescription);

            this.DelayBind(() =>
            {
                MvxFluentBindingDescriptionSet<TasksViewHolder, TaskRequestModel> set = this.CreateBindingSet<TasksViewHolder, TaskRequestModel>();

                set.Bind(NameTaskHolder).To(x => x.TaskName);
                set.Bind(DescriptionTaskHolder).To(y => y.TaskDescription);

                set.Apply();
            });
        }

        #endregion Constructors

        #region Properties

        public TextView NameTaskHolder { get; set; }
        public TextView DescriptionTaskHolder { get; set; }

        #endregion Properties
    }
}