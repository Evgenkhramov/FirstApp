using Acr.UserDialogs;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel, IListHandler
    {
        #region Variables
        int _userId;
        private readonly IDBTaskService _dBTaskService;

        #endregion Variables

        #region Constructors

        public TaskListViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService, IUserDialogs userDialogs) : base(navigationService, userDialogs)
        {
            _userId = int.Parse(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));
            _dBTaskService = dBTaskService;

            DeleteItemCommand = new MvxCommand<int>(RemoveTaskCollectionItem);
            DeleteItemCommandiOS = new MvxCommand<int>(RemoveCollectionItemiOS);
            AddData();
            ShowTaskChangedView = new MvxAsyncCommand<TaskRequestModel>(CollectionItemClick);
        }

        #endregion Constructors

        #region Properties

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

        private MvxObservableCollection<TaskRequestModel> _taskCollection;
        public MvxObservableCollection<TaskRequestModel> TaskCollection
        {
            get => _taskCollection;
            set
            {
                _taskCollection = value;
                RaisePropertyChanged(() => TaskCollection);
            }
        }

        #endregion Properties

        #region Commands  

        public IMvxCommand<TaskRequestModel> ShowTaskChangedView { get; set; }

        private MvxCommand _refreshCommand;

        public MvxCommand RefreshTaskCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(AddData);

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

        public IMvxCommand<int> DeleteItemCommandiOS { get; set; }

        #endregion Commands

        #region Methods

        public void AddData()
        {
            IsRefreshTaskCollection = true;

            List<TaskModel> list = _dBTaskService.LoadListItemsTask(_userId);

            TaskCollection = new MvxObservableCollection<TaskRequestModel>();

            foreach (var item in list)
            {
                TaskRequestModel element = new TaskRequestModel();

                element.Id = item.Id;
                element.UserId = item.UserId;
                element.TaskName = item.TaskName;
                element.TaskDescription = item.TaskDescription;
                element.VmHandler = this;
                TaskCollection.Add(element);
            }

            IsRefreshTaskCollection = false;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            MvxObservableCollection<TaskRequestModel> obsSender = sender as MvxObservableCollection<TaskRequestModel>;

            int element = eventArgs.OldItems.Count;
        }

        public async Task CollectionItemClick(TaskRequestModel model)
        {
            await _navigationService.Navigate<TaskDetailsViewModel, TaskRequestModel>(model);
        }

        public void RemoveTaskCollectionItem(int itemId)
        {
            TaskRequestModel itemForDelete = null;
            _dBTaskService.DeleteTaskFromTable(itemId);
            foreach (TaskRequestModel item in TaskCollection)
            {
                if (item.Id == itemId)
                {
                    itemForDelete = item;
                }
            }

            TaskCollection.Remove(item: itemForDelete);
        }

        public void RemoveCollectionItemiOS(int itemId)
        {
            var idForDB = TaskCollection[itemId].Id;
            TaskCollection.RemoveAt(itemId);
            _dBTaskService.DeleteTaskFromTable(idForDB);
        }

        #endregion Methods

        #region Overrides

        public override void ViewAppearing()
        {
            RefreshTaskCommand.Execute();
        }

        #endregion Overrides
    }
}
