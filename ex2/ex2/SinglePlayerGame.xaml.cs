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
        /// is btn disabled
        /// </summary>
        bool isBtnDisabled;

        public bool IsServerFailed { set; get; }

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
            this.isBtnDisabled = false;
            this.IsServerFailed = false;
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
            if (vm.MazeSolve == null)
                vm.SolveGame(this.name);
            else
                myMazeBoard.AnimationSolve();
        }

        /// <summary>
        /// stnMainMenu_Click event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">routedEventArgs</param>
        private void stnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

        /// <summary>
        /// add listen to keyBoard
        /// </summary>
        public void AddListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
            this.KeyDown += MazeBoard_KeyDown;
        }

        /// <summary>
        /// remove listen to keyBoard
        /// </summary>
        public void RemoveListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
        }

        /// <summary>
        /// enable buttons
        /// </summary>
        public void EnableButtons()
        {
            stnRestart.IsEnabled = true;
            stnSolve.IsEnabled = true;
            stnMainMenu.IsEnabled = true;
            this.isBtnDisabled = false;
        }

        /// <summary>
        /// disable buttons
        /// </summary>
        public void DisableButtons()
        {
            stnRestart.IsEnabled = false;
            stnSolve.IsEnabled = false;
            stnMainMenu.IsEnabled = false;
            this.isBtnDisabled = true;
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.isBtnDisabled && !IsServerFailed)
            {
                Window areYouSure = new AreYouSure();
                if (areYouSure.ShowDialog() == true)
                {
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                }
                else
                    e.Cancel = true;
            }
            else if(this.isBtnDisabled)
            {
                MessageBox.Show("wait for animation to finish");
                e.Cancel = true;
            }
        }
    }
}
