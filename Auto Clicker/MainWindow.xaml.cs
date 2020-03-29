using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
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
        GlobalKeyboardHook _keyboardHooks;

        public MainWindow()
        {
            InitializeKeyLogger();
            InitializeTimer();
            InitializeComponent();
        }

        private void InitializeKeyLogger()
        {
            _keyboardHooks = new GlobalKeyboardHook(null);
            _keyboardHooks.KeyboardPressed += OnKeyPressed;
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

         private void rbnOff_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnOff.IsChecked == true)
            {
                TurnOffAutoclicker();
            }
        }

        private void rbnOn_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnOn.IsChecked == true)
            {
                TurnOnAutoclicker();
            }
        }

        private void txtInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intervalValue_ms;

            if (int.TryParse(txtInterval.Text, out intervalValue_ms))
            {
                SetTimerInterval(intervalValue_ms);
            }
        }

        private static void ClickTheMouse()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        private void TurnOnAutoclicker()
        {
            if (!_dispatcherTimer.IsEnabled)
            {
                _dispatcherTimer.Start();
            }
        }

        private void TurnOffAutoclicker()
        {
            _dispatcherTimer.Stop();
        }

         private void SetTimerInterval(int intervalValue_ms)
        {
            _dispatcherTimer.Interval = TimeSpan.FromMilliseconds(intervalValue_ms);
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            HandleAutoClickerShortcut(e);
        }

        private void HandleAutoClickerShortcut(GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp &&
                e.KeyboardData.Key == Keys.F6)
            {
                rbnOn.IsChecked = !rbnOn.IsChecked;
                rbnOff.IsChecked = !rbnOn.IsChecked; //make sure the off rbn is in the right state
            }
        }
    }
}
