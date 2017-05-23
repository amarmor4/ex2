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
        /// <summary>
        /// MultiPlayerGame window
        /// </summary>
        MultiPlayerGame MultiWindow;

        /// <summary>
        /// is closed due to start game
        /// </summary>
        bool isClosedDueToStartGame;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="multiPlayerGame">multiPlayerGame window</param>
        public Spinner(MultiPlayerGame multiPlayerGame)
        {
            InitializeComponent();
            this.MultiWindow = multiPlayerGame;
            this.isClosedDueToStartGame = false;
        }

        /// <summary>
        /// stnMainMenu_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// when gid end event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            gifSpinner.Position = new TimeSpan(0, 0, 1);
            gifSpinner.Play();
        }

        /// <summary>
        /// close due to start game
        /// </summary>
        public void ClosedDueToStartGame()
        {
            this.isClosedDueToStartGame = true;
            this.Close();
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isClosedDueToStartGame)
            {
                if (this.MultiWindow != null)
                {
                    this.MultiWindow.Close();
                }
            }
        }
    }
}
