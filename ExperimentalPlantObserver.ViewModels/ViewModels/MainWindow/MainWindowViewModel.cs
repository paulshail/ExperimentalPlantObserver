using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using ExperimentalPlantObserver.ViewModels.ViewModels.HeartbeatMonitor;
using ExperimentalPlantObserver.ViewModels.ViewModels.Tabs;
using Microsoft.Extensions.Configuration;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.MainWindow
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public Task Initialize { get; set; }

        #region ViewModels

        private CSVReadViewModel _csvReadViewModel;
        private HistoryViewModel _historyViewModel;
        private HomeViewModel _homeViewModel;
        private LiveViewModel _liveViewModel;
        private SettingsViewModel _settingsViewModel;
        private MainPageViewModel _mainPageViewModel;

        #endregion

        #region Configuration

        private IConfiguration _configuration;

        #endregion

        #region Services

        private IClusterService _clusterService;
        private ISensorService _sensorService;
        private IPlotHelperService _plotHelperService;

        #endregion

        #region CTOR

        public MainWindowViewModel(CSVReadViewModel csvReadViewModel,
                                HistoryViewModel historyViewModel,
                                HomeViewModel homeViewModel,
                                LiveViewModel liveViewModel,
                                SettingsViewModel settingsViewModel,
                                MainPageViewModel mainPageViewModel,
                                ISensorService sensorService,
                                IClusterService clusterSevice,
                                IPlotHelperService plotHelperService)
        {

            // Main page
            _mainPageViewModel = mainPageViewModel;

            // tabs
            _csvReadViewModel = csvReadViewModel;
            _historyViewModel = historyViewModel;
            _homeViewModel = homeViewModel;
            _liveViewModel = liveViewModel;
            _settingsViewModel = settingsViewModel;

            //Config
            //_configuration = configuration;

            // sevices
            _sensorService = sensorService;
            _clusterService = clusterSevice;
            _plotHelperService = plotHelperService;

            //AR heartbeat checker
            this.HeartbeatMonitor = new ARHeartbeatMonitor("Test");

            // Main page
            DisplayedContent = new MainPageViewModel(
            _csvReadViewModel,
            _historyViewModel,
            _homeViewModel,
            _liveViewModel,
            _settingsViewModel,
            _sensorService,
            _clusterService,
            _plotHelperService);

        }

        #endregion


        #region Properties

        private ViewModelBase _displayedContent;

        public ViewModelBase DisplayedContent
        {
            get => _displayedContent;
            set
            {
                _displayedContent = value;
                OnPropertyChanged(nameof(DisplayedContent));
            }
        }

        private ViewModelBase _heartbeatMonitor;

        public ViewModelBase HeartbeatMonitor
        {
            get => _heartbeatMonitor;
            set
            {
                _heartbeatMonitor = value;
                OnPropertyChanged(nameof(HeartbeatMonitor));
            }
        }

        #endregion

    }
}
