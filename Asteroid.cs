using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    class Asteroid
    {
        // Position of the asteroid, starting at the right edge of the screen
        public Vector2 position = new Vector2(600, 300);
        // Speed at which the asteroid moves to the left
        public int speed;
        // Radius of the asteroid for collision detection
        public int radius = 59;

        public Asteroid(int newSpeed)
        {
            // Initialize the asteroid's speed
            speed = newSpeed;
            // Start at the right edge with a random vertical position
            position = new Vector2(1380, new Random().Next(0, 721));
        }

        public void asteroidUpdate(GameTime gameTime)
        {
            // Update the asteroid's position based on its speed and the elapsed time since the last update
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            position.X -= speed * dt;
        }
    }
}
