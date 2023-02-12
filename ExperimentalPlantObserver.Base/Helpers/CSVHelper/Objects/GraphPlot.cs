using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects
{
    public class GraphPlot
    {

        public GraphPlot(int missingPoints, string title)
        {
            MissingPoints = missingPoints;
            DataPoints = new LineSeries
            {
                Title = title,
                LineStyle = LineStyle.Solid
            };
        }

        public int MissingPoints { get; set; }

        public LineSeries DataPoints { get; set; }

        public void AddMissingPoint()
        {
            MissingPoints++;
        }

        public LineSeries GetDataPoints()
        {
            return DataPoints;
        }

    }
}
