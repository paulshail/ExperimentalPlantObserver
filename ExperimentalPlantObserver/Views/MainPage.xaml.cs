using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExperimentalPlantObserver.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {

        private bool _stateClosed;

        public MainPage()
        {
            InitializeComponent();
            // init to display nav bar
            NavigationSelectionArea.Visibility = Visibility.Visible;
            GridMenu.Width = 200;
        }

        private void ButtonMenu_OnClick(object sender, RoutedEventArgs e)
        {
            if (_stateClosed)
            {
                ApplyStoryBoard("OpenMenu");


                NavigationSelectionArea.Visibility = Visibility.Visible;
            }
            else
            {

                ApplyStoryBoard("CloseMenu");

                NavigationSelectionArea.Visibility = Visibility.Collapsed;
            }

            _stateClosed = !_stateClosed;
        }

        private void ApplyStoryBoard(string storyboardName)
        {
            Storyboard sb = FindResource(storyboardName) as Storyboard;
            sb?.Begin();
        }
    }
}

