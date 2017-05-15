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
            DependencyProperty.Register("MazePath", typeof(string), typeof(MazeBoard));

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
            DependencyProperty.Register("MazeSolve", typeof(string), typeof(MazeBoard));

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
            this.DrawIcons(InitialStateCol, InitialStateRow, RectHeight, RectWidth, "chicken_bride_right.png");
            this.DrawIcons(GoalStateCol, GoalStateRow, RectHeight, RectWidth, "ring.png");
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
                        this.AddRectToBoard(col, row, height, width);
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
        private void DrawIcons(int stateCol,int stateRow,double height, double width, string file)
        {
            int col = Convert.ToInt32(stateCol * width);
            int row = Convert.ToInt32(stateRow * height);
            this.AddImageRectToBoard(col, row, height, width, file);
        }

        /// <summary>
        /// add image rect to board
        /// </summary>
        /// <param name="col">col</param>
        /// <param name="row">row</param>
        /// <param name="height">rect height</param>
        /// <param name="width">rect width</param>
        /// <param name="file">image location</param>
        public void AddImageRectToBoard(int col, int row, double height, double width, string file)
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(@file, UriKind.Relative));
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill = imgBrush;
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
        private void AddRectToBoard(int col, int row, double height, double width)
        {
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill= new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(rect, col);
            Canvas.SetTop(rect, row);
            myCanvas.Children.Add(rect);
        }

        /// <summary>
        /// reset player location from current state to inital state
        /// </summary>
        public void ResetCurrentState()
        {
            if (!myCanvas.Children.Contains(Player))
                myCanvas.Children.Add(Player);
            CurrntStateRow = InitialStateRow;
            CurrntStateCol = InitialStateCol;
            this.movePlayer(Player, Convert.ToInt32(CurrntStateCol * RectWidth), "Left");
            this.movePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");  
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
                        if (MazePath[CurrntStateRow * Cols + CurrntStateCol + 1] == '0')
                            CurrntStateCol += 1;   
                        break;
                    case Key.Left:
                        if (MazePath[CurrntStateRow * Cols + CurrntStateCol - 1] == '0')
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

                if(CurrntStateRow==GoalStateRow && CurrntStateCol==GoalStateCol)
                {
                    MessageBox.Show("winner");
                    myCanvas.Children.Remove(Player);
                }
                    
                if (e.Key== Key.Right || e.Key== Key.Left)
                    this.movePlayer(Player, Convert.ToInt32(CurrntStateCol * RectWidth), "Left");
                if (e.Key == Key.Up || e.Key == Key.Down)
                    this.movePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");  
            }
            catch { }
            e.Handled = true;
        }

        /// <summary>
        /// move player
        /// </summary>
        /// <param name="player">player to move</param>
        /// <param name="location">location to move</param>
        /// <param name="setDirection">Left or Top for Canvas.SetLeft/Canvas.Top</param>
        public void movePlayer(UIElement player, int location, string setDirection)
        {
            if(setDirection=="Left")
                Canvas.SetLeft(player, location);
            else if(setDirection=="Top")
                Canvas.SetTop(player, location);
        }

        public void test()
        {
            MessageBox.Show("solve test");
        }
    }
}
