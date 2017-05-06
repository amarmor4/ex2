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
        /// <summary>
        /// constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// event click on single player btn
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnSinglePlayer_click(object sender, RoutedEventArgs e)
        {
            Window singleManu = new singlePlayerManu();
            this.Hide();
            singleManu.ShowDialog();
        }

        /// <summary>
        /// event click on multiplayer btn
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnMultiPlayer_click(object sender, RoutedEventArgs e)
        {
            Window multiManu = new MultiPlayerManu();
            this.Hide();
            multiManu.ShowDialog();
        }

        /// <summary>
        /// event click on settings btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnSettings_click(object sender, RoutedEventArgs e)
        {
            Window settings = new SettingManu();
            this.Hide();
            settings.ShowDialog();
        }
    }
}
