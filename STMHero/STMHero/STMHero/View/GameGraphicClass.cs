using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Controler;
using STMHero.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace STMHero.View
{
    public class GameGraphicClass
    {
        SpriteBatch spriteBatch;

        GraphicClass logo;
        public static GraphicClass backGround;
        MenuButtonGraphicClass[] menuButtons;
        MenuButtonGraphicClass[] moreButtons;
        int numberOfGameStates = 4;
        int numberOfMoreStates = 3;

        public GameGraphicClass()
        {
            logo = new GraphicClass();
            backGround = new GraphicClass();

            moreButtons = new MenuButtonGraphicClass[numberOfMoreStates];
            for (int i = 0; i < numberOfMoreStates; i++)
                moreButtons[i] = new MenuButtonGraphicClass();

            menuButtons = new MenuButtonGraphicClass[numberOfGameStates];
            for (int i = 0; i < numberOfGameStates; i++)
                menuButtons[i] = new MenuButtonGraphicClass();


        }

        public void Draw(StateClass state)
        {

            backGround.Draw(spriteBatch);
            switch (state.gameState)
            {
                case GameState.LOGO:
                    DrawLogo();
                    break;
                case GameState.MENU:
                    {
                        menuButtons[(int)(state.menuState)].HL = true;
                        for (int i = 0; i < numberOfGameStates; i++)
                            menuButtons[i].Draw(spriteBatch);
                        menuButtons[(int)(state.menuState)].HL = false;

                    }
                    break;
                case GameState.MORE:
                    {
                        moreButtons[(int)(state.moreState)].HL = true;
                        for (int i = 0; i < numberOfMoreStates; i++)
                            moreButtons[i].Draw(spriteBatch);
                        moreButtons[(int)(state.moreState)].HL = false;
                    }
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

            menuButtons[0].texture = Content.Load<Texture2D>("play");
            menuButtons[1].texture = Content.Load<Texture2D>("editor");
            menuButtons[2].texture = Content.Load<Texture2D>("more");
            menuButtons[3].texture = Content.Load<Texture2D>("exit");

            menuButtons[0].textureHL = Content.Load<Texture2D>("play_hl");
            menuButtons[1].textureHL = Content.Load<Texture2D>("editor_hl");
            menuButtons[2].textureHL = Content.Load<Texture2D>("more_hl");
            menuButtons[3].textureHL = Content.Load<Texture2D>("exit_hl");

            moreButtons[0].texture = Content.Load<Texture2D>("name");
            moreButtons[1].texture = Content.Load<Texture2D>("credits");
            moreButtons[2].texture = Content.Load<Texture2D>("control");

            moreButtons[0].textureHL = Content.Load<Texture2D>("name_hl");
            moreButtons[1].textureHL = Content.Load<Texture2D>("credits_hl");
            moreButtons[2].textureHL = Content.Load<Texture2D>("control_hl");

            //updating position to the center of screen
            Vector2 centeredPositon = new Vector2(spriteBatch.GraphicsDevice.Viewport.Width / 2, spriteBatch.GraphicsDevice.Viewport.Height / 2);
            //scaling to size of screen
            float scale = ((float)spriteBatch.GraphicsDevice.Viewport.Width / backGround.texture.Width) < ((float)spriteBatch.GraphicsDevice.Viewport.Height / backGround.texture.Height) ? ((float)spriteBatch.GraphicsDevice.Viewport.Width / backGround.texture.Width) : ((float)spriteBatch.GraphicsDevice.Viewport.Height / backGround.texture.Height);

            logo.position = centeredPositon;
            logo.scale = scale;

            backGround.position = centeredPositon;
            backGround.scale = scale;

            for (int i = 0; i < numberOfGameStates; i++)
            {
                menuButtons[i].position = centeredPositon + new Vector2(0 , menuButtons[i].texture.Height * (i-1.5f));
                menuButtons[i].scale = scale;
            }

            for (int i = 0; i < numberOfMoreStates; i++)
            {
                moreButtons[i].position = centeredPositon + new Vector2(0, moreButtons[i].texture.Height * (i - 1.5f));
                moreButtons[i].scale = scale;
            }

        }
        
        void DrawLogo()
        {
            logo.Draw(spriteBatch);
        }
    }
}
