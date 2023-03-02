using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DTOs
{
    public class SensorMeasurementDTO
    {

        // Reference to the sensor
        public int FK_sensor_Id { get; set; }

        // Hold all data points in measurement DTO object
        public ObservableCollection<MeasurementDTO> Measurements { get; set; }

        // Unit only held in memory once
        public string MeasurementUnit { get; set; }

        // Symbol only held in memory once
        public string MeasurementSymbol { get; set; }

    }
}
