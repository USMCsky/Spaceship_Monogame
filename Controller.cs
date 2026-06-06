using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Controller
    {
        // List to hold the asteroids and a timer to control when new asteroids are added
        public List<Asteroid> asteroids = new List<Asteroid>();
        public double timer = 2;
        public double maxtime  = 2;
        public int nextSpeed = 240;
        public bool inGame = false;

        // Constructor to initialize the timer
        public void conUpdate(GameTime gameTime)
        {
            // Update the timer and add a new asteroid to the list every 2 seconds
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState state = Keyboard.GetState();
                if(state.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                }
            }

            // If the timer has reached zero, add a new asteroid and reset the timer
            if (timer <= 0)
            {
                asteroids.Add(new Asteroid(nextSpeed));
                timer = maxtime;

                if(maxtime > 0.6)
                {
                    maxtime -= 0.1;
                }

                // Increase the speed of the next asteroid to make it more challenging
                if (nextSpeed < 720)
                {
                    nextSpeed += 4;
                }

            }
        }
    }
}
