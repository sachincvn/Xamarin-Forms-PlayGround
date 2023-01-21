using MvvmCross.IoC;
using MvvmCross.ViewModels;
using FormsPlay.Core.ViewModels.Root;

namespace FormsPlay.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<RootViewModel>();
        }
    }
}
