using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        private int userId;
      
        public TaskListViewModel(IMvxNavigationService navigationService) : base(navigationService)
        {         
            AddSomeData();
        }

        private MvxObservableCollection<TaskModel> _taskCollection;
        public MvxObservableCollection<TaskModel> TaskCollection
        {
            get => _taskCollection;
            set
            {
                _taskCollection = value;
                RaisePropertyChanged(() => TaskCollection);
            }
        }

        public void AddSomeData()
        {
            TaskCollection = new MvxObservableCollection<TaskModel>();
            TaskModel someTask = new TaskModel();
            someTask.TaskName = "FirstTask";
            someTask.TaskDescription = "This is first task";
            TaskCollection.Add(someTask);
        }

        public MvxAsyncCommand CreateNewTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Navigate<ChangeTaskViewModel>();
                });
            }
        }
    }
}
