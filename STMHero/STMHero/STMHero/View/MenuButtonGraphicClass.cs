using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STMHero.View
{
    public class MenuButtonGraphicClass : GraphicClass
    {
        public Texture2D textureHL { get; set; }
        public bool HL;

        public MenuButtonGraphicClass()
        {
            position = new Vector2(0, 0); 
            scale = 1f;
            HL = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (HL)
                spriteBatch.Draw(textureHL, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1);
            else
                spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0f, new Vector2(texture.Width / 2, texture.Height / 2), scale, SpriteEffects.None, 1);
        }
    }
}
