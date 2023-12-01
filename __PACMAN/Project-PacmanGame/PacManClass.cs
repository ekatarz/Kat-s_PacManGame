using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class PacManClass
    {
      //  private List<PictureBox> ForageList;
        private string[] directionsArray = { "Right", "Left", "Up", "Down" };
        private Random rnd = new Random();
        private string direction = "Right";
        private int score, lives = 3;
        //private Ghost ghost1, ghost2, ghost3;
        private Timer timerPacmanDirection;
        private Timer timerGhostDirection;
        private PictureBox pbPacman;
        private Label lblScore;
        private PictureBox pblives1, pblives2, pblives3;
        private ImageList imageList1;


        public PacManClass(
            List<PictureBox> forageList,
            Timer timerPacmanDirection,
            Timer timerGhostDirection,
            PictureBox pbPacman,
            Label lblScore,
            PictureBox pblives1,
            PictureBox pblives2,
            PictureBox pblives3,
            ImageList imageList1,
            PictureBox ghost1PictureBox,
            PictureBox ghost2PictureBox,
            PictureBox ghost3PictureBox)
        {
            
            this.timerPacmanDirection = timerPacmanDirection;
            this.timerGhostDirection = timerGhostDirection;
            this.pbPacman = pbPacman;
            this.lblScore = lblScore;
            this.pblives1 = pblives1;
            this.pblives2 = pblives2;
            this.pblives3 = pblives3;
            this.imageList1 = imageList1;

            /*ghost1 = new Ghost(ghost1PictureBox);
            ghost2 = new Ghost(ghost2PictureBox);
            ghost3 = new Ghost(ghost3PictureBox);*/
        }

        public PacManClass(PictureBox pbPacman, Label lblScore, PictureBox pblives1, PictureBox pblives2, PictureBox pblives3, ImageList imageList1)
        {
            this.pbPacman = pbPacman;
            this.lblScore = lblScore;
            this.pblives1 = pblives1;
            this.pblives2 = pblives2;
            this.pblives3 = pblives3;
            this.imageList1 = imageList1;
        }


        public void GoToReturn()
        {
            lives--;

            if (lives == 2)
            {
                pblives1.Visible = false;
            }
            else if (lives == 1)
            {
                pblives2.Visible = false;
            }
            else if (lives == 0)
            {
                pblives3.Visible = false;
                timerPacmanDirection.Stop();
                timerGhostDirection.Stop();
                HandleGameOver();
            }

            pbPacman.Location = new Point(37, 37);
            pbPacman.BackgroundImage = imageList1.Images[0];
            pbPacman.BackColor = Color.Transparent;
            direction = "Right";
        }

        public void ResetEverything()
        {
            lives = 3;
            pblives1.Visible = true;
            pblives2.Visible = true;
            pblives3.Visible = true;

         
           
            timerPacmanDirection.Start();

            timerGhostDirection.Start();

          /*  ghost1.ResetLocation(new Point(390, 240));
            ghost3.ResetLocation(new Point(490, 240));
            ghost2.ResetLocation(new Point(590, 240));

            ghost1.BringToFront();
            ghost2.BringToFront();
            ghost3.BringToFront();*/

            score = 0;
            lblScore.Text = 0.ToString();
        }



        public void HandleForageCollision(PictureBox forage)
        {
            if (pbPacman.Bounds.IntersectsWith(forage.Bounds))
            {
                // Handle collision logic for the forage element
                // For example, removing the forage from the form or updating the score
              
                // You can still update the score here if needed
            }
        }


        // In PacManClass.cs
        public void HandleGhostCollision(Ghost ghost)
        {
            if ((pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left))
            {
                GoToReturn();
            }
        }




        private void HandleGameOver()
        {
            DialogResult result = MessageBox.Show("Your score=> " + score + "\n Want to Play Again?", "Pacman", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                ResetEverything();
            }
            else if (result == DialogResult.Cancel)
            {
                ApplicationExit();
            }
        }
        // In PacManClass.cs
        public void HandleKeyPress(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    direction = "Up";
                    break;
                case Keys.Down:
                    direction = "Down";
                    break;
                case Keys.Left:
                    direction = "Left";
                    break;
                case Keys.Right:
                    direction = "Right";
                    break;
                default:
                    break;
            }
        }



        private void ApplicationExit()
        {
            Application.Exit();
        }
    }
}