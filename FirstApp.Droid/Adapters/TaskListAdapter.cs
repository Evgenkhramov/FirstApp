using Android.Support.V7.Widget;
using Android.Views;
using FirstApp.Droid.Holders;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace FirstApp.Droid.Adaptere
{
    public class TaskListAdapter : MvxRecyclerAdapter
    {
        public TaskListAdapter(IMvxAndroidBindingContext bindingContext)
            : base(bindingContext)
        {
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var itemBindingContext = new MvxAndroidBindingContext(parent.Context, BindingContext.LayoutInflaterHolder);
            var view = InflateViewForHolder(parent, viewType, itemBindingContext);
            return new TasksViewHolder(view, itemBindingContext)
            {
                Click = ItemClick,
                LongClick = ItemLongClick
            };
        }
    }
}