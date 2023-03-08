using ExperimentalPlantObserver.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Interfaces
{
    public interface IClusterRepository<Tid, Tdomain> : IRepository<Tid, Tdomain>
    {

        public ObservableCollection<MeasurementUnitDTO> GetMeasurementUnitsForCluster(Tid clusterId);

    }
}
