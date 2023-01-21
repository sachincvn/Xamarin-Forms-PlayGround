using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using FormsPlay.Core.ViewModels.Home;
using FormsPlay.Core.ViewModels.Menu;
using FormsPlay.Core.ViewModels.Main;

namespace FormsPlay.Core.ViewModels.Root
{
    public class RootViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public RootViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
        }


        public override async void ViewAppearing()
        {
            base.ViewAppearing();
            await _navigationService.Navigate<MenuViewModel>();
            await _navigationService.Navigate<HomeViewModel>();
        }
    }
}
