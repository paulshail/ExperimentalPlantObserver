using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Models.Models
{
    public class ClusterDataType : ModelBase
    {
        [Key]
        public int PK_clusterDataType_Id { get; init; }
        public string dataTypeName { get; init; }
        public string dataTypeSymbol { get; init; }

    }
}
