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

namespace ex2
{
    /// <summary>
    /// Interaction logic for MultiPlayerGame.xaml
    /// </summary>
    public partial class MultiPlayerGame : Window
    {
        public MultiPlayerGame()
        {
            InitializeComponent();
        }

        /// <summary>
        /// stnMainMenu_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Window areYouSure = new AreYouSure();
            if (areYouSure.ShowDialog() == true)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }
    }
}
