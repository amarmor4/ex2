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
    /// Interaction logic for MultiPlayerManu.xaml
    /// </summary>
    public partial class MultiPlayerManu : Window
    {
        public MultiPlayerManu()
        {
            InitializeComponent();
        }

        private void txtRemoveName(object sender, RoutedEventArgs e)
        {
            if (txtMazeName.Text == "enter name here")
            {

                txtMazeName.Text = "";
            }
        }
        private void txtRemoveRow(object sender, RoutedEventArgs e)
        {
            if (txtRows.Text == "enter rows here")
            {
                txtRows.Text = "";
            }
        }
        private void txtRemoveCol(object sender, RoutedEventArgs e)
        {
            if (txtCols.Text == "enter columns here")
            {
                txtCols.Text = "";
            }
        }

        private void txtDefName(object sender, RoutedEventArgs e)
        {
            if (txtMazeName.Text == "")
            {

                txtMazeName.Text = "enter name here";
            }
        }
        private void txtDefRows(object sender, RoutedEventArgs e)
        {
            if (txtRows.Text == "")
            {
                txtRows.Text = "enter rows here";
            }
        }
        private void txtDefCols(object sender, RoutedEventArgs e)
        {
            if (txtCols.Text == "")
            {
                txtCols.Text = "enter columns here";
            }
        }

        private void btnStart_click(object sender, RoutedEventArgs e)
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
                //double.TryParse(candidate, out num)
                if (!int.TryParse(txtRows.Text, out row) || !int.TryParse(txtCols.Text, out col))
                {
                    MessageBox.Show("rows & cols must be an integer");
                }
            }
        }

        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
