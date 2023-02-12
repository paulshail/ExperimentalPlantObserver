using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Implementation;
using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Objects;
using ExperimentalPlantObserver.ViewModels.Commands;
using ExperimentalPlantObserver.ViewModels.Tools;
using Microsoft.Win32;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class CSVReadViewModel : ViewModelBase
    {

        public Task Initialise { get; set; }

        #region Ctor

        public CSVReadViewModel()
        {

            // Reset the page
            this.ReadCSVButtonText = "Open CSV";
            this.IsLoadingSpinnerVisible = false;
            this.CSVHeadersVisible = false;
            this.PlotModel = new PlotModel();

        }

        #endregion

        #region Properties

        #region LoadingSpinner

        private bool _isLoadingSpinnerVisible;

        public bool IsLoadingSpinnerVisible
        {
            get => _isLoadingSpinnerVisible;
            set
            {
                _isLoadingSpinnerVisible = value;
                OnPropertyChanged(nameof(IsLoadingSpinnerVisible));
            }
        }

        #endregion

        private bool _isReadCSVEnabled;

        public bool IsReadCSVEnabled
        {
            get => _isReadCSVEnabled;
            set
            {
                _isReadCSVEnabled = value;
                OnPropertyChanged(nameof(IsReadCSVEnabled));
            }
        }

        private string _readCSVButtonText;

        public string ReadCSVButtonText
        {
            get => _readCSVButtonText;
            set
            {
                _readCSVButtonText = value;
                OnPropertyChanged(nameof(ReadCSVButtonText));
            }
        }

        private string _fileName;

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        // to hold he list of CSV headings for checkboxes
        private ObservableCollection<string> _csvHeaders;

        public ObservableCollection<string> CSVHeaders
        {
            get => _csvHeaders;
            set
            {
                _csvHeaders = value;
                OnPropertyChanged(nameof(CSVHeaders));
            }

        }

        private ObservableCollection<CSVColumn> _csvData;

        public ObservableCollection<CSVColumn> CSVData
        {
            get => _csvData;
            set
            {
                _csvData = value;
                OnPropertyChanged(nameof(CSVData));
            }
        }

        private bool _csvHeadersVisible;
        public bool CSVHeadersVisible
        {
            get => _csvHeadersVisible;
            set
            {
                _csvHeadersVisible = value;
                OnPropertyChanged(nameof(CSVHeadersVisible));
            }
        }

        #region OxyPlot

        private GraphPlot _graphPlot;

        public GraphPlot GraphPlot
        {
            get => _graphPlot;
            set
            {
                _graphPlot = value;
                OnPropertyChanged(nameof(GraphPlot));
            }
        }
        
        private PlotModel _plotModel;
        public PlotModel PlotModel
        {
            get => _plotModel;
            set
            {
                _plotModel = value;
                OnPropertyChanged(nameof(PlotModel));
            }
        }


        #endregion

        #endregion

        #region Commands


        public RelayCommand OpenFileExplorerCommand =>
            new RelayCommand(delegate
            {
                try
                {
                    OpenFileDialog OpenFile = new OpenFileDialog();
                    string fileName = "";
                    OpenFile.Filter = "Comma Separated Variable(*.csv)|*.csv";
                    OpenFile.ShowDialog();
                    fileName = OpenFile.FileName;

                    FileName = fileName;

                    // TODO fix toast messages
                    if (!String.IsNullOrEmpty(fileName))
                    {

                        CSVReader reader = new CSVReader(fileName);

                        CSVHeaders = reader.GetHeaders();

                        if (CSVHeaders == null)
                        {
                            NotificationMessageHandler.AddError("Error", "The CSV file contained no data");
                        }
                        else
                        {

                            CSVData = reader.GetData(CSVHeaders);

                            Debug.WriteLine("Test");

                            NotificationMessageHandler.AddSuccess("Success", "File was loaded");

                        }


                    }
                    else
                    {
                        NotificationMessageHandler.AddInfo("Info", "No file was selected");
                    }

                }
                catch (Exception ex)
                {
                    NotificationMessageHandler.AddError("Error", "There was an exception opening the file");
                }

            });

        public RelayCommand PlotGraphCommand =>
        new RelayCommand(delegate
        {

            CSVReader reader = new CSVReader(FileName);

            CSVData.Where(x => x.Header.Equals("X")).FirstOrDefault().IsXAxis = true;
            
            GraphPlot =  reader.CreateDataPoints(CSVData);

            // Graph Plot contains object of type oxy series
            PlotModel.Series.Add(GraphPlot.GetDataPoints());
            PlotModel.InvalidatePlot(true);
        });

        public RelayCommand ClearPlotCommand =>
            new RelayCommand(delegate
            {

                PlotModel.Series.Clear();

            });

        #endregion

    }
}
