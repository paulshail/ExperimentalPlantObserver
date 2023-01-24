using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("Sensor")]
        public int FK_sensor_Id { get; init; }
        [ForeignKey("MeasurementUnit")]
        public int FK_measurementUnit_Id { get; init; }
    
    }
}
