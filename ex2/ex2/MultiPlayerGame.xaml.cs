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

        Window spinner;


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
            if (command == "Start")
            {
                this.spinner = new Spinner();
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
            Window areYouSure = new AreYouSure();
            if (areYouSure.ShowDialog() == true)
            {
                this.CloseGame();
                MainWindow win = (MainWindow)Application.Current.MainWindow;
                win.Show();
                this.Close();
            }
        }

        public void Start(string name, int rows, int cols)
        {
            this.vm.StartGame(name, rows, cols);
        }

        public void Join(string name)
        {
            this.vm.Join(name);
        }

        private void MazeBoard_KeyDown(object sender, KeyEventArgs e)
        {
            myMazeBoard.KeyBoardDown(myMazeBoard, e);
            this.vm.Play(e.Key.ToString());
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
                var kea = new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), 0,key);
                otherMazeBoard.KeyBoardDown(otherMazeBoard, kea);
            }
        }

        public void CloseGame()
        {
            this.vm.Close();
        }

        public void CloseSpinner()
        {
            if (this.spinner != null)
                this.spinner.Close();
        }
    }
}
