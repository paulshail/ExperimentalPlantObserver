﻿using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Repository.Implementation;
using ExperimentalPlantObserver.Repository.Interfaces;
using ExperimentalPlantObserver.Services.Interfaces;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Implementation
{
    public class SensorService : ISensorService
    {

        private readonly ISensorRepository<int, SensorDTO> _sensorRepository;

        public SensorService(ISensorRepository<int, SensorDTO> _sensorRepository)
        {
            this._sensorRepository = _sensorRepository;
        }

        public async Task<SensorMeasurementDTO> GetMeasurementsForSensorWithMeasurementIdStartDateEndDate(int sensorId, int measurementId, DateTime startDate, DateTime endDate)
        {

            SensorMeasurementDTO toReturn = new SensorMeasurementDTO
            {
                FK_sensor_Id = sensorId
            };

            toReturn.Measurements = await Task.FromResult(_sensorRepository.GetMeasurementsForSensor(sensorId, measurementId, startDate, endDate));

            return toReturn;        
        }


        public async Task<ObservableCollection<SensorMeasurementDTO>> GetMeasurementsForAllSensorsWithMeasurementIdStartDateEndDate(ObservableCollection<int> sensorsInCluster, int measurementId, DateTime startDate, DateTime endDate)
        {

            var toReturn = new ObservableCollection<SensorMeasurementDTO>();

            foreach(int sensorId in sensorsInCluster)
            {


                toReturn.Add(await GetMeasurementsForSensorWithMeasurementIdStartDateEndDate(sensorId, measurementId, startDate, endDate));

            }

            return toReturn;

        }
    }
}
