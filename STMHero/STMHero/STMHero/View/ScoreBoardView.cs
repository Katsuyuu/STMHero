using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Model;
using Microsoft.Xna.Framework;
namespace STMHero.View
{
    class ScoreBoardView
    {
        private SongResources songProporties;

        public ScoreBoardView(SongResources _songResources)
        {
            songProporties = _songResources;
        }
        public TotalRecords totalRecords { get; set; }
        public void Draw()
        {

            songProporties.spriteBatch.Draw(songProporties.backGround, new Vector2(0, 0), Color.White);
            int height = 120;
            int index = 1;
            foreach (var Results in totalRecords.results)
            {
                songProporties.spriteBatch.DrawString(songProporties.boldFont, index.ToString(), new Vector2(70, height), Results.newResult?Color.Orange:Color.Black);
                songProporties.spriteBatch.DrawString(songProporties.boldFont, Results.Name, new Vector2(150, height), Results.newResult?Color.Orange:Color.Black);
                songProporties.spriteBatch.DrawString(songProporties.boldFont, Results.CurretScore.ToString(), new Vector2(400, height), Results.newResult ? Color.Orange : Color.Black);
                songProporties.spriteBatch.DrawString(songProporties.boldFont, Results.MaxCombo.ToString(), new Vector2(500, height), Results.newResult ? Color.Orange : Color.Black);
                height += 45;
                index++;
            }
        }
    }
}
