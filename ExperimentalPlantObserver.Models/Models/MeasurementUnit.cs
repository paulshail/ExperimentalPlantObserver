using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class MeasurementUnit : ModelBase
    {
        [Key]
        public int PK_measurementUnit_Id { get; init; }

        public string measurementUnit { get; init; }

        public string measurementSymbol { get; init; }
    }
}
