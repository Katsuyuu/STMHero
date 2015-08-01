using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using STMHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.View
{
    public class EditorView
    {
        private SongResources songResources;
        public EditorView(SongResources _songProporties)
        {
          
            songResources = _songProporties;
            SongStart();
        }

        public void SongStart()
        {
            try
            {
                MediaPlayer.Play(songResources.CurretSong);
            }
            catch
            { }
        }

        internal void DrawButton(int buttonNumber, Button x)
        {
            if (x.active == false)
                x.buttonImage = songResources.buttons.ElementAt(buttonNumber).texture;
            else
                x.buttonImage = songResources.pushedButtons.ElementAt(buttonNumber).texture;
            x.Draw(songResources.spriteBatch);
        }
        public void Draw()
        {
            songResources.spriteBatch.Draw(songResources.ButtonWay, new Vector2(135, 40), Color.White);
            songResources.spriteBatch.Draw(songResources.checkRectangle, new Vector2(135 + (songResources.checkRectangle.Width / 2), 20), null, Color.White, 0f, new Vector2(songResources.checkRectangle.Width / 2, 0), 0.5f, SpriteEffects.None, 0);
            songResources.spriteBatch.Draw(songResources.buttonP, new Vector2(1200, 415), Color.White);
            songResources.spriteBatch.Draw(songResources.buttonA, new Vector2(1200, 450), Color.White);
            songResources.spriteBatch.Draw(songResources.buttonS, new Vector2(1200, 490), Color.White);
            songResources.spriteBatch.Draw(songResources.buttonD, new Vector2(1200, 530), Color.White);
            songResources.spriteBatch.Draw(songResources.buttonF, new Vector2(1200, 570), Color.White);
            songResources.spriteBatch.Draw(songResources.buttonEsc, new Vector2(1200, 617), Color.White);
            songResources.spriteBatch.Draw(songResources.pushedButtons[0].texture, new Vector2(1100, 450), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songResources.spriteBatch.Draw(songResources.pushedButtons[1].texture, new Vector2(1100, 490), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songResources.spriteBatch.Draw(songResources.pushedButtons[2].texture, new Vector2(1100, 530), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songResources.spriteBatch.Draw(songResources.pushedButtons[3].texture, new Vector2(1100, 570), null, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0);
            songResources.spriteBatch.DrawString(songResources.font, "EXIT", new Vector2(1100, 610), Color.White);
            songResources.spriteBatch.DrawString(songResources.font, "SAVE", new Vector2(1100, 410), Color.White);

            
        }
    }
}
