using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Spaceship
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D shipSprite;
        Texture2D asteroidSprite;
        Texture2D spaceSprite;
        SpriteFont gameFont;
        SpriteFont timerFont;

        Ship player = new Ship();  // Create an instance of the Ship class to represent the player's spaceship
        Controller gameController = new Controller();  // Create an instance of the Controller class to manage asteroids


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipSprite = Content.Load<Texture2D>("ship");
            spaceSprite = Content.Load<Texture2D>("space");
            asteroidSprite = Content.Load<Texture2D>("asteroid");

            gameFont = Content.Load<SpriteFont>("spaceFont");
            timerFont = Content.Load<SpriteFont>("timerFont");

            // Load persisted high score
            gameController.LoadHighScore();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update the player's spaceship based on input - only if the game is currently active (inGame is true)
            if (gameController.inGame)
            {
                player.shipUpdate(gameTime);
            }

            // Update the game controller, which will manage the asteroids and add new ones as needed
            gameController.conUpdate(gameTime);

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);

                int sum = gameController.asteroids[i].radius + player.radius;
                if(Vector2.Distance(gameController.asteroids[i].position, player.position) < sum)
                {
                    // If a collision is detected between the player's ship and an asteroid, reset the game state
                    player.position = Ship.defaultPosition;  // Reset the player's position to the default starting position
                    gameController.asteroids.Clear();  // Clear all asteroids from the game
                    gameController.inGame = false;  // Set inGame to false to indicate that the game is no longer active
                    // Check if we have a new high score and save it
                    gameController.TryUpdateHighScore();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.Draw(spaceSprite, new Vector2(0, 0), Color.White);
            // Draw the player's spaceship at its current position
            _spriteBatch.Draw(shipSprite, new Vector2(player.position.X-34, player.position.Y-50), Color.White);
            // Draw each asteroid in the list of asteroids managed by the game controller
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                _spriteBatch.Draw(asteroidSprite, new Vector2(gameController.asteroids[i].position.X - gameController.asteroids[i].radius, gameController.asteroids[i].position.Y - gameController.asteroids[i].radius), Color.White);
            }

            // If the game is not currently active (inGame is false), display a message prompting the player to start the game
            if (gameController.inGame == false)
            {
                string menuMessage = "Press Enter to Start";
                // Measure the size of the text to center it on the screen
                Vector2 sizeOfText = gameFont.MeasureString(menuMessage);
                // Calculate the horizontal position to center the text on the screen
                int halfWidth = _graphics.PreferredBackBufferWidth / 2 - (int)sizeOfText.X / 2;
                // Draw the menu message at the calculated position
                _spriteBatch.DrawString(gameFont, menuMessage, new Vector2(halfWidth, 200), Color.White);
                // Show high score
                string hs = "High Score: " + Math.Round(gameController.highScore, 2);
                Vector2 hsSize = gameFont.MeasureString(hs);
                int hsX = _graphics.PreferredBackBufferWidth / 2 - (int)hsSize.X / 2;
                _spriteBatch.DrawString(gameFont, hs, new Vector2(hsX, 260), Color.Yellow);
            }

            // Draw the timer showing how long the player has survived in the game, rounded to 2 decimal places
            _spriteBatch.DrawString(timerFont, "Time: " + Math.Round(gameController.totalTime, 2), new Vector2(10, 10), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
