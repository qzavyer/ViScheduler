using System;
using System.Configuration;
using System.Windows;
using System.Windows.Threading;

namespace ViScheduler
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private int _periodCounter;
        public MainWindow()
        {
            InitializeComponent();
            Hide();
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 1, 0);
            dispatcherTimer.Start();
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            var setWin = new SettingsWindow();
            setWin.ShowDialog();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _periodCounter++;
            var period = Convert.ToInt32(ConfigurationManager.AppSettings["Period"]);
            var periodDev = period/10;
            var periodMin = period-periodDev;
            var rand = new Random();
            var val = rand.Next(periodDev*2);
            var periodVal = val + periodMin;
            if (_periodCounter < periodVal) return;
            _periodCounter = 0;
            var notify = new NotifyWindow();
            notify.Show();
        }

        private void ShowButton_Click(object sender, RoutedEventArgs e)
        {
            var notify = new NotifyWindow();
            notify.Show();
        }
        private void NotifyButton_Click(object sender, RoutedEventArgs e)
        {
            var cont = new ContentManager();
            cont.Show();
        }
    }
}
