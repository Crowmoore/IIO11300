using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class MyWindow
    {
        #region Variables
        private double height;
        private double width;
        //private double area;
        #endregion
        #region Properties
        //read-only properties
        public double Area
        {
            get
            {
                return height * width;
            }
        }
        public float Price
        {
            get
            {
                return CalculatePrice();
            }
        }
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                //tässä kohdassa voisi tarvittaessa tehdä tarkistuksia
                height = value;
            }
        }
        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        #endregion
        #region Constructors
        #endregion
        #region Methods
        public double CalculateArea()
        {
            return height * width;
        }
        private float CalculatePrice()
        {
            //hinta lasketaan työn ja materiaalimenekin ja katteen avulla
            float roi = 100;
            float work = 200;
            float material;
            material = 100 * ((float)width * (float)height / 1000000);
            return (roi + work + material);
        }
        #endregion
    }
    public class IkkunaVE0
    {
        //joskus tehdään näin
        //ei näin
        public double height;
        public double width;
        public double CalculateArea()
        {
            return width * height;
        }

    }
    public class BusinessLogicWindow
    {
        /// <summary>
        /// CalculatePerimeter calculates the perimeter of a window
        /// </summary>
        public static double CalculatePerimeter(double width, double height)
        {
            return (2 * width + 2 * height) / 1000;
        }
        public static double CalculateArea(double width, double height)
        {
            return (width * height) / 1000000;
        }
        public static double CalculateBorderArea(double width, double height, double border)
        {
            return (((2 * border + height) * (2 * border + width) - (height * width)) / 1000000);
        }
    }
}
