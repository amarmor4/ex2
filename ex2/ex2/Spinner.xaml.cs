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
    /// Interaction logic for Spinner.xaml
    /// </summary>
    public partial class Spinner : Window
    {
        public Spinner()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.LightYellow);
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

        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            gifSpinner.Position = new TimeSpan(0, 0, 1);
            gifSpinner.Play();
        }
    }
}
