using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.ViewModels.ViewModels.Tabs;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
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

        #region Services

        private IClusterService _clusterService;
        private ISensorService _sensorService;

        #endregion

        #region CTOR

        public MainWindowViewModel(CSVReadViewModel csvReadViewModel,
                                HistoryViewModel historyViewModel,
                                HomeViewModel homeViewModel,
                                LiveViewModel liveViewModel,
                                SettingsViewModel settingsViewModel,
                                MainPageViewModel mainPageViewModel,
                                ISensorService sensorService,
                                IClusterService clusterSevice)
        {

            // Main page
            _mainPageViewModel = mainPageViewModel;

            // tabs
            _csvReadViewModel = csvReadViewModel;
            _historyViewModel = historyViewModel;
            _homeViewModel = homeViewModel;
            _liveViewModel = liveViewModel;
            _settingsViewModel = settingsViewModel;
            
            // sevices
            _sensorService = sensorService;
            _clusterService = clusterSevice;

            DisplayedContent = new MainPageViewModel(
            _csvReadViewModel,
            _historyViewModel,
            _homeViewModel,
            _liveViewModel,
            _settingsViewModel,
            _sensorService,
            _clusterService);

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
        
        #endregion

    }
}
