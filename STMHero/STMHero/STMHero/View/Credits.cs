using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Controler;
using STMHero.Model;
using STMHero.View;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace STMHero.Model
{
    static class Credits
    {
        static SpriteBatch spriteBatch;
        static GraphicClass[] CreditsTexture;
        static int Positions  = 2;
        
        public static void Draw()
        {
            for (int i = 0; i < Positions; i++)
            {
                CreditsTexture[i].Draw(spriteBatch);
            }
        }

        public static void LoadContent(SpriteBatch spriteBatch, ContentManager Content)
        {
            Credits.spriteBatch = spriteBatch;

            CreditsTexture = new GraphicClass[Positions];

            for (int i = 0; i < Positions; i++)
			{
                CreditsTexture[i] = new GraphicClass();
			}

            Vector2 centeredPositon = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2);
            float scale = ((float)spriteBatch.GraphicsDevice.Viewport.Width / GameGraphicClass.backGround.texture.Width) < ((float)spriteBatch.GraphicsDevice.Viewport.Height / GameGraphicClass.backGround.texture.Height) ? ((float)spriteBatch.GraphicsDevice.Viewport.Width / GameGraphicClass.backGround.texture.Width) : ((float)spriteBatch.GraphicsDevice.Viewport.Height / GameGraphicClass.backGround.texture.Height);

            CreditsTexture[0].texture = Content.Load<Texture2D>("dys");
            CreditsTexture[1].texture = Content.Load<Texture2D>("madz");

            CreditsTexture[0].position = centeredPositon - new Vector2(0, (CreditsTexture[0].texture.Height / 2));
            CreditsTexture[1].position = centeredPositon + new Vector2(0, (CreditsTexture[1].texture.Height / 2));
        }

    }
}
