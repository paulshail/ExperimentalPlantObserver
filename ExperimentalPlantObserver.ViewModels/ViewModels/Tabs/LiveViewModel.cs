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
using System.Diagnostics.Metrics;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using ExperimentalPlantObserver.Base.Helpers.PlotViewHelper;
using System.Windows.Threading;
using System.Windows;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class LiveViewModel : ViewModelBase
    {

        #region Services

        private readonly IClusterService _clusterService;
        private readonly ISensorService _sensorService;
        private readonly IPlotHelperService _plotHelperService;

        #endregion

        #region vars

        private DispatcherTimer _refreshDispatchTimer;

        #endregion

        public Task Initialise { get; set; }

        #region Ctor

        public LiveViewModel(IClusterService clusterService, ISensorService sensorService, IPlotHelperService plotHelperService)
        {

            _clusterService = clusterService;
            _sensorService = sensorService;
            _plotHelperService = plotHelperService;

            IsPlotting = false;

            Initialise = LoadClusters();

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

                    LoadMeasurements(SelectedCluster.ClusterId);
                    LoadSensorCluster(SelectedCluster.ClusterId);

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
                    IsRefreshTimeSelectVisible = true;
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

        private TimeSpan _refreshTimeSpan;
        public TimeSpan RefreshTimeSpan
        {
            get => _refreshTimeSpan;
            set
            {
                _refreshTimeSpan = value;
                OnPropertyChanged(nameof(RefreshTimeSpan));

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

                if (IsPlotAverage != null)
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

        private bool _isRefreshTimeSelectVisible;

        public bool IsRefreshTimeSelectVisible
        {
            get => _isRefreshTimeSelectVisible;
            set
            {
                _isRefreshTimeSelectVisible = value;
                OnPropertyChanged(nameof(IsRefreshTimeSelectVisible));
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
                if (IsPlotVisible == true)
                {
                    IsRefreshTimerVisible = true;
                }
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

        #endregion

        #region Graph Plot Properties

        private bool _isPlotting;

        public bool IsPlotting
        {
            get => _isPlotting;
            set
            {
                _isPlotting = value;
                OnPropertyChanged(nameof(IsPlotting));
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


        private ViewResolvingPlotModel _liveDataPlot;
        public ViewResolvingPlotModel LiveDataPlot
        {
            get => _liveDataPlot;
            set
            {
                _liveDataPlot = value;
                OnPropertyChanged(nameof(LiveDataPlot));
            }
        }

        #endregion

        #endregion

        #region Commands

        public RelayCommand TimeSelectionCommand =>
            new RelayCommand(param =>
            {

                // return if command param is null (shouldn't happen)
                if (param == null) { return; }
                string interval = param.ToString();

                TimeScaleSelection = interval;

                NotificationMessageHandler.AddInfo("Time Scale", "Time scale set to " + interval);

            });

        public RelayCommand PlotTypeCommand =>
             new RelayCommand(param =>
             {
                 if (param == null) { return; }
                 string plotType = param.ToString();

                 switch (plotType)
                 {
                     case "avg":
                         IsPlotAverage = null;
                         NotificationMessageHandler.AddInfo("Not Implemented", "Cluster average has not been implemented");
                         break;
                     case "sensor":
                         IsPlotAverage = false;
                         NotificationMessageHandler.AddInfo("Sensors", "Plot type set to individual sensors");
                         break;
                     default:
                         IsPlotAverage = null;
                         break;
                 }

             });

        public RelayCommand PlotLiveDataCommand =>
            new RelayCommand(async delegate
            {
            if (!IsPlotting)
            {

                // Can do double parse since this input is validated earlier in the UI
                RefreshTimeSpan = ResetRefreshTimer();
                _refreshDispatchTimer = BeginRefreshTimer();
                    _refreshDispatchTimer.Start();

                    if (IsPlotAverage == true)
                    {
                        NotificationMessageHandler.AddInfo("Not Implemented", "Cluster average has not been implemented");
                    }
                    else if(IsPlotAverage == false)
                    {
                        if (GetStartDate() != DateTime.Now)
                        {

                            SensorMeasurements = await _sensorService.GetMeasurementsForAllSensorsWithMeasurementIdStartDateEndDate(SelectedCluster.ClusterSensors, SelectedMeasurementUnit.PK_measurementUnit_Id, GetStartDate(), DateTime.Now);
                            if (SensorMeasurements.Count() > 0)
                            {

                                LiveDataPlot = _plotHelperService.CreateDataPlot(SensorMeasurements, SelectedMeasurementUnit, GetStartDate(), DateTime.Now);

                            }
                            else
                            {
                                NotificationMessageHandler.AddInfo("No data", "No sensor measurements are available");
                            }
                        }
                        else
                        {
                            NotificationMessageHandler.AddError("Error", "Error setting start date");
                        }
                    }
                }
            });

        #endregion

        #region Methods

        private async Task LoadClusters()
        {
            Clusters = await _clusterService.GetAllClustersAsync();
        }

        private async Task LoadSensorCluster(int clusterId)
        {
             SelectedCluster.ClusterSensors = await _clusterService.GetSensorsForCluster(clusterId);
        }

        private async Task LoadMeasurements(int clusterId)
        {
            Measurements = await _clusterService.GetMeasurementUnitsForCluster(clusterId);
        }

        // This is a really poor way of doing the update
        private async Task UpdateSensorReadings()
        {

            
            var toReplaceSensors = new ObservableCollection<SensorMeasurementDTO>();

            foreach(SensorMeasurementDTO measurements in SensorMeasurements)
            {

               // Overwrite current readings
               toReplaceSensors.Add(await _sensorService.GetMeasurementsSinceLastReading(measurements, SelectedMeasurementUnit, GetStartDate()));

            }

            SensorMeasurements = toReplaceSensors;

            LiveDataPlot = _plotHelperService.CreateDataPlot(SensorMeasurements, SelectedMeasurementUnit, GetStartDate(), DateTime.Now);

        }

        private DispatcherTimer BeginRefreshTimer()
        {
            return new DispatcherTimer(new TimeSpan(0,0,1), DispatcherPriority.Normal, (sender, args) =>
            {
                if(RefreshTimeSpan == TimeSpan.Zero)
                {
                    RefreshTimeSpan = ResetRefreshTimer();
                    UpdateSensorReadings();
                }
                RefreshTimeSpan = RefreshTimeSpan.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
        }

        private TimeSpan ResetRefreshTimer()
        {
            return TimeSpan.FromMinutes(Double.Parse(RefreshTimer));
        }

        private DateTime GetStartDate()
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
                    return DateTime.Now;
                    break;
            }
        }


        #endregion

    }
}
