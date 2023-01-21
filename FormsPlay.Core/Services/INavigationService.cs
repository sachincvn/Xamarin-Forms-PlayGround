using FormsPlay.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FormsPlay.Core.Services
{
    public interface INavigationService
    {
        Task NavigateToAsync();
    }
}
