using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects
{
    public class CSVColumn
    {
        public CSVColumn(string header, ObservableCollection<double> recordedValues)
        {
            Header = header;
            RecordedValues = recordedValues;
        }

        public string Header { get; set; }

        public ObservableCollection<double> RecordedValues { get; set; }

    }
}
