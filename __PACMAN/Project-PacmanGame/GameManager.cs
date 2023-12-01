using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class GameManager
    {
       
        private string[] directionsArray = { "Right", "Left", "Up", "Down" };
        private Random rnd = new Random();
        private int score, lives = 3;
        private PacManClass pacMan;
        private Ghost ghost1, ghost2, ghost3;
        private Timer timerPacmanDirection;
        private Timer timerGhostDirection;
        private PictureBox pbPacman;
        private Label lblScore;
        private PictureBox pblives1, pblives2, pblives3;
        private ImageList imageList1;

        public GameManager(
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

            pacMan = new PacManClass(pbPacman, lblScore, pblives1, pblives2, pblives3, imageList1);
            ghost1 = new Ghost(ghost1PictureBox);
            ghost2 = new Ghost(ghost2PictureBox);
            ghost3 = new Ghost(ghost3PictureBox);
        }

        public void StartGame()
        {
            timerPacmanDirection.Start();
            CreateForage();
            CreateWall();
            CreateGhost();
            timerGhostDirection.Start();
            ghost1.BringToFront();
            ghost2.BringToFront();
            ghost3.BringToFront();
        }

        private void CreateForage()
        {
            // Your implementation for creating forage
        }

        private void CreateWall()
        {
            // Your implementation for creating walls
        }

        private void CreateGhost()
        {
            // Your implementation for creating ghosts
        }

        public void HandleKeyPress(KeyEventArgs e)
        {
            pacMan.HandleKeyPress(e);
        }

        public void HandleGameTick()
        {

            HandleGhostMovement();
            
        }


        private void HandleGhostMovement()
        {
            string ghost1Move = directionsArray[rnd.Next(0, 4)];
            string ghost2Move = directionsArray[rnd.Next(0, 4)];
            string ghost3Move = directionsArray[rnd.Next(0, 4)];

            ghost1.Move(ghost1Move);
            ghost2.Move(ghost2Move);
            ghost3.Move(ghost3Move);
        }

    


        public void StopGame()
        {
            timerPacmanDirection.Stop();
            timerGhostDirection.Stop();
        }
    }

}