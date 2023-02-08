using ExperimentalPlantObserver.Base.Helpers.CSVHelper.Implementation;
using ExperimentalPlantObserver.ViewModels.Commands;
using ExperimentalPlantObserver.ViewModels.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.Tabs
{
    public class  CSVReadViewModel : ViewModelBase
    {

        public Task Initialise { get; set; }

        #region Ctor

        public CSVReadViewModel()
        {

            this.ReadCSVButtonText = "Open CSV";

        }

        #endregion

        #region Properties

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


                    // TODO fix toast messages
                    if (!String.IsNullOrEmpty(fileName))
                    {

                        CSVReader reader = new CSVReader(fileName);    

                        var csvHeaders = reader.GetHeaders();

                        if(csvHeaders == null)
                        {
                            NotificationMessageHandler.AddError("Error", "The CSV file contained no data");
                        }
                        else
                        {
                            NotificationMessageHandler.AddSuccess("Success", "File was loaded");
                        }

                        
                    }
                    else
                    {
                        NotificationMessageHandler.AddInfo("Info", "No file was selected");
                    }

                }
                catch(Exception ex)
                {
                    NotificationMessageHandler.AddError("Error", "There was an exception opening the file");
                }

            });

        #endregion

    }
}
