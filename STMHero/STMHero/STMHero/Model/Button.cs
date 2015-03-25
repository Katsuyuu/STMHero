using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace STMHero.Model
{
    public class Button
    {
        public int timeSpawn { get; set; }
        public bool active { get; set; }
        public Texture2D buttonImage { get; set; }
        public Vector2 buttonPosition { get; set; }
        public buttonType Type { get; set; }

        public int width
        {
            get
            {
                return buttonImage.Width;
            }
        }
        public int height
        {
            get
            {
                return buttonImage.Height;
            }
        }


        public Button()
        {
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(buttonImage, buttonPosition, Color.White);
        }

        public void Update(GameTime gameTime)
        {
            buttonPosition = new Vector2(buttonPosition.X, buttonPosition.Y + 2);

            if (buttonPosition.Y > 600)
                active = false;
        }

    }
}
