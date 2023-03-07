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

                if(_selectedCluster != null)
                {
                    IsMeasurementsVisible = true;
                }
            }
        }

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
                _isRefreshTimerVisible= value;
                OnPropertyChanged(nameof(IsRefreshTimerVisible));
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

        #region Methods

        private async Task LoadClusters()
        {

            _clusters = await _clusterService.GetAllClustersAsync();
            Debug.Write("test");
        }

        #endregion

    }
}
