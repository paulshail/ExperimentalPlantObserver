using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Interface;
using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.CSVHelper.Implementation
{
    public class CSVReader : ICSVReader
    {

        #region vars

        private string filePath;

        #endregion

        #region ctor

        public CSVReader(string filePath)
        {
            this.filePath = filePath;
        }

     
        #endregion

        #region methods
        public ObservableCollection<CSVHeader> GetHeaders()
        {
            try
            {
                using(var reader = new StreamReader(filePath))
                {


                    string headerString = reader.ReadLine();

                    if (!String.IsNullOrEmpty(headerString))
                    {

                        ObservableCollection<CSVHeader> headers = new ObservableCollection<CSVHeader>();

                        foreach(string header in headerString.Split(","))
                        {
                            headers.Add(new CSVHeader(header));
                        };

                        return headers;
                    
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        public ObservableCollection<CSVColumn> GetData(ObservableCollection<CSVHeader> headers)
        {

            try
            {

                ObservableCollection<CSVColumn> toReturn = new ObservableCollection<CSVColumn>();

                foreach (CSVHeader header in headers)
                {
                    CSVColumn newColumn = new CSVColumn();

                    newColumn.Header = header.HeaderName;
                    newColumn.RecordedValues = new ObservableCollection<string>();

                    toReturn.Add(newColumn);
                }

                using (var reader = new StreamReader(filePath))
                {
                    // ignore headers
                    reader.ReadLine();

                    while (!reader.EndOfStream)
                    {
                        var values = reader.ReadLine().Split(",");

                        for (int i = 0; i < values.Length; i++)
                        {
                            toReturn[i].RecordedValues.Add(values[i]);
                        }
                    }
                }

                return toReturn;
            }
            catch
            {
                return null;
            }
         }

        public GraphPlot CreateDataPoints(CSVColumn x, CSVColumn y)
        {
            
                // Zero missing points passed to the contructor;
                GraphPlot toReturn = new GraphPlot(0, filePath);

                if(x == null || y == null)
                {
                    return null;
                }

                // Create datapoints object
                IList<DataPoint> dataPoints = new ObservableCollection<DataPoint>();

                for(int i = 0; i < x.RecordedValues.Count; i++)
                {
                    try
                    {
                        DataPoint point = new(Convert.ToDouble(x.RecordedValues[i]), Convert.ToDouble(y.RecordedValues[i]));
                        toReturn.DataPoints.Points.Add(point);
                    }
                    catch
                    {
                        // If it cannot be converted add missing point
                        toReturn.AddMissingPoint();
                    }
                }

                // Attach the datapoints from the CSV
                return toReturn;

            }
        #endregion
    }
}
