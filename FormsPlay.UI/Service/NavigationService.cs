using FormsControls.Base;
using FormsPlay.Core.Services;
using FormsPlay.Core.ViewModels;
using FormsPlay.UI.Pages;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FormsPlay.UI.Service
{
    public class NavigationService : INavigationService
    {
        private readonly IPageAnimation _animation;
        private readonly INavigation _navigation;
        public NavigationService()
        {
            //_animation = animation;
            //_navigation = navigation;
        }
        public async Task NavigateToAsync()
        {
            await _navigation.PushAsync(new MyNewPage { PageAnimation = _animation });

            //return InternalNavigateToAsync(typeof(TViewModel));
        }

        private async Task InternalNavigateToAsync(Type type)
        {
            AnimationPage page = CreatePage(type);
            if (page != null)
            {
                _animation.Type = AnimationType.Fade;
                _animation.Subtype = AnimationSubtype.Default;
               // await _navigation.PushAsync(new page { PageAnimation = _animation });
            }
        }

        private AnimationPage CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
            }
            try
            {
                AnimationPage page = Activator.CreateInstance(pageType) as AnimationPage;
                return page;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace(".Core.ViewModels.", ".UI.Pages.").Replace("ViewModel", "Page");
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, GetType().Assembly.FullName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }
    }
}
