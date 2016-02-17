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
using System.Timers;
using System.Windows.Threading;

namespace Tehtava5
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Snake snake = new Snake();
        List<Point> snakePoints;
        List<Point> applePoints;
        int maxApples = 20;
        bool isPaused = false;
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        Point currentPosition;
        int direction;
        int previousDirection;
        public GameWindow()
        {
            InitializeComponent();
            snakePoints = new List<Point>();
            applePoints = new List<Point>();          
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += new EventHandler(timerTick);
            timer.Start();

            this.KeyDown += new KeyEventHandler(OnButtonKeyDown);
            paintSnake(snake.StartingPoint);
            currentPosition = snake.StartingPoint;
            for(int i = 0; i < maxApples; i++)
            {
                paintApple(i);
            }
        }

        private void paintApple(int index)
        {
            Point applePoint = new Point(random.Next(30, 800), random.Next(30, 600));

            Ellipse apple = new Ellipse();
            apple.Fill = Brushes.Red;
            apple.Width = 10;
            apple.Height = 10;

            Canvas.SetTop(apple, applePoint.Y);
            Canvas.SetLeft(apple, applePoint.X);
            gameCanvas.Children.Insert(index, apple);
            applePoints.Insert(index, applePoint);
        }
        private void timerTick(object sender, EventArgs e)
        {
            switch (direction)
            {
                case 1:
                    currentPosition.X -= snake.Speed;
                    paintSnake(currentPosition);
                    break;
                case 2:
                    currentPosition.Y -= snake.Speed;
                    paintSnake(currentPosition);
                    break;
                case 3:
                    currentPosition.X += snake.Speed;
                    paintSnake(currentPosition);
                    break;
                case 4:
                    currentPosition.Y += snake.Speed;
                    paintSnake(currentPosition);
                    break;  
            }
            if ((currentPosition.X < 5) || (currentPosition.X > 795) ||
                (currentPosition.Y < 5) || (currentPosition.Y > 595))
                GameOver();
            int n = 0;
            foreach(Point point in applePoints)
            {
                if((Math.Abs(point.X - currentPosition.X) < 10) && (Math.Abs(point.Y - currentPosition.Y) < 10))
                {
                    snake.Length += 10;
                    snake.Score += 10;
                    snake.Speed += 0.2f;
                    if(snake.Speed > snake.MaxSpeed)
                    {
                        snake.Speed = snake.MaxSpeed;
                    }
                    applePoints.RemoveAt(n);
                    gameCanvas.Children.RemoveAt(n);
                    paintApple(n);
                    break;
                }
                n++;
                tbScore.Text = "Score: " + snake.Score;
            }
          
            for (int i = 0; i < (snakePoints.Count - 10 * 2); i++)
            {
                Point point = new Point(snakePoints[i].X, snakePoints[i].Y);
                if ((Math.Abs(point.X - currentPosition.X) < (10)) &&
                     (Math.Abs(point.Y - currentPosition.Y) < (10)))
                {
                    GameOver();
                    break;
                }
            }
        }
        private void GameOver()
        {
            timer.Stop();
            tbFinalScore.Text = "Your score is " + snake.Score + "\nESC to quit";
        }
        private void OnButtonKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    if (previousDirection != 2)
                        direction = 4;
                    break;
                case Key.Up:
                    if (previousDirection != 4)
                        direction = 2;
                    break;
                case Key.Left:
                    if (previousDirection != 3)
                        direction = 1;
                    break;
                case Key.Right:
                    if (previousDirection != 1)
                        direction = 3;
                    break;
                case Key.P:
                    if (!isPaused)
                    {
                        timer.Stop();
                        isPaused = true;
                    }
                    else
                    {
                        timer.Start();
                        isPaused = false;
                    }
                    break;
                case Key.Escape:
                    this.Close();
                    break;
            }
            previousDirection = direction;
        }
        private void paintSnake(Point currentPosition)
        {
            Ellipse newPoint = snake.addPoint();
            Canvas.SetTop(newPoint, currentPosition.Y);
            Canvas.SetLeft(newPoint, currentPosition.X);

            int count = gameCanvas.Children.Count;
            gameCanvas.Children.Add(newPoint);
            snakePoints.Add(currentPosition);

            if(count > snake.Length)
            {
                gameCanvas.Children.RemoveAt(count - snake.Length + (maxApples - 1));
                snakePoints.RemoveAt(count - snake.Length);
            }
        }
    }
}
