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
using MazeGUI.Controls;

namespace ex2
{
    /// <summary>
    /// Interaction logic for SingalPlayerGame.xaml
    /// </summary>
    public partial class SinglePlayerGame : Window
    {
        private SinglePlayerGameViewModel vm;
        String name;
        int rows;
        int cols;
        
        public SinglePlayerGame(string mazeName, int mazeRows, int mazeCols)
        {
            this.name = mazeName;
            this.rows = mazeRows;
            this.cols = mazeCols;
            ITelnetClient telnetClient = new TelnetClient();
            SinglePlayerGameModel model = new SinglePlayerGameModel(telnetClient);
            vm = new SinglePlayerGameViewModel(model);
            this.DataContext = vm;
            InitializeComponent();
            this.StartGame();
            this.Show();
            mazeBoard.DrawMaze();
            this.KeyDown += MazeBoard_KeyDown;
        }

        private void StartGame()
        {
            vm.StartGame(this.name, this.rows, this.cols);
        }

        private void stnRestart_Click(object sender, RoutedEventArgs e)
        {
            Window areYouSure = new AreYouSure();
            if (areYouSure.ShowDialog() == true)
            {
                mazeBoard.ResetCurrentState();
            }
        }

        private void stnSolve_Click(object sender, RoutedEventArgs e)
        {
            vm.SolveGame(this.name);
        }

        private void stnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            Window areYouSure = new AreYouSure();
            if (areYouSure.ShowDialog()==true)
            {
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }

        private void MazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            mazeBoard.KeyBoardDown(sender, e);
        }
    }
}
