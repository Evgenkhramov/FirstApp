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
        #region Constructors

        public TasksViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            int screenWidth = Resources.System.DisplayMetrics.WidthPixels;

            TextView nameTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskName);

            nameTaskHolder.LayoutParameters.Width = screenWidth - Constants.AndroidTaskItemSwipeWidth;

            TextView descriptionTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskShortDescription);

            this.DelayBind(() =>
            {
                MvxFluentBindingDescriptionSet<TasksViewHolder, TaskRequestModel> set = this.CreateBindingSet<TasksViewHolder, TaskRequestModel>();

                set.Bind(nameTaskHolder).To(x => x.TaskName);
                set.Bind(descriptionTaskHolder).To(y => y.TaskDescription);

                set.Apply();
            });
        }

        #endregion Constructors
    }
}