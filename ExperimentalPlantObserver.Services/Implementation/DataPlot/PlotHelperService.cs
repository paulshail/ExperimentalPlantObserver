using ExperimentalPlantObserver.Base.Helpers.PlotViewHelper;
using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Implementation.DataPlot
{
    public class PlotHelperService : IPlotHelperService
    {
        public ViewResolvingPlotModel CreateDataPlot(ObservableCollection<SensorMeasurementDTO> sensorMeasurements)
        {
            
        }
    }
}
