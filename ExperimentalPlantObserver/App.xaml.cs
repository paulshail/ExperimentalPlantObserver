using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Repository.Implementation;
using ExperimentalPlantObserver.Repository.Interfaces;
using ExperimentalPlantObserver.Services.Implementation;
using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.ViewModels.ViewModels.MainWindow;
using Microsoft.EntityFrameworkCore;
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

        private string _connString = "Data Source=RONLAPTOP;Initial Catalog=EPODatabase;Integrated Security=True; Trust Server Certificate=true";

        private string _environmentTag;

        private IConfiguration _configuration;


        //protected override void OnStartup(StartupEventArgs e)
        //{

        //    string env = null;


        //    // Check for env in startup args
        //    for (int i = 0; i < e.Args.Length; i++)
        //    {
        //        if (e.Args[i].Equals("-config"))
        //        {
        //            _environmentTag = e.Args[i + 1];
        //            switch (e.Args[i + 1])
        //            {
        //                case "Live":
        //                    env = "appsettings.json";
        //                    break;
        //                case "Test":
        //                    env = "appsettings.Development.json";
        //                    break;
        //                default:
        //                    env = null;
        //                    break;
        //            }
        //        }


        //        //
        //        var builder = new ConfigurationBuilder()
        //            .SetBasePath(Environment.CurrentDirectory)
        //            .AddJsonFile(env, optional: false, reloadOnChange: false);

        //        _configuration = builder.Build();

        //        base.OnStartup(e);

        //    }


        //}


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Registering App settings file
            // Check for env in startup args

            var e = Environment.GetCommandLineArgs();
            string env = null;

            for (int i = 0; i < e.Length; i++)
            {
                if (e[i].Equals("-config"))
                {
                    _environmentTag = e[i + 1];
                    switch (e[i + 1])
                    {
                        case "Live":
                            env = "appsettings.json";
                            break;
                        case "Test":
                            env = "appsettings.Development.json";
                            break;
                        default:
                            env = null;
                            break;
                    }
                }
            }

                //
                var configBuilder = new ConfigurationBuilder()
                    .SetBasePath(Environment.CurrentDirectory)
                    .AddJsonFile(env, optional: false, reloadOnChange: false);

                _configuration = configBuilder.Build();


                // Register View model
                containerRegistry.Register<MainWindowViewModel>();

            // Register Services
            containerRegistry.Register<IClusterService, ClusterService>();
            containerRegistry.Register<ISensorService, SensorService>();

            // Register Repositories
            containerRegistry.Register<IClusterRepository<int, ClusterDTO>, ClusterRepository>();
            containerRegistry.Register<ISensorRepository<int, SensorDTO>, SensorRepository>();

            // Register DB with connection string
            var builder = new DbContextOptionsBuilder<PlantDataContext>();
            builder.UseSqlServer(_configuration.GetConnectionString(_environmentTag));
            containerRegistry.RegisterInstance(builder.Options);
            containerRegistry.Register<PlantDataContext>();
        }

        protected override Window CreateShell()
        {
            MainWindow window = Container.Resolve<MainWindow>();
            return window;
        }

    }
}
