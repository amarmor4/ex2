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

        private int InitialStateRow {get { return Int32.Parse(InitialState[3].ToString()); }}

        private int InitialStateCol { get { return Int32.Parse(InitialState[1].ToString()); }}

        private int GoalStateRow { get { return Int32.Parse(GoalState[3].ToString()); }}

        private int GoalStateCol { get { return Int32.Parse(GoalState[1].ToString()); } }

        public void DrawMaze()
        {
            double rowHeight = 300/Rows;
            double colWidth = 300/Cols;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (MazePath[i * Cols + j] == '1' || InitialStateRow == i && InitialStateCol == j || GoalStateRow == i && GoalStateCol == j)
                    {
                        int col = Convert.ToInt32(j * colWidth);
                        int row = Convert.ToInt32(i * rowHeight);
                        if (MazePath[i * Cols + j] == '1')
                            AddRectToBoard(col, row, rowHeight, colWidth);
                        if (InitialStateRow == i && InitialStateCol == j)
                            AddImageRectToBoard(col, row, rowHeight, colWidth, "chicken_bride_right.png");
                        if (GoalStateRow == i && GoalStateCol == j)
                            AddImageRectToBoard(col, row, rowHeight, colWidth, "ring.png");
                    }
                }
            }
        }

        public void AddImageRectToBoard(int col, int row, double height, double width, string file)
        {
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = new BitmapImage(new Uri(@file, UriKind.Relative));
            Rectangle rect = new Rectangle();
            rect.Height = height;
            rect.Width = width;
            rect.Fill = imgBrush;
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
            myCanvas.Children.Add(rect);
        }
    }
}
