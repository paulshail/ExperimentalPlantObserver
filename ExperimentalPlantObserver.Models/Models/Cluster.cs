using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    
    public class Cluster : ModelBase
    {
        public int PK_cluster_Id { get; init; }

        public string clusterName { get; init; }
        [ForeignKey("ClusterLocation")]
        public int FK_clusterLocation_Id { get; init; }
        [ForeignKey("ClusterCrop")]
        public int FK_clusterCrop_Id { get; init; }
        [ForeignKey("ClusterSoil")]
        public int FK_clusterSoil_Id { get; init; }

    }
}
