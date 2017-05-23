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
    /// Interaction logic for AreYouSure.xaml
    /// </summary>
    public partial class AreYouSure : Window
    {
        /// <summary>
        /// if clicked sure
        /// </summary>
        bool isClickedSure;

        /// <summary>
        /// constructor
        /// </summary>
        public AreYouSure()
        {
            InitializeComponent();
            this.isClickedSure = false;
        }

        /// <summary>
        /// cancel click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        public void btnCancel_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// sure click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        public void btnSure_click(object sender, RoutedEventArgs e)
        {
            this.isClickedSure = true;
            this.Close();
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.isClickedSure)
                this.DialogResult = true;
            else
                this.DialogResult = false;
        }
    }
}
