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
        private readonly IDBTaskService _dBTaskService;
        public TaskListViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService) : base(navigationService)
        {
            _dBTaskService = dBTaskService;
            //_dBTaskService = Mvx.IoCProvider.Resolve<IDBTaskService>();
            AddSomeData();
            //ShowTaskChangedView = new MvxAsyncCommand<TaskModel>(ShowTaskChanged);
        }
        public IMvxCommand ShowTaskChangedView { get; set; }

        public void AddSomeData()
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

        //private async Task ShowTaskChanged(TaskModel _taskCreate)
        //{
        //    var result = await _navigationService.Navigate<TaskDetailsViewModel, TaskModel>(_taskCreate);

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
    }
}
