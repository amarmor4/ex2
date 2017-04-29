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

namespace ex2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void clickSinglePlayer(object sender, RoutedEventArgs e)
        {
            Window singleManu = new singlePlayerManu();
            this.Hide();
            singleManu.Show();
        }

        private void clickMultiPlayer(object sender, RoutedEventArgs e)
        {
            Window multiManu = new MultiPlayerManu();
            this.Hide();
            multiManu.Show();
        }

        private void settings_click(object sender, RoutedEventArgs e)
        {
            Window settings = new SettingManu();
            this.Hide();
            settings.Show();
        }
    }
}
