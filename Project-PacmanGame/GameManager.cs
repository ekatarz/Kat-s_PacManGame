using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class GameManager
    {
        private FormPacman form;
        private PacManClass pacMan;
        private List<GhostClass> ghosts;
        private List<PictureBox> forageList;
        private List<Label> walls;
        private Timer gameTimer;
        private int score;
        private int lives;

        public GameManager(FormPacman form, PacManClass pacMan, List<GhostClass> ghosts, List<PictureBox> forageList, List<Label> walls)
        {
            this.form = form;
            this.pacMan = pacMan;
            this.ghosts = ghosts;
            this.forageList = forageList;
            this.walls = walls;

            InitializeGame();
        }

        private void InitializeGame()
        {
            score = 0;
            lives = 3;
            SetupGameTimer();
        }

        private void SetupGameTimer()
        {
            gameTimer = new Timer();
            gameTimer.Interval = 100; 
            gameTimer.Tick += new EventHandler(GameLoop);
            gameTimer.Start();
        }

        public void StartGame()
        {
            score = 0;
            lives = 3;
            ResetPositions();
            gameTimer.Start();
        }

        public void PauseGame()
        {
            gameTimer.Stop();
        }

        public void ResumeGame()
        {
            gameTimer.Start();
        }

        public void EndGame()
        {
            gameTimer.Stop();
            MessageBox.Show("Game Over! Your score: " + score);
            
        }

        private void GameLoop(object sender, EventArgs e)
        {
            pacMan.Move();
            foreach (var ghost in ghosts)
            {
                ghost.Move();
                if (ghost.CheckCollisionWithPacMan(pacMan.pacManPictureBox))
                {
                    lives--;
                    if (lives <= 0) EndGame();
                    else ResetPositions();
                }
            }

            CheckForageCollision();
            UpdateScoreAndLivesDisplay();
        }

        private void CheckForageCollision()
        {
            foreach (var forage in forageList)
            {
                if (forage.Visible && pacMan.pacManPictureBox.Bounds.IntersectsWith(forage.Bounds))
                {
                    forage.Visible = false;
                    score++;
                }
            }
        }

        private void UpdateScoreAndLivesDisplay()
        {
            form.UpdateScore(score);
            form.UpdateLives(lives);
        }

        private void ResetPositions()
        {
            // Reset positions of PacMan and ghosts
            pacMan.Reset();
            foreach (var ghost in ghosts)
            {
                ghost.ResetPosition(); 
            }
        }

        public void ChangePacManDirection(KeyEventArgs e)
        {
            pacMan.ChangeDirection(e);
        }
    }
}
