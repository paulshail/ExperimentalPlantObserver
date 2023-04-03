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

        private CSVReadViewModel _csvReadViewModel;
        private HistoryViewModel _historyViewModel;
        private HomeViewModel _homeViewModel;
        private LiveViewModel _liveViewModel;
        private SettingsViewModel _settingsViewModel;

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
                    this._homeViewModel = new HomeViewModel();
                    this.CurrentView = _homeViewModel;
            }
            else if (CurrentView.GetType() == typeof(LiveViewModel))
            {
                    this._liveViewModel = new LiveViewModel(_clusterService, _sensorService, _plotHelperService);
                    this.CurrentView = _liveViewModel;
            }
            else if (CurrentView.GetType() == typeof(HistoryViewModel))
            {
                    this._historyViewModel = new HistoryViewModel(_clusterService, _sensorService, _plotHelperService);
                    this.CurrentView = _historyViewModel;
            }
            else if (CurrentView.GetType() == typeof(CSVReadViewModel))
            {
                    this._csvReadViewModel = new CSVReadViewModel();
                    this.CurrentView = _csvReadViewModel;
            }
                        
            });

        #endregion


    }
}
