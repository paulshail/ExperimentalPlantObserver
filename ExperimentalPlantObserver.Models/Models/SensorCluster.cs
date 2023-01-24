using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class SensorCluster
    {
        [Key]
        public int PK_sensorCluster_Id { get; init; }
        [ForeignKey("Sensor")]
        public int FK_sensor_Id { get; init; }
        [ForeignKey("Cluster")]
        public int FK_cluster_Id { get; init; }

    }
}
