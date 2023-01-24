using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class SensorMeasurement
    {
        [Key]
        public int PK_sensorMeasurement_Id { get; init; }

        public double measurementValue { get; init; }

        public DateTime dateOfMeasurement { get; init; }

        public int FK_sensor_Id { get; init; }

        public int FK_measurementUnit_Id { get; init; }
    
    }
}
