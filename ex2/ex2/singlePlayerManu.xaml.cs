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
    public partial class SinglePlayerManu : Window
    {
        bool isStartGame;

        /// <summary>
        /// constructor
        /// </summary>
        public SinglePlayerManu()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.LightYellow);
            this.isStartGame = false;
        }

        /// <summary>
        /// event click on start btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnStart_click(object sender, RoutedEventArgs e)
        {
            if (mazeFildes.valid_ok(sender, e) == true)
            {
                string name = mazeFildes.txtMazeName.Text;
                int rows = int.Parse(mazeFildes.txtRows.Text);
                int cols = int.Parse(mazeFildes.txtCols.Text);
                this.isStartGame = true;
                this.Close();
                Window singlePlayerGame = new SinglePlayerGame(name, rows, cols);
            }
        }

        /// <summary>
        /// event click on cancel btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isStartGame)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
        }
    }
}
