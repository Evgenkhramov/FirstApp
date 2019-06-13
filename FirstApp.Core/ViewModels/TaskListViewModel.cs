using Acr.UserDialogs;
using FirstApp.Core.Entities;
using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Plugin.SecureStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel, IListHandler
    {
        #region Variables

        private MvxSubscriptionToken _pushToken;
        private readonly int _userId;
        private readonly ITaskService _taskService;
        private readonly IMvxMessenger _mvxMessenger;

        #endregion Variables

        #region Constructors

        public TaskListViewModel(IMvxNavigationService navigationService, ITaskService taskService, IUserDialogs userDialogs, IMvxMessenger mvxMessenger) : base(navigationService, userDialogs)
        {
            _mvxMessenger = mvxMessenger;
            _taskService = taskService;

            _pushToken = _mvxMessenger.Subscribe<TaskPushMessage>(OpenTaskFromPush);

            _userId = int.Parse(CrossSecureStorage.Current.GetValue(Constants.SequreKeyForUserIdInDB));

            DeleteItemCommand = new MvxCommand<int>(RemoveTaskCollectionItem);
            DeleteItemCommandiOS = new MvxCommand<int>(RemoveCollectionItemiOS);
            ShowTaskChangedView = new MvxCommand<TaskRequestModel>(ClickOnCollectionItem);

            AddData();
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

            List<TaskEntity> list = _taskService.LoadListItemsTask(_userId);

            TaskCollection = new MvxObservableCollection<TaskRequestModel>();

            foreach (var item in list)
            {
                TaskRequestModel element = new TaskRequestModel
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    TaskName = item.TaskName,
                    TaskDescription = item.TaskDescription,
                    VmHandler = this
                };

                TaskCollection.Add(element);
            }

            IsRefreshTaskCollection = false;
        }

        public void OpenTaskFromPush(TaskPushMessage messege)
        {
            if (messege == null)
            {
                return;
            }

            int taskId = Convert.ToInt32(messege.PushMessage);

            TaskRequestModel task = TaskCollection[taskId];//FirstOrDefault<TaskRequestModel>(x => x.Id == taskId);

            if (task == null)
            {
                return;
            }

            ClickOnCollectionItem(task);

            _mvxMessenger.Unsubscribe<TaskPushMessage>(_pushToken);
        }

        public void ClickOnCollectionItem(TaskRequestModel model)
        {
            _navigationService.Navigate<TaskDetailsViewModel, TaskRequestModel>(model);
        }

        public void RemoveTaskCollectionItem(int itemId)
        {
            TaskRequestModel itemForDelete = TaskCollection.FirstOrDefault(x => x.Id == itemId);

            _taskService.DeleteTask(itemId);

            TaskCollection.Remove(item: itemForDelete);
        }

        public void RemoveCollectionItemiOS(int itemId)
        {
            int idForDB = TaskCollection[itemId].Id;

            TaskCollection.RemoveAt(itemId);

            _taskService.DeleteTask(idForDB);
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
