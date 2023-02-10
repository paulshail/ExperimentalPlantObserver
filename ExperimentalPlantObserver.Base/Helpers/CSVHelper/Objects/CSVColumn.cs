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
        public CSVColumn(string header, ObservableCollection<string> recordedValues)
        {
            Header = header;
            RecordedValues = recordedValues;
        }
        public CSVColumn()
        {

        }

        public string Header { get; set; }

        public ObservableCollection<string> RecordedValues { get; set; }

    }
}
