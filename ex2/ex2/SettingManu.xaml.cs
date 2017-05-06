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
        private SettingsViewModel vm;

        public SettingManu()
        {
            InitializeComponent();
            ISettingsModel settingsModel=new ApplicationSettingsModel();
            vm = new SettingsViewModel(settingsModel);
            this.DataContext = vm;
        }

        private void btnOk_click(object sender, RoutedEventArgs e)
        {
            vm.SaveSettings(); 
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }

        private void btnCancel_click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
