using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.AppModels
{
    public class ClusterAM
    {
        public int ClusterId { get; set; }

        public string ClusterName { get; set; }

        public string ClusterLocation { get; set; }

        public string ClusterCrop { get; set; }

        public string ClusterSoil { get; set; }
    }
}
