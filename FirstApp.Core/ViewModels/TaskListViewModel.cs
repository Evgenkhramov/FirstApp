using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel
    {
        public int taskItem;
        private readonly IDBTaskService _dBTaskService;
        public TaskListViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService) : base(navigationService)
        {
            _dBTaskService = dBTaskService;
            //_dBTaskService = Mvx.IoCProvider.Resolve<IDBTaskService>();
            AddData();
            ShowTaskChangedView = new MvxAsyncCommand<TaskModel>(ShowTaskChanged);
        }
        public IMvxCommand ShowTaskChangedView { get; set; }

        public void AddData()
        {
            TaskCollection = new MvxObservableCollection<TaskModel>();
            TaskCollection.AddRange(_dBTaskService.LoadListItemsTask());
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

        private async Task ShowTaskChanged(TaskModel _taskCreate)
        {
            var result = await _navigationService.Navigate<TaskDetailsViewModel, TaskModel>(_taskCreate);
        }

        public MvxAsyncCommand<int> DeleteItem
        {
            get
            {
                return new MvxAsyncCommand<int>(async (taskId) =>
                {
                    _dBTaskService.DeleteTaskFromTable(taskId);
                });
             }
        }

        public MvxAsyncCommand<int> DeleteItemFromList
        {
            get
            {
                return new MvxAsyncCommand<int>(async (position) =>
                {
                    TaskCollection.RemoveAt(position);
                });
            }
        }

        //public MvxAsyncCommand DeleteThisItem
        //{
        //    get
        //    {
        //        return new MvxAsyncCommand(async () =>
        //        {
        //            await _navigationService.Navigate<TaskDetailsViewModel>();
        //        });
        //    }
        //}

        public MvxAsyncCommand CreateNewTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Navigate<TaskDetailsViewModel>();

                });
            }
        }

        public override void ViewAppearing()
        {
            AddData();
        }
    }
}
