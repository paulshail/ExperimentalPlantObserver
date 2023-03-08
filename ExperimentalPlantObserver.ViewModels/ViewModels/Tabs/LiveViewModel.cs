using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Ioc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentalPlantObserver.Services.Interfaces;
using System.Diagnostics;
using System.Windows.Navigation;
using ExperimentalPlantObserver.ViewModels.Tools;
using ExperimentalPlantObserver.ViewModels.Commands;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class LiveViewModel : ViewModelBase
    {

        #region Services

        private readonly IClusterService _clusterService;

        #endregion

        public Task Initialise { get; set; }

        #region Ctor

        public LiveViewModel(IClusterService clusterService)
        {

            _clusterService = clusterService;

            Initialise = LoadClusters();

            RefreshTimer = "";

        }

        #endregion

        #region Properties

        // Combo boxes
        private ObservableCollection<ClusterDTO> _clusters;

        public ObservableCollection<ClusterDTO> Clusters
        {
            get => _clusters;
            set
            {
                _clusters = value;
                OnPropertyChanged(nameof(Clusters));
            }
        }

        private ObservableCollection<MeasurementUnitDTO> _measurements;

        public ObservableCollection<MeasurementUnitDTO> Measurements
        {
            get => _measurements;
            set
            {
                _measurements = value;
                OnPropertyChanged(nameof(Measurements));
            }
        }

        private ObservableCollection<SensorMeasurementDTO> _sensorMeasurements;

        public ObservableCollection<SensorMeasurementDTO> SensorMeasurements
        {
            get => _sensorMeasurements;
            set
            {
                _sensorMeasurements = value;
                OnPropertyChanged(nameof(SensorMeasurements));
            }
        }

        private string _refreshTimer;

        public string RefreshTimer
        {
            get => _refreshTimer;
            set
            {
                _refreshTimer = value;
                OnPropertyChanged(nameof(RefreshTimer));

                if (!String.IsNullOrEmpty(RefreshTimer))
                {
                    IsPlotSelectionVisible = true;
                }
            }
        }

        private string _timerSelection;

        public string TimerSelection
        {
            get => _timerSelection;
            set
            {
                _timerSelection = value;
                OnPropertyChanged(nameof(TimerSelection));
            
                if(TimerSelection != null)
                {
                    IsRefreshTimerVisible = true;
                }
            }
        }

        private ClusterDTO _selectedCluster;

        public ClusterDTO SelectedCluster
        {
            get => _selectedCluster;
            set
            {

                _selectedCluster = value;
                OnPropertyChanged(nameof(SelectedCluster));

                if (_selectedCluster != null)
                {

                    LoadMeasurments(SelectedCluster.ClusterId);

                    if (Measurements.Count() > 0)
                    {
                        IsMeasurementsVisible = true;
                    }
                    else
                    {
                        NotificationMessageHandler.AddError("No data", "This cluster has no sensor measurements associated");
                    }

                    // Cluster selection would mean everything else needs to be nulled
                    SelectedMeasurementUnit = null;
                    IsPlotAverage = false;

                    // Hide UI components
                    IsPlotSelectionVisible = false;
                    IsTimeSelectionVisible = false;
                    IsRefreshTimerVisible = false;
                    IsPlotVisible = false;

                }
            }
        }

        private MeasurementUnitDTO _selectedMeasurementUnit;

        public MeasurementUnitDTO SelectedMeasurementUnit
        {
            get => _selectedMeasurementUnit;
            set
            {

                _selectedMeasurementUnit = value;
                OnPropertyChanged(nameof(SelectedMeasurementUnit));

                if (_selectedMeasurementUnit != null)
                {
                    IsTimeSelectionVisible = true;
                }
            }
        }

        private bool _isPlotAverage;

        public bool IsPlotAverage
        {
            get => _isPlotAverage;
            set
            {
                _isPlotAverage = value;
                OnPropertyChanged(nameof(IsPlotAverage));

                if(IsPlotAverage != null)
                {
                    IsPlotVisible = true;
                }
            }
        }


        #region Component visibility
        private bool _isMeasurementsVisible;

        public bool IsMeasurementsVisible
        {
            get => _isMeasurementsVisible;
            set
            {
                _isMeasurementsVisible = value;
                OnPropertyChanged(nameof(IsMeasurementsVisible));
            }
        }

        private bool _isTimeSelectionVisible;

        public bool IsTimeSelectionVisible
        {
            get => _isTimeSelectionVisible;
            set
            {
                _isTimeSelectionVisible = value;
                OnPropertyChanged(nameof(IsTimeSelectionVisible));
            }
        }

        private bool _isRefreshTimerVisible;

        public bool IsRefreshTimerVisible
        {
            get => _isRefreshTimerVisible;
            set
            {
                _isRefreshTimerVisible = value;
                OnPropertyChanged(nameof(IsRefreshTimerVisible));
            }
        }

        

        private bool _isPlotSelectionVisible;

        public bool IsPlotSelectionVisible
        {
            get => _isPlotSelectionVisible;
            set
            {
                _isPlotSelectionVisible = value;
                OnPropertyChanged(nameof(IsPlotSelectionVisible));
            }
        }

        private bool _isPlotVisible;

        public bool IsPlotVisible
        {
            get => _isPlotVisible;
            set
            {
                _isPlotVisible = value;
                OnPropertyChanged(nameof(IsPlotVisible));
            }
        }

        #endregion

        #endregion

        #region Commands

        public RelayCommand TimeSelectionCommmand =>
            new RelayCommand(param => 
            {

                // return if command param is null (shouldn't happen)
                if(param== null) { return; }
                string interval = param.ToString();

                TimerSelection = interval;

            });

        public RelayCommand PlotAverageCommand =>
            new RelayCommand(delegate
            {

                IsPlotAverage = true;

            });

        public RelayCommand PlotSensorsCommand =>
            new RelayCommand(delegate 
            {

                IsPlotAverage = false;

            });


        #endregion

        #region Methods

        private async Task LoadClusters()
        { 
            Clusters = await _clusterService.GetAllClustersAsync();
        }

        private async Task LoadMeasurments(int clusterId)
        {
            Measurements = await _clusterService.GetMeasurementUnitsForCluster(clusterId);
        }

        #endregion

    }
}
