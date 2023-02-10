using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Interface;
using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects;
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


        #endregion
    }
}
