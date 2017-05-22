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
        /// <summary>
        /// single player game viewModel
        /// </summary>
        private SinglePlayerGameViewModel vm;

        /// <summary>
        /// maze name
        /// </summary>
        String name;

        /// <summary>
        /// maze rows
        /// </summary>
        int rows;

        /// <summary>
        /// maze cols
        /// </summary>
        int cols;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="mazeRows">maze rows</param>
        /// <param name="mazeCols">maze cols</param>
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
            this.Background = new SolidColorBrush(Colors.LightYellow);
            this.StartGame();
        }

        /// <summary>
        /// start game - generate game
        /// </summary>
        private void StartGame()
        {
            vm.StartGame(this.name, this.rows, this.cols);
        }

        /// <summary>
        /// restart game
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnRestart_Click(object sender, RoutedEventArgs e)
        {
            Window areYouSure = new AreYouSure();
            if (areYouSure.ShowDialog() == true)
            {
                myMazeBoard.ResetCurrentState();
            }
        }

        /// <summary>
        /// stnSolve_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnSolve_Click(object sender, RoutedEventArgs e)
        {
            vm.SolveGame(this.name);
        }

        /// <summary>
        /// stnMainMenu_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
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

        /// <summary>
        /// MazeBoard_KeyDown event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void MazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            myMazeBoard.KeyBoardDown(myMazeBoard, e);
        }

        public void AddListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
            this.KeyDown += MazeBoard_KeyDown;
        }

        public void RemoveAddListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
        }

        public void EnableButtons()
        {
            stnRestart.IsEnabled = true;
            stnSolve.IsEnabled = true;
            stnMainMenu.IsEnabled = true;
        }

        public void DisableButtons()
        {
            stnRestart.IsEnabled = false;
            stnSolve.IsEnabled = false;
            stnMainMenu.IsEnabled = false;
            myMazeBoard.AnimationEndedChanged += delegate () {
                this.EnableButtons();
                
            };
        }
    }
}
