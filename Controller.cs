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
        public double timer;

        public void conUpdate(GameTime gameTime)
        {
            // Update the timer and add a new asteroid to the list every 2 seconds
            timer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                asteroids.Add(new Asteroid(250));
                timer = 2;
            }
        }
    }
}
