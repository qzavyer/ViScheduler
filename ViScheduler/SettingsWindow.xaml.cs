using System;
using System.Configuration;
using System.Windows;

namespace ViScheduler
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow
    {
        private int _period;
        private const int MinPeriod = 60;
        private const int MaxPeriod = 360;
        private int _show;
        private const int MinShow = 10;
        private const int MaxShow = 60;

        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var period = config.AppSettings.Settings["Period"].Value;
            NotifyPeriodField.Text = period;
            _period = Convert.ToInt32(period);
            var show = config.AppSettings.Settings["Show"].Value;
            NotifyTimeField.Text = show;
            _show = Convert.ToInt32(show);
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Period"].Value = NotifyPeriodField.Text;
            config.AppSettings.Settings["Show"].Value = NotifyTimeField.Text;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            Close();
        }

        private void NotifyPeriodUp_Click(object sender, RoutedEventArgs e)
        {
            _period++;
            if (_period > MaxPeriod) _period--;
            NotifyPeriodField.Text = _period.ToString("D");
        }

        private void NotifyPeriodDn_Click(object sender, RoutedEventArgs e)
        {
            _period--;
            if (_period < MinPeriod) _period++;
            NotifyPeriodField.Text = _period.ToString("D");
        }

        private void NotifyTimeUp_Click(object sender, RoutedEventArgs e)
        {
            _show++;
            if (_show > MaxShow) _show--;
            NotifyTimeField.Text = _show.ToString("D");
        }

        private void NotifyTimeDn_Click(object sender, RoutedEventArgs e)
        {
            _show--;
            if (_show < MinShow) _show++;
            NotifyTimeField.Text = _show.ToString("D");
        }

        private void NotifyPeriodField_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                _period = Convert.ToInt32(NotifyPeriodField.Text);
                if (_period > MaxPeriod) _period = MaxPeriod;
                if (_period < MinPeriod) _period = MinPeriod;
                NotifyPeriodField.Text = _period.ToString("D");
            }
            catch
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var period = config.AppSettings.Settings["Period"].Value;
                NotifyPeriodField.Text = period;
            }
        }

        private void NotifyTimeField_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                _show = Convert.ToInt32(NotifyTimeField.Text);
                if (_show > MaxShow) _show = MaxShow;
                if (_show < MinShow) _show = MinShow;
                NotifyTimeField.Text = _show.ToString("D");
            }
            catch
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var show = config.AppSettings.Settings["Show"].Value;
                NotifyTimeField.Text = show;
            }
        }
    }
}
