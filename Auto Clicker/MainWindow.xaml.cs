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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AutoClicker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _dispatcherTimer;

        public MainWindow()
        {
            InitializeTimer();
            InitializeComponent();
        }

        private void rbnOn_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnOn.IsChecked == true)
            {
                TurnOnAutoclicker();
            }
        }

        private void TurnOnAutoclicker()
        {
            _dispatcherTimer.Start();
        }

        private void InitializeTimer()
        {
            if (_dispatcherTimer != null)
            {
                _dispatcherTimer.Stop();
            }
            else
            {
                _dispatcherTimer = new DispatcherTimer();
                _dispatcherTimer.Tick += TimerTick;
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            ClickTheMouse();
        }

        private static void ClickTheMouse()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        private void rbnOff_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnOff.IsChecked == true)
            {
                TurnOffAutoclicker();
            }
        }

        private void TurnOffAutoclicker()
        {
            _dispatcherTimer.Stop();
        }

        private void txtInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intervalValue_ms;

            if (int.TryParse(txtInterval.Text, out intervalValue_ms))
            {
                SetTimerInterval(intervalValue_ms);
            }
        }

        private void SetTimerInterval(int intervalValue_ms)
        {
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(intervalValue_ms);
        }


    }
}
