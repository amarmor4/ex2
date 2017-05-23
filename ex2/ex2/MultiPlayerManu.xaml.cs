using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MultiPlayerManu.xaml
    /// </summary>
    public partial class MultiPlayerManu : Window
    {
        /// <summary>
        /// multi player game view model 
        /// </summary>
        private MultiPlayerGameViewModel vm;

        /// <summary>
        /// is start game
        /// </summary>
        bool isStartGame;

        /// <summary>
        /// constructor
        /// </summary>
        public MultiPlayerManu()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.LightYellow);
            ITelnetClient telnetClient = new TelnetClient();
            MultiPlayerGameModel model = new MultiPlayerGameModel(telnetClient);
            this.vm = new MultiPlayerGameViewModel(model);
            this.DataContext = this.vm;
            this.isStartGame = false;
            vm.GetListToJoin();      
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
                Window multiPlayerGame = new MultiPlayerGame("Start", name, rows, cols);                
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
        /// event click on join btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnJoin_click(object sender, RoutedEventArgs e)
        {
            if (comboGamesList.SelectedItem.ToString()==null)
                MessageBox.Show("can't join without choose a game");
            Window multiPlayerGame = new MultiPlayerGame("Join", comboGamesList.SelectedItem.ToString(), 0, 0);
            this.isStartGame = true;
            this.Close();
        }

        /// <summary>
        /// combobox selected item
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void comboSelectedItem(object sender, RoutedEventArgs e)
        {
            btnJoin.IsEnabled = true;
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!this.isStartGame)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
            }
        }

        private void comboGamesList_DropDownOpened(object sender, EventArgs e)
        {

        }
    }
}
