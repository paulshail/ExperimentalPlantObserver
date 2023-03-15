using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.Models.DTOs;
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
    public class ClusterRepository : BaseRepository, IClusterRepository<int, ClusterDTO>
    {


        public ClusterRepository(PlantDataContext _plantDatabase) : base(_plantDatabase) { }

        public ClusterDTO Get(int id)
        {

            var toReturn = (from cluster in _plantDatabase.Cluster
                           join location in _plantDatabase.ClusterLocation on cluster.FK_clusterLocation_Id equals location.PK_clusterLocation_Id
                           join soil in _plantDatabase.ClusterSoil on cluster.FK_clusterSoil_Id equals soil.PK_clusterSoil_Id
                           join crop in _plantDatabase.ClusterCrop on cluster.FK_clusterCrop_Id equals crop.PK_clusterCrop_Id
                           where cluster.PK_cluster_Id == id
                           select new ClusterDTO
                           {
                               ClusterId = id,
                               ClusterName = cluster.clusterName,
                               ClusterLocation = location.clusterLocation,
                               ClusterSoil = soil.clusterSoilType,
                               ClusterCrop = crop.cropName,
                               ClusterSensors = GetSensors(id)
                           }).FirstOrDefault();

            return toReturn;

        }

        public ObservableCollection<ClusterDTO> GetAll()
        {
            var toReturn = from cluster in _plantDatabase.Cluster
                            join location in _plantDatabase.ClusterLocation on cluster.FK_clusterLocation_Id equals location.PK_clusterLocation_Id
                            join soil in _plantDatabase.ClusterSoil on cluster.FK_clusterSoil_Id equals soil.PK_clusterSoil_Id
                            join crop in _plantDatabase.ClusterCrop on cluster.FK_clusterCrop_Id equals crop.PK_clusterCrop_Id
                            select new ClusterDTO
                            {
                                ClusterId = cluster.PK_cluster_Id,
                                ClusterName = cluster.clusterName,
                                ClusterLocation = location.clusterLocation,
                                ClusterSoil = soil.clusterSoilType,
                                ClusterCrop = crop.cropName
                            };

            return new ObservableCollection<ClusterDTO>(toReturn);

        }


        public ObservableCollection<int> GetSensors(int clusterId) 
        {


            var toReturn = from sensorClusters in _plantDatabase.SensorCluster
                           where sensorClusters.FK_cluster_Id == clusterId
                           select sensorClusters.FK_sensor_Id;

            return new ObservableCollection<int>(toReturn);                        

        }

        public ObservableCollection<MeasurementUnitDTO> GetMeasurementUnitsForCluster(int clusterId)
        {

            var measurementUnits = (from measurements in _plantDatabase.MeasurementUnit
                                    join sensorMeasurements in _plantDatabase.SensorMeasurement on measurements.PK_measurementUnit_Id equals sensorMeasurements.FK_measurementUnit_Id
                                    join sensor in _plantDatabase.Sensor on sensorMeasurements.FK_sensor_Id equals sensor.PK_sensor_Id
                                    join sensorCluster in _plantDatabase.SensorCluster on sensor.PK_sensor_Id equals sensorCluster.FK_sensor_Id
                                    join cluster in _plantDatabase.Cluster on sensorCluster.FK_cluster_Id equals cluster.PK_cluster_Id
                                    where cluster.PK_cluster_Id == clusterId
                                    select new MeasurementUnitDTO
                                    {
                                        MeasurementSymbol = measurements.measurementSymbol,
                                        MeasurementUnit = measurements.measurementUnit,
                                        PK_measurementUnit_Id = measurements.PK_measurementUnit_Id
                                    }).Distinct();

            return new ObservableCollection<MeasurementUnitDTO>(measurementUnits);

        }


        public SensorMeasurementDTO GetClusterAverageWithMeasurementId(int clusterId, int measurementId)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<int> GetSensorIdsInCluster(int clusterId)
        {
            var toReturn = from sensorCluster in _plantDatabase.SensorCluster
                           where sensorCluster.FK_cluster_Id == clusterId
                           select sensorCluster.FK_sensor_Id;

            return new ObservableCollection<int>(toReturn);
        }
    }
}
