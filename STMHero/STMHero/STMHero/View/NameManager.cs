using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace STMHero
{
    static class NameManager
    {
        static SpriteBatch spriteBatch;
        static SpriteFont spriteFont;
        static SpriteFont spriteFont2;
        static int characterNumber = 6;
        static int characterIndex;
        static char[] characters;
        static bool changed = false;
        static public string GetName()
        {
            if (changed)
                return new string(characters);
            
            return "ANONIM";
        }


        static public void LoadContent(SpriteBatch spriteBatch,  ContentManager Content)
        {
            NameManager.spriteBatch = spriteBatch;
            spriteFont = Content.Load<SpriteFont>("Font/nameManager");
            spriteFont2 = Content.Load<SpriteFont>("Font/nameManager2");
            characters = new char[characterNumber];
            characterIndex = 0;
            for (int i = 0; i < characterNumber; i++)
                characters[i] = 'A';
        }

        static public void CheckKey(KeyboardState keyboardState)
        {
            changed = true;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (++characters[characterIndex] > 'Z')
                    characters[characterIndex] = 'A';

            } else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (--characters[characterIndex] < 'A')
                    characters[characterIndex] = 'Z';

            } else if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (--characterIndex < 0)
                    characterIndex = characterNumber - 1;

            } else if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (++characterIndex > characterNumber - 1)
                    characterIndex = 0;
            }

        }

        static public void Draw()
        {
            spriteBatch.DrawString(spriteFont2, "Enter Player Name:", new Vector2((spriteBatch.GraphicsDevice.Viewport.Width / 2 - spriteFont2.MeasureString("Enter Player Name").X / 2), (spriteBatch.GraphicsDevice.Viewport.Height / 2 - 300)), Color.Black);
            for (int i = 0; i < characterNumber; i++)
			{
                if (i != characterIndex)
                    spriteBatch.DrawString(spriteFont, characters[i].ToString(), new Vector2((spriteBatch.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString(characters[i].ToString()).X / 2) + ((i - 2) * 150) - 75, (spriteBatch.GraphicsDevice.Viewport.Height / 2 - 150)), Color.White);
                else
                    spriteBatch.DrawString(spriteFont, characters[i].ToString(), new Vector2((spriteBatch.GraphicsDevice.Viewport.Width / 2 - spriteFont.MeasureString(characters[i].ToString()).X / 2) + ((i - 2) * 150) - 75, (spriteBatch.GraphicsDevice.Viewport.Height / 2 - 150)), Color.Orange);
                
			}
        }
    }
}
