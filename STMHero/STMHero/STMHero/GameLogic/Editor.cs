using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using STMHero.GameLogic;
using STMHero.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Xml.Serialization;

namespace STMHero.Model
{
    public class Editor : IGame
    {

        public bool End { get; private set; }
        public GameResult Results { get; set; }
        public readonly List<Button> GameButtons = new List<Button>();
        public readonly List<Button> ButtonInEditor = new List<Button>();
        private MyTimer gameTimer;
        private EditorView editorView;
        STMKeyboardState now;
        STMKeyboardState last = new STMKeyboardState();

        public Editor(SongResources songResources)
        {
            editorView = new EditorView(songResources);
            SetGameTimer(59700);

        }
        internal void AddButton(buttonEnum type, float x)
        {
            Button tempButton = new Button()
            {
                active = false,
                Type = type,
                timeSpawn = MediaPlayer.PlayPosition.Seconds * 1000 + MediaPlayer.PlayPosition.Milliseconds,
                buttonPosition = new Vector2(x, 40)
            };
            GameButtons.Add(tempButton);
            ButtonInEditor.Add(tempButton);
        }

        public void Update()
        {
            if (VirtualComPort.isConnected)
            {
                now = VirtualComPort.GetState();
                if (now.A != last.A || now.S != last.S || now.D != last.D || now.F != last.F)
                {

                    if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.A))
                        AddButton(buttonEnum.blue, 362.5f);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.S))
                        AddButton(buttonEnum.red, 462.5f);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.D))
                        AddButton(buttonEnum.green, 562.5f);
                    else if (VirtualComPort.keyboardStateSTM.IsKeyDown(Keys.F))
                        AddButton(buttonEnum.yellow, 662.5f); ;

                    last = now;
                }
            }
            ReloadElements();
        }
        private void ReloadElements()
        {
            foreach (Button x in ButtonInEditor)
                x.Update((MediaPlayer.PlayPosition.Seconds * 1000 + MediaPlayer.PlayPosition.Milliseconds));
            if (ButtonInEditor.Count != 0)
            {
                for (int i = 0; i < ButtonInEditor.Count; i++)
                {
                    if (ButtonInEditor.ElementAt(i).buttonPosition.Y > 600)
                    {
                        ButtonInEditor.ElementAt(i).buttonPosition = new Vector2(ButtonInEditor.ElementAt(i).buttonPosition.X, 40);
                        ButtonInEditor.RemoveAt(i);
                    }
                }
            }
        }
        public void Update(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.A))
            {
                AddButton(buttonEnum.blue, 362.5f);
            }
            else if (keyboardState.IsKeyDown(Keys.S))
            {
                AddButton(buttonEnum.red, 462.5f);
            }
            else if (keyboardState.IsKeyDown(Keys.D))
            {
                AddButton(buttonEnum.green, 562.5f);

            }
            else if (keyboardState.IsKeyDown(Keys.F))
            {
                AddButton(buttonEnum.yellow, 662.5f);
            }
            else if (keyboardState.IsKeyDown(Keys.P))
            {
                Save();
            }
            Update();
        }

        internal void Save()
        {
            //testy
            foreach (var x in GameButtons)
            {
                x.buttonPosition = new Vector2(x.buttonPosition.X, 40);
            }
            FileManager.SaveSong("AFI", "Edytowana piosenka AFI", GameButtons);
            End = true;
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
            End = true;
            Save();
        }

        public void Draw()
        {
            editorView.Draw();
            if (ButtonInEditor.Count != 0)
                foreach (Button x in ButtonInEditor)
                {
                    if (x.Type == buttonEnum.blue)
                    {
                        editorView.DrawButton(0, x);
                    }
                    else if (x.Type == buttonEnum.green)
                    {
                        editorView.DrawButton(1, x);
                    }
                    else if (x.Type == buttonEnum.red)
                    {
                        editorView.DrawButton(2, x);
                    }
                    else
                    {
                        editorView.DrawButton(3, x);
                    }
                }
        }
    }
}
