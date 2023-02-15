using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Interface
{
    public interface ICSVReader
    {

        public ObservableCollection<CSVHeader> GetHeaders();

        public ObservableCollection<CSVColumn> GetData(ObservableCollection<CSVHeader> headers);

        public GraphPlot CreateDataPoints(CSVColumn x, CSVColumn y);
    }
}
