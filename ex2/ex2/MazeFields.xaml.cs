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
using ex2;

namespace MazeGUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeFields.xaml
    /// </summary>
    public partial class MazeFields : UserControl
    {
        /// <summary>
        /// maze fields viewModel
        /// </summary>
        private MazeFieldsViewModel vm;

        /// <summary>
        /// constructor
        /// </summary>
        public MazeFields()
        {
            InitializeComponent();
            IMazeFieldsModel mazeFieldsModel = new ApplicationMazeFieldsModel();
            vm = new MazeFieldsViewModel(mazeFieldsModel);
            this.DataContext = vm;
        }

        /// <summary>
        /// check if all form is valid
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        public void valid_ok(object sender, RoutedEventArgs e)
        {
            int rows, cols;
            if (txtMazeName.Text == "" || txtMazeName.Text == "enter name here" || txtRows.Text == "" || txtCols.Text == "")
                MessageBox.Show("some fileds are missing");
            else
            {
                if (!int.TryParse(txtRows.Text, out rows) || !int.TryParse(txtCols.Text, out cols))
                    MessageBox.Show("rows & cols must be an integers");
                else if(rows<=0 || cols<=0)
                    MessageBox.Show("rows & cols must be positive integers");
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
    }
}
