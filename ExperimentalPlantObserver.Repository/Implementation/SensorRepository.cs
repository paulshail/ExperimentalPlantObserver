using ExperimentalPlantObserver.Models.DataContext;
using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Implementation
{
    public class SensorRepository : BaseRepository, ISensorRepository<int, SensorDTO>
    {

        

        public SensorRepository(PlantDataContext _plantDatabase) : base(_plantDatabase){}

        public SensorDTO Get(int id)
        {
            var sensor = (from sensors in _plantDatabase.Sensors
                          join sensorTypes in _plantDatabase.SensorTypes on sensors.FK_sensorType_Id equals sensorTypes.PK_sensorType_Id
                          where sensors.PK_sensor_Id == id
                          select new SensorDTO
                          {
                              SensorId = id,
                              SensorName = sensors.sensorName,
                              SensorType = sensorTypes.sensorTypeName
                          }).FirstOrDefault();

            return sensor;
        }

        public ObservableCollection<SensorDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public SensorDTO GetWithMeasurements(int id)
        {

            var sensor = (from sensors in _plantDatabase.Sensors
                          join sensorTypes in _plantDatabase.SensorTypes on sensors.FK_sensorType_Id equals sensorTypes.PK_sensorType_Id
                          where sensors.PK_sensor_Id == id
                          select new SensorDTO
                          {
                              SensorId = id,
                              SensorName = sensors.sensorName,
                              SensorType = sensorTypes.sensorTypeName,
                              MeasurementTypes = GetMeasurementTypes(id)                              
                          }).FirstOrDefault();

            return sensor;

        }


        public SensorMeasurementDTO GetSensorReadingsForDate(int sensorId, DateTime startDate, DateTime endDate)
        {

            throw new NotImplementedException();

        }

        public ObservableCollection<int> GetMeasurementTypes(int sensorId)
        {

            var measurementTypes = _plantDatabase.SensorMeasurements.Where(s => s.FK_sensor_Id == sensorId).DistinctBy(x => x.FK_measurementUnit_Id).Select(m => m.FK_measurementUnit_Id);

            return new ObservableCollection<int>(measurementTypes);

        }

        public ObservableCollection<MeasurementDTO> GetMeasurements(int sensorId, int measurementId)
        {

            var measurementValues = from measurements in _plantDatabase.SensorMeasurements
                                    where measurements.FK_sensor_Id == sensorId
                                    where measurements.FK_measurementUnit_Id == measurementId
                                    orderby measurements.dateOfMeasurement
                                    select new MeasurementDTO
                                    {
                                        DateOfMeasurement = measurements.dateOfMeasurement,
                                        MeasurementValue = measurements.measurementValue
                                    };

            return new ObservableCollection<MeasurementDTO>(measurementValues);

        }
    }
}
