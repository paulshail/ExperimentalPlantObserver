using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {

        public Task Initialise { get; set; }

        #region Properties

        private ViewModelBase _currentView;

        public ViewModelBase CurrentView
        {
            get => _currentView;
            set
            {

            }
        }

        #endregion

    }
}
