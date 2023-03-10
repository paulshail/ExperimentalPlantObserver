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
        private readonly ISensorService _sensorService;

        #endregion

        public Task Initialise { get; set; }

        #region Ctor

        public LiveViewModel(IClusterService clusterService, ISensorService sensorService)
        {

            _clusterService = clusterService;
            _sensorService = sensorService;

            Initialise = LoadClusters();

            RefreshTimer = "";

        }

        #endregion

        #region Properties

        // Items source for combo box for cluster
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

        // Items source for combo box for measurements
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

        // Selected item from cluster combo box
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
                    IsPlotAverage = null;


                    // Hide UI components
                    IsPlotTypeSelectionVisible = false;
                    IsTimeScaleSelectionVisible = false;
                    IsRefreshTimerVisible = false;
                    IsPlotVisible = false;

                }
            }
        }

        // Selected Item for measurement unit combo box
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
                    IsTimeScaleSelectionVisible = true;
                }
            }
        }

        // String value from button press
        private string _timeScaleSelection;
        public string TimeScaleSelection
        {
            get => _timeScaleSelection;
            set
            {
                _timeScaleSelection = value;
                OnPropertyChanged(nameof(TimeScaleSelection));

                if (TimeScaleSelection != null)
                {
                    IsRefreshTimerVisible = true;
                }
            }
        }

        // Time between database checks
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

                    try
                    {
                        Double.Parse(RefreshTimer);
                        IsPlotTypeSelectionVisible = true;
                    }
                    catch
                    {
                        NotificationMessageHandler.AddError("Error", "Enter a numeric value for the refresh timer");
                        TimeScaleSelection = null;
                        IsPlotTypeSelectionVisible = false;
                        IsPlotVisible = false;
                    }
                    
                }
            }
        }

        
        // Selecting if cluster average or individual sensors are used
        private bool? _isPlotAverage;

        public bool? IsPlotAverage
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


        // In order of appearance

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

        private bool _isTimeScaleSelectionVisible;

        public bool IsTimeScaleSelectionVisible
        {
            get => _isTimeScaleSelectionVisible;
            set
            {
                _isTimeScaleSelectionVisible = value;
                OnPropertyChanged(nameof(IsTimeScaleSelectionVisible));
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


        private bool _isPlotTypeSelectionVisible;

        public bool IsPlotTypeSelectionVisible
        {
            get => _isPlotTypeSelectionVisible;
            set
            {
                _isPlotTypeSelectionVisible = value;
                OnPropertyChanged(nameof(IsPlotTypeSelectionVisible));
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

        #region Graph Plot Properties

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

        #endregion

        #endregion

        #region Commands

        public RelayCommand TimeSelectionCommand =>
            new RelayCommand(param => 
            {

                // return if command param is null (shouldn't happen)
                if(param== null) { return; }
                string interval = param.ToString();

                TimeScaleSelection = interval;

            });

        public RelayCommand PlotTypeCommand =>
             new RelayCommand(param => 
             {
                if(param== null) { return; }
                 string plotType = param.ToString();

                 switch (plotType)
                 {
                     case "avg":
                         IsPlotAverage = true;
                         break;
                     case "sensor":
                         IsPlotAverage = null;
                         NotificationMessageHandler.AddInfo("Not Implemented", "Cluster average has not been implemented");
                         break;
                     default:
                         IsPlotAverage = null;
                         break;
                 }

             });

        public RelayCommand PlotLiveDataCommand =>
            new RelayCommand(delegate
            {

                if (GetStartDate() != null)
                {

                    SensorMeasurements = _sensorService.GetMeasurementsForAllSensorsWithMeasurementIdStartDateEndDate(SelectedCluster.ClusterSensors, SelectedMeasurementUnit.PK_measurementUnit_Id, GetStartDate(), DateTime.Now);
                
                }
                else
                {
                    NotificationMessageHandler.AddError("Error", "Error setting start date");
                }

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

        public DateTime? GetStartDate()
        {

            switch (TimeScaleSelection)
            {
                case "day":
                    return DateTime.Now.AddDays(-1);
                    break;
                case "week":
                    return DateTime.Now.AddDays(-7);
                case "month":
                    return DateTime.Now.AddMonths(-1);
                default:
                    return null;
                    break;
            }


        }

        #endregion

    }
}
