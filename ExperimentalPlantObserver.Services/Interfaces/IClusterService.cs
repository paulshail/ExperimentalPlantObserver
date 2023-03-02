using ExperimentalPlantObserver.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Interfaces
{
    public interface IClusterService
    {

        public Task<ObservableCollection<ClusterDTO>> GetAllClusters();

        public Task<ClusterDTO> GetCluster(int id);

    }
}
