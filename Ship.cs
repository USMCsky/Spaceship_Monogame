using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Spaceship
{
    
    class Ship  // This class represents the player's spaceship
    {
        static public Vector2 defaultPosition = new Vector2(640, 360);  // Default starting position for the ship
        public Vector2 position = defaultPosition;
        public int speed = 300;

        public void shipUpdate(GameTime gameTime)
        {
            // Handle input to move the ship
            KeyboardState state = Keyboard.GetState();

            // Get the time elapsed since the last update (in seconds)
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;  

            if (state.IsKeyDown(Keys.A))
                position.X -= speed * dt;  // Move left
            if (state.IsKeyDown(Keys.D))
                position.X += speed * dt;  // Move right
            if (state.IsKeyDown(Keys.W))
                position.Y -= speed * dt;  // Move up
            if (state.IsKeyDown(Keys.S))
                position.Y += speed * dt;  // Move down
        }
    }
}
