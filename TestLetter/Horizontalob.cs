using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestLetter
{
    public class Horizontalob : obstacle, IGameEntity
    {
    //setting for how many px per sec it moves at
        private int speed;
        public Horizontalob()
        {
            this.Left = 800;
            this.Top = 0;
            this.Width= 50;
            this.Height=800;
            this.BackColor = Color.Blue;
            this.DoubleBuffered = true;
            Random random = new Random();
            //

            //this.Left = x;

        }

        public override void Update(Panel c)
        {
            // Call the method MoveGameEntity, passing left direction as an argument
            MoveGameEntity(Direction.left);
            if (Left < -100)
            {
                Random random = new Random();
                this.speed = -5;
            }
            MoveGameEntity(Direction.left);
            if (Right > 800)
            {
                Random random = new Random();
                this.speed = 5;
            }

            // If the current Left property of the apple is smaller than -50 remove it from the panel c

        }
        public  void MoveGameEntity(Direction direction)
        {
            //if (direction == Direction.left) { 
            this.Left -= speed;
            //}
            // Decrease the Left property by the value of speed
        }
   
        }
    }

