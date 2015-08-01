using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using STMHero.Model;
using System.Timers;
using STMHero.Controler;

namespace STMHero.View
{
    public class SongView 
    {
        #region Values
       
        public SongResources songProporties;
 
      

        public readonly List<Button> ButtonInGame = new List<Button>();
        #endregion
        public SongView(SongResources proporties)
        {
            songProporties = proporties;
            SongStart();
        }


        private void SongStart()
        {
            try
            {
                MediaPlayer.Play(songProporties.CurretSong);
            }
            catch
            {
                // wyswietlenie na ekranie komunikatu o problemie piosenki 
            }
        }
        internal void FailEffect()
        {
            songProporties.guitarFail.Play();
        }



        internal void DrawButton(int buttonNumber, Button x)
        {
            if (x.active == false)
                x.buttonImage = songProporties.buttons.ElementAt(buttonNumber).texture;
            else
                x.buttonImage = songProporties.pushedButtons.ElementAt(buttonNumber).texture;

            x.Draw(songProporties.spriteBatch);
        }
        public void Draw(GameResult Results)
        {
            songProporties.spriteBatch.Draw(songProporties.results, new Vector2(1000, 17), Color.White);
            songProporties.spriteBatch.Draw(songProporties.ButtonWay, new Vector2(135, 40), Color.White);
            songProporties.spriteBatch.Draw(songProporties.checkRectangle, new Vector2(135, 420), Color.White);
            songProporties.spriteBatch.DrawString(songProporties.boldFont, Results.CurretScore.ToString(), new Vector2(1150, 32), Color.Black);
            songProporties.spriteBatch.DrawString(songProporties.boldFont, Results.CurretCombo.ToString(), new Vector2(1150, 102), Color.Black);
            songProporties.spriteBatch.DrawString(songProporties.font, Results.CurretScore.ToString(), new Vector2(1150, 32), Color.White);
            songProporties.spriteBatch.DrawString(songProporties.font, Results.CurretCombo.ToString(), new Vector2(1150, 102), Color.White);
            songProporties.spriteBatch.Draw(songProporties.buttonA, new Vector2(1200, 450), Color.White);
            songProporties.spriteBatch.Draw(songProporties.buttonS, new Vector2(1200, 490), Color.White);
            songProporties.spriteBatch.Draw(songProporties.buttonD, new Vector2(1200, 530), Color.White);
            songProporties.spriteBatch.Draw(songProporties.buttonF, new Vector2(1200, 570), Color.White);
            songProporties.spriteBatch.Draw(songProporties.buttonEsc, new Vector2(1200, 617), Color.White);
            songProporties.spriteBatch.Draw(songProporties.pushedButtons[0].texture, new Vector2(1100,450), null, Color.White, 0f, new Vector2(0,0), 0.5f, SpriteEffects.None, 0);
            songProporties.spriteBatch.Draw(songProporties.pushedButtons[1].texture, new Vector2(1100, 490), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songProporties.spriteBatch.Draw(songProporties.pushedButtons[2].texture, new Vector2(1100, 530), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songProporties.spriteBatch.Draw(songProporties.pushedButtons[3].texture, new Vector2(1100, 570), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songProporties.spriteBatch.DrawString(songProporties.font, "EXIT", new Vector2(1100, 610), Color.White);
            
        }
    }
}
