using ExperimentalPlantObserver.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Repository.Implementation
{
    public abstract class BaseRepository
    {


        protected readonly PlantDataContext _plantDatabase;

        public BaseRepository(PlantDataContext plantDatabase)
        {
            _plantDatabase = plantDatabase;
        }
    }
}
