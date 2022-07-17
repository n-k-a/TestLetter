using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestLetter
{
    public class Verticalob : obstacle, IGameEntity
    {
        //setting for how many px per sec it moves at
        private int speed;
        public Verticalob()
        {
            this.Left = 0;
            this.Top = -100;
            this.Width = 786;
            this.Height = 50;
            this.BackColor = Color.Blue;
            this.DoubleBuffered = true;
            Random random = new Random();


            //this.Left = x;

        }

        public override void Update(Panel c)
        {
            // Call the method MoveGameEntity, passing left direction as an argument
            MoveGameEntity(Direction.down);
            if (Top < -50)
            {
                // If the current top property of the apple is smaller than -50 reverse it's direction

                this.speed = 5;
            }
            MoveGameEntity(Direction.down);

            if (Bottom > 350)

            {            // If the current bottom property of the apple is bigger than 350 than reverse its directon

                this.speed = -5;
            }


        }
        public void MoveGameEntity(Direction direction)
        {
            //if (direction == Direction.left) { 
            this.Top += speed;
            //}
            // Decrease the Left property by the value of speed
        }

    }
}

