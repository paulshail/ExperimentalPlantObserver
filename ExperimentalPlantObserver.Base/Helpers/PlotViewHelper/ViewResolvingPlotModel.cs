using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.Base.Helpers.PlotViewHelper
{
    /// <summary>
    /// Use this sub implementation of the <see cref="PlotModel"/> if the view will be declared using data template.
    /// Because views will be automatically generated, and new view will be different this causes current version to throw an error.
    /// </summary>
    public class ViewResolvingPlotModel : PlotModel, IPlotModel
    {
        private static readonly Type BaseType = typeof(ViewResolvingPlotModel).BaseType;
        private static readonly MethodInfo BaseAttachMethod = BaseType
            .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly)
            .Where(methodInfo => methodInfo.IsFinal && methodInfo.IsPrivate)
            .FirstOrDefault(methodInfo => methodInfo.Name.EndsWith(nameof(IPlotModel.AttachPlotView)));

        void IPlotModel.AttachPlotView(IPlotView plotView)
        {
           
            if (plotView != null && PlotView != null && !Equals(plotView, PlotView))
            {
                BaseAttachMethod.Invoke(this, new object[] { null });
                BaseAttachMethod.Invoke(this, new object[] { plotView });
            }
            else
            {
                BaseAttachMethod.Invoke(this, new object[] { plotView });
            }
        }
    }
}
