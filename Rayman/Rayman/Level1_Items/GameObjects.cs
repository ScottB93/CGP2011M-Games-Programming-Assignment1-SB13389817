using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
//using Microsoft.Xna.Framework.GamerServices;

namespace Escape 
{
    
    class GameObjects : Game
    {
        //Textures
        Texture2D Background, Tree, Tree2, Face, orbCollect, Rock, Rock2, Bines, flowers, flowers2;
     //Loads Textures
        public void LoadContent(ContentManager content)
        {
            Background = content.Load<Texture2D>("LevelBackground");
            Tree = content.Load<Texture2D>("Tree");
            Tree2 = content.Load<Texture2D>("Tree2");
            Face = content.Load<Texture2D>("Face");
            orbCollect = content.Load<Texture2D>("orbCollect");
            Rock = content.Load<Texture2D>("Rock");
            Rock2 = content.Load<Texture2D>("Rock2");
            Bines = content.Load<Texture2D>("Bines");
           flowers = content.Load<Texture2D>("Flowers");
           flowers2 = content.Load<Texture2D>("Flowers2");
        }
        public void Update(GameTime gameTime)
        {
            
        }
        //Draws Textures
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, new Rectangle(0, 0, 800, 500), Color.White);
            spriteBatch.Draw(Rock, new Rectangle(-25, 70, 400, 400), Color.White);
            spriteBatch.Draw(Rock2, new Rectangle(525, 340, 209, 242), Color.White);
            spriteBatch.Draw(Tree, new Rectangle(0, 345, 95, 70), Color.White);
            spriteBatch.Draw(Tree2, new Rectangle(250, 345, 77,76), Color.White);
            spriteBatch.Draw(Face, new Rectangle(600,0, 77, 76), Color.White);
            spriteBatch.Draw(orbCollect, new Rectangle(0, 0, 77, 76), Color.White);
            spriteBatch.Draw(Bines, new Rectangle(610, 305, 69,34), Color.White);
            spriteBatch.Draw(flowers, new Rectangle(675, 370, 100, 50), Color.White);
            spriteBatch.Draw(flowers2, new Rectangle(100, 390, 122, 30), Color.White);
        }
    }
}
