using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ViScheduler
{
    /// <summary>
    /// Логика взаимодействия для NotifyWindow.xaml
    /// </summary>
    public partial class NotifyWindow
    {
        private int _showCounter = 0;
        public NotifyWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _showCounter = 0;
            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            IPhraseLoader loader = new PhraseLoader(@"phrases.xml");
            InfoBlock.Text = loader.GetPhrase();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            _showCounter++;
            var show = Convert.ToInt32(ConfigurationManager.AppSettings["Show"]);
            if (_showCounter < show) return;
            Close();
        }
    }
}
