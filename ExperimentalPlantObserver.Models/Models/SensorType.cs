using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class SensorType : ModelBase
    {
        [Key]
        public int PK_sensorType_Id { get; init; }

        public string sensorTypeName { get; init; }
    }
}
