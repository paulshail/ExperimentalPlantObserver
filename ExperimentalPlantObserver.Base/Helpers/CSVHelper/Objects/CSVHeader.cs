using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects
{
    public class CSVHeader
    {

        public CSVHeader(string csvHeader)
        {

            this.csvHeader = csvHeader;
            isSelectedX = false;
            isSelectedY = false;

        }

        public string csvHeader { get; set; }

        public bool isSelectedX { get; set; }

        public bool isSelectedY { get; set; }


    }
}
