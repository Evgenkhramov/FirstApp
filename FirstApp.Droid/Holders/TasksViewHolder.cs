using Android.Content.Res;
using Android.Views;
using Android.Widget;
using FirstApp.Core.Models;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace FirstApp.Droid.Holders
{
    public class TasksViewHolder : MvxRecyclerViewHolder
    {
        public static int ScreenWidth;
        public TextView NameTaskHolder { get; set; }
        public TextView DescriptionTaskHolder { get; set; }

        public TasksViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {
            ScreenWidth = Resources.System.DisplayMetrics.WidthPixels;
            NameTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskName);
            NameTaskHolder.LayoutParameters.Width = ScreenWidth - 75;
            DescriptionTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskShortDescription);      
       
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TasksViewHolder, TaskModel>();
                set.Bind(this.NameTaskHolder).To(x => x.TaskName);
                set.Bind(this.DescriptionTaskHolder).To(y => y.TaskDescription);
                set.Apply();
            });       
        }
    }
}