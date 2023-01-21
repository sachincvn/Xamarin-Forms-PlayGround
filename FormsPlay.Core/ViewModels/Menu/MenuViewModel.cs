using FormsPlay.Core.ViewModels.Home;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsPlay.Core.ViewModels.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        public IMvxAsyncCommand ShowDetailPageAsyncCommand { get; private set; }

        public MenuViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;
            MenuItemList = new MvxObservableCollection<string>()
            {
                "Home",
                "ClockView",
            };

            ShowDetailPageAsyncCommand = new MvxAsyncCommand(ShowDetailPageAsync);
        }

        private ObservableCollection<string> _menuItemList;
        public ObservableCollection<string> MenuItemList
        {
            get => _menuItemList;
            set => SetProperty(ref _menuItemList, value);
        }

        private async Task ShowDetailPageAsync()
        {
            // Implement your logic here.
            switch (SelectedMenuItem)
            {
                case "Home":
                    await _navigationService.Navigate<HomeViewModel>();
                    break;
                case "ClockView":
                    await _navigationService.Navigate<ClockViewViewModel>();
                    break;
                default:
                    break;
            }

            if (Application.Current.MainPage is FlyoutPage masterDetailPage)
            {
                masterDetailPage.IsPresented = false;
            }
            else if (Application.Current.MainPage is NavigationPage navigationPage
                     && navigationPage.CurrentPage is FlyoutPage nestedMasterDetail)
            {
                nestedMasterDetail.IsPresented = false;
            }
        }

        private string _selectedMenuItem;
        public string SelectedMenuItem
        {
            get => _selectedMenuItem;
            set => SetProperty(ref _selectedMenuItem, value);
        }
    }
}
