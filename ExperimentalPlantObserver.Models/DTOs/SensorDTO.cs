using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DTOs
{
    public class SensorDTO
    {

        public int SensorId { get; set; }

        public string SensorType { get; set; }

        public string SensorName { get; set; }

        // Unique object for each type of measurement
        public ObservableCollection<SensorMeasurementDTO> SensorMeasurements{ get; set;}

        public ObservableCollection<int> MeasurementTypes { get; set; }

    }
}
