using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_PacmanGame
{
    public class Ghost
    {
        private PictureBox ghostPictureBox;

        public Ghost(PictureBox pictureBox)
        {
            ghostPictureBox = pictureBox;
        }

        public void Move(string direction)
        {
            //logic for ghost movement based on the given direction
            if (direction == "Up")
            {
                if (50 <= ghostPictureBox.Top && !(ghostPictureBox.Top == 340 && (ghostPictureBox.Left == 340 || ghostPictureBox.Left == 390 || ghostPictureBox.Left == 440 || ghostPictureBox.Left == 490 || ghostPictureBox.Left == 540 || ghostPictureBox.Left == 590 || ghostPictureBox.Left == 640)))
                {
                    ghostPictureBox.Top -= 50;
                }
            }
            else if (direction == "Down")
            {
                if (ghostPictureBox.Top <= 540 - 30 && !(ghostPictureBox.Top == 140 && (ghostPictureBox.Left == 640 && ghostPictureBox.Right == 685)) && !(ghostPictureBox.Top == 140 && (ghostPictureBox.Left == 340 && ghostPictureBox.Right == 385)) && !(ghostPictureBox.Top == 240 && (ghostPictureBox.Left == 390 || ghostPictureBox.Left == 440 || ghostPictureBox.Left == 490 || ghostPictureBox.Left == 540 || ghostPictureBox.Left == 590)))
                {
                    ghostPictureBox.Top += 50;
                }
            }
            else if (direction == "Right")
            {
                if (1050 >= ghostPictureBox.Right && !(ghostPictureBox.Right == 335 && (ghostPictureBox.Top == 190 || ghostPictureBox.Top == 240 || ghostPictureBox.Top == 290)) && !(ghostPictureBox.Right == 635 && (ghostPictureBox.Top == 190 || ghostPictureBox.Top == 240)))
                {
                    ghostPictureBox.Left += 50;
                }
            }
            else if (direction == "Left")
            {
                if (50 <= ghostPictureBox.Left && !(ghostPictureBox.Right == 735 && (ghostPictureBox.Top == 190 || ghostPictureBox.Top == 240 || ghostPictureBox.Top == 290)) && !(ghostPictureBox.Right == 435 && (ghostPictureBox.Top == 190 || ghostPictureBox.Top == 240)))
                {
                    ghostPictureBox.Left -= 50;
                }
            }
        }

        // Add properties and methods needed by PacManClass
        public int Top
        {
            get { return ghostPictureBox.Top; }
        }

        public int Bottom
        {
            get { return ghostPictureBox.Bottom; }
        }

        public int Left
        {
            get { return ghostPictureBox.Left; }
        }

        public int Right
        {
            get { return ghostPictureBox.Right; }
        }

        public void BringToFront()
        {
            ghostPictureBox.BringToFront();
        }

        public void ResetLocation(Point location)
        {
            ghostPictureBox.Location = location;
        }
    }
}

