using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using STMHero.Model;
using STMHero.View;
using STMHero.GameLogic;
using System.Threading;

namespace STMHero.Controler
{
    public class GMClass
    {
        public GameGraphicClass gameGraphic;
        public GameStageController songPlayer;
        //public EditorView editorView;

        public SongResources songResources;

        public StateClass state { get; set; }

        //tutaj zczytujemy klawisz i dobieramy odpowiednie zachowaie do stanu gry
        public bool CheckButton(KeyboardState keyboardState)
        {
            // jak gramy to obslugą klawiatury zajmuje sie songPlayer
            if (state.gameState == GameState.PLAYING || state.gameState == GameState.EDITING)
            {
                songPlayer.Update(keyboardState);
            }


            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                switch (state.gameState)
                {
                    case GameState.LOGO:
                        break;
                    case GameState.MENU:
                        break;
                    case GameState.PLAYING:
                        break;
                    case GameState.EDITING:
                        break;
                    case GameState.NAME:
                        state.gameState = GameState.MENU;
                        break;
                    case GameState.CREDITS:
                        state.gameState = GameState.MORE;
                        break;
                    case GameState.CONTROL:
                        break;
                    case GameState.MORE:
                        state.gameState = GameState.MENU;
                        break;
                    default:
                        break;
                }
            }

            if (state.gameState == GameState.NAME)
            {
                NameManager.CheckKey(keyboardState);
            }

            // dla klawisza ENTER
            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                switch (state.gameState)
                {
                    case GameState.NAME:
                        state.gameState = GameState.MENU;
                        break;
                    // jak wyswietlamy logo to przelaczamy się na wyswietlanie menu
                    case GameState.LOGO:
                        state.gameState = GameState.MENU;
                        break;
                    // jak wyswietlamy menu to w zaleznosci od tego co jest zaznaczone
                    case GameState.MENU:
                        switch (state.menuState)
                        {
                            //wyswietlamy liste piosenek
                            case MenuState.PLAY:
                                {
                                    // TUTAJ MAGIA DYSPUTA                                

                                    songPlayer = new GameStageController(songResources,true);
                                    state.gameState = GameState.PLAYING;
                                }
                                break;
                            // wyswietlamy creditsy
                            case MenuState.MORE:
                                {
                                    state.gameState = GameState.MORE;
                                }
                                break;
                            // wychodzimy z gry
                            case MenuState.EXIT:
                                return true;
                            // dys se wlacza editor
                            case MenuState.EDITOR:
                                {
                                    songPlayer = new GameStageController(songResources, false);
                                    
                                    state.gameState = GameState.EDITING;
                                }
                                break;
                            default:
                                break;
                        }
                        break;
                    
                    //Dodatkowe opcje
                    case GameState.MORE:
                        {
                            switch (state.moreState)
                            {
                                case MoreState.NAME:
                                    state.gameState = GameState.NAME;
                                    break;
                                case MoreState.CREDITS:
                                    state.gameState = GameState.CREDITS;
                                    break;
                                case MoreState.CONTROL:
                                    Thread thread = new Thread(VirtualComPort.Connect);
                                    thread.Start();
                                    break;
                                default:
                                    break;
                            }
                        }   
                        break;
                    // jak gramy piosenka (tu pewnie zostanie puste - nic nie robimy jak klikniemy enter grając w grę )
                    case GameState.PLAYING:
                        break;
                    default:
                        break;
                }
            }


            // dla klawisza UP
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                switch (state.gameState)
                {
                    case GameState.LOGO:
                        break;
                    case GameState.MENU:
                        {
                            int temp = (int)state.menuState - 1;
                            if (temp < 0)
                                temp = 3;
                            state.menuState = (MenuState)temp;
                        }
                        break;
                    case GameState.MORE:
                        {
                            int temp = (int)state.moreState - 1;
                            if (temp < 0)
                                temp = 2;
                            state.moreState = (MoreState)temp;
                        }
                        break;
                    case GameState.PLAYING:
                        break;
                    default:
                        break;
                }
            }

            //Dla klawisza DOWN
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
            {
                switch (state.gameState)
                {
                    case GameState.LOGO:
                        break;
                    case GameState.MENU:
                        {
                            int temp = (int)state.menuState + 1;
                            if (temp > 3)
                                temp = 0;
                            state.menuState = (MenuState)temp;
                        }
                        break;
                    case GameState.MORE:
                        {
                            int temp = (int)state.moreState + 1;
                            if (temp > 2)
                                temp = 0;
                            state.moreState = (MoreState)temp;
                        }
                        break;
                    case GameState.PLAYING:
                        break;
                    default:
                        break;
                }
            }

            return false;
        }

        public GMClass()
        {
            state = new StateClass();
            gameGraphic = new GameGraphicClass();
            // TUTAJ MAGIA DYSPUTA
            songResources = new SongResources();
        }

        public void Update()
        {
            if (state.gameState == GameState.PLAYING || state.gameState == GameState.EDITING)
            {
                if (songPlayer._currentStage == GameStages.MENU)
                    state.gameState = GameState.MENU;
                songPlayer.Update();
            }

        }

        public void Draw()
        {
            gameGraphic.Draw(state);
            switch (state.gameState)
            {
                case GameState.LOGO:
                    break;
                case GameState.MENU:
                    break;
                case GameState.PLAYING:
                    songPlayer.Draw();
                    break;
                case GameState.EDITING:
                    songPlayer.Draw();
                    break;
                case GameState.NAME:
                    NameManager.Draw();
                    break;
                case GameState.CREDITS:
                    Credits.Draw();
                    break;
                case GameState.CONTROL:
                    break;
                case GameState.MORE:
                    break;
                default:
                    break;
            }
        }
    }
}