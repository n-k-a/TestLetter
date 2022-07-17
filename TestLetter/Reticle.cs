using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestLetter
{
    public class Reticle : PictureBox, IGameEntity
    {
        
        private int velocity = 10;
        private int x;
        private int y;
        public Reticle(int x1, int y1){
            this.Image = Image.FromFile(@"imageobjects\reticle\reticle_neutral.png");
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            ((Bitmap)this.Image).MakeTransparent(((Bitmap)this.Image).GetPixel(1, 1));
            this.BackColor = Color.Transparent;
            this.Height = 29;
            this.Width = 40;
            x = x1;
            y = y1;
            this.Location = new Point(x, y);
            x = this.Location.X;
            this.DoubleBuffered = true;

        }
        public void MoveGameEntity(Direction direction)
        {
            if (direction == Direction.right)
            {

                this.Location = new Point(x += velocity, y);
            }
            if (direction == Direction.left)
            {

                this.Location = new Point(x -= velocity, y);
            }
            if (direction == Direction.up)
            {

                this.Location = new Point(x, y -= velocity);

            }
            if (direction == Direction.down)
            {

                this.Location = new Point(x, y += velocity);

            }
        }

        public void Update(Panel theCanvas)
        {
            throw new NotImplementedException();
        }
    }
}
