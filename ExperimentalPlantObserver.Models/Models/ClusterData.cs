using Prism.Modularity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class ClusterData : ModelBase
    {
        [Key]
        public int PK_clusterData_Id { get; init; }
        public double measurementValue { get; init; }
        public DateTime dateOfMeasurement { get; init; }
        [ForeignKey("Cluster")]
        public int FK_cluster_Id { get; init; }
        [ForeignKey("ClusterDataType")]
        public int FK_clusterDataType_Id { get; init; }
    }
}
