using ExperimentalPlantObserver.Base.Helpers.Calculations;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ExperimentalPlantObserver.UnitTests.StatisticsCalculatorTests
{
    public class StatisticsCalculatorTests
    {

        [Fact]
        public void TestingAverageCalculator_WithHumanCalculatedAverage()
        {

            //Arrange
            double[] testValues = new double[] 
            {
                
                6.5, 7.5, 10.5, 20, 14.5

            };

            double expectedAverage = testValues.Average();

            ObservableCollection<double> values = new ObservableCollection<double>(testValues);

            var testValue = StatisticsCalculator.CalculateAverage(values);


            Assert.Equal(expectedAverage, testValue);
        }


    }
}
