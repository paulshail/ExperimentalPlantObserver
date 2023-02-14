using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects
{
    public class CSVHeader
    {

        public CSVHeader(string headerName)
        {

            HeaderName = headerName;
            IsSelectedX = false;
            IsSelectedY = false;

        }

        public string HeaderName { get; set; }

        public bool IsSelectedX { get; set; }

        public bool IsSelectedY { get; set; }


    }
}
