using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.ViewModels.Commands;
using ExperimentalPlantObserver.ViewModels.ViewModels.Tabs;
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

        #endregion

        #region Ctor

        public MainPageViewModel(CSVReadViewModel csvReadViewModel, 
                                HistoryViewModel historyViewModel,
                                HomeViewModel homeViewModel,
                                LiveViewModel liveViewModel,
                                SettingsViewModel settingsViewModel,
                                ISensorService sensorService,
                                IClusterService cluster)
        {

            IsLoadingSpinnerVisible = false;

            _csvReadViewModel = csvReadViewModel;
            _historyViewModel = historyViewModel;
            _homeViewModel = homeViewModel;
            _liveViewModel = liveViewModel;
            _settingsViewModel = settingsViewModel;
            _sensorService = sensorService;

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


        #endregion

        #region Commands

        public RelayCommand HomeViewCommand =>
            new RelayCommand(delegate
            {
                // Do not change the view model is on home page
                if(!(CurrentView.GetType() == typeof(HomeViewModel)))
                {
                    this.CurrentView = _homeViewModel;
                }
            });

        public RelayCommand LiveViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _liveViewModel;
            });

        public RelayCommand HistoryViewCommand =>
            new RelayCommand(delegate 
            {
                this.CurrentView = _historyViewModel;
            });

        public RelayCommand CSVReadViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _csvReadViewModel;
            });
        public RelayCommand SettingsViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = _settingsViewModel;
            });

        #endregion


    }
}
