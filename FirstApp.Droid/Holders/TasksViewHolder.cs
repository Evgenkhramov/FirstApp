using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
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
        public TextView NameTaskHolder { get; set; }
        public TextView DescriptionTaskHolder { get; set; }
       
        //public Typeface newTypeface1 = Typeface.CreateFromAsset(Application.Context.Assets, "NK123.otf");

        public TasksViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {

            NameTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskName);
            DescriptionTaskHolder = itemView.FindViewById<TextView>(Resource.Id.TaskShortDescription);
            
            //NameTaskHolder.SetTypeface(newTypeface1, TypefaceStyle.Normal);

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