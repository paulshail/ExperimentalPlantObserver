using ExperimentalPlantObserver.Models.AppModels;
using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace ExperimentalPlantObserver.Repository.Implementation
{
    public class ClusterRepository : IClusterRepository<int, ClusterAM>
    {

        private PlantDataContext _plantDatabase;

        public ClusterRepository(PlantDataContext _plantDatabase)
        {

            this._plantDatabase= _plantDatabase;

        }

        public ClusterAM Get(int id)
        {

            var toReturn = (from cluster in _plantDatabase.Clusters
                           join location in _plantDatabase.ClusterLocations on cluster.FK_clusterLocation_Id equals location.PK_clusterLocation_Id
                           join soil in _plantDatabase.ClusterSoil on cluster.FK_clusterSoil_Id equals soil.PK_clusterSoil_Id
                           join crop in _plantDatabase.ClusterCrops on cluster.FK_clusterCrop_Id equals crop.PK_clusterCrop_Id
                           where cluster.PK_cluster_Id == id
                           select new ClusterAM
                           {
                               ClusterId = id,
                               ClusterName = cluster.clusterName,
                               ClusterLocation = location.clusterLocationName,
                               ClusterSoil = soil.clusterSoilType,
                               ClusterCrop = crop.cropName
                           }).FirstOrDefault();

            return toReturn;

        }

        public ObservableCollection<ClusterAM> GetAll()
        {
            var toReturn = from cluster in _plantDatabase.Clusters
                            join location in _plantDatabase.ClusterLocations on cluster.FK_clusterLocation_Id equals location.PK_clusterLocation_Id
                            join soil in _plantDatabase.ClusterSoil on cluster.FK_clusterSoil_Id equals soil.PK_clusterSoil_Id
                            join crop in _plantDatabase.ClusterCrops on cluster.FK_clusterCrop_Id equals crop.PK_clusterCrop_Id
                            select new ClusterAM
                            {
                                ClusterId = cluster.PK_cluster_Id,
                                ClusterName = cluster.clusterName,
                                ClusterLocation = location.clusterLocationName,
                                ClusterSoil = soil.clusterSoilType,
                                ClusterCrop = crop.cropName
                            };

            return new ObservableCollection<ClusterAM>(toReturn);

        }
    }
}
