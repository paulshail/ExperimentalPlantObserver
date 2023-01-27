using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace ExperimentalPlantObserver.Base.Behaviours.WindowBehaviours
{
    /// <summary>
    /// MinimizeOnClickBehaviour is an attached proeprty that can be attached to buttons
    /// to minimize the buttons host window.
    /// </summary>
    public static class MinimizeOnClickBehaviour
    {
        /// <summary>
        /// Attach property to minimuze host window
        /// </summary>
        public static readonly DependencyProperty MinimizeWindowProperty =
            DependencyProperty.RegisterAttached
            (
                "IsWindowMinimized",
                typeof(bool),
                typeof(MinimizeOnClickBehaviour),
                new PropertyMetadata(false, MinimizeWindowPropertyChanged)
            );

        public static bool GetMinimizeWindowProperty(DependencyObject obj)
        {
            return (bool)obj.GetValue(MinimizeWindowProperty);
        }


        public static void SetMinimizeWindowProperty(DependencyObject obj, bool value)
        {
            obj.SetValue(MinimizeWindowProperty, value);
        }


        public static void MinimizeWindowPropertyChanged(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            if (property is Button button)
                button.Click += OnClick;
        }

        private static void OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                Window window = Window.GetWindow(btn);
                if (window != null)
                    window.WindowState = WindowState.Minimized;
            }
        }
    }
}
