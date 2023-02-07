using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
