using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using ExperimentalPlantObserver.ViewModels.Commands;
using ExperimentalPlantObserver.ViewModels.ViewModels.HeartbeatMonitor;
using ExperimentalPlantObserver.ViewModels.ViewModels.Tabs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public Task Initialise { get; set; }

        #region ViewModels

        private readonly CSVReadViewModel _csvReadViewModel;
        private readonly HistoryViewModel _historyViewModel;
        private readonly HomeViewModel _homeViewModel;
        private readonly LiveViewModel _liveViewModel;
        private readonly SettingsViewModel _settingsViewModel;

        #endregion

        #region Services

        private readonly IClusterService _clusterService;
        private readonly ISensorService _sensorService;
        private readonly IPlotHelperService _plotHelperService;

        #endregion

        #region Ctor

        public MainPageViewModel(CSVReadViewModel csvReadViewModel, 
                                HistoryViewModel historyViewModel,
                                HomeViewModel homeViewModel,
                                LiveViewModel liveViewModel,
                                SettingsViewModel settingsViewModel,
                                ISensorService sensorService,
                                IClusterService clusterService,
                                IPlotHelperService plotHelperService)
        {

            IsLoadingSpinnerVisible = false;

            // ViewModels
            _csvReadViewModel = csvReadViewModel;
            _historyViewModel = historyViewModel;
            _homeViewModel = homeViewModel;
            _liveViewModel = liveViewModel;
            _settingsViewModel = settingsViewModel;
            
            // Services            
            _sensorService = sensorService;
            _clusterService = clusterService;
            _plotHelperService = plotHelperService;

            // Set to home when software is started
            TabTitle = "Home";
            this.CurrentView = homeViewModel;

        }

        #endregion

        #region Properties

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }


        private bool _isLoadingSpinnerVisible;

        public bool IsLoadingSpinnerVisible
        {
            get => _isLoadingSpinnerVisible;
            set
            {
                _isLoadingSpinnerVisible = value;
                OnPropertyChanged(nameof(IsLoadingSpinnerVisible));
            }
        }

        private string _tabTitle;
        public string TabTitle
        {
            get => _tabTitle;
            set
            {
                _tabTitle = value;
                OnPropertyChanged(nameof(TabTitle));
            }
        }

        #endregion

        #region Commands

        public RelayCommand HomeViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _homeViewModel;
                TabTitle = "Home";
            });

        public RelayCommand LiveViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _liveViewModel;
                TabTitle = "Live";
            });

        public RelayCommand HistoryViewCommand =>
            new RelayCommand(delegate 
            {
                this.CurrentView = _historyViewModel;
                TabTitle = "History";
            });

        public RelayCommand CSVReadViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _csvReadViewModel;
                TabTitle = "CSV Read";
            });
        public RelayCommand SettingsViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _settingsViewModel;
                TabTitle = "Settings";
            });

        public RelayCommand RefreshViewCommand =>
            new RelayCommand(delegate
            {

            if (CurrentView.GetType() == typeof(HomeViewModel))
            {
                this.CurrentView = new HomeViewModel();
            }
            else if (CurrentView.GetType() == typeof(LiveViewModel))
            {
                this.CurrentView = new LiveViewModel(_clusterService, _sensorService, _plotHelperService);
            }
            else if (CurrentView.GetType() == typeof(HistoryViewModel))
            {
                    this.CurrentView = new HistoryViewModel(_clusterService, _sensorService, _plotHelperService);
            }
            else if (CurrentView.GetType() == typeof(CSVReadViewModel))
            {
                    this.CurrentView = new CSVReadViewModel();
            }
                        
            });

        #endregion


    }
}
