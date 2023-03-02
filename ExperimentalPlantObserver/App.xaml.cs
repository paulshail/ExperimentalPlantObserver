using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.Services.Implementation;
using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.ViewModels.ViewModels.MainWindow;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.Windows;

namespace ExperimentalPlantObserver
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {


        private string _environmentTag;


        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            string env = null;

            for (int i = 0; i < e.Args.Length; i++)
            {
                if(e.Args[i].Equals("-config"))
                {
                    _environmentTag = e.Args[i + 1];
                    switch(e.Args[i+1])
                    {
                        case "Live":
                            env = "appsettings.json";
                            break;
                        case "Test":
                            env = "appsettings.json";
                            break;
                        default:
                            env = null;
                            break;
                    }
                }

                //var builder = new ConfigurationBuilder()
                 //   .SetBasePath(Environment.CurrentDirectory)




            }


        }



        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<MainWindowViewModel>();
            containerRegistry.Register<IClusterService, ClusterService>();
            containerRegistry.Register<ISensorService, SensorService>();
        }

        protected override Window CreateShell()
        {
            MainWindow window = Container.Resolve<MainWindow>();
            return window;
        }

    }
}
