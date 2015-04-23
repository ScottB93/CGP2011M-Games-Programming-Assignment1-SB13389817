using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Escape
{
    class MainMenu : Game
    {
        //Timers and Sound
        TimeSpan timer = new TimeSpan(0, 0, 10);
        TimeSpan loadingTimer = new TimeSpan(0, 0, 10);
        TimeSpan completetimer = new TimeSpan(0, 0, 10);
        SoundEffectInstance clickVol;
        public SoundEffect clickSound;
        
        Rayman player = new Rayman(); // Rayman Class
        Rayman2 player2 = new Rayman2(); // Rayman 2 Class
        GameObjects objects = new GameObjects(); // GameObjects Class
        Object2 objects2 = new Object2(); //Object 2 Class
        //######################################################
        //          DIFFERENT PAGES WITHIN THE GAME            #
        //######################################################
        enum GameState {Splash, MainMenu, Loading, Level, Level2,Complete}
        GameState gameState;

        List<GUIElement> splash = new List<GUIElement>();
        List<GUIElement> main = new List<GUIElement>();
        List<GUIElement> loading = new List<GUIElement>();
        List<GUIElement> level = new List<GUIElement>();
        List<GUIElement> level2 = new List<GUIElement>();
        List<GUIElement> complete = new List<GUIElement>();
        public MainMenu()
        {
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //          Splash Page         ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            
            splash.Add(new GUIElement("SplashScreenTitle"));

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //          Main Page           ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            main.Add(new GUIElement("MenuBackground"));
            main.Add(new GUIElement("MenuRayman"));
            main.Add(new GUIElement("Title"));
            main.Add(new GUIElement("Start"));
            main.Add(new GUIElement("Quit"));
            
            
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //          Loading Page        ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            loading.Add(new GUIElement("LoadingScreen"));
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //          Level Page          ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            level.Add(new GUIElement(""));
            player.Initialize();

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //          Level2 Page         ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            level2.Add(new GUIElement(""));
            player2.Initialize();

            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //         Complete Page        ~
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            complete.Add(new GUIElement("MC"));
            
        }
        
        //#############################################
        //          LOADCONTENTS IN PAGES             #
        //#############################################
        public void LoadContent(ContentManager content)
        {
            clickSound = content.Load<SoundEffect>("Button");
            clickVol = clickSound.CreateInstance();
            clickVol.IsLooped = false;
            clickVol.Volume = 0.16f;

            
            //Objects load in splash page
            foreach (GUIElement element in splash)
            {
                element.LoadContent(content);
                element.CentreElement(600, 800);
                element.clickEvent += OnClick;
               
            }

            
            splash.Find(x => x.AssetName == "SplashScreenTitle").MoveElement(0,-50);

            //Objects load in main page
            foreach (GUIElement element in main)
            {
                element.LoadContent(content);
                element.CentreElement(600, 800);
                element.clickEvent += OnClick;
                
            }

            main.Find(x => x.AssetName == "MenuBackground").MoveElement(0, -50);
            main.Find(x => x.AssetName == "MenuRayman").MoveElement(0, -25);
            main.Find(x => x.AssetName == "Start").MoveElement(200, -25);
            main.Find(x => x.AssetName == "Quit").MoveElement(-275, -220);
            main.Find(x => x.AssetName == "Title").MoveElement(0, -230);

            foreach (GUIElement element in loading)
            {
                element.LoadContent(content);
                element.CentreElement(600, 800);
                element.clickEvent += OnClick;
            }
            loading.Find(x => x.AssetName == "LoadingScreen").MoveElement(0, -50);
            //Objects load in level page
            foreach (GUIElement element in level)
            {
               
                objects.LoadContent(content);
                player.LoadContent(content);
              
            }
            foreach (GUIElement element in level2)
            {
                player2.LoadContent(content);
                objects2.loadContent(content);
                
            }
            foreach (GUIElement element in complete)
            {
                element.LoadContent(content);
                element.CentreElement(600, 800);
                element.clickEvent += OnClick;
            }
            complete.Find(x => x.AssetName == "MC").MoveElement(0, -50);
        }
        //############################################
        //              UPDATES PAGES                #
        //############################################
        public void Update(GameTime gameTime)
        {
            switch (gameState)
            {
                    //Updates splash page
                case GameState.Splash:
                    foreach (GUIElement element in splash)
                    {
                        if(timer > TimeSpan.Zero)
                        {
                            timer -= gameTime.ElapsedGameTime;
                        }
                        if(timer < TimeSpan.Zero)
                        {
                            timer = TimeSpan.Zero;
                            gameState = GameState.MainMenu;
                        }
                        element.Update();
                    }
                    break;
                    //Updates main page
                case GameState.MainMenu:
                foreach(GUIElement  element in main)
            {
                element.Update();
            }
                break;

                case GameState.Loading:
                foreach (GUIElement element in loading)
                {
                    if (loadingTimer > TimeSpan.Zero)
                    {
                        loadingTimer -= gameTime.ElapsedGameTime;
                    }
                    if (loadingTimer < TimeSpan.Zero)
                    {
                        loadingTimer = TimeSpan.Zero;
                        gameState = GameState.Level;
                    }
                    element.Update();
                }

                break;
                    //Updates level page
                case GameState.Level:
                foreach (GUIElement element in level)
                {
                    objects.Update(gameTime);
                    player.Update(gameTime);

                    if (player.FinishCollide())
                    {
                        player.finishSound.Play();
                        gameState = GameState.Level2;
                    }
                   
                }
                
                break;
                case GameState.Level2:
                   foreach (GUIElement element in level2)
                {
                   player2.Update(gameTime);
                       if(player2.FinishCollide())
                       {
                           gameState = GameState.Complete;
                       }
                }
                
                break;
                case GameState.Complete:
                foreach (GUIElement element in complete)
                {
                    if (completetimer > TimeSpan.Zero)
                    {
                        completetimer -= gameTime.ElapsedGameTime;
                    }
                    if (completetimer < TimeSpan.Zero)
                    {
                        completetimer = TimeSpan.Zero;
                        gameState = GameState.MainMenu;
                    }
                    element.Update();
                }

                break;
                default:
                    break;
            }
           
        }
        //################################################
        //               DRAWS ON PAGES                  #
        //################################################
        public void Draw(SpriteBatch spriteBatch)
        {
            switch (gameState)
            {
                    //Draws elements on Splash page
                case GameState.Splash:
                    foreach (GUIElement element in splash)
                    {
                        element.Draw(spriteBatch);
                    }
                    break;
                    //Draws elements on main page
                case GameState.MainMenu:
                    foreach(GUIElement element in main)
            {
                element.Draw(spriteBatch);
                
            }
                    break;

                case GameState.Loading:
                    foreach (GUIElement element in loading)
                    {
                        element.Draw(spriteBatch);

                    }
                    break;
                    //Draws elements on level page
                case GameState.Level:
                    foreach (GUIElement element in level)
                    {
                        objects.Draw(spriteBatch);
                        player.Draw(spriteBatch);
                        
                    }
                    
                    break;
                case GameState.Level2:
                    foreach (GUIElement element in level2)
                    {
                        
                        objects2.Draw(spriteBatch);
                        player2.Draw(spriteBatch);
                    }
                    break;
                case GameState.Complete:
                    foreach (GUIElement element in complete)
                    {
                        
                        element.Draw(spriteBatch);
                    }
                    break;
                default:
                    break;
            }
            
        }
        //##############################################
        //          MOUSE INPUT (CLICK EVENT)          #
        //##############################################
        public void OnClick(string element)
        {
            
            if (element == "Start")
            {
                //loads level
                gameState = GameState.Loading;
                clickSound.Play();
                
            }
                
        }
    }
}
