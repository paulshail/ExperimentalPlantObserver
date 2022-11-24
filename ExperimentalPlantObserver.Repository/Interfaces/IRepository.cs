using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Interfaces
{
    public interface IRepository<Tid, Tdomain>
    {

        public Tdomain Get(Tid id);

        public ObservableCollection<Tdomain> GetAll();

        public ObservableCollection<Tdomain> GetByTimeFrame(DateTime startTime, DateTime endTime);

        public bool Delete(Tid id);

        public bool DeleteCollection(ObservableCollection<Tdomain> toDelete);

        public bool UpdatebyId(Tid id);

        public bool UpdateObject(Tdomain toUpdate);

        public bool UpdateObjectCollection(ObservableCollection<Tdomain> toUpdateCollection);       

    }
}
