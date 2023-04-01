using ExperimentalPlantObserver.Base.Helpers.PlotViewHelper;
using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Interfaces.DataPlot;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
        public ViewResolvingPlotModel CreateLinearDataPlot(ObservableCollection<SensorMeasurementDTO> sensorMeasurements, MeasurementUnitDTO measurementUnit, DateTime startDate, DateTime endDate)
        {

            var toReturnPlot = new ViewResolvingPlotModel();

            toReturnPlot.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = DateTimeAxis.ToDouble(startDate), Maximum = DateTimeAxis.ToDouble(endDate), StringFormat="dd/MM/yy HH:mm",
            Angle = 90});

            toReturnPlot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = measurementUnit.MeasurementUnit + " / " + measurementUnit.MeasurementSymbol
            });

            ObservableCollection<LineSeries> sensorMeasurments = new ObservableCollection<LineSeries>();

            foreach(var sensorMeasurement in sensorMeasurements)
            {

                var lineSeriesToAdd = new LineSeries();

                lineSeriesToAdd.LineStyle = LineStyle.Solid; 

                foreach(var sensorReading in sensorMeasurement.Measurements)
                {

                    lineSeriesToAdd.Points.Add(new DataPoint(DateTimeAxis.ToDouble(sensorReading.DateOfMeasurement), sensorReading.MeasurementValue)); ;

                }

                toReturnPlot.Series.Add(lineSeriesToAdd);
            }

            toReturnPlot.InvalidatePlot(true);

            return toReturnPlot;

        }

        public ViewResolvingPlotModel CreateScatterDataPlot(ObservableCollection<SensorMeasurementDTO> sensorMeasurements, MeasurementUnitDTO measurementUnit, DateTime startDate, DateTime endDate)
        {
            var toReturnPlot = new ViewResolvingPlotModel();

            toReturnPlot.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = DateTimeAxis.ToDouble(startDate),
                Maximum = DateTimeAxis.ToDouble(endDate),
                StringFormat = "dd/MM/yy HH:mm",
                Angle = 90
            });

            toReturnPlot.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Title = measurementUnit.MeasurementUnit + " / " + measurementUnit.MeasurementSymbol
            });

            ObservableCollection<ScatterSeries> sensorMeasurments = new ObservableCollection<ScatterSeries>();

            foreach (var sensorMeasurement in sensorMeasurements)
            {

                var scatterSeriesToAdd = new ScatterSeries();

                scatterSeriesToAdd.MarkerType = MarkerType.Cross;

                foreach (var sensorReading in sensorMeasurement.Measurements)
                {

                    scatterSeriesToAdd.Points.Add(new ScatterPoint(DateTimeAxis.ToDouble(sensorReading.DateOfMeasurement), sensorReading.MeasurementValue)); ;

                }

                toReturnPlot.Series.Add(scatterSeriesToAdd);
            }

            toReturnPlot.InvalidatePlot(true);

            return toReturnPlot;
        }
    }
}
