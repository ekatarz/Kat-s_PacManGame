using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class GhostClass
    {
        public PictureBox GhostPictureBox { get; private set; }
        private string[] directionsArray = { "Right", "Left", "Up", "Down" };
        private Random rnd = new Random();
        private string currentDirection;
        private Point initialPosition; 

        public Rectangle Bounds
        {
            get { return GhostPictureBox.Bounds; }
        }

        private List<Label> walls;

        public GhostClass(PictureBox ghostPictureBox, List<Label> walls)
        {
            this.GhostPictureBox = ghostPictureBox;
            this.walls = walls;
            this.initialPosition = ghostPictureBox.Location; // set walls position
            currentDirection = directionsArray[rnd.Next(directionsArray.Length)];
        }

        public void ResetPosition()
        {
            GhostPictureBox.Location = initialPosition; // reset to the initial position
            currentDirection = directionsArray[rnd.Next(directionsArray.Length)]; // reset direction
                                                                                  
        }

        public void Move()
        {
            // increase the chance of changing direction
            if (rnd.Next(10) < 4) // 40% chance to change direction
            {
                currentDirection = directionsArray[rnd.Next(directionsArray.Length)];
            }

            switch (currentDirection)
            {
                case "Up":
                    MoveUp();
                    break;
                case "Down":
                    MoveDown();
                    break;
                case "Left":
                    MoveLeft();
                    break;
                case "Right":
                    MoveRight();
                    break;
            }
        }


        private bool CanMove(int x, int y)
        {
            Rectangle nextPosition = new Rectangle(x, y, GhostPictureBox.Width, GhostPictureBox.Height);
            foreach (var wall in walls)
            {
                if (nextPosition.IntersectsWith(wall.Bounds))
                {
                    return false; // Collision detected
                }
            }
            return true; // No collision
        }


        private void MoveUp()
        {
            int nextTop = GhostPictureBox.Top - 50;
            if (nextTop > 0 && CanMove(GhostPictureBox.Left, nextTop))
            {
                GhostPictureBox.Top = nextTop;
            }
            else
            {
                currentDirection = directionsArray[rnd.Next(directionsArray.Length)]; // Change direction
            }
        }
        private void MoveDown()
        {
            int nextBottom = GhostPictureBox.Bottom + 50;
            if (nextBottom < GhostPictureBox.Parent.ClientSize.Height && CanMove(GhostPictureBox.Left, GhostPictureBox.Top + 50))
            {
                GhostPictureBox.Top += 50;
            }
            else
            {
                currentDirection = directionsArray[rnd.Next(directionsArray.Length)]; // Change direction
            }
        }


        private void MoveLeft()
        {
            int nextLeft = GhostPictureBox.Left - 50;
            if (nextLeft > 0 && CanMove(nextLeft, GhostPictureBox.Top))
            {
                GhostPictureBox.Left = nextLeft;
            }
            else
            {
                currentDirection = directionsArray[rnd.Next(directionsArray.Length)]; // Change direction
            }
        }


        private void MoveRight()
        {
            int nextRight = GhostPictureBox.Right + 50;
            if (nextRight < GhostPictureBox.Parent.ClientSize.Width && CanMove(GhostPictureBox.Left + 50, GhostPictureBox.Top))
            {
                GhostPictureBox.Left += 50;
            }
            else
            {
                currentDirection = directionsArray[rnd.Next(directionsArray.Length)]; // Change direction
            }
        }



        public bool CheckCollisionWithPacMan(PictureBox pacManPictureBox)
        {
            return GhostPictureBox.Bounds.IntersectsWith(pacManPictureBox.Bounds);
        }
    }
}
