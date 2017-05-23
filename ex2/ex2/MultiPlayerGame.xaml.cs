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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ex2
{
    /// <summary>
    /// Interaction logic for MultiPlayerGame.xaml
    /// </summary>
    public partial class MultiPlayerGame : Window
    {
        /// <summary>
        /// multi player game view model;
        /// </summary>
        private MultiPlayerGameViewModel vm;

        /// <summary>
        /// maze name
        /// </summary>
        string mazeName;

        /// <summary>
        /// second player close game
        /// </summary>
        public bool SecondPlayerCloseGame { set; get; }

        /// <summary>
        /// is server failed
        /// </summary>
        public bool IsServerFailed { set; get; }

        /// <summary>
        /// spinner window
        /// </summary>
        Spinner spinner;

        /// <summary>
        /// constructor
        /// </summary>
        public MultiPlayerGame(string command, string name, int rows, int cols)
        {
            ITelnetClient telnetClient = new TelnetClient();
            MultiPlayerGameModel model = new MultiPlayerGameModel(telnetClient);
            this.vm = new MultiPlayerGameViewModel(model);
            this.DataContext = this.vm;
            InitializeComponent();
            this.Background = new SolidColorBrush(Colors.LightYellow);
            this.mazeName = name;
            if (command == "Start")
            {
                this.spinner = new Spinner(this);
                this.spinner.Show();
                this.Start(name, rows, cols);
            }
            if (command == "Join")
                this.Join(name);
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
        /// start game commnad
        /// </summary>
        /// <param name="name">maze name</param>
        /// <param name="rows">rows</param>
        /// <param name="cols">cols</param>
        public void Start(string name, int rows, int cols)
        {
            this.vm.StartGame(name, rows, cols);
        }

        /// <summary>
        /// join commnad
        /// </summary>
        /// <param name="name"></param>
        public void Join(string name)
        {
            this.vm.Join(name);
        }

        /// <summary>
        /// maze board keyDown
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        private void MazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            myMazeBoard.KeyBoardDown(myMazeBoard, e);
            this.vm.Play(e.Key.ToString());
        }

        /// <summary>
        /// add listen to keyboard
        /// </summary>
        public void AddListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
            this.KeyDown += MazeBoard_KeyDown;
        }

        /// <summary>
        /// remove listen to keyboard
        /// </summary>
        public void RemoveListenToKeyBoard()
        {
            this.KeyDown -= MazeBoard_KeyDown;
        }

        /// <summary>
        /// make rival move
        /// </summary>
        /// <param name="move"></param>
        public void RivalMove(string move)
        {
            Key key;
            key = Key.Space;
            switch (move)
            {
                case "right":
                    key = Key.Right;
                    break;
                case "left":
                    key = Key.Left;
                    break;
                case "up":
                    key = Key.Up;
                    break;
                case "down":
                    key = Key.Down;
                    break;
            }
            if (key != Key.Space)
            {
                var kea = new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0, key);
                otherMazeBoard.KeyBoardDown(otherMazeBoard, kea);
            }
        }

        /// <summary>
        /// close game
        /// </summary>
        public void CloseGame()
        {
            this.vm.Close(this.mazeName);
        }

        /// <summary>
        /// close spinner
        /// </summary>
        public void CloseSpinner()
        {
            if (this.spinner != null)
                this.spinner.ClosedDueToStartGame();              
        }

        /// <summary>
        /// closing window event.
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">System.ComponentModel.CancelEventArgs</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.SecondPlayerCloseGame && !IsServerFailed)
            {
                Window areYouSure = new AreYouSure();
                if (areYouSure.ShowDialog() == true)
                {
                    try {
                        this.CloseGame();
                    }
                    catch (Exception) { }
                    MainWindow win = (MainWindow)Application.Current.MainWindow;
                    win.Show();
                }
                else
                    e.Cancel = true;
            }
        }
    }
}
