using ExperimentalPlantObserver.Base.Helpers.PlotViewHelper;
using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Interfaces;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using ExperimentalPlantObserver.ViewModels.Commands;
using ExperimentalPlantObserver.ViewModels.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class HistoryViewModel : ViewModelBase
    {
        #region vars

        private readonly IClusterService _clusterService;
        private readonly ISensorService _sensorService;
        private readonly IPlotHelperService _plotHelperService;

        #endregion


        public Task Initialise { get; set; }

        #region Ctor

        public HistoryViewModel(IClusterService clusterService, ISensorService serviceService, IPlotHelperService plotHelperService)
        {

            _clusterService = clusterService;
            _sensorService = serviceService;
            _plotHelperService = plotHelperService;

            IsPlotting = false;

            Initialise = LoadClusters();

            // Default to a week
            StartDate = DateTime.Now.AddDays(-7);
            EndDate = DateTime.Now;

            // Date will always be valid since its set in the constructor
            IsDateValid = true;
            IsSetup = true;
            
        }

        #endregion

        #region Properties

        // Checks that the page is setup
        private bool _isSetup;
        public bool IsSetup
        {
            get => _isSetup;
            set
            {
                _isSetup = value;
                OnPropertyChanged(nameof(IsSetup));
            }
        }

        private bool _isDateValid;
        public bool IsDateValid
        {
            get => _isDateValid;
            set
            {
                _isDateValid = value;
                OnPropertyChanged(nameof(IsDateValid));
                if (IsSetup)
                {
                    if(IsDateValid)
                    {
                        IsPlotTypeSelectionVisible = true;
                    }
                    else
                    {
                        IsPlotTypeSelectionVisible = false;
                    }
                }
            }
        }


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
                    if (IsDateValid)
                    {
                        IsPlotTypeSelectionVisible = true;
                    }
                }
            }
        }


        // Start date for history search
        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
                if (IsSetup)
                {
                    if (StartDate > EndDate && IsSetup)
                    {
                        NotificationMessageHandler.AddError("Error", "Start date must be before end date");
                        IsDateValid = false;
                    }
                    else
                    {
                        NotificationMessageHandler.AddSuccess("Start date set", "Start date set to: " + StartDate);
                        IsDateValid = true;

                    }
                }
            }
        }

        // End date for history search
        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
                if (IsSetup)
                {
                    if (StartDate > EndDate)
                    {
                        NotificationMessageHandler.AddError("Error", "Start date must be before end date");
                        IsDateValid = false;
                    }
                    else
                    {
                        NotificationMessageHandler.AddSuccess("End date set", "End date set to: " + EndDate);
                        IsDateValid = true;
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

        private bool _isPlotTypeSelectionVisible;

        public bool IsPlotTypeSelectionVisible
        {
            get => _isPlotTypeSelectionVisible;
            set
            {
                _isPlotTypeSelectionVisible = value;
                OnPropertyChanged(nameof(IsPlotTypeSelectionVisible));
                if(IsPlotAverage != null)
                {
                    IsPlotVisible = true;
                }
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


        private ViewResolvingPlotModel _historyDataPlot;
        public ViewResolvingPlotModel HistoryDataPlot
        {
            get => _historyDataPlot;
            set
            {
                _historyDataPlot = value;
                OnPropertyChanged(nameof(HistoryDataPlot));
               
            }
        }

        #endregion

        #endregion

        #region Commands

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

        public RelayCommand PlotHistoryDataCommand =>
            new RelayCommand(async delegate
            {
                if (!IsPlotting)
                {
                    if (IsPlotAverage == true)
                    {
                        NotificationMessageHandler.AddInfo("Not Implemented", "Cluster average has not been implemented");
                    }
                    else
                    {
                        SensorMeasurements = await _sensorService.GetMeasurementsForAllSensorsWithMeasurementIdStartDateEndDate(SelectedCluster.ClusterSensors, SelectedMeasurementUnit.PK_measurementUnit_Id, StartDate, EndDate);

                        bool sensorHasMeasurements = false;

                        foreach(SensorMeasurementDTO sensor in SensorMeasurements)
                        {
                            if(sensor.Measurements.Count() > 0)
                            {
                                sensorHasMeasurements = true;
                            }
                        }
                        
                        if (sensorHasMeasurements)
                        {

                            if (IsPlotAverage == false)
                            {

                                HistoryDataPlot = _plotHelperService.CreateDataPlot(SensorMeasurements, SelectedMeasurementUnit, StartDate, EndDate);

                                IsPlotVisible = true;
                            
                            }
                            else if (IsPlotAverage == true)
                            {

                                // NOT IMPLEMENTED

                            }
                        }
                        else
                        {
                            NotificationMessageHandler.AddError("No data", "No sensor measurements available in the chosen time frame");
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

        #endregion

    }
}
