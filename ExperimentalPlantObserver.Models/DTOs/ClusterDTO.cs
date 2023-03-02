using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.DTOs
{
    public class ClusterDTO
    {
        public int ClusterId { get; set; }

        public string ClusterName { get; set; }

        public string ClusterLocation { get; set; }

        public string ClusterCrop { get; set; }

        public string ClusterSoil { get; set; }

        public ObservableCollection<int> ClusterSensors { get; set; }

    }
}
