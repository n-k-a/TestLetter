using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TestLetter
{
    class letterObj :PictureBox
    {
        private string name;
        private int alphabetindex;

        public letterObj(int alphabetindex) {
            this.alphabetindex = alphabetindex;
            name = ((Letters)alphabetindex).ToString();
            this.Height = 30;
            this.Width = 30;


            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Image.FromFile(getImage(alphabetindex));
                     this.BackColor = Color.Transparent;
        }


        /*public letterObj()
        {
            alphabetindex = GetRandom();
            name = ((Letters)alphabetindex).ToString();
            createLetterPicbox();
        }

    */
        public string getName() {
            return name;
        }
        public void setName(string n) {
            n = name;
        }
        public int getalphin() {
            return alphabetindex;
        }

        public int setAlphIn(int a) {
            alphabetindex = a;
            return alphabetindex;

        }


        public string getImage(int alphabetIndex)
        {
            int index = alphabetindex;
            string letter = ((Letters)index).ToString();
            return (@"imageobjects\Letters\" + letter + ".png");

        }
}
}
