using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using FormsControls.Base;
using FormsPlay.Core.Services;
using MvvmCross;
using MvvmCross.Commands;
using Xamarin.Forms;

namespace FormsPlay.Core.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        private List<string> _items;
        public List<string> Items { get => _items; set => SetProperty(ref _items, value); }
        public IMvxCommand AddMoreCommand { get; }
        public IMvxCommand NavigateCommand { get; }
        public HomeViewModel()
        {
            Items = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                Items.Add(string.Empty);
            }
            AddMoreCommand = new MvxCommand(() =>
            {
                Items.Add(string.Empty);
            });

            NavigateCommand = new MvxCommand(NavigateCommandAsync);
        }

        private void NavigateCommandAsync()
        {
            var navigationService = Mvx.IoCProvider.Resolve<INavigationService>();
            navigationService.NavigateToAsync();
        }
    }
}
