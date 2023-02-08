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

        #region Ctor

        public MainPageViewModel()
        {

            this.CurrentView = new HomeViewModel();

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

        #endregion

        #region Commands

        public RelayCommand HomeViewCommand =>
            new RelayCommand(delegate
            {
                // Do not change the view model is on home page
                if(!(CurrentView.GetType() == typeof(HomeViewModel)))
                {
                    this.CurrentView = new HomeViewModel();
                }
            });

        public RelayCommand LiveViewCommand =>
            new RelayCommand(delegate
            {


                this.CurrentView = new LiveViewModel();
            });

        public RelayCommand HistoryViewCommand =>
            new RelayCommand(delegate 
            {
                this.CurrentView = new HistoryViewModel();
            });

        public RelayCommand CSVReadViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = new CSVReadViewModel();
            });
        public RelayCommand SettingsViewCommand =>
            new RelayCommand(delegate
            {
                this.CurrentView = new SettingsViewModel();
            });

        #endregion


    }
}
