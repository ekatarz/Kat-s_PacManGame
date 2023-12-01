using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public partial class FormPacman : Form
    {
        private const int NumberOfGhosts = 5;
        private List<PictureBox> ForageList = new List<PictureBox>();
        private List<PictureBox> ghosts = new List<PictureBox>();
        private string[] directionsArray = { "Right", "Left", "Up", "Down" };
        private Random rnd = new Random();
        private string direction = "Right";
        private int score, lives = 3;

        private List<string> ghostResourceNames = new List<string>
        {
            "RedGhost.png",
            "GhostImage2"
            // Add other resource names as needed
        };


        public FormPacman()
        {
            
            InitializeComponent();
            RepeatGhostImages();
            CreateGhosts();
            // Creating a new instance of the PacMan class,
            // assigning it to the pacMan variable
            // pacMan = new PacMan(ForageList, timerPacmanDirection, timerGhostDirection);
            // parameters passed to the PacMan constructor
        } // queue - score board 
          // add levels? 

        public void Win()
        {
            timerPacmanDirection.Stop();
            timerGhostDirection.Stop();
            DialogResult result = MessageBox.Show("Congratulations!" + "\n Want to play again?", "Pacman", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                ResetEverything();
            }
            else if (result == DialogResult.Cancel)
            {
                ApplicationExit();
            }

            pbPacman.Location = new Point(37, 37);
            pbPacman.BackgroundImage = imageList1.Images[0];
            pbPacman.BackColor = Color.Transparent;
            direction = "Right";
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

            pbPacman.Location = new Point(37, 37);
            pbPacman.BackgroundImage = imageList1.Images[0];
            pbPacman.BackColor = Color.Transparent;
            direction = "Right";
        }
       





        public void ApplicationExit()
        {
            Application.Exit();
        }

        public void ResetEverything()
        {
            // Reset ghosts
            foreach (var ghost in ghosts)
            {
                this.Controls.Remove(ghost);
            }
            ghosts.Clear();
            CreateGhosts();

            // Reset other elements
            for (int i = 0; i < 3; i++)
            {
                ghosts[i].Location = new Point(390 + i * 100, 240); // Adjust the initial position as needed
                ghosts[i].BringToFront();
            }

            lives = 3;
            pblives1.Visible = true;
            pblives2.Visible = true;
            pblives3.Visible = true;

            for (int i = 0; i < 210; i++)
            {
                this.Controls.Remove(ForageList[i]);
            }
            ForageList.Clear();

            timerPacmanDirection.Start();
            CreateForage();
            timerGhostDirection.Start();

            score = 0;
            lblScore.Text = 0.ToString();
        }


        public void ResetGhosts()
        {
            foreach (var ghost in ghosts)
            {
                this.Controls.Remove(ghost);
            }
            ghosts.Clear();
            CreateGhosts();
        }


        public void CreateForage() //creates a grid of dots on the form
        {
            for (int i = 50; i <= 1050; i += 50)
            {
                for (int j = 50; j <= 500; j += 50)
                {
                    PictureBox forage = new PictureBox();

                    forage.Name = "forage" + i + j;
                    forage.Size = new Size(20, 20);
                    forage.Location = new Point(i, j);
                    forage.BackgroundImageLayout = ImageLayout.Stretch;
                    forage.BackColor = Color.Transparent;
                    forage.BackgroundImage = Properties.Resources.Forage;
                    forage.BackgroundImageLayout = ImageLayout.Stretch;
                    ForageList.Add(forage);
                    this.Controls.Add(forage);
                }
            }
            ForageList[0].Visible = false;
            this.Controls.Remove(ForageList[0]);
            this.Controls.Remove(ForageList[65]);
            this.Controls.Remove(ForageList[64]);
            this.Controls.Remove(ForageList[63]);
            this.Controls.Remove(ForageList[75]);
            this.Controls.Remove(ForageList[85]);
            this.Controls.Remove(ForageList[95]);
            this.Controls.Remove(ForageList[105]);
            this.Controls.Remove(ForageList[115]);
            this.Controls.Remove(ForageList[125]);
            this.Controls.Remove(ForageList[124]);
            this.Controls.Remove(ForageList[123]);
        }

        public void CreateWall() //creates blue walls around the game area and adds another set of walls inside
        {
            for (int i = 0; i <= 1100; i += 20)
            {
                for (int j = 0; j <= 540; j += 20)
                {
                    if (i == 0 || j == 0 || i == 1100 || j == 540)
                    {
                        Label wall = new Label();
                        wall.Name = "Wall" + i + j;
                        wall.Size = new Size(15, 15);
                        wall.Location = new Point(i, j);
                        wall.BackColor = Color.Blue;
                        wall.BringToFront();
                        this.Controls.Add(wall);
                    }

                    if (((i >= 340 && i <= 360) && (j >= 200 && j <= 300)) || ((j >= 300 && j <= 320) && (i >= 340 && i <= 660)) || ((i >= 640 && i <= 660) && (j <= 300 && j >= 200)))
                    {
                        Label wall2 = new Label();
                        wall2.Name = "Wall2" + i + j;
                        wall2.Size = new Size(15, 15);
                        wall2.Location = new Point(i, j);
                        wall2.BackColor = Color.Blue;
                        wall2.BringToFront();
                        this.Controls.Add(wall2);
                    }

                }
            }

        }

        public void CreateGhosts()
        {
            for (int i = 0; i < NumberOfGhosts; i++)
            {
                PictureBox ghost = new PictureBox();
                ghost.Location = new Point(390 + i * 100, 240);
                ghost.BackgroundImage = imageListGhost.Images[i % imageListGhost.Images.Count];
                ghost.BackgroundImageLayout = ImageLayout.Zoom;
                ghost.Size = new Size(45, 45);
                ghost.BackColor = Color.Transparent;
                ghost.BringToFront();
               
                //ghosts.Add(ghost);
                //this.Controls.Add(ghost);
            }
        }


        private void FormPacman_Load(object sender, EventArgs e)
        {
            timerPacmanDirection.Start();
            CreateForage();
            CreateWall();
            InitializeGhosts(); // Modified this line
           
            timerGhostDirection.Start();
        }



        private void timerPacmanDirection_Tick(object sender, EventArgs e)
        {
            timerPacmanDirection.Interval = 100;
            Movement();
            lblScore.Text = score.ToString();
        }


        public void UpdateGhostDirections()
        {
            for (int i = 0; i < ghosts.Count; i++)
            {
                string ghostMove = directionsArray[rnd.Next(0, 4)];
                GhostMove(ghostMove, ghosts[i]);
            }
        }

        private void FormPacman_KeyDown(object sender, KeyEventArgs e)//triggered when a key is pressed. sets the direction of Pacman based on the pressed key
        {

            if (e.KeyCode == Keys.Up)
            {
                direction = "Up";
                pbPacman.BackgroundImage = imageList1.Images[3];
                pbPacman.BackColor = Color.Transparent;
            }
            else if (e.KeyCode == Keys.Down)
            {
                direction = "Down";
                pbPacman.BackgroundImage = imageList1.Images[2];
                pbPacman.BackgroundImage = imageList1.Images[2];
                pbPacman.BackColor = Color.Transparent;
            }
            else if (e.KeyCode == Keys.Right)
            {
                direction = "Right";
                pbPacman.BackgroundImage = imageList1.Images[0];
                pbPacman.BackColor = Color.Transparent;
            }
            else if (e.KeyCode == Keys.Left)
            {
                direction = "Left";
                pbPacman.BackgroundImage = imageList1.Images[1];
                pbPacman.BackColor = Color.Transparent;
            }
        }

        public void Movement()
        {
            if (score == 198)
            {
                Win();
            }

            if (direction == "Up")
            {
                if (50 <= pbPacman.Top && !(pbPacman.Top == 337 && (pbPacman.Left == 337 || pbPacman.Left == 387 || pbPacman.Left == 437 || pbPacman.Left == 487 || pbPacman.Left == 537 || pbPacman.Left == 587 || pbPacman.Left == 637)))
                {
                    pbPacman.Top -= 50;

                    // Check collisions with forage
                    foreach (var item in ForageList)
                    {
                        if (pbPacman.Top < item.Bottom && pbPacman.Bottom > item.Top && pbPacman.Left <= item.Right && pbPacman.Right >= item.Left)
                        {
                            if (item.Visible)
                            {
                                score++;
                                Controls.Remove(item);
                                item.Visible = false;
                            }
                        }
                    }

                    // Check collisions with ghosts
                    foreach (var ghost in ghosts)
                    {
                        if (pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left)
                        {
                            GoToReturn();
                        }
                    }
                }
            }
            else if (direction == "Down")
            {
                if (this.ClientSize.Height - 30 >= pbPacman.Bottom && !(pbPacman.Top == 137 && (pbPacman.Left == 637 && pbPacman.Right == 687)) && !(pbPacman.Top == 137 && (pbPacman.Left == 337 && pbPacman.Right == 387)) && !(pbPacman.Top == 237 && (pbPacman.Left == 387 || pbPacman.Left == 437 || pbPacman.Left == 487 || pbPacman.Left == 537 || pbPacman.Left == 587)))
                {
                    pbPacman.Top += 50;

                    // Check collisions with forage
                    foreach (var item in ForageList)
                    {
                        if (pbPacman.Top < item.Bottom && pbPacman.Bottom > item.Top && pbPacman.Left <= item.Right && pbPacman.Right >= item.Left)
                        {
                            if (item.Visible)
                            {
                                score++;
                                Controls.Remove(item);
                                item.Visible = false;
                            }
                        }
                    }

                    // Check collisions with ghosts
                    foreach (var ghost in ghosts)
                    {
                        if (pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left)
                        {
                            GoToReturn();
                        }
                    }
                }
            }
            else if (direction == "Right")
            {
                if (1050 >= pbPacman.Right && !(pbPacman.Right == 337 && (pbPacman.Top == 187 || pbPacman.Top == 237 || pbPacman.Top == 287)) && !(pbPacman.Right == 637 && (pbPacman.Top == 187 || pbPacman.Top == 237)))
                {
                    pbPacman.Left += 50;

                    // Check collisions with forage
                    foreach (var item in ForageList)
                    {
                        if (pbPacman.Top <= item.Bottom && pbPacman.Bottom >= item.Top && pbPacman.Left < item.Right && pbPacman.Right > item.Left)
                        {
                            if (item.Visible)
                            {
                                score++;
                                Controls.Remove(item);
                                item.Visible = false;
                            }
                        }
                    }

                    // Check collisions with ghosts
                    foreach (var ghost in ghosts)
                    {
                        if (pbPacman.Top <= ghost.Bottom && pbPacman.Bottom >= ghost.Top && pbPacman.Left < ghost.Right && pbPacman.Right > ghost.Left)
                        {
                            GoToReturn();
                        }
                    }


                }
            }
            else if (direction == "Left")
            {
                if (50 <= pbPacman.Left && !(pbPacman.Right == 737 && (pbPacman.Top == 187 || pbPacman.Top == 237 || pbPacman.Top == 287)) && !(pbPacman.Right == 437 && (pbPacman.Top == 187 || pbPacman.Top == 237)))
                {
                    pbPacman.Left -= 50;

                    // Check collisions with forage
                    foreach (var item in ForageList)
                    {
                        if (pbPacman.Top <= item.Bottom && pbPacman.Bottom >= item.Top && pbPacman.Left < item.Right && pbPacman.Right > item.Left)
                        {
                            if (item.Visible)
                            {
                                score++;
                                Controls.Remove(item);
                                item.Visible = false;
                            }
                        }
                    }

                    // Check collisions with ghosts
                    foreach (var ghost in ghosts)
                    {
                        if (pbPacman.Top <= ghost.Bottom && pbPacman.Bottom >= ghost.Top && pbPacman.Left < ghost.Right && pbPacman.Right > ghost.Left)
                        {
                            GoToReturn();
                        }
                    }

                }
            }
            foreach (var ghost in ghosts)
            {
                ghost.BringToFront();
            }
        }


        private void timerGhostDirection_Tick(object sender, EventArgs e)
        {
            timerGhostDirection.Interval = 200;

            for (int i = 0; i < ghosts.Count; i++)
            {
                string ghostMove = directionsArray[rnd.Next(0, 4)]; // Use 4 since there are four directions
                GhostMove(ghostMove, ghosts[i]);
            }
        }



        private void RepeatGhostImages()
        {
            const int numberOfGhosts = 10;

            if (imageListGhost != null && imageListGhost.Images.Count > 0)
            {
                for (int i = 0; i < numberOfGhosts; i++)
                {
                    PictureBox ghost = new PictureBox();
                    ghost.BackColor = Color.Transparent;
                    ghost.Size = new Size(50, 50);
                    ghost.SizeMode = PictureBoxSizeMode.Zoom;
                    ghost.BackgroundImage = imageListGhost.Images[i % imageListGhost.Images.Count];
                    //this.Controls.Add(ghost);
                }
            }
            else
            {
                Console.WriteLine("Error: imageListGhost is not initialized or has no images.");
            }
        }

        private void InitializeGhosts()
        {
            const int numberOfGhosts = 5; // Update the number of ghosts as needed

            Random random = new Random();

            for (int i = 0; i < numberOfGhosts; i++)
            {
                PictureBox ghost = new PictureBox();
                ghost.BackColor = Color.Transparent;
                ghost.Size = new Size(45, 45);
                ghost.SizeMode = PictureBoxSizeMode.Zoom;

                // Randomly select a ghost resource name
                string selectedResourceName = ghostResourceNames[random.Next(0, ghostResourceNames.Count)];

              

                // Adjust the initial position within the game boundaries
                int initialX = random.Next(50, this.ClientSize.Width - ghost.Width);
                int initialY = random.Next(50, this.ClientSize.Height - ghost.Height);

                ghost.Location = new Point(initialX, initialY);
                ghost.BringToFront();
                ghosts.Add(ghost);
                this.Controls.Add(ghost);
            }
        }

        public void GhostMove(string directionGhost, PictureBox ghost)
        {
            int originalTop = ghost.Top;
            int originalLeft = ghost.Left;
            int ghostSpeed = 50;

            int nextLeft = ghost.Left;
            int nextTop = ghost.Top;

            if (directionGhost == "Up" && ghost.Top > 50)
            {
                nextTop -= ghostSpeed;
            }
            else if (directionGhost == "Down" && ghost.Bottom < this.ClientSize.Height - 30)
            {
                nextTop += ghostSpeed;
            }
            else if (directionGhost == "Right" && ghost.Right < 1050)
            {
                nextLeft += ghostSpeed;
            }
            else if (directionGhost == "Left" && ghost.Left > 50)
            {
                nextLeft -= ghostSpeed;
            }

            // Check collisions with walls
            ghost.Left = nextLeft;
            ghost.Top = nextTop;

            if (CheckCollisionWithWalls(ghost))
            {
                // Restore the original position if there's a collision
                ghost.Top = originalTop;
                ghost.Left = originalLeft;
            }

            // Check collisions with other ghosts
            foreach (var otherGhost in ghosts.Where(g => g != ghost))
            {
                if (ghost.Bounds.IntersectsWith(otherGhost.Bounds))
                {
                    // Restore the original position if there's overlap with another ghost
                    ghost.Top = originalTop;
                    ghost.Left = originalLeft;
                    break;
                }
            }

            // Check collisions with Pacman
            if (pbPacman.Top < ghost.Bottom && pbPacman.Bottom > ghost.Top && pbPacman.Left <= ghost.Right && pbPacman.Right >= ghost.Left)
            {
                GoToReturn();
            }
        }

        private bool CheckCollisionWithWalls(PictureBox character)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Label && control.BackColor == Color.Blue)
                {
                    if (character.Bounds.IntersectsWith(control.Bounds))
                    {
                        return true; // Collision with a wall detected
                    }
                }
            }
            return false; // No collision with walls
        }






    }
}
