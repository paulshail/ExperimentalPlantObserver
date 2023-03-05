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

        public LiveViewModel(IClusterService clusterService)
        {

            _clusterService = clusterService;

            Initialise = LoadClusters();

        }

        #endregion

        #region Properties

        private ObservableCollection<ClusterDTO> _clusters;

        public ObservableCollection<ClusterDTO> Clusters
        {
            get => _clusters;
            set
            {
                _clusters = value;
                OnPropertyChanged(nameof(Clusters));
            }
        }

        private ClusterDTO _selectedCluster;

        public ClusterDTO SelectedCluster
        {
            get => _selectedCluster;
            set
            {
                _selectedCluster = value;
                OnPropertyChanged(nameof(SelectedCluster));
            }
        }

        private bool _isMeasurementsVisible;

        public bool IsMeasurementsVisible
        {
            get => _isMeasurementsVisible;
            set
            {
                _isMeasurementsVisible = value;
                OnPropertyChanged(nameof(IsMeasurementsVisible));
            }
        }

        private bool _isPlotSelectionVisible;

        public bool IsPlotSelectionVisible
        {
            get => _isPlotSelectionVisible;
            set
            {
                _isPlotSelectionVisible = value;
                OnPropertyChanged(nameof(IsPlotSelectionVisible));
            }
        }

        private bool _isTimeSelectionVisible;

        public bool IsTimeSelectionVisible
        {
            get => _isTimeSelectionVisible;
            set
            {
                _isTimeSelectionVisible = value;
                OnPropertyChanged(nameof(IsTimeSelectionVisible));
            }
        }

        private bool _isPlotVisible;

        public bool IsPlotVisible
        {
            get => _isPlotVisible;
            set
            {
                _isPlotVisible = value;
                OnPropertyChanged(nameof(IsPlotVisible));
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
