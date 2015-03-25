using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace STMHero.View
{
    class GraphicClass
    {
        public Vector2 position { get; set; }
        public Vector2 size { get; set; }
        public Texture2D texture { get; set; }
        public float scale { get; set; }

        public GraphicClass()
        {
            position = new Vector2(0, 0);
            scale = 1f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White,0f,new Vector2(texture.Width/2,texture.Height/2),scale,SpriteEffects.None,1);
        }
    }
}
