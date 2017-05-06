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

namespace MazeGUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeFields.xaml
    /// </summary>
    public partial class MazeFields : UserControl
    {
        /// <summary>
        /// constructor
        /// </summary>
        public MazeFields()
        {
            InitializeComponent();
        }

        /// <summary>
        /// check if all form is valid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        public void valid_ok(object sender, RoutedEventArgs e)
        {
            int row, col;
            if (txtMazeName.Text == "" || txtMazeName.Text == "enter name here"
                || txtRows.Text == "" || txtRows.Text == "enter rows here"
                || txtCols.Text == "" || txtCols.Text == "enter columns here")
            {
                MessageBox.Show("some fileds are missing");
            }
            else
            {
                if (!int.TryParse(txtRows.Text, out row) || !int.TryParse(txtCols.Text, out col))
                {
                    MessageBox.Show("rows & cols must be an integer");
                }
            }
        }

        /// <summary>
        /// clear name filed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtRemoveName(object sender, RoutedEventArgs e)
        {
            if (txtMazeName.Text == "enter name here")
            {
                txtMazeName.Text = "";
            }
        }

        /// <summary>
        /// clear row filed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtRemoveRow(object sender, RoutedEventArgs e)
        {
            if (txtRows.Text == "enter rows here")
            {
                txtRows.Text = "";
            }
        }

        /// <summary>
        /// clear column filed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtRemoveCol(object sender, RoutedEventArgs e)
        {
            if (txtCols.Text == "enter columns here")
            {
                txtCols.Text = "";
            }
        }

        /// <summary>
        /// set default text filed - name
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtDefName(object sender, RoutedEventArgs e)
        {
            if (txtMazeName.Text == "")
            {
                txtMazeName.Text = "enter name here";
            }
        }

        /// <summary>
        /// set default text filed - rows
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtDefRows(object sender, RoutedEventArgs e)
        {
            if (txtRows.Text == "")
            {
                txtRows.Text = "enter rows here";
            }
        }

        /// <summary>
        /// default text filed - columns
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void txtDefCols(object sender, RoutedEventArgs e)
        {
            if (txtCols.Text == "")
            {
                txtCols.Text = "enter columns here";
            }
        }
    }
}
