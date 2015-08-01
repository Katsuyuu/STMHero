using System.Collections.Generic;
using System.Linq;
using STMHero.Model;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Timers;
using STMHero.View;
using STMHero.GameLogic;
namespace STMHero.Controler
{
    public class SongController : IGame
    {
        internal SongView songPlayer;
        public SongManager CurretSong;

        public readonly List<Button> ButtonInGame = new List<Button>();
        private MyTimer gameTimer;
        public bool End { get; private set; }
        public GameResult Results { get; set; }
        private int curretElement = 0;


        STMKeyboardState now;
        STMKeyboardState last = new STMKeyboardState();


        public SongController(SongResources proporties)
        {
            songPlayer = new SongView(proporties);
            Results = new GameResult();
            CurretSong = FileManager.LoadSong("AFI.xml");
            SetGameTimer(59700);
        }

        private void SetGameTimer(int interval)
        {
            gameTimer = new MyTimer(interval);
            gameTimer.AutoReset = false;
            gameTimer.Elapsed += EndSong;
            gameTimer.Start();
        }



        private void EndSong(object sender, ElapsedEventArgs e)
        {
            if (Results.CurretCombo > Results.MaxCombo)
                Results.SetNewHighScore(Results.CurretCombo);
            End = true;
        }

        public void Update()
        {
            if (VirtualComPort.isConnected)
            {
                now = VirtualComPort.GetState();
                if (now.A != last.A || now.S != last.S || now.D != last.D || now.F != last.F)
                {

                    if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.A))
                        UpdateButtons(buttonEnum.blue);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.S))
                        UpdateButtons(buttonEnum.red);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.D))
                        UpdateButtons(buttonEnum.green);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.F))
                        UpdateButtons(buttonEnum.yellow);

                    last = now;
                }
            }

            LoadNewElements();
            ChangeList();
        }
        public void Update(KeyboardState keyboardState)
        {
            if (!VirtualComPort.isConnected)
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    UpdateButtons(buttonEnum.blue);

                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    UpdateButtons(buttonEnum.red);

                }
                else if (keyboardState.IsKeyDown(Keys.D))
                {

                    UpdateButtons(buttonEnum.green);

                }
                else if (keyboardState.IsKeyDown(Keys.F))
                {
                    UpdateButtons(buttonEnum.yellow);
                }
            Update();
        }

        /// <summary>
        /// Sprawdzanie pozycji pierwszego elementu z kolekcji oraz odpowiednia obrobka jego
        /// </summary>
        public void UpdateButtons(buttonEnum type)
        {
            var x = EnumerateList(type);
            if (x.Count() == 0)
            {
                LostCombo();
            }
            else
            {
                Button timerButton = getFirstElement(x, type);
                if (timerButton.buttonPosition.Y < 430 || timerButton.buttonPosition.Y > 520)
                {
                    LostCombo();
                    timerButton.CorrectClick = false;
                }
                else
                {
                    Results.AddClick();
                    AddPoints();
                    timerButton.CorrectClick = true;
                }
                setTimer(timerButton);
            }
        }

        public void Draw()
        {
            songPlayer.Draw(Results);
            if (ButtonInGame.Count != 0)
                for (int i = 0; i < ButtonInGame.Count; i++)
                {
                    Button x = ButtonInGame.ElementAt(i);
                    if (x.Type == buttonEnum.blue)
                    {
                        songPlayer.DrawButton(0, x);
                    }
                    else if (x.Type == buttonEnum.green)
                    {
                        songPlayer.DrawButton(1, x);
                    }
                    else if (x.Type == buttonEnum.red)
                    {
                        songPlayer.DrawButton(2, x);
                    }
                    else
                    {
                        songPlayer.DrawButton(3, x);
                    }
                }
        }


        #region methods

        private Button getFirstElement(IEnumerable<Button> x, buttonEnum type)
        {
            Button Min = x.First();
            if (x.Count() > 1)
                for (int i = 1; i < x.Count(); i++)
                {
                    if (x.ElementAt(i).timeSpawn <= Min.timeSpawn && x.ElementAt(i).Type == type && x.ElementAt(i).buttonPosition.Y >= 450)
                        Min = x.ElementAt(i);
                }
            return Min;
        }

        private IEnumerable<Button> EnumerateList(buttonEnum type)
        {
            var x = from files in ButtonInGame
                    where files.buttonPosition.Y <= 600 && files.Type == type && files.active == false
                    select files;
            return x;
        }


        private void AddPoints()
        {
            Results.AddCombo();
            if (Results.CurretCombo >= 20 && Results.CurretCombo <= 50)
                Results.AddPoints(Results.Points * 2);
            else if (Results.CurretCombo > 50)
                Results.AddPoints(Results.Points * 5);
            else
                Results.AddPoints(Results.Points);
        }

        private void LostCombo()
        {
            songPlayer.FailEffect();
            if (Results.CurretCombo > Results.MaxCombo)
                Results.SetNewHighScore(Results.CurretCombo);
            Results.LostCombo();
        }
        /// <summary>
        /// Zmiana pozycji kazdego z przycisku, ewentualne usuniecie przycisku w przypadku gdy wyjdzie poza odpowiedni obszar
        /// </summary>
        internal void ChangeList()
        {
            for (int i = 0; i < ButtonInGame.Count; i++)
            {
                try
                {
                    ButtonInGame.ElementAt(i).Update((MediaPlayer.PlayPosition.Seconds * 1000 + MediaPlayer.PlayPosition.Milliseconds + 2000));

                }
                catch
                {
                    break;
                }
            }

            if (ButtonInGame.Count != 0)
            {
                for (int i = 0; i < ButtonInGame.Count; i++)
                {
                    // czasami blad wystepuje przy uzyskaniu i wiekszego niz wielkosc kolekcji
                    if (i > ButtonInGame.Count)
                        continue;
                    if (ButtonInGame.ElementAt(i).buttonPosition.Y > 600)
                    {
                        Results.LostCombo();
                        ButtonInGame.RemoveAt(i);
                    }
                }
            }
        }


        internal void LoadNewElements()
        {
            int l = MediaPlayer.PlayPosition.Seconds * 1000 + MediaPlayer.PlayPosition.Milliseconds + 2000;
            if (curretElement < CurretSong.songComposition.Count)

                if (CurretSong.songComposition.ElementAt(curretElement).timeSpawn <= l)
                {
                    ButtonInGame.Add(CurretSong.songComposition.ElementAt(curretElement));
                    curretElement++;

                }

        }

        private void setTimer(Button x)
        {
            x.active = true;
            MyTimer t = new MyTimer(150);
            t.Tag = x;
            t.Elapsed += t_Elapsed;
            t.Start();
        }

        private void t_Elapsed(object sender, ElapsedEventArgs e)
        {
            MyTimer timer = (MyTimer)sender;
            Button x = (Button)timer.Tag;

            ButtonInGame.Remove(x);
        }

        #endregion



    }
}
