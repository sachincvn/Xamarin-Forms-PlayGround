using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using FormsPlay.Core.ViewModels.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MvvmCross.Binding.Extensions;
using System.Collections.ObjectModel;
using FormsPlay.UI.Controls;

namespace FormsPlay.UI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [MvxMasterDetailPagePresentation(Position = MasterDetailPosition.Detail, NoHistory = true)]
    public partial class HomePage : MvxContentPage<HomeViewModel>
    {
        #region Fields

        private const double TranslatedHeaderX = 15;

        private const double TranslatedHeaderY = 10;

        private bool loaded;

        private bool isNavigationInQueue;

        private double actualHeaderX;

        private double actualHeaderY;

        private double headerDeltaX;

        private double headerDeltaY;

        private double scrollDensity;

        private double width;

        private double height;

        #endregion

        private ObservableCollection<string> _itemsCollection;
        public ObservableCollection<string> ItemsCollection { get => _itemsCollection; set { _itemsCollection = value; } }
        public HomePage()
        {
            InitializeComponent();
            ItemsCollection = new ObservableCollection<string>();

            for (int i = 0; i < 20; i++)
            {
                ItemsCollection.Add(i.ToString());
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.MainPage is NavigationPage navigationPage)
            {
                navigationPage.BarTextColor = Color.White;
                navigationPage.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width == this.width && height == this.height)
            {
                return;
            }
        }


        private void ListView_OnScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!this.loaded)
            {
                this.scrollDensity = Application.Current.MainPage.Width / this.listView.WidthInPixel;

                this.headerDeltaX = this.actualHeaderX - TranslatedHeaderX;
                this.headerDeltaY = this.actualHeaderY - TranslatedHeaderY;
                this.loaded = true;
            }

            var scrollValue = e.Position * this.scrollDensity;

            var factor = (scrollValue + 215) / 215;

            if (scrollValue <= -215)
            {
                this.ActionBar.IsVisible = true;
            }
            else if (scrollValue > -215)
            {
                this.HeaderImage.Opacity = factor;
                this.ActionBar.IsVisible = false;
                //this.SettingsIcon.TranslationY = scrollValue * -1;
                //this.CodeViewerIcon.TranslationY = scrollValue * -1;
            }
        }

        private void ListView_OnSelectionChanged(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null || this.isNavigationInQueue)
            {
                return;
            }
        }



        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //for (int i = 1; i < 6; i++)
            //{
            //    myCollectionView.ScrollTo(i, position: ScrollToPosition.End, animate: true);
            //    await Task.Delay(1000);
            //}


            //await boxView.TranslateTo(MyFrame.Width - boxView.Width, 0, 3000);
            //await Task.Delay(3000);
            //await boxView.TranslateTo(boxView.TranslationX -( MyFrame.Width- boxView.Width), 0, 3000);
        }

    }
}
