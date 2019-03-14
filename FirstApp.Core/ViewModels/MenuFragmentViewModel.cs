using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Core.ViewModels
{
    public class MenuFragmentViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public MenuFragmentViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            //ShowPlanetsCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<PlanetsViewModel>());
            //ShowPeopleCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<PeopleViewModel>());
            //ShowStatusCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<StatusViewModel>());
        }

        // MvvmCross Lifecycle

        // MVVM Properties

        // MVVM Commands
        //public IMvxCommand ShowStatusCommand { get; private set; }
        //public IMvxCommand ShowPlanetsCommand { get; private set; }
        //public IMvxCommand ShowPeopleCommand { get; private set; }

        // Private methods
    }
}
