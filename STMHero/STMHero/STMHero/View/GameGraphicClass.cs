using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Controler;
using STMHero;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace STMHero.View
{
    public class GameGraphicClass
    {
        SpriteBatch spriteBatch;

        GraphicClass logo;
        GraphicClass backGround;

        public GameGraphicClass()
        {
            logo = new GraphicClass();
            backGround = new GraphicClass();
        }

        public void Draw(GameState gameState)
        {

            backGround.Draw(spriteBatch);
            switch (gameState)
            {
                case GameState.LOGO:
                    DrawLogo();
                    break;
                case GameState.MENU:
                    break;
                case GameState.CHOOSING_SONG:
                    break;
                case GameState.PLAYING:
                    break;
                default:
                    break;
            }
        }
        public void LoadContent(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.spriteBatch = spriteBatch;
            logo.texture = Content.Load<Texture2D>("logo");
            backGround.texture = Content.Load<Texture2D>("background");

            //updating position to the center of screen
            Vector2 centeredPositon = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2);

            logo.position = centeredPositon;
            backGround.position = centeredPositon;

            //scaling to size of screen
            float scale = ((float)spriteBatch.GraphicsDevice.Viewport.Width / backGround.texture.Width) < ((float)spriteBatch.GraphicsDevice.Viewport.Height / backGround.texture.Height) ? ((float)spriteBatch.GraphicsDevice.Viewport.Width / backGround.texture.Width) : ((float)spriteBatch.GraphicsDevice.Viewport.Height / backGround.texture.Height);

            backGround.scale = scale;
            logo.scale = scale; 
        }
        
        void DrawLogo()
        {
            logo.Draw(spriteBatch);
        }
    }
}
