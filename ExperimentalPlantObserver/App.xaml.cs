using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.ViewModels.ViewModels.MainWindow;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System.Diagnostics;
using System.Windows;

namespace ExperimentalPlantObserver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);


        }



        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowViewModel>();
        }

        protected override  Window CreateShell()
        { 
            MainWindow window = Container.Resolve<MainWindow>();
            return window;
        }

        protected void RegisterServices()
        {

        }

    }
}
