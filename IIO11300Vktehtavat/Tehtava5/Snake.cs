using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tehtava5
{
    class Snake
    {
        #region Attributes
        private float headSize = 10f;
        private float speed = 1f;
        private float maxSpeed = 5f;
        private int length = 30;
        private int score = 0;
        private Point startingPoint = new Point(400, 300);
        #endregion
        #region Properties
        public float MaxSpeed
        {
            get
            {
                return maxSpeed;
            }
        }
        public int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }
        public int Length
        {
            get
            {
                return length;
            }
            set
            {
                length = value;
            }
        }
        public float HeadSize
        {
            get
            {
                return headSize;
            }
        }
        public Point StartingPoint
        {
            get
            {
                return startingPoint;
            }
        }
        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
        #endregion
        #region Methods
        public Ellipse addPoint()
        {
            Ellipse point = new Ellipse();
            point.Fill = Brushes.Green;
            point.Width = 10;
            point.Height = 10;
            return point;
        }
        #endregion
    }
}
