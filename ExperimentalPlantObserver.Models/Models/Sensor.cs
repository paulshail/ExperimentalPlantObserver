using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class Sensor
    {

        [Key]
        public int PK_sensor_Id { get; set; }

        public int FK_sensorType_Id { get; set; }
    }
}
