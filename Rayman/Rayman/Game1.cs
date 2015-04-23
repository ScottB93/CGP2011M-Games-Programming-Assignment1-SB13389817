#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;


#endregion

namespace Escape
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        Rayman player = new Rayman();
        GameObjects objects = new GameObjects();
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MainMenu main = new MainMenu();
        Vector2 screenPosition;
        SoundEffectInstance bgThemeLoop;



        public SoundEffect backgroundMusic;
        

        bool paused = false;
      
        public Game1()
            : base()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }
        
       
        protected override void Initialize()
        {
            //Sets window size
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 500;
            graphics.ApplyChanges();
            
            IsMouseVisible = true;
           
            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

                backgroundMusic = Content.Load<SoundEffect>("mainMusic");
           
             
      
            main.LoadContent(Content);

            bgThemeLoop = backgroundMusic.CreateInstance();
            bgThemeLoop.IsLooped = true;

            bgThemeLoop.Volume = 0.16f;
            bgThemeLoop.Play();
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
                Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.F))
            {
                //Allows the screen to go full size when [F] is pressed
               
                this.graphics.IsFullScreen = true;
                this.graphics.ApplyChanges();
                Window.IsBorderless = true;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.C))
            {
                //Returns to standard window size if [C] is pressd
                this.graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                Window.IsBorderless = false;
            }
            
                main.Update(gameTime);
                base.Update(gameTime);
            }
         
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            
            main.Draw(spriteBatch);
                

            spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }
}
