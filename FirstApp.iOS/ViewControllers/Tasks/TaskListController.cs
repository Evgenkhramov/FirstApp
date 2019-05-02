using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS
{
    public partial class TaskListController : MvxViewController<TaskListViewModel>
    {
        public TaskListController () : base (nameof(TaskListController), null)
        {
        }
    }
}