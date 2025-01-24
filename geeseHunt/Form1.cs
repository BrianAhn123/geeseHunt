using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace geeseHunt
{   //Notes
    //Was able to get borders somewhat working
    //but bottom and right border do not work and when goose 1 hits the corner it forces through the border 

    //goose2 just goes up and past the border 
    
    // planned to add powerups after goose were done
    // more sounds but had no time 
    // gun works as well as reloading 
    // points as well work as planned 

    //overall code if no where near finished and needed more planning

    public partial class Form1 : Form
    {
        //Player 
        Rectangle crosshair = new Rectangle(400, 225, 20, 20);
        Rectangle crosshairPiece = new Rectangle(408, 225, 5, 40);
        Rectangle crosshairPiece2 = new Rectangle(408, 205, 5, 40);
        Rectangle crosshairPiece3 = new Rectangle(402, 232, 40, 5);
        Rectangle crosshairPiece4 = new Rectangle(380, 232, 40, 5);


        //Geese 
        Rectangle goose1 = new Rectangle(300, 100, 50, 50);
        Rectangle goose2 = new Rectangle(300, 100, 50, 50);
        List<Rectangle> geeses = new List<Rectangle>();

        //keys 
        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool spacePressed = false;
        bool enterPressed = false;

        int score = 0;

        int pause = 150;

        int crosshairSpeed = 10;
        int geesexSpeed = 4;
        int geeseySpeed = 6;
        int framestoMove = 20;

        int geese2xSpeed = -4;
        int geese2ySpeed = -6;
        int frames2toMove = 20;

        int ammo = 3;

        int grassHeight = 50;

        SoundPlayer shoot = new SoundPlayer(Properties.Resources.shot);

        Random randGen = new Random();

        Pen redPen = new Pen(Color.Red);
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush greenBrush = new SolidBrush(Color.Green);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            //draw goose 
            e.Graphics.FillRectangle(whiteBrush, goose1);

            e.Graphics.FillRectangle(whiteBrush, goose2);

            for (int i = 0; i < geeses.Count; i++)
            {
                e.Graphics.FillRectangle(whiteBrush, geeses[i]);
            }

            //Draw tall grass 
            e.Graphics.FillRectangle(greenBrush, 0, this.Height - grassHeight, this.Width, grassHeight);

            //Draw Crosshair
            e.Graphics.FillEllipse(redBrush, crosshair);
            e.Graphics.FillRectangle(redBrush, crosshairPiece);
            e.Graphics.FillRectangle(redBrush, crosshairPiece2);
            e.Graphics.FillRectangle(redBrush, crosshairPiece3);
            e.Graphics.FillRectangle(redBrush, crosshairPiece4);

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Space:
                    spacePressed = true;
                    break;
                case Keys.Enter:
                    enterPressed = true;
                    break; 
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Space:
                    spacePressed = false;
                    break;
                case Keys.Enter:
                    enterPressed = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // move crosshair 
            if (leftPressed && crosshair.X > 0)
            {
                crosshair.X -= crosshairSpeed;
                crosshairPiece.X -= crosshairSpeed;
                crosshairPiece2.X -= crosshairSpeed;
                crosshairPiece3.X -= crosshairSpeed;
                crosshairPiece4.X -= crosshairSpeed;
            }

            if (rightPressed && crosshair.X < this.Width - crosshair.Width)
            {
                crosshair.X += crosshairSpeed;
                crosshairPiece.X += crosshairSpeed;
                crosshairPiece2.X += crosshairSpeed;
                crosshairPiece3.X += crosshairSpeed;
                crosshairPiece4.X += crosshairSpeed;
            }

            if (upPressed && crosshair.Y > 0)
            {
                crosshair.Y -= crosshairSpeed;
                crosshairPiece.Y -= crosshairSpeed;
                crosshairPiece2.Y -= crosshairSpeed;
                crosshairPiece3.Y -= crosshairSpeed;
                crosshairPiece4.Y -= crosshairSpeed;
            }

            if (downPressed && crosshair.Y < this.Height - grassHeight)
            {
                crosshair.Y += crosshairSpeed;
                crosshairPiece.Y += crosshairSpeed;
                crosshairPiece2.Y += crosshairSpeed;
                crosshairPiece3.Y += crosshairSpeed;
                crosshairPiece4.Y += crosshairSpeed;
            }

            //if space is pressed, while crosshair is on duck
            if (ammo > 0)
            {
                if (spacePressed && crosshair.IntersectsWith(goose1))
                {
                    spacePressed = false;
                    score += 100;
                    ammo--;
                    shoot.Play();
                }

                if (spacePressed && crosshair.IntersectsWith(goose2))
                {
                    spacePressed = false;
                    score += 100;
                    ammo--;
                    shoot.Play();
                }

                else if (spacePressed)
                {
                    spacePressed = false;
                    shoot.Play();
                    ammo--;
                }
            }

            if (ammo == 0 && enterPressed)
            {
                ammo = 3;
            }


            //Move Goose1
            int randValue = randGen.Next(-10, 10);
            int randValue2 = randGen.Next(-10, 10);
            int randFrame = randGen.Next(5, 30);

            goose1.X += geesexSpeed;
            goose1.Y += geeseySpeed;
            framestoMove--;

                if (goose1.X > 0 && goose1.X < this.Width - goose1.Width && goose1.Y > 0 && goose1.Y < this.Height - 50)
                {
                    if (framestoMove == 0)
                    {
                        geesexSpeed = randValue;
                        geeseySpeed = randValue2;
                        framestoMove = randFrame;
                    }
                }
                else
                {
                    if (goose1.X < 0)
                    {
                        geesexSpeed = randGen.Next(1, 10);
                        geeseySpeed = randGen.Next(-10, -1);
                        framestoMove = randGen.Next(5, 30);
                    }

                    if (goose1.Y < 0)
                    {
                        geesexSpeed = randGen.Next(-10, -1);
                        geeseySpeed = randGen.Next(1, 10);
                        framestoMove = randGen.Next(5, 30);
                    }

                    if (goose1.X > this.Width - goose1.Width)
                    {
                        geesexSpeed = randGen.Next(1, 10);
                        geeseySpeed = randGen.Next(-10, -1);
                        framestoMove = randGen.Next(5, 30);
                    }


                    if (goose1.Y > this.Height - 50)
                    {
                        geesexSpeed = randGen.Next(-10, -1);
                        geeseySpeed = randGen.Next(1, 10);
                        framestoMove = randGen.Next(5, 30);
                    }
                }
            

            //move goose2 
            goose2.X += geese2xSpeed;
            goose2.Y += geese2ySpeed;
            frames2toMove--;

            if (goose2.X > 0 && goose2.X < this.Width - goose2.Width && goose2.Y > 0 && goose2.Y < this.Height - goose1.Height)
            {
                

                if (frames2toMove == 0)
                {
                    frames2toMove = randFrame;
                    geese2xSpeed = randValue2;
                    geese2ySpeed = randValue;
                }
            }




            scoreLabel.Text = $"{score}";
            ammoLabel.Text = $"Ammo: {ammo}";

            Refresh();
        }

    }
}

