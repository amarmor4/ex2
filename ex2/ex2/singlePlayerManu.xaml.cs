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
using System.Windows.Shapes;
using MazeGUI.Controls;

namespace ex2
{
    /// <summary>
    /// Interaction logic for singlePlayerManu.xaml
    /// </summary>
    public partial class singlePlayerManu : Window
    {
        /// <summary>
        /// constructor
        /// </summary>
        public singlePlayerManu()
        {
            InitializeComponent();

        }

        /// <summary>
        /// event click on start btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnStart_click(object sender, RoutedEventArgs e)
        {
            mazeFildes.valid_ok(sender, e);
        }

        /// <summary>
        /// event click on cancel btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
