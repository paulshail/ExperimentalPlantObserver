using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Services.Interfaces
{
    public interface ISensorService
    {
        public Task<SensorMeasurementDTO> GetMeasurementsForSensorWithMeasurementIdStartDateEndDate(int sensorId, int measurementId, DateTime startDate, DateTime endDate);

    }
}
