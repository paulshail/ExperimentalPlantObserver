using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Repository.Interfaces;
using ExperimentalPlantObserver.Services.Interfaces;
using System;
using System.Collections.Generic;
using Prism.Ioc;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ExperimentalPlantObserver.Services.Implementation
{
    public class ClusterService : IClusterService
    {

        private readonly IClusterRepository<int, ClusterDTO> _clusterRepository;


        public ClusterService(IClusterRepository<int, ClusterDTO> clusterRepository)
        {

            _clusterRepository = clusterRepository;

        }

        public async Task<ObservableCollection<ClusterDTO>> GetAllClustersAsync()
        {
            return await Task.FromResult(_clusterRepository.GetAll());
        }

        public Task<ClusterDTO> GetCluster(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<MeasurementUnitDTO>> GetMeasurementUnitsForCluster(int clusterId)
        {
            return await Task.FromResult(_clusterRepository.GetMeasurementUnitsForCluster(clusterId));
        }

        public async Task<SensorMeasurementDTO> GetClusterAverageWithMeasurementIds(int clusterId, int measurementId)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<SensorMeasurementDTO>> GetMeasurementsForSensorClusterWithMeasurementIds(int clusterId, int measurementId)
        {
            return await Task.FromResult(_clusterRepository.GetMeasurementsForSensorClusterWithMeasurementId(clusterId, measurementId));
        }

    }
}
