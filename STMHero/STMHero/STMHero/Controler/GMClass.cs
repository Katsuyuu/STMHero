using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.View;
using Microsoft.Xna.Framework.Input;

namespace STMHero.Controler
{
    public class GMClass
    {
        public GameGraphicClass gameGraphic;
        GameState gameState { get; set; }
        
        public void ButtonPressed(Keys key)
        {
            switch (key)
            {
                case Keys.Enter:
                    {
                        switch (gameState)
                        {
                            case GameState.LOGO:
                                gameState = GameState.MENU;
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
                        break;
                    }
                default:
                    break;
            }
        }

        public GMClass()
        {
            gameGraphic = new GameGraphicClass();
            gameState = GameState.LOGO;
        }

        public void Draw()
        {
            gameGraphic.Draw(gameState);
        }

    }
}
