using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class PacManClass
    {
        public PictureBox pacManPictureBox;
        private ImageList imageList;
        private string direction;
        private int score;
        private List<Label> walls;

        public PacManClass(PictureBox pictureBox, ImageList images, List<Label> walls)
        {
            pacManPictureBox = pictureBox;
            imageList = images;
            this.walls = walls;
            direction = "Right";
            score = 0;
            pacManPictureBox.BackgroundImage = imageList.Images[0];
            pacManPictureBox.BackColor = Color.Transparent;
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public void Move()
        {
            switch (direction)
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
            Rectangle nextPosition = new Rectangle(x, y, pacManPictureBox.Width, pacManPictureBox.Height);
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
            int nextTop = pacManPictureBox.Top - 50;
            if (nextTop >= 0 && CanMove(pacManPictureBox.Left, nextTop)) // boundry
            {
                pacManPictureBox.Top = nextTop;
            }
        }

        private void MoveDown()
        {
            int nextBottom = pacManPictureBox.Bottom + 50;
            if (nextBottom <= pacManPictureBox.Parent.ClientSize.Height && CanMove(pacManPictureBox.Left, pacManPictureBox.Top + 50)) // Adjusted boundary check
            {
                pacManPictureBox.Top += 50;
            }
        }

        private void MoveLeft()
        {
            int nextLeft = pacManPictureBox.Left - 50;
            if (nextLeft >= 0 && CanMove(nextLeft, pacManPictureBox.Top)) // boundry
            {
                pacManPictureBox.Left = nextLeft;
            }
        }

        private void MoveRight()
        {
            int nextRight = pacManPictureBox.Right + 50;
            if (nextRight <= pacManPictureBox.Parent.ClientSize.Width && CanMove(pacManPictureBox.Left + 50, pacManPictureBox.Top)) // bondry 
            {
                pacManPictureBox.Left += 50;
            }
        }


        public void ChangeDirection(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                direction = "Up";
                pacManPictureBox.BackgroundImage = imageList.Images[3];
            }
            else if (e.KeyCode == Keys.Down)
            {
                direction = "Down";
                pacManPictureBox.BackgroundImage = imageList.Images[2];
            }
            else if (e.KeyCode == Keys.Left)
            {
                direction = "Left";
                pacManPictureBox.BackgroundImage = imageList.Images[1];
            }
            else if (e.KeyCode == Keys.Right)
            {
                direction = "Right";
                pacManPictureBox.BackgroundImage = imageList.Images[0];
            }
        }

        public void EatForage(PictureBox forage)
        {
            if (forage.Visible)
            {
                score++;
                forage.Visible = false;
                forage.Parent.Controls.Remove(forage);
            }
        }

       
        public void Reset()
        {
            pacManPictureBox.Location = new Point(37, 37);
            pacManPictureBox.BackgroundImage = imageList.Images[0];
            pacManPictureBox.BackColor = Color.Transparent;
            direction = "Right";
            score = 0;
        }
    }
}
