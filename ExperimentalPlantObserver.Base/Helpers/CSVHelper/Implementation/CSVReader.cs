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
        public ObservableCollection<string> GetHeaders()
        {
            try
            {
                using(var reader = new StreamReader(filePath))
                {


                    string headerString = reader.ReadLine();

                    if (!String.IsNullOrEmpty(headerString))
                    {

                        ObservableCollection<string> headers = new ObservableCollection<string>(headerString.Split(","));

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

        public ObservableCollection<CSVColumn> GetData(ObservableCollection<string> headers)
        {

            try
            {

                ObservableCollection<CSVColumn> toReturn = new ObservableCollection<CSVColumn>();

                foreach (string header in headers)
                {
                    CSVColumn newColumn = new CSVColumn();

                    newColumn.Header = header;
                    newColumn.RecordedValues = new ObservableCollection<string>();
                    newColumn.IsXAxis = false;

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

        public GraphPlot CreateDataPoints(ObservableCollection<CSVColumn> selectedColumns)
        {
            // Only two columns should be selected
            if(!(selectedColumns.Count == 2))
            {
                return null;
            }
            else
            {
                // Zero missing points passed to the contructor;
                GraphPlot toReturn = new GraphPlot(0, filePath);

                CSVColumn x = selectedColumns.Where(x => x.IsXAxis == true).FirstOrDefault();
                CSVColumn y = selectedColumns.Where(y => y.IsXAxis == false).FirstOrDefault();

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
        }

        #endregion
    }
}
