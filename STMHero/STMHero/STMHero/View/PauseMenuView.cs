using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using STMHero.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STMHero.View
{
    public class PauseMenuView
    {
        public PauseStages pauseStages;
        private SongResources songResources;
        public bool Exit;
        public PauseMenuView(SongResources _songResources)
        {
            songResources = _songResources;
            pauseStages = PauseStages.RETURN;
            Exit = false;
        }

        public void Update(KeyboardState keyboardState)
        {
            if(keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down))
            {
                if (pauseStages == PauseStages.RETURN)
                    pauseStages = PauseStages.QUIT;
                else
                    pauseStages = PauseStages.RETURN;
            }
            if (keyboardState.IsKeyDown(Keys.Enter))
                Exit = true;
        }

        public void Draw()
        {
            if (pauseStages == PauseStages.RETURN)
            {
                songResources.spriteBatch.Draw( songResources.choosenPlay ,new Vector2(50,100), null, Color.White);
                songResources.spriteBatch.Draw(songResources.exit, new Vector2(50, 300), null, Color.White);
             }
            else
            {
                songResources.spriteBatch.Draw(songResources.play, new Vector2(50, 100), null, Color.White);
                songResources.spriteBatch.Draw(songResources.choosenExit, new Vector2(50, 300), null, Color.White);
            }

            songResources.spriteBatch.Draw(songResources.logo, new Vector2(300, 70), null, Color.White); 
        }
    }
}
