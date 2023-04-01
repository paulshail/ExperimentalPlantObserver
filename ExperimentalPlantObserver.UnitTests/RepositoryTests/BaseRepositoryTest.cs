using ExperimentalPlantObserver.Models.DataContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.UnitTests.RepositoryTests
{
    public class BaseRepositoryTest
    {

        protected PlantDataContext DbContext;

        protected PlantDataContext GetPlantDataContext()
        {
            //var options = new DbContextOptionsBuilder<PlantDataContext>()
            //    .UseInMemoryDatabase(Guid.NewGuid().ToString())
            //    .Options;

            //    return new PlantDataContext(options);

            return null;
        }

    }
}
