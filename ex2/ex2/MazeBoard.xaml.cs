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
        public MazeBoard()
        {
            InitializeComponent();
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        
        public string MazePath
        {
            get { return (string)GetValue(MazePathProperty); }
            set { SetValue(MazePathProperty, value); }
        }
        

        public string InitialState
        {
            get { return (string)GetValue(InitialStateProperty); }
            set {
                SetValue(InitialStateProperty, value);
            }
        }

        public string GoalState
        {
            get { return (string)GetValue(GoalStateProperty); }
            set {
                SetValue(GoalStateProperty, value);
            }
        }

        int currntStateRow;

        public int CurrntStateRow
        {
            get { return this.currntStateRow; }
            set { this.currntStateRow = value; }
        }

        int currentStateCols;

        public int CurrntStateCols
        {
            get { return this.currentStateCols; }
            set { this.currentStateCols = value; }
        }

        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard));

        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard));

        
        public static readonly DependencyProperty MazePathProperty =
            DependencyProperty.Register("MazePath", typeof(string), typeof(MazeBoard));
            

        public static readonly DependencyProperty InitialStateProperty =
            DependencyProperty.Register("InitialState", typeof(string), typeof(MazeBoard));

        public static readonly DependencyProperty GoalStateProperty =
            DependencyProperty.Register("GoalState", typeof(string), typeof(MazeBoard));

        private int InitialStateRow{ get{ return Int32.Parse(InitialState.Split(',')[0]); }}

        private int InitialStateCol { get { return Int32.Parse(InitialState.Split(',')[1]); } }

        private int GoalStateRow { get { return Int32.Parse(GoalState.Split(',')[0]); }}

        private int GoalStateCol { get { return Int32.Parse(GoalState.Split(',')[1]); } }

        private UIElement Player { get; set; }

        private double RectHeight { get { return 300 / Rows; } }

        private double RectWidth { get { return 300 / Cols; } }

        public void DrawMaze()
        {
            this.DrawWalls(RectHeight, RectWidth);
            this.DrawIcons(InitialStateCol, InitialStateRow, RectHeight, RectWidth, "chicken_bride_right.png", "player");
            this.DrawIcons(GoalStateCol, GoalStateRow, RectHeight, RectWidth, "ring.png", "goal");
            foreach (UIElement ue in myCanvas.Children)
                if (((System.Windows.FrameworkElement)ue).Name == "player")
                    Player = ue;   
            ResetCurrentState();
        }

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

        private void DrawIcons(int stateCol,int stateRow,double height, double width, string file, string name)
        {
            int col = Convert.ToInt32(stateCol * width);
            int row = Convert.ToInt32(stateRow * height);
            this.AddImageRectToBoard(col, row, height, width, file, name);
        }

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

        private void AddRectToBoard(int col, int row, double height, double width)
        {
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill= new SolidColorBrush(Colors.Black);
            Canvas.SetLeft(rect, col);
            Canvas.SetTop(rect, row);
            rect.Name = "wall";
            myCanvas.Children.Add(rect);
        }

        public void ResetCurrentState()
        {
            CurrntStateRow = InitialStateRow;
            CurrntStateCols = InitialStateCol;
            this.movePlayer(Player, Convert.ToInt32(CurrntStateCols * RectWidth), "Left");
            this.movePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");
        }

        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            try {
                switch (e.Key)
                {
                    case Key.Right:
                        if (MazePath[CurrntStateRow * Cols + CurrntStateCols + 1] == '0')
                            CurrntStateCols += 1;   
                        break;
                    case Key.Left:
                        if (MazePath[CurrntStateRow * Cols + CurrntStateCols - 1] == '0')
                            CurrntStateCols -= 1;
                        break;
                    case Key.Up:
                        if (MazePath[(CurrntStateRow - 1) * Cols + CurrntStateCols] == '0')
                            CurrntStateRow -= 1;
                        break;
                    case Key.Down:
                        if (MazePath[(CurrntStateRow + 1) * Cols + CurrntStateCols] == '0')
                            CurrntStateRow += 1;
                        break;
                }

                if(e.Key== Key.Right || e.Key== Key.Left)
                    this.movePlayer(Player, Convert.ToInt32(CurrntStateCols * RectWidth), "Left");
                if (e.Key == Key.Up || e.Key == Key.Down)
                    this.movePlayer(Player, Convert.ToInt32(CurrntStateRow * RectHeight), "Top");
                
            }
            catch { }
            e.Handled = true;
        }

        public void movePlayer(UIElement player, int location, string setDirection)
        {
            if(setDirection=="Left")
                Canvas.SetLeft(player, location);
            else if(setDirection=="Top")
                Canvas.SetTop(player, location);
        }
    }
}
