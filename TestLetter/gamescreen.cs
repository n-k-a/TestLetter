using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Odbc;


namespace TestLetter
{
    public partial class gamescreen : Form
    {
        //used for image of cursor
        private Reticle reticle;
        //lists for storing the values used to create letter objects on the gamepanel
        private List<letterObj> letterObjects;
        private List<Word> wordDictionary;
        List<string> selectedword;
        //bool to check if words are loaded into panel
        bool loadedword;
        //passing the selected difficuty over
        Mode difficulty = Form1.difficulty;
        //values set up to be used for either labels on the gamescreen
        int index;
        int basescore;
        int time;
        private static int score;
        string hint;
        int wordscompleted;
        int wrongclick;
        int lettersleft;
        //for selecting wors
        Random r = new Random();
        //list to store obstacle objects
        List<Horizontalob> obstacleH;
        List<Verticalob> obstacleV;
        

        public gamescreen()
        {

            InitializeComponent();
            //initialising variables
            //wrongclick, score and wordscompleted set to zero as game starts
            wrongclick = 0;
            score = 0;
            wordscompleted = 0;
            basescore = 10;
            loadedword = false;
            obstacleH = new List<Horizontalob>();
            obstacleV = new List<Verticalob>();
            selectedword = new List<string>();
            //hiding panel for gameover until criteria for it is reached
            panel1.Hide();
            label4.Hide();
            textBox1.Hide();
            label3.Hide();
            //use the loadDict method to read from db for wor values
            wordDictionary = loadDict();
            //initialising but hiding reticle value used to change cursor
            reticle = new Reticle(0, 0);
            reticle.Hide();
            gamePanel.Controls.Add(reticle);
            gamePanel.BackColor = Color.Transparent;
            //testlist();
            letterObjects = new List<letterObj>();
            lettersleft = selectedword.Count;
            //setting up first word to be searched for
            findrandword();
            //starting timers
            timer1.Start();
            //timer1running = true;
            timer2.Start();
            timer4.Start();
            //creating countdown timer using difficulty settings
            time = setdifftimer();
            //setting up labels to show status in game
            label1.Text = "Time Left: " + time.ToString();
            label2.Text = "Score: " + score.ToString();
            label5.Text = "Words Completed: " + wordscompleted.ToString();
            label6.Text = "Incorrect clicks: " + wrongclick.ToString();
            timer3.Start();

        }


        public void addCorrectLetter()
        {
            int windex;
            Random random = new Random();
            for (int i = 0; i < selectedword.Count; i++)
            {
                string str = selectedword[i];
                Enum.TryParse(selectedword[i], out Letters getletter);
                windex = (int)getletter;
                letterObj c;
                c = new letterObj(windex);

                c.Left = random.Next(0, 756);
                c.Top = random.Next(0, 316);
                gamePanel.Controls.Add(c);
                letterObjects.Add(c);
                letterObjects[i].MouseClick += clickedev;


                Console.WriteLine(c.getName() + c.Left.ToString() + c.Left.ToString());
            }


        }

      
        //
        private void clickedev(object sender, MouseEventArgs e)
        {
            letterObj sc = (letterObj)sender;
            if (letterObjects[index] == (letterObj)sender || letterObjects[index].getName() == sc.getName())
            {
                gamePanel.Controls.Remove(sc);
                textBox2.Text += sc.getName();
                letterObjects.RemoveAt(index);
            }
            else
            {
                //increment wrong click
                wrongclick++;
            }
        }

        public void findrandword()
        {
            int n = r.Next(0, wordDictionary.Count - 1);
            string wordselected = wordDictionary[n].Wordname;
            Console.WriteLine(wordselected);
            hint = wordDictionary[n].Definition;
            foreach (char c in wordselected)
            {
                selectedword.Add(c.ToString());
                Console.WriteLine(c.ToString());

            }

        }
        /* public void removeCorrectLetter() { 
      gamePanel.Controls.Remove(c);
             letterObjects.Remove(c);
 }*/
        //public void saverecord(){}
        //public void showrecord(){} 

        private static OleDbConnection GetConnection()
        {
            string connString;
            connString = @"Provider=Microsoft.ACE.OLEDB.12.0; Data Source=|DataDirectory|\Database31.accdb";
            //  change to your connection string
            return new OleDbConnection(connString);
        }
        
        //creation of a list holding the MSAccess table Dictionaryofword's row values
        private static List<Word> loadDict()
        {
            //initialises a new list to return
            List<Word> readList = new List<Word>();
            //setting up connection, comand and query
            OleDbConnection myConnection = GetConnection();
            string myQuery = "SELECT * FROM Dictionaryofwords";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
            try
            {
                myConnection.Open();
                OleDbDataReader myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                {
                    //when reading throught table, each row is saved to a new Word object which is then saved to the wordlist in this method
                    Word w = new Word(myReader["WordName"].ToString(), myReader["Definition"].ToString());
                    Console.WriteLine(w.Definition + w.Wordname);
                   
                    readList.Add(w);
                }
                //return the list
                return readList;

            }

            catch (Exception ex)
            {
                MessageBox.Show("d" + ex);
                return null;
            }
            finally
            {
                myConnection.Close();
            }

        }
        /* public void testlist()
         {
             string s = "";
             foreach (var item in wordDictionary)
             {
                 s = item.Wordname + item.Definition;
                 Console.WriteLine(s);
                richTextBox2.AppendText(item.Wordname + item.Definition + "\n");
                 richTextBox2.AppendText(s + "\n");
                 richTextBox2.AppendText(hint + "\n");
                 foreach (string st in selectedword) {
                     richTextBox2.AppendText(st + "\n");
                 }


             }
         }*/

        public int setdifftimer()
        {
            //creates an integer to store the timer value in this method
            int i = 0;
            //switch case created based off of the value of difficulty
            switch (difficulty)
            {
                //depending on set enum value, the integer is set to a different value for this count down timer
                case Mode.EASY:
                    i = 30;
                    break;
                case Mode.NORMAL:
                    i = 15;
                    break;
                    
                case Mode.HARD:
                    i = 10;
                    break;
                default:
                    //if nothing, just end loop
                    break;

            }
            //returns selected timer
            return i;

        }
        //adding controls and bringing the obstacles to the gamePane;
        public void Addobs(Horizontalob h, Verticalob v)
        {
            // Add the apple to the list and the canvas
            obstacleH.Add(h);
            gamePanel.Controls.Add(h);
            obstacleV.Add(v);
            gamePanel.Controls.Add(v);
        }
        //unused
        public void Removeobs(Horizontalob h, Verticalob v)
        {
            // Remove the apple from the list and the canvas
            obstacleH.Remove(h);
            gamePanel.Controls.Remove(h);
            obstacleV.Remove(v);
            gamePanel.Controls.Remove(v);
        }
        //timer creates a custom cursor object and checks if objects are loaded
        private void timer1_Tick(object sender, EventArgs e)
        {

            Bitmap img = new Bitmap(new Bitmap(reticle.Image), 30, 30);
            gamePanel.Cursor = new Cursor(img.GetHicon());
            //if loadedword is false
            if (loadedword == false)

            {
                //add letterobjs from current word to the gamePanel
                addCorrectLetter();
                //loadedword is set to true to prevent further spawning of words
                loadedword = true;

            }
            
            //loadedword = true;
            //  while (timer1running == true){

            //}

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            //every time the timer ticks, these labels are refreshed
            label2.Text = "Score: " + score.ToString();
            label5.Text = "Words completed: " + wordscompleted.ToString();
            label1.Text = "Time Left: " + time.ToString();
            label6.Text = "Incorrect clicks: " + wrongclick.ToString();
            //every tick, this value decreases by 1
            time--;
            //if the letterobjects list is empty
            if (letterObjects.Count == 0)
            {
                //remove clear selected word lists and textbox for displaying clicked words
                textBox2.Text = "";
                selectedword.Clear();
                //find a new word from the list of Db values
                findrandword();
                //completed word count is increased by 1
                wordscompleted++;
                //set loadedword to false to put these values as the letterobjs
                loadedword = false;
                //add score for completion
                score += 100;
                //score += (timescore - (basescore * time));
               // score += ((basescore / 2) * time);
               // Console.WriteLine(score.ToString());
               //take away basescore*wrongclick from score
                score -= (basescore * wrongclick);
                Console.WriteLine(score.ToString());
                //reset wrong click as new word is set
                wrongclick = 0;
                Random r = new Random();
                //reset timer
                time = setdifftimer();
                //refresh labels
                label1.Text = "Time Left: " + time.ToString();
                label2.Text = "Score: " + score.ToString();
                label5.Text = "Words Completed: " + wordscompleted.ToString();
                label6.Text = "Incorrect clicks: " + wrongclick.ToString();

            }
            //if the coundown timer is below 0
            if (time < 0)
            {
                //stop all timers and clear words
                timer1.Stop();
                //timer1running = false;
               // label1.Text = "Time left: " + time.ToString();
                timer2.Stop();
                timer3.Stop();
                timer4.Stop();
                wordDictionary.Clear();
                letterObjects.Clear();
                //trigger gameover
                gameover();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            /* for (int i = 0; i < letterObjects.Count; i++)
            foreach (letterObj a in letterObjects)
            {
                letterObjects[i].Update(gamePanel);
                if (DetectCollision(Cursor, a))
                {
                    reticle.Image = Image.FromFile(@"C:\Users\Nkem\Documents\workappdev\imageobjects\reticle\" + "reticle_locked.png");
                    Bitmap img2 = new Bitmap(new Bitmap(reticle.Image), 30, 30);

                    Cursor.Current = new Cursor(img2.GetHicon());
                    //   gamePanel.Cursor = img2.GetHicon();
                }

            }*/

        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            /*gamePanel.BackColor = Color.Red;
            wrongclktime--;
            if (gamePanel.BackColor == Color.Red && wrongclktime == 0)
            {

                gamePanel.BackColor = Color.Transparent;*/
                //create obstacles
            Horizontalob h = new Horizontalob();
            Verticalob v = new Verticalob();

            //add said obstacles through through addobs
            Addobs(h,v);
            //keep using Update to to check if the values have moved
            obstacleH[0].Update(gamePanel);
            obstacleV[0].Update(gamePanel);

            //for all values currently on screen
            for (int i = 0; i < letterObjects.Count; i++)
            {
                //check if they're colliding with the objects
                if ((DetectCollision(obstacleH[0], letterObjects[i]))|| (DetectCollision(obstacleV[0], letterObjects[i])))
                {
                    //force these to hide
                    letterObjects[i].Hide();
                }
                else {
                    //if it's not colliding, still show the letterobjs
                    letterObjects[i].Show();
                }

            }
        }


        public void gameover()
        {
            Point cursor = PointToClient(MousePosition);
            // Cursor.Show();
            //            richTextBox1.Hide();
            //show the panel for gameover to submit
            panel1.Show();
            panel1.Cursor.CopyHandle();
            label4.Show();
            textBox1.Show();
            panel1.Controls.Add(textBox1);
            label3.Show();
            panel1.BackColor = Color.Gray;
            //show final score
            label4.Text = "Final Score: " + score.ToString() + "" + "Words cleared: " + wordscompleted.ToString();
            //wordDictionary.Clear();
            //letterObjects.Clear();
            gamePanel.BackColor = Color.FromArgb(25, Color.DarkGray); ;


        }

        public bool DetectCollision(PictureBox ctrl1, PictureBox ctrl2)
        {
            //draw hitboxes surrounding the objects
            Rectangle ctrl1rect = new Rectangle(ctrl1.Left, ctrl1.Top, ctrl1.Width, ctrl1.Height);
            Rectangle ctrl2rect = new Rectangle(ctrl2.Left, ctrl2.Top, ctrl2.Width, ctrl2.Height);
            ctrl1rect.Intersect(ctrl2rect);
            return !(ctrl1rect == Rectangle.Empty);

            // Detect the collision between ctrl1 and ctrl2 by creating rectangles around them
            // and checking whether the intersection is empty or not
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

        }


        /* if ()
         {
         && selectedword[index] == letterObjects[index].getName() && (DetectCollision(Cursor, letterObjects[index]))
             
             index++;
         }*/



        /*        private void MouseClicked(object sender, EventArgs e)
                {


                    }
                }*/

            //event for if the checkbox has changed
        private void hinttick_CheckedChanged(object sender, EventArgs e)
        {
            //if ticked
            if (hinttick.Checked == true)
            {//show the hints
                Hinttxt.Text = hint;
            }
            else
            {
                //do not show if not ticked
                Hinttxt.Text = "";
            }
        }

        private void gamescreen_Load(object sender, EventArgs e)
        {

        }


        //doesn't work.
        private void submitscore_Click(object sender, EventArgs e)
        {
            List<string> rname = new List<string>();
            List<int> rscore = new List<int>();
            List<int> rwordct = new List<int>();
            string[] scorearr = new string[3];



            scorearr[0] = textBox1.Text;
            scorearr[1] = score.ToString();
            scorearr[2] = wordscompleted.ToString();
            textBox1.Text = textBox1.Text.ToUpper();
            OleDbConnection myConnection = GetConnection();
            string myQuery = "SELECT * FROM Ranking";
            OleDbCommand myCommand = new OleDbCommand(myQuery, myConnection);
            textBox1.Hide();
            myConnection.Open();
            OleDbDataReader myReader = myCommand.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    string rn = (myReader["Name"]).ToString();
                    int rs = (int)myReader["Score"];
                    int rw = (int)myReader["No of completed words"];

                    Console.WriteLine(rn + rs.ToString() + rw.ToString());

                    rname.Add(rn);
                    rscore.Add(rs);
                    rwordct.Add(rw);

                }
            }


            catch (Exception ex)
            {
                MessageBox.Show("d" + ex);
            }
            finally
            {
                myConnection.Close();
            }

            for (int i = 0; i < rname.Count; i++)
            {
                if (rscore[i] < Int32.Parse(scorearr[1]))
                {
                    rname.Insert(i, scorearr[0]);
                    rscore.Insert(i, Int32.Parse(scorearr[1]));
                    rwordct.Insert(i, Int32.Parse(scorearr[2]));
                    rname.RemoveAt(20);
                    rscore.RemoveAt(20);
                    rwordct.RemoveAt(20);
                    break;
                }
                else
                {
                    rname.Insert(19, scorearr[0]);
                    rscore.Insert(19, Int32.Parse(scorearr[1]));
                    rwordct.Insert(19, Int32.Parse(scorearr[2]));
                    rname.RemoveAt(20);
                    rscore.RemoveAt(20);
                    rwordct.RemoveAt(20);
                }
            }

            OleDbConnection myConnection2 = GetConnection();

            {
               
                myConnection2.Open();
                for (int i = 0; i < rname.Count; i++)
                {

                    string myQuery2 = "INSERT INTO Ranking( Name, Score, No of completed words) VALUES (" + rname[i] + "," + rscore[i] + "," + rwordct[i] + ")";
                    OleDbCommand myCommand2 = new OleDbCommand(myQuery2, myConnection2);
                    try
                    {

                        myCommand2.ExecuteNonQuery();
                        Console.WriteLine(rname[i].ToString() + rscore[i].ToString() + rwordct[i].ToString());

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Exception in DBHandler" + ex);
                    }
                    finally
                    {
                        MessageBox.Show("updated rankings");

                    }
                }
                    myConnection2.Close();
                
            }
        }

        private void gamePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}