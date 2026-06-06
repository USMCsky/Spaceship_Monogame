using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            player.shipUpdate(gameTime);
            // Update the game controller, which will manage the asteroids and add new ones as needed
            gameController.conUpdate(gameTime);
            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                gameController.asteroids[i].asteroidUpdate(gameTime);
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

            for (int i = 0; i < gameController.asteroids.Count; i++)
            {
                _spriteBatch.Draw(asteroidSprite, new Vector2(gameController.asteroids[i].position.X - gameController.asteroids[i].radius, gameController.asteroids[i].position.Y - gameController.asteroids[i].radius), Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
