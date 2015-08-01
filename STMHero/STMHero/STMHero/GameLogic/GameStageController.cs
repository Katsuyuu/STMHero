using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using STMHero.Controler;
using STMHero.Model;
using STMHero.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.GameLogic
{

    public class GameStageController
    {
        #region Variables
        internal GameStages _currentStage;
        private SongResources songResources;
        private bool EndGame;
        #endregion
        #region Windows and Controller
        private IGame actualSongType;
        private ScoreBoardView scoreBoardView;
        private PauseMenuView pauseMenuView;
        #endregion


        public GameStageController(SongResources proporties, bool isGameEnable)
        {
            songResources = proporties;
            _currentStage = GameStages.PLAYING;
            if (isGameEnable)
                actualSongType = new SongController(proporties);
            else
                actualSongType = new Editor(proporties);

        }
        public void Update()
        {
            if (!EndGame)
                CheckEndGame();
            if (_currentStage == GameStages.PLAYING)
                actualSongType.Update();
        }
        public void Update(KeyboardState keyboardState)
        {
            //zakonczenie gry 
            if (!EndGame)
                CheckEndGame();
            if (EndGame)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                    _currentStage = GameStages.MENU;
            }
            // opcja dla pauzy oraz powrotu do dalszej gry
            else
            {
                if (keyboardState.IsKeyDown(Keys.Escape))
                {
                    if (_currentStage == GameStages.PLAYING)
                    {
                        _currentStage = GameStages.PAUSE;
                        pauseMenuView = new PauseMenuView(songResources);
                        MediaPlayer.Pause();
                    }
                    else
                    {
                        _currentStage = GameStages.PLAYING;
                        MediaPlayer.Resume();
                    }
                }
                if (pauseMenuView != null)
                    if (pauseMenuView.Exit == true)
                    {
                        if (pauseMenuView.pauseStages == PauseStages.RETURN)
                        {
                            _currentStage = GameStages.PLAYING;
                            MediaPlayer.Resume();
                        }
                        else
                            _currentStage = GameStages.MENU;
                    }
                    else
                    {
                        pauseMenuView.Update(keyboardState);
                    }

                if (_currentStage == GameStages.PLAYING)
                    actualSongType.Update(keyboardState);
            }
        }
        public void Draw()
        {
            if (_currentStage == GameStages.PLAYING)
                actualSongType.Draw();
            if (_currentStage == GameStages.SCOREBOARD)
                scoreBoardView.Draw();
            if (_currentStage == GameStages.PAUSE)
                pauseMenuView.Draw();

        }

        #region Methods
        private void CheckEndGame()
        {
            if (actualSongType.End)
            {
                if (actualSongType is SongController)
                {
                    _currentStage = GameStages.SCOREBOARD;
                    scoreBoardView = new ScoreBoardView(songResources);
                    DrawResults();
                }
                else
                    _currentStage = GameStages.MENU;
                MediaPlayer.Stop();
                EndGame = true;
            }

        }

        private void DrawResults()
        {

            var temp = ManageResults();
            scoreBoardView.totalRecords = temp;
            FileManager.SaveResults("Results.xml", temp);

        }

        //TEST
        private TotalRecords ManageResults()
        {
            if (actualSongType is SongController)
            {
                var temp = FileManager.LoadResults("Results.xml");
                foreach (GameResult item in temp.results)
                {
                    if (item.newResult == true)
                        item.newResult = false;
                }
                actualSongType.Results.Name = NameManager.GetName();
                actualSongType.Results.newResult = true;
                temp.results.Add(actualSongType.Results);

                temp.results.Sort(delegate(GameResult x, GameResult y)
                {
                    return y.CurretScore.CompareTo(x.CurretScore);
                });

                if (temp.results.Count > 10)
                    temp.results = temp.results.
                        Select((n) => n).
                        Take(10)
                        .ToList();
                return temp;
            }
            else
                return null;
        }

        #endregion

    }
}
