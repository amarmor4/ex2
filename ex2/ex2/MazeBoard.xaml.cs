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

        private int InitialStateRow{ get{ return Int32.Parse(InitialState.Split(',')[1]); }}

        private int InitialStateCol { get { return Int32.Parse(InitialState.Split(',')[0]); } }

        private int GoalStateRow { get { return Int32.Parse(GoalState.Split(',')[1]); }}

        private int GoalStateCol { get { return Int32.Parse(GoalState.Split(',')[0]); } }

        public void DrawMaze()
        {
            double rowHeight = 300/Rows;
            double colWidth = 300/Cols;

            this.DrawWalls(rowHeight, colWidth);
            this.DrawIcons(InitialStateCol, InitialStateRow, rowHeight, colWidth, "chicken_bride_right.png", "current");
            this.DrawIcons(GoalStateCol, GoalStateRow, rowHeight, colWidth, "ring.png", "goal");
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
                        if (MazePath[i * Cols + j] == '1')
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
            Canvas.SetLeft(rect, row);
            Canvas.SetTop(rect, col);
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
        }

        public void KeyBoardDown(object sender, KeyEventArgs e)
        {
            switch(e.Key)
            {
                case Key.Right:

                    break;
            }
        }
    }
}
