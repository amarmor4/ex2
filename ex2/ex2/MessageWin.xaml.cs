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
    /// Interaction logic for MessageWin.xaml
    /// </summary>
    public partial class MessageWin : Window
    {
        public MessageWin(string msg)
        {
            InitializeComponent();
            txtBlockMsg.Text = msg;
        }


        /// <summary>
        /// stnMainMenu_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnMainMenu_Click(object sender, RoutedEventArgs e)
        {             
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
        }
    }
}
