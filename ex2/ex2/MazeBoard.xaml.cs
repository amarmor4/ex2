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
using System.Windows.Media.Animation;
using System.Threading;


namespace MazeGUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl
    {
        /// <summary>
        /// constructor
        /// </summary>
        public MazeBoard()
        {
            InitializeComponent();
        }

        public delegate void AnimationEnded();

        public event AnimationEnded AnimationEndedChanged;

        public bool CloseGame
        {
            get { return (bool)GetValue(CloseGameProperty); }
            set { SetValue(CloseGameProperty, value); }
        }

        /// <summary>
        /// mode play dp
        /// </summary>
        public string ModePlay {
            get { return (string)GetValue(ModePlayProperty); }
            set { SetValue(ModePlayProperty, value); }
        }


        /// <summary>
        /// rows dp
        /// </summary>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        /// <summary>
        /// cols dp
        /// </summary>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        /// <summary>
        /// maze path dp
        /// </summary>
        public string MazePath
        {
            get { return (string)GetValue(MazePathProperty); }
            set { SetValue(MazePathProperty, value); }
        }
        
        /// <summary>
        /// initial state dp
        /// </summary>
        public string InitialState
        {
            get { return (string)GetValue(InitialStateProperty); }
            set {
                SetValue(InitialStateProperty, value);
            }
        }

        /// <summary>
        /// goal state dp
        /// </summary>
        public string GoalState
        {
            get { return (string)GetValue(GoalStateProperty); }
            set {
                SetValue(GoalStateProperty, value);
            }
        }

        /// <summary>
        /// move solve dp
        /// </summary>
        public string MazeSolve
        {
            get { return (string)GetValue(MazeSolveProperty); }
            set
            {
                SetValue(MazeSolveProperty, value);
            }
        }

        public string Move
        {
            get { return (string)GetValue(MoveProperty); }
            set
            {
                SetValue(MoveProperty, value);
            }
        }

        public bool ServerFailed
        {
            get { return (bool)GetValue(ServerFailedProperty); }
            set
            {
                SetValue(ServerFailedProperty, value);
            }
        }

        /// <summary>
        /// currnt state row
        /// </summary>
        int currntStateRow;

        /// <summary>
        /// currnt state row property
        /// </summary>
        public int CurrntStateRow
        {
            get { return this.currntStateRow; }
            set { this.currntStateRow = value; }
        }

        /// <summary>
        /// current state col 
        /// </summary>
        int currentStateCol;

        /// <summary>
        /// current state col property
        /// </summary>
        public int CurrntStateCol
        {
            get { return this.currentStateCol; }
            set { this.currentStateCol = value; }
        }

        /// <summary>
        /// DependencyProperty ModePlayProperty
        /// </summary>
        public static readonly DependencyProperty ModePlayProperty =
            DependencyProperty.Register("ModePlay", typeof(string), typeof(MazeBoard));

        /// <summary>
        /// DependencyProperty RowsProperty
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard));

        /// <summary>
        /// DependencyProperty ColsProperty
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));

        /// <summary>
        /// DependencyProperty MazePathProperty
        /// </summary>
        public static readonly DependencyProperty MazePathProperty =
            DependencyProperty.Register("MazePath", typeof(string), typeof(MazeBoard), new PropertyMetadata(onMazePathPropertyChanged));

        /// <summary>
        /// DependencyProperty InitialStateProperty
        /// </summary>
        public static readonly DependencyProperty InitialStateProperty =
            DependencyProperty.Register("InitialState", typeof(string), typeof(MazeBoard));

        /// <summary>
        /// DependencyProperty GoalStateProperty
        /// </summary>
        public static readonly DependencyProperty GoalStateProperty =
            DependencyProperty.Register("GoalState", typeof(string), typeof(MazeBoard));

        /// <summary>
        /// DependencyProperty MazeSolveProperty
        /// </summary>
        public static readonly DependencyProperty MazeSolveProperty =
            DependencyProperty.Register("MazeSolve", typeof(string), typeof(MazeBoard), new PropertyMetadata(onMazeSolvePropertyChanged));

        public static readonly DependencyProperty MoveProperty =
            DependencyProperty.Register("Move", typeof(object), typeof(MazeBoard), new PropertyMetadata(null,onMovePropertyChanged, moveCallBack));

        public static readonly DependencyProperty CloseGameProperty =
            DependencyProperty.Register("CloseGame", typeof(bool), typeof(MazeBoard), new PropertyMetadata(onCloseGamePropertyChanged));

        public static readonly DependencyProperty ServerFailedProperty =
            DependencyProperty.Register("ServerFailed", typeof(bool), typeof(MazeBoard), new PropertyMetadata(onServerFailedPropertyPropertyChanged));

        /// <summary>
        /// onMazePathPropertyChanged metadata
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void onMazePathPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
                ((MazeBoard)d).DrawMaze();
                ((MazeBoard)d).ShowWindow();
        }

        /// <summary>
        /// onMazeSolvePropertyChanged metadata - single player
        /// </summary>
        /// <param name="d">DependencyObject</param>
        /// <param name="e">DependencyPropertyChangedEventArgs</param>
        private static void onMazeSolvePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).AnimationSolve();
        }

        private static void onMovePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).RivalMove();
        }

        private static object moveCallBack(DependencyObject d, object baseValue)
        {
            if (baseValue != null && (d.GetValue(MoveProperty) == baseValue))
            {
                d.SetCurrentValue(MoveProperty, null);
                d.SetCurrentValue(MoveProperty, baseValue);
            }
            return baseValue;
        }

        private static void onCloseGamePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).CloseThisGame();
        }

        private static void onServerFailedPropertyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MazeBoard)d).ServerFailedExist();
        }

        public SinglePlayerGame SingleWindow { get{ return (SinglePlayerGame)Window.GetWindow(this); }}

        public MultiPlayerGame MultiWindow { get { return (MultiPlayerGame)Window.GetWindow(this); } }


        /// <summary>
        /// initial state row propery
        /// </summary>
        private int InitialStateRow{ get{ return Int32.Parse(InitialState.Split(',')[0]); }}

        /// <summary>
        /// initial state col propery
        /// </summary>
        private int InitialStateCol { get { return Int32.Parse(InitialState.Split(',')[1]); } }

        /// <summary>
        /// goal state row propery
        /// </summary>
        private int GoalStateRow { get { return Int32.Parse(GoalState.Split(',')[0]); }}

        /// <summary>
        /// goal state col propery
        /// </summary>
        private int GoalStateCol { get { return Int32.Parse(GoalState.Split(',')[1]); } }

        /// <summary>
        /// player propery
        /// </summary>
        private UIElement Player { get; set; }


        /// <summary>
        /// rectangle height propery
        /// </summary>
        private double RectHeight { get { return 300 / Rows; } }

        /// <summary>
        /// rectangle width property
        /// </summary>
        private double RectWidth { get { return 300 / Cols; } }

        /// <summary>
        /// draw maze
        /// </summary>
        public void DrawMaze()
        {
            this.DrawWalls(RectHeight, RectWidth);
            this.DrawIcons(InitialStateCol, InitialStateRow, RectHeight, RectWidth, "pokeball.png", "player");
            this.DrawIcons(GoalStateCol, GoalStateRow, RectHeight, RectWidth, "jigglypuff.png", "goal");
            foreach (UIElement ue in myCanvas.Children)
                if (((System.Windows.FrameworkElement)ue).Name == "player")
                    Player = ue; 
            ResetCurrentState();
        }

        /// <summary>
        /// draw walls
        /// </summary>
        /// <param name="height">height wall</param>
        /// <param name="width">width wall</param>
        private void DrawWalls(double height, double width)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (MazePath[i * Cols + j] == '1')
                    {
                        int col = Convert.ToInt32(j * width);
                        int row = Convert.ToInt32(i * height);
                        this.AddRectToBoard(col, row, height, width, "wall");
                    }
                }
            }
        }

        /// <summary>
        /// draw icones
        /// </summary>
        /// <param name="stateCol">state col</param>
        /// <param name="stateRow">state row</param>
        /// <param name="height">rect height</param>
        /// <param name="width">rect width</param>
        /// <param name="file">image location</param>
        /// <param name="name">name</param>
        private void DrawIcons(int stateCol,int stateRow,double height, double width, string file, string name)
        {
            int col = Convert.ToInt32(stateCol * width);
            int row = Convert.ToInt32(stateRow * height);
            this.AddImageRectToBoard(col, row, height, width, file, name);
        }

        /// <summary>
        /// add image rect to board
        /// </summary>
        /// <param name="col">col</param>
        /// <param name="row">row</param>
        /// <param name="height">rect height</param>
        /// <param name="width">rect width</param>
        /// <param name="file">image location</param>
        /// <param name="file">name</param>
        public void AddImageRectToBoard(int col, int row, double height, double width, string file, string name)
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(@file, UriKind.Relative));
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill = imgBrush;
            rect.Name = name;
            Canvas.SetLeft(rect, col);
            Canvas.SetTop(rect, row);
            myCanvas.Children.Add(rect);           
        }

        /// <summary>
        /// add rect to board
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <param name="name">name</param>
        private void AddRectToBoard(int col, int row, double height, double width, string name)
        {
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill= new SolidColorBrush(Colors.Black);
            rect.Name = name;
            Canvas.SetLeft(rect, col);
            Canvas.SetTop(rect, row);
            myCanvas.Children.Add(rect);
        }

        /// <summary>
        /// reset player location from current state to inital state
        /// </summary>
        public void ResetCurrentState()
        {
            if (myCanvas.Children.Contains(Player))
                myCanvas.Children.Remove(Player);
            myCanvas.Children.Add(Player);
            CurrntStateRow = InitialStateRow;
            CurrntStateCol = InitialStateCol;
            this.MovePlayer(Player, Convert.ToInt32(CurrntStateCol * RectWidth), "Left");
            this.MovePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");
            this.ListenToKeyBoard();           
        }

        /// <summary>
        /// KeyBoardDown event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">KeyEventArgs</param>
        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            try {
                switch (e.Key)
                {
                    case Key.Right:
                        if (MazePath[CurrntStateRow * Cols + (CurrntStateCol + 1)] == '0')
                            CurrntStateCol += 1;
                        break;
                    case Key.Left:
                        if (MazePath[CurrntStateRow * Cols + (CurrntStateCol - 1)] == '0')
                            CurrntStateCol -= 1;
                        break;
                    case Key.Up:
                        if (MazePath[(CurrntStateRow - 1) * Cols + CurrntStateCol] == '0')
                            CurrntStateRow -= 1;
                        break;
                    case Key.Down:
                        if (MazePath[(CurrntStateRow + 1) * Cols + CurrntStateCol] == '0')
                            CurrntStateRow += 1;
                        break;
                }
                if (e.Key == Key.Right || e.Key == Key.Left)
                    this.MovePlayer(Player, Convert.ToInt32(CurrntStateCol * RectWidth), "Left");
                else if (e.Key == Key.Up || e.Key == Key.Down)
                    this.MovePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");
                if (CurrntStateRow == GoalStateRow && CurrntStateCol == GoalStateCol)
                {
                    this.RemoveListenToKeyBoard();
                    if (((MazeBoard)sender).Name == "myMazeBoard")
                        MessageBox.Show("you won");
                    else
                        MessageBox.Show("you lose");                                
                }
            }
            catch { }
            if(((MazeBoard)sender).Name == "myMazeBoard")
                e.Handled = true;
        }


        /// <summary>
        /// move player
        /// </summary>
        /// <param name="player">player to move</param>
        /// <param name="location">location to move</param>
        /// <param name="setDirection">Left or Top for Canvas.SetLeft/Canvas.Top</param>
        public void MovePlayer(UIElement player, int location, string setDirection)
        {
            if(setDirection=="Left")
                Canvas.SetLeft(player, location);
            else if(setDirection=="Top")
                Canvas.SetTop(player, location);
        }

        /// <summary>
        /// animation solve.
        /// </summary>
        public void AnimationSolve()
        {
            
            this.RemoveListenToKeyBoard();
            this.DisableButtons();
            this.ResetCurrentState();        
            Task animation = new Task((stateObj) => 
            {
                var paramsArr = (object[])stateObj;
                string MazeSolve = (string)paramsArr[0];
                int CurrntStateRow=(int)paramsArr[1];
                int CurrntStateCol = (int)paramsArr[2];
                int GoalStateRow = (int)paramsArr[3];
                int GoalStateCol = (int)paramsArr[4];
                double RectHeight = (double)paramsArr[5];
                double RectWidth=(double)paramsArr[6];
                UIElement Player=(UIElement)paramsArr[7];
                int InitialStateRow=(int)paramsArr[8];
                int InitialStateCol = (int)paramsArr[9];
                for (int i = 0; i < MazeSolve.Length; i++)
                {
                    char c = (Char)MazeSolve[i];
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        DoubleAnimation da = new DoubleAnimation();
                        da.Duration = new Duration(TimeSpan.FromSeconds(1));
                        if (c == '1' || c == '0')
                        {
                            da.From = CurrntStateCol * RectWidth;
                            if (c == '1')
                                CurrntStateCol += 1;
                            else if (c == '0')
                                CurrntStateCol -= 1;
                            da.To = CurrntStateCol * RectWidth;
                            Player.BeginAnimation(Canvas.LeftProperty, da);
                        }
                        else if (c == '2' || c == '3')
                        {
                            da.From = CurrntStateRow * RectHeight;
                            if (c == '2')
                                CurrntStateRow -= 1;
                            else if (c == '3')
                                CurrntStateRow += 1;
                            da.To = CurrntStateRow * RectHeight;
                            Player.BeginAnimation(Canvas.TopProperty, da);
                        }
                    }));
                    Thread.Sleep(1000);                   
                }
                MessageBox.Show("show soultion path ended");
                this.Dispatcher.Invoke((Action)(() =>
                {
                    
                 }));
             },
            new object[] { MazeSolve, CurrntStateRow, CurrntStateCol, GoalStateRow, GoalStateCol, RectHeight, RectWidth, Player, InitialStateRow, InitialStateCol});
            animation.Start();
            this.EnableButtons();
            
        }

        /// <summary>
        /// show window.
        /// </summary>
        public void ShowWindow()
        {
            if(ModePlay=="Multi" && MultiWindow!=null)
            {
                MultiWindow.CloseSpinner();
            }
            Window window = Window.GetWindow(this);
            if (window != null)
                window.Show();
        }

        /// <summary>
        /// add listen to key board
        /// </summary>
        public void ListenToKeyBoard()
        {
            if (ModePlay == "Single" && SingleWindow!=null)
                SingleWindow.AddListenToKeyBoard();
            else if(ModePlay=="Multi" && MultiWindow!=null)
                MultiWindow.AddListenToKeyBoard();                     
        }

        /// <summary>
        /// remove listen to key board
        /// </summary>
        public void RemoveListenToKeyBoard()
        {
            if (ModePlay == "Single" && SingleWindow != null)
                SingleWindow.RemoveAddListenToKeyBoard();           
            else if (ModePlay == "Multi" && MultiWindow != null)
                MultiWindow.RemoveAddListenToKeyBoard();           
        }

        public void RivalMove()
        {
            if (ModePlay == "Multi" && MultiWindow != null)
                MultiWindow.RivalMove(Move);           
        }

        public void EnableButtons()
        {
            if (ModePlay == "Single" && SingleWindow != null)
                SingleWindow.EnableButtons();           
        }

        public void DisableButtons()
        {
            if (ModePlay == "Single" && SingleWindow != null)
                SingleWindow.DisableButtons();           
        }

        public void CloseThisGame()
        {
            if (CloseGame == true)
            {
                if (ModePlay == "Multi" && MultiWindow != null)
                {
                    MultiWindow.Close();
                    MessageWin win = new MessageWin("seconde player exit from game");
                    win.Show();
                }
            }

        }

        public void ServerFailedExist()
        {
            if (ServerFailed == true)
            {
                if (ModePlay == "Single" && SingleWindow != null)
                {
                    SingleWindow.Close();
                    MessageWin win = new MessageWin("connection to server failed");
                    win.Show();                    
                }
                if (ModePlay == "Multi" && MultiWindow != null)
                {
                    MultiWindow.Close();
                    MessageWin win = new MessageWin("connection to server failed");
                    win.Show();                                           
                }
            }        
        }
    }
}
