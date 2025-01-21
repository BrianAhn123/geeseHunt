﻿using System;
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
{
    public partial class Form1 : Form
    {
        //Player 
        Rectangle crosshair = new Rectangle(400, 225, 20, 20);
        Rectangle crosshairPiece = new Rectangle(408, 225, 5, 40);
        Rectangle crosshairPiece2 = new Rectangle(408, 205, 5, 40);
        Rectangle crosshairPiece3 = new Rectangle(402, 232, 40, 5);
        Rectangle crosshairPiece4 = new Rectangle(380, 232, 40, 5);


        //Geese 
        Rectangle goose1 = new Rectangle(10, 200, 50, 50);
        List<Rectangle> geeses = new List<Rectangle>();

        //keys 
        bool leftPressed = false;
        bool rightPressed = false;
        bool upPressed = false;
        bool downPressed = false;
        bool spacePressed = false;

        int score = 0;

        int pause = 150;

        int crosshairSpeed = 10;
        int geeseSpeed = 4;
        int ammo = 3;

        int grassHeight = 100;

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
                    score++;
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

             



            //spawn goose


            /* int randValue = randGen.Next(1, 101);

            if (randValue > 95)
            {
                int x = randGen.Next(30, this.Width);
                Rectangle newGeese = new Rectangle(x, 400, 10, 2);
                geeses.Add(newGeese);
            } */

            //move duck 



            /*for (int i = 0; i < geeses.Count; i++)
            {
                int y = geeses[i].Y + geeseSpeed;
                int x = geeses[i].Y + geeseSpeed;
                geeses[i] = new Rectangle(x, y, 10, 10);

            }  */

            //Move Goose 
            int randValue = randGen.Next(-10, 10);

            for (int i = 0; i < randValue; i++)
            {
                int x = randValue + geeseSpeed;
                int y = randValue + geeseSpeed;
                goose1 = new Rectangle(x, y, 50, 50);
                
            }







            scoreLabel.Text = $"{score}";
            ammoLabel.Text = $"Ammo: {ammo}";

            Refresh();
        }

    }
}

