using ExperimentalPlantObserver.Models.DTOs;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Interfaces
{
    public interface ISensorRepository<Tid, Tdomain> : IRepository<Tid, Tdomain>
    {
        public SensorMeasurementDTO GetSensorReadingsForDate(int sensorId, DateTime startDate, DateTime endDate);

      // public SensorMeasurementDTO GetMeasurementsForSensorWithMeasurementIdStartDateEndDate(Tid sensorId, Tid measurementId, DateTime startDate, DateTime endDate);
    
        public IList<MeasurementDTO> GetMeasurementsForSensor(Tid sensorId, Tid measurementId, DateTime startDate, DateTime endDate);
    }
}
