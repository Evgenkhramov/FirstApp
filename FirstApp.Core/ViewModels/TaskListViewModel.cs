using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel, IListHandler
    {
        public int taskItem;
        private readonly IDBTaskService _dBTaskService;

        public TaskListViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService) : base(navigationService)
        {
            _dBTaskService = dBTaskService;
            DeleteItemCommand = new MvxCommand<int>(RemoveCollectionItem);
            AddData();
            ShowTaskChangedView = new MvxAsyncCommand<TaskModel>(CollectionItemClick);
        }
        public IMvxCommand<TaskModel> ShowTaskChangedView { get; set; }

        public void AddData()
        {
            IsRefreshTaskCollection = true;

            var list = _dBTaskService.LoadListItemsTask();

            foreach (var item in list)
            {
                item.VmHandler = this;
                //item.DeleteItemVMCommand = DeleteItemCommand;
                //item.ItemClickVMCommand = ShowTaskChangedView;
            }

            TaskCollection = new MvxObservableCollection<TaskModel>();
            TaskCollection.AddRange(list);
           
            IsRefreshTaskCollection = false;
        }

        private bool _isRefreshTaskCollection;
        public bool IsRefreshTaskCollection
        {
            get => _isRefreshTaskCollection;
            set
            {
                _isRefreshTaskCollection = value;
                RaisePropertyChanged(() => IsRefreshTaskCollection);
            }
        }

        private MvxCommand _refreshCommand;
        public MvxCommand RefreshTaskCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(AddData);

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

        public IMvxCommand<int> ItemClickCommand { get; set; }

        public IMvxCommand<int> DeleteItemCommand { get; set; }

        public override void ViewAppearing()
        {
            RefreshTaskCommand.Execute();
        }

        public async Task CollectionItemClick(TaskModel model)
        {
            var result = await _navigationService.Navigate<TaskDetailsViewModel, TaskModel>(model);
        }
        
        public void RemoveCollectionItem(int itemId)
        {
            TaskModel _itemForDelete = null;
            _dBTaskService.DeleteTaskFromTable(itemId);
            foreach (TaskModel item in TaskCollection)
            {
                if (item.Id == itemId)
                {
                    _itemForDelete = item;
                }
            }

            TaskCollection.Remove(item: _itemForDelete);
        }
    }
}
