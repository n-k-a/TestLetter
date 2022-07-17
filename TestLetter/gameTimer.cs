using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLetter
{
    class gameTimer
    {
        int timerCount;
        public gameTimer(Mode difficulty)
        {
      
            timerCount = NewMethod(difficulty);
        }

        private static int NewMethod(Mode difficulty)
        {
            int i = 0;
            switch (difficulty)
            {
                case Mode.EASY:
                    i = 180;
                    break;
                case Mode.NORMAL:
                    i = 120;
                    break;
                case Mode.HARD:
                    i= 60;
                    break;
                 default:
                    break;

            }
            return i;
        }
    }
}
