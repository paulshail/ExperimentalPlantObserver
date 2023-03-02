using ExperimentalPlantObserver.Models.AppModels;
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

        public Task<ObservableCollection<ClusterAM>> GetAllClusters();

    }
}
