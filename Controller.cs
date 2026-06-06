using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Globalization;

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
        public double totalTime = 0;
        public double highScore = 0;

        // File path to store the high score locally
        private string HighScorePath
        {
            get
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Spaceship");
                return Path.Combine(dir, "highscore.txt");
            }
        }

        public void LoadHighScore()
        {
            try
            {
                string path = HighScorePath;
                if (File.Exists(path))
                {
                    var txt = File.ReadAllText(path);
                    if (double.TryParse(txt, NumberStyles.Float, CultureInfo.InvariantCulture, out double val))
                    {
                        highScore = val;
                    }
                }
            }
            catch
            {
                // ignore errors reading high score
            }
        }

        public void SaveHighScore()
        {
            try
            {
                string path = HighScorePath;
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, highScore.ToString(CultureInfo.InvariantCulture));
            }
            catch
            {
                // ignore write errors
            }
        }

        public void TryUpdateHighScore()
        {
            if (totalTime > highScore)
            {
                highScore = totalTime;
                SaveHighScore();
            }
        }

        // Constructor to initialize the timer
        public void conUpdate(GameTime gameTime)
        {
            // Update the timer and add a new asteroid to the list every 2 seconds
            if (inGame)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                totalTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState state = Keyboard.GetState();
                if(state.IsKeyDown(Keys.Enter))
                {
                    inGame = true;
                    totalTime = 0;
                    timer = 2;
                    maxtime = 2;
                    nextSpeed = 240;
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
