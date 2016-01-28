using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMK.IT.IIO11300
{
    public class Lotto
    {
        #region Variables
        private string selectedGame;
        private int totalNumbers;
        private int totalSpecialNumbers;
        private int numbersToDraw;
        private int totalDraws;
        private int specialNumberMaxRange;
        #endregion
        #region Properties
        public string SelectedGame
        {
            get
            {
                return selectedGame;
            }
        }
        public int TotalNumbers
        {
            get
            {
                return totalNumbers;
            }
        }
        public int TotalSpecialNumbers
        {
            get
            {
                return totalSpecialNumbers;
            }
        }
        public int TotalDraws
        {
            get
            {
                return totalDraws;
            }
        }
        #endregion
        #region Methods
        public void SetupGame(string game, int draws)
        {
            switch(game)
            {
                case "Lotto":
                    selectedGame = "Lotto";
                    totalNumbers = 39;
                    totalSpecialNumbers = 2;
                    numbersToDraw = 7;
                    specialNumberMaxRange = TotalNumbers;
                    totalDraws = draws;
                    break;
                case "Viking Lotto":
                    selectedGame = "Viking Lotto";
                    totalNumbers = 48;
                    totalSpecialNumbers = 0;
                    numbersToDraw = 6;
                    totalDraws = draws;
                    break;
                case "EuroJackpot":
                    selectedGame = "EuroJackpot";
                    totalNumbers = 50;
                    totalSpecialNumbers = 2;
                    numbersToDraw = 5;
                    specialNumberMaxRange = 8;
                    totalDraws = draws;
                    break;
                default:
                    break;
            }
        }
        public int[] GetPrimaryNumbers()
        {
            Random random = new Random();
            int[] numbers = new int[numbersToDraw];
            int number;
            for(int i = 0; i < numbersToDraw; i++)
            {
                number = random.Next(1, totalNumbers + 1);
                if(!numbers.Contains(number))
                {
                    numbers[i] = number;
                }
                else
                {
                    i--;
                }
            }
            return numbers;
        }
        public int[] GetSpecialNumbers(int[] primaryNumbers)
        {
            if (selectedGame != "Viking Lotto")
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                int[] numbers = new int[totalSpecialNumbers];
                int number;
                for (int i = 0; i < totalSpecialNumbers; i++)
                {
                    number = random.Next(1, specialNumberMaxRange + 1);
                    if (!numbers.Contains(number) && !primaryNumbers.Contains(number))
                    {
                        numbers[i] = number;
                    }
                    else
                    {
                        i--;
                    }
                }
                return numbers;
            }
            else
            {
                return new int[] { 0, 0 };
            }
            
        }  
        #endregion
    }

    class BLLotto
    {

    }
}
