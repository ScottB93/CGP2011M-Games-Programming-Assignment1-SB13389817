using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Design;

namespace Escape
{
    class Object2
    {
        Rayman player = new Rayman();
        Texture2D background2, Face, orbCollect;
        //Loads textures
        public void loadContent(ContentManager content)
        {
            background2 = content.Load<Texture2D>("Level2Background");
            Face = content.Load<Texture2D>("Face");
            orbCollect = content.Load<Texture2D>("orbCollect");
        }
        public void Update(GameTime gameTime)
        {

        }
        // draws
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background2, new Rectangle(0,0,800, 500), Color.White);
            spriteBatch.Draw(Face, new Rectangle(600, 0, 77, 76), Color.White);
            spriteBatch.Draw(orbCollect, new Rectangle(0, 0, 77, 76), Color.White);
        }
    }
}
