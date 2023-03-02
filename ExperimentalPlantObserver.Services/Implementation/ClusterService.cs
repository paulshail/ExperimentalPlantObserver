using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Implementation
{
    public class ClusterService : IClusterService
    {

        public ClusterService()
        {

        }

        public Task<ObservableCollection<ClusterDTO>> GetAllClusters()
        {
            throw new NotImplementedException();
        }

        public Task<ClusterDTO> GetCluster(int id)
        {
            throw new NotImplementedException();
        }
    }
}
