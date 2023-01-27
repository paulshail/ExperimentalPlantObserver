using ExperimentalPlantObserver.ViewModels.ViewModels.Tabs;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.MainWindow
{
    public class MainWindowViewModel : ViewModelBase
    {
        
        public Task Initialize { get; set; }



        #region Properties

        private ViewModelBase _displayedContent;

        public ViewModelBase DisplayedContent
        {
            get => _displayedContent;
        }
        
        #endregion

    }
}
