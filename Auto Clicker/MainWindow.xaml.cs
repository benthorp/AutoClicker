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
        DispatcherTimer _autoClickTimer;
        DispatcherTimer _autoTypeTimer;
        GlobalKeyboardHook _keyboardHooks;

        private uint[] _keysToPress = new uint[] { KeyPresser.ONE, KeyPresser.TWO, KeyPresser.THREE, KeyPresser.FOUR, KeyPresser.FIVE, KeyPresser.SIX };
        private int _nextKeyIndex = 0;

        public MainWindow()
        {
            InitializeKeyLogger();
            InitializeClickTimer();
            InitializeKeyTimer();
            InitializeComponent();
        }

        #region AutoClicker
        private void InitializeClickTimer()
        {
            if (_autoClickTimer != null)
            {
                _autoClickTimer.Stop();
            }
            else
            {
                _autoClickTimer = new DispatcherTimer();
                _autoClickTimer.Tick += AutoClickTimerTick;
            }
        }

        private void AutoClickTimerTick(object sender, EventArgs e)
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
                SetAutoclickTimerInterval(intervalValue_ms);
            }
        }

        private static void ClickTheMouse()
        {
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftDown);
            MouseOperations.MouseEvent(MouseOperations.MouseEventFlags.LeftUp);
        }

        private void TurnOnAutoclicker()
        {
            if (!_autoClickTimer.IsEnabled)
            {
                _autoClickTimer.Start();
            }
        }

        private void TurnOffAutoclicker()
        {
            _autoClickTimer.Stop();
        }

        private void SetAutoclickTimerInterval(int intervalValue_ms)
        {
            _autoClickTimer.Interval = TimeSpan.FromMilliseconds(intervalValue_ms);
        }
        #endregion

        #region AutoTyper
        private void InitializeKeyTimer()
        {
            if (_autoTypeTimer != null)
            {
                _autoTypeTimer.Stop();
            }
            else
            {
                _autoTypeTimer = new DispatcherTimer();
                _autoTypeTimer.Tick += AutoKeyTimerTick;
            }
        }

        private void AutoKeyTimerTick(object sender, EventArgs e)
        {
            PressNextNumberKey();
        }

        private void PressNextNumberKey()
        {
            KeyPresser.PressKey(_keysToPress[_nextKeyIndex]);

            if (_nextKeyIndex >= _keysToPress.Length - 1)
            {
                _nextKeyIndex = 0;
            }
            else
            {
                _nextKeyIndex++;
            }
        }

        private void rbnAutotypeOn_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnAutotypeOn.IsChecked == true)
            {
                TurnOnAutotypeTimer();
            }
        }

        private void rbnAutotypeOff_Checked(object sender, RoutedEventArgs e)
        {
            if (rbnAutotypeOff.IsChecked == true)
            {
                TurnOffAutotypeTimer();
            }
        }

        private void txtAutotypeInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            int intervalValue_ms;

            if (int.TryParse(txtAutotypeInterval.Text, out intervalValue_ms))
            {
                SetAutotypeTimerInterval(intervalValue_ms);
            }
        }

        private void TurnOnAutotypeTimer()
        {
            if (!_autoTypeTimer.IsEnabled)
            {
                _autoTypeTimer.Start();
            }
        }

        private void TurnOffAutotypeTimer()
        {
            _autoTypeTimer.Stop();
        }
        
        private void SetAutotypeTimerInterval(int intervalValue_ms)
        {
            _autoTypeTimer.Interval = TimeSpan.FromMilliseconds(intervalValue_ms);
        }

        #endregion

        #region KeyLogger
        private void InitializeKeyLogger()
        {
            _keyboardHooks = new GlobalKeyboardHook(null);
            _keyboardHooks.KeyboardPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            HandleAutoClickerShortcut(e);
            HandleAutoTyperShortcut(e);

            //if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
            //{
            //    txtKeypresses.Text = e.KeyboardData.Key.ToString();
            //}
        }

        private void HandleAutoClickerShortcut(GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp &&
                e.KeyboardData.Key == Keys.F6)
            {
                e.Handled = true;
                rbnOn.IsChecked = !rbnOn.IsChecked;
                rbnOff.IsChecked = !rbnOn.IsChecked; //make sure the off rbn is in the right state
            }
        }

        private void HandleAutoTyperShortcut(GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp &&
                e.KeyboardData.Key == Keys.F5)
            {
                e.Handled = true;
                rbnAutotypeOn.IsChecked = !rbnAutotypeOn.IsChecked;
                rbnAutotypeOff.IsChecked = !rbnAutotypeOn.IsChecked; //make sure the off rbn is in the right state
            }
        }

        #endregion

    }
}
