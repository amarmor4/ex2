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
    /// Interaction logic for SettingManu.xaml
    /// </summary>
    public partial class SettingManu : Window
    {
        /// <summary>
        /// settings viewModel
        /// </summary>
        private SettingsViewModel vm;

        /// <summary>
        /// constructor
        /// </summary>
        public SettingManu()
        {
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.LightYellow);
            ISettingsModel settingsModel=new ApplicationSettingsModel();
            vm = new SettingsViewModel(settingsModel);
            this.DataContext = vm;
        }

        /// <summary>
        /// event click on start btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnOk_click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings(); 
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        /// <summary>
        /// event click on cancel btn.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routed event args</param>
        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
