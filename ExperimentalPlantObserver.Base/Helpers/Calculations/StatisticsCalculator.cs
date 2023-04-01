using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.Calculations
{
    public static class StatisticsCalculator
    {

        public static double CalculateAverage(ObservableCollection<double> values)
        {

            if (values.Count > 0)
            {

                double sumOfTotal = 0;

                foreach (var value in values)
                {
                    sumOfTotal += value;
                }

                return Math.Round(sumOfTotal / values.Count, 2);

            }
            else
            {
                return 0;
            }
        }
    }
}
