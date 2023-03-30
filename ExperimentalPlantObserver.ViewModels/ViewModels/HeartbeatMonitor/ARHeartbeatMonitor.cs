using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels.HeartbeatMonitor
{
    public class ARHeartbeatMonitor : ViewModelBase
    {

        #region vars

        private BackgroundWorker _heartbeatCheckWorker;

        private string _fileLocation;

        #endregion

        #region Properties

        private string _heartbeatStatus;
        public string HeartbeatStatus
        {
            get => _heartbeatStatus;
            set 
            {
                _heartbeatStatus = value;
                OnPropertyChanged(nameof(HeartbeatStatus));
            }

        }

        private string _heartbeatValue;

        public string HeartbeatValue
        {
            get => _heartbeatValue;
            set
            {
                _heartbeatValue = value;
                OnPropertyChanged(nameof(HeartbeatValue));
            }
        }

        #endregion

        public Task Initialise { get; set; }

        public ARHeartbeatMonitor(string fileLocation)
        {

            _fileLocation = fileLocation;

            _heartbeatCheckWorker= new BackgroundWorker();
            _heartbeatCheckWorker.DoWork += _heartbeatCheckWorker_DoWork;
            _heartbeatCheckWorker.WorkerSupportsCancellation = true;
            _heartbeatCheckWorker.RunWorkerCompleted += _heartbeatCheckWorker_RunWorkerCompleted;

            StartHeartbeatCheck();
        }

        private void _heartbeatCheckWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            StartHeartbeatCheck();
        }

        private void _heartbeatCheckWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                var heartbeatLastWritten = File.GetLastWriteTime(_fileLocation);

                if (heartbeatLastWritten == null)
                {
                    if (heartbeatLastWritten >= DateTime.Now.AddMinutes(-5))
                    {
                        HeartbeatValue = "OFFLINE";
                        HeartbeatStatus = "OFFLINE";
                    }
                    else if (heartbeatLastWritten >= DateTime.Now.AddMinutes(-2))
                    {
                        HeartbeatValue = "ALERT";
                        HeartbeatStatus = "ALERT";
                    }
                    else if (heartbeatLastWritten < DateTime.Now.AddMinutes(-2))
                    {
                        HeartbeatValue = "RUNNING";
                        HeartbeatStatus = "RUNNING";
                    }
                }
                else
                {
                    HeartbeatValue = "ERROR";
                    HeartbeatStatus = "CANNOT FIND HEARTBEAT FILE";
                }
            }
            catch
            {

                HeartbeatValue = "ERROR";
                HeartbeatStatus = "ERROR OCCURRED WHILE CHECKING HEARTBEAT";

            }

            Thread.Sleep(60000);

        }

        private void StartHeartbeatCheck()
        {
            _heartbeatCheckWorker.RunWorkerAsync();
        }

    }
}
