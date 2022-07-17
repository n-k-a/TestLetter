using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;

namespace TestLetter
{
    public partial class Form1 : Form
    {
        //static enum for difficulty settings
        public static Mode difficulty;
        //reticle for anim
        private Reticle reticle;
        //two counter timers
       private int time1;
        private int time2;

        public Form1()
        {
            InitializeComponent();
            Console.WriteLine("YO " + AppDomain.CurrentDomain.BaseDirectory.ToString());
            //set values
            time1 = 10;
            time2 = 10;
            //set placement and size and form controls for the reticle
            reticle = new Reticle(134, 93);
            reticle.Size = new Size(100, 100);
            this.Controls.Add(reticle);
            //create paint events
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            //start timer for reticle anim
            timer1.Start();
            //hide buttons for values
            easy.Visible = false;
            Medium.Visible = false;
            Hard.Visible = false;
            Console.WriteLine("Test " +Environment.CurrentDirectory);
            Console.WriteLine("Test 2" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase));

        }
        //whens start game is clicked, show the difficulty buttons
        private void button3_Click(object sender, EventArgs e)
        {
            easy.Visible = true;
            Medium.Visible = true;
            Hard.Visible = true;
        }


        //close and quit the game
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        //for each of these different difficulty buttons, 
        private void Hard_Click(object sender, EventArgs e)
        {
            //sets the difficulty enum
            difficulty = Mode.HARD;
            //the current form is closed, 
            this.Hide();
            //the gamescreen opens and is shwon
            Form gamescreen = new gamescreen();
            gamescreen.Show();
        }

        private void Medium_Click(object sender, EventArgs e)
        {
            difficulty = Mode.NORMAL;
            this.Hide();
            Form gamescreen = new gamescreen();
            gamescreen.Show();
        }

        private void easy_Click(object sender, EventArgs e)
        {
            difficulty = Mode.EASY;
            this.Hide();
            Form gamescreen = new gamescreen();
            gamescreen.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Form1_Paint(object sender, PaintEventArgs e){
            //paints the title on the form
		Graphics g = e.Graphics;
            g.DrawString("Spell to Score", new Font("Helvetica", 40, FontStyle.Italic), new SolidBrush(System.Drawing.Color.Red), 248, 124);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //decrease time1
            time1--;
            //if equal to zero, change image to reticle locked
            if (time1 == 0 ) {
                reticle.Image = Image.FromFile(@"C:imageobjects\reticle\" + "reticle_locked.png");
                ((Bitmap)reticle.Image).MakeTransparent(((Bitmap)reticle.Image).GetPixel(1, 1));
                reticle.BackColor = Color.Transparent;
                reticle.Update();
                //start time 2
time2 = 10;               
            }
            //decrease time 2
            time2--;
            //if time2 is equal to zero, change the image back to the neutral reticle
            if (time2 == 0)
            {
                reticle.Image = Image.FromFile(@"imageobjects\reticle\" + "reticle_neutral.png");
                ((Bitmap)reticle.Image).MakeTransparent(((Bitmap)reticle.Image).GetPixel(1, 1));
                reticle.BackColor = Color.Transparent;
                reticle.Update();
                //start time1 again to cause a loop
                time1 = 10;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
