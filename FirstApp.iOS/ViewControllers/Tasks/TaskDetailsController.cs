using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using System;

namespace FirstApp.iOS
{
    public partial class TaskDetailsController : MvxViewController<TaskDetailsViewModel>
    {
        public TaskDetailsController() : base(nameof(TaskDetailsController), null)
        {
        }
    }
}