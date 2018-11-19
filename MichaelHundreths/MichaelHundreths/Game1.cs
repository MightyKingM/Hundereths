using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace MichaelHundreths
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Circle> circle;
        SpriteFont font;
        Rectangle mousepos;
        Random random;
        SpriteFont gamefont;
        StreamReader reader;
        StreamWriter writer;
        bool game = true;
        int score = 0;
        int hiscore = 0;
        int ultimatehighscore;
        //C:\Users\VisitorDL8\Documents\Visual Studio 2017\Projects\MichaelHundreths\Data
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            mousepos = new Rectangle(100, 100, 1, 1);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("Font");
            gamefont = Content.Load<SpriteFont>("GameFont");
            random = new Random();
            reader = new StreamReader("C:\\Users\\MichaelWang\\Documents\\Visual Studio 2017\\Projects\\MichaelHundreths\\Data.txt");
            ultimatehighscore = int.Parse(reader.ReadLine());
            reader.Close();
            // TODO: use this.Content to load your game content here
            circle = new List<Circle>();
            for(int i = 0; i < 4; i++)
            {
                circle.Add(new Circle(Content.Load<Texture2D>("ball"), new Vector2(i * 200, i * 200), Color.White,i + 1));
            }
            
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                
            }
          if(game == true)
           {
                MouseState mouse = Mouse.GetState();
                // TODO: Add your update logic here
                mousepos = new Rectangle(mouse.X, mouse.Y, 1, 1);
                for (int i = 0; i < circle.Count; i++)
                {
                    for (int x = 0; x < circle.Count; x++)
                    {
                        if (i != x)
                        {
                            if (circle[i].Bounce(circle[x]))
                            {
                                if (circle[i].Intersects(new Vector2(mousepos.X, mousepos.Y)) || circle[x].Intersects(new Vector2(mousepos.X, mousepos.Y)))
                                {
                                    game = false;
                                }
                            }
                        }
                    }
                    if (circle[i].Intersects(new Vector2(mousepos.X, mousepos.Y)))
                    {
                        circle[i].grow(10);
                        score++;
                    }

                    circle[i].move(GraphicsDevice.Viewport.Height - 110, GraphicsDevice.Viewport.Width);

                }
                if(score > hiscore)
                {
                    hiscore = score;
                }
                if(hiscore > ultimatehighscore)
                {
                    ultimatehighscore = hiscore;
                }
            }
          else
          {
          }
                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if(game)
            {
                for (int i = 0; i < circle.Count; i++)
                {
                    circle[i].Draw(spriteBatch);
                    circle[i].DrawText(spriteBatch, font);
                }
                spriteBatch.DrawString(gamefont, "Score: " + score.ToString() + " Hiscore: " + hiscore, new Vector2(0,950), Color.Blue);

            }
            else
            {
                spriteBatch.DrawString(gamefont, "Game Over", new Vector2((GraphicsDevice.Viewport.Width - font.MeasureString("Game Over").X) / (7/2) , 100), Color.Blue);
                spriteBatch.DrawString(gamefont, "Score: " + score.ToString() + " Hiscore: " + hiscore, new Vector2(0, 200), Color.Red);
                spriteBatch.DrawString(gamefont, "Ultimate High Score:" + ultimatehighscore, new Vector2(0, 310), Color.Red);
                spriteBatch.DrawString(gamefont, "Press R to play again.", new Vector2(0, 420), Color.Red);
                spriteBatch.DrawString(gamefont, "Press C to reset data.", new Vector2(0, 530), Color.Red);
                writer = new StreamWriter("C:\\Users\\MichaelWang\\Documents\\Visual Studio 2017\\Projects\\MichaelHundreths\\Data.txt");
                writer.WriteLine(ultimatehighscore);
                writer.Close();
                if ( Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    score = 0;
                    random = new Random();
                    circle = new List<Circle>();
                    for (int i = 0; i < 4; i++)
                    {
                        circle.Add(new Circle(Content.Load<Texture2D>("ball"), new Vector2(i * 200, i * 200), Color.White, i + 1));
                    }
                    game = true;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    writer = new StreamWriter("C:\\Users\\VisitorDL8\\Documents\\Visual Studio 2017\\Projects\\MichaelHundreths\\Data.txt");
                    writer.WriteLine("0");
                    writer.Close();
                }
                }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
