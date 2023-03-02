using ExperimentalPlantObserver.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Interfaces
{
    public interface ISensorRepository<Tid, Tdomain> : IRepository<Tid, Tdomain>
    {

        public SensorMeasurementDTO GetSensoReadingsForDate(int sensorId, DateTime startDate, DateTime endDate);

        
    }
}
