using ExperimentalPlantObserver.ViewModels.Tools;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentalPlantObserver.ViewModels.ViewModels
{
    public abstract class ViewModelBase : BindableBase
    {
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private DelegateCommand? _errorToastCommand;
        private DelegateCommand? _successToastCommand;

        public DelegateCommand ErrorToastCommand =>
            _errorToastCommand ??= new DelegateCommand(ShowErrorToastMessage);

        public DelegateCommand SuccessToastCommand =>
            _successToastCommand ??= new DelegateCommand(ShowSuccessToastMessage);

        private static void ShowErrorToastMessage()
        {
            NotificationMessageHandler.AddError("Error", "I am an error toast message");
        }

        private static void ShowSuccessToastMessage()
        {
            NotificationMessageHandler.AddSuccess("Success", "I am a success toast message");
        }

    }
}
