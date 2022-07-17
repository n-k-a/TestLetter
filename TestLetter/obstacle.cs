using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestLetter
{
    public abstract class obstacle : PictureBox
    {
        //speed value
        private int speed = 5;

        //method to tell th
        public abstract void Update(Panel c)
;       
       
    }
}
