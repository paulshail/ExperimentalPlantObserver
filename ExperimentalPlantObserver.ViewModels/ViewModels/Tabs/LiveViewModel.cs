using ExperimentalPlantObserver.Models.DTOs;
using ExperimentalPlantObserver.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using Prism.Ioc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExperimentalPlantObserver.Services.Interfaces;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class LiveViewModel : ViewModelBase
    {

        #region Services

        private readonly IClusterService _clusterService;

        #endregion



        public Task Initialise { get; set; }

        #region Ctor

        public LiveViewModel()
        {

            _clusterService = ContainerLocator.Current.Resolve<IClusterService>();

            Initialise = LoadClusters();

        }

        #endregion

        #region Properties

        private ObservableCollection<ClusterDTO> _clusters;

        public  ObservableCollection<ClusterDTO> Clusters
        {
            get => _clusters;
            set
            {
                _clusters = value;
                OnPropertyChanged(nameof(Clusters));
            }
        }

        #endregion

        #region Methods

        private async Task LoadClusters()
        {

            _clusters = await _clusterService.GetAllClustersAsync();

        }

        #endregion

    }
}
