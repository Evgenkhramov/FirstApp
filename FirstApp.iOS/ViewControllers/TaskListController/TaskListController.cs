using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using UIKit;

namespace FirstApp.iOS.ViewControllers.TaskList
{
    public class TaskListController : MvxViewController<TaskListViewModel>
    {
        public TaskListController()
        {
        }
    }
}