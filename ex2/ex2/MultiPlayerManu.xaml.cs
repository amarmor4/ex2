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

        private void textRemoveName(object sender, RoutedEventArgs e)
        {
            if (mazeNameValue.Text == "enter name here")
            {

                mazeNameValue.Text = "";
            }
        }
        private void textRemoveRow(object sender, RoutedEventArgs e)
        {
            if (rowNum.Text == "enter rows here")
            {
                rowNum.Text = "";
            }
        }
        private void textRemoveCol(object sender, RoutedEventArgs e)
        {
            if (colNum.Text == "enter columns here")
            {
                colNum.Text = "";
            }
        }

        private void defTextName(object sender, RoutedEventArgs e)
        {
            if (mazeNameValue.Text == "")
            {

                mazeNameValue.Text = "enter name here";
            }
        }
        private void defTextRow(object sender, RoutedEventArgs e)
        {
            if (rowNum.Text == "")
            {
                rowNum.Text = "enter rows here";
            }
        }
        private void defTextCol(object sender, RoutedEventArgs e)
        {
            if (colNum.Text == "")
            {
                colNum.Text = "enter columns here";
            }
        }

        private void startGameClick(object sender, RoutedEventArgs e)
        {
            int row, col;
            if (mazeNameValue.Text == "" || mazeNameValue.Text == "enter name here"
                || rowNum.Text == "" || rowNum.Text == "enter rows here"
                || colNum.Text == "" || colNum.Text == "enter columns here")
            {
                MessageBox.Show("some fileds are missing");
            }
            else
            {
                //double.TryParse(candidate, out num)
                if (!int.TryParse(rowNum.Text, out row) || !int.TryParse(colNum.Text, out col))
                {
                    MessageBox.Show("rows & cols must be an integer");
                }
            }
        }
    }
}
