using ExperimentalPlantObserver.Base.Helpers.PlotViewHelper;
using ExperimentalPlantObserver.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Interfaces.DataPlot
{
    public interface IPlotHelperService
    {

        public ViewResolvingPlotModel CreateLinearDataPlot(ObservableCollection<SensorMeasurementDTO> sensorMeasurements, MeasurementUnitDTO measurementUnit, DateTime startDate, DateTime endDate);

        public ViewResolvingPlotModel CreateScatterDataPlot(ObservableCollection<SensorMeasurementDTO> sensorMeasurements, MeasurementUnitDTO measurementUnit, DateTime startDate, DateTime endDate);

    }
}
