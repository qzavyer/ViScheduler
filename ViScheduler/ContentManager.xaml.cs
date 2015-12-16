using System.Windows;
using System.Windows.Controls;

namespace ViScheduler
{
    /// <summary>
    /// Логика взаимодействия для ContentManager.xaml
    /// </summary>
    public partial class ContentManager
    {
        public ContentManager()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPhrases();
        }

        private void LoadPhrases()
        {
            IPhraseLoader loader = new PhraseLoader(@"phrases.xml");
            var phrases = loader.GetAllPhrases();
            var stackPanel = new StackPanel {Orientation = Orientation.Vertical};
            foreach (var phrase in phrases)
            {
                stackPanel.Children.Add(new Label {Content = phrase.Value});
                var button = new Button {Content = "Удалить", Tag = phrase.Key};
                button.Click += DeleteButton_Click;
                stackPanel.Children.Add(button);
            }
            ScrollViewer.Content = stackPanel;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var rsltMessageBox = MessageBox.Show("Удалить запись?", "Подтверждение",
                MessageBoxButton.OKCancel, MessageBoxImage.Information);
            if(rsltMessageBox!=MessageBoxResult.OK) return;
            var button = sender as Button;
            if(button==null) return;
            var id = button.Tag.ToString();
            IPhraseLoader loader = new PhraseLoader(@"phrases.xml");
            loader.DeletePhrase(id);
            LoadPhrases();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var phrase = NewLine.Text;
            NewLine.Text = null;
            IPhraseLoader loader = new PhraseLoader(@"phrases.xml");
            loader.AddPhrase(phrase);
            LoadPhrases();
        }
    }
}
