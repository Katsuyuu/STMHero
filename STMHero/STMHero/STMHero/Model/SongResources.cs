using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STMHero.Controler;
using STMHero.View;
using STMHero.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace STMHero.Model
{
    public class SongResources
    {
        public SpriteBatch spriteBatch;

        public List<GraphicClass> buttons;
        public List<GraphicClass> pushedButtons;

        public SoundEffect guitarFail;
        public Song CurretSong;


        public Texture2D ScoreBoard;
        public Texture2D ButtonWay;
        public Texture2D checkRectangle;
        public SpriteFont font;
        public SpriteFont boldFont;

        #region menu buttons
        public Texture2D play;
        public Texture2D exit;
        public Texture2D choosenPlay;
        public Texture2D choosenExit;
        public Texture2D backGround;
        public Texture2D results;
        //buttons to help player
        public Texture2D buttonA;
        public Texture2D buttonS;
        public Texture2D buttonD;
        public Texture2D buttonF;
        public Texture2D buttonP;
        public Texture2D buttonEsc;
        
        #endregion

        public Texture2D logo;
        public SongResources()
        {
            buttons = new List<GraphicClass>();
            pushedButtons = new List<GraphicClass>();
        }

        public void LoadContent(SpriteBatch spriteBatch, ContentManager Content)
        {
            this.spriteBatch = spriteBatch;

            for (int i = 0; i < 4; i++)
            {
                buttons.Add(new GraphicClass());
                pushedButtons.Add(new GraphicClass());
            }
            buttons[0].texture = Content.Load<Texture2D>("blue_np");
            buttons[1].texture = Content.Load<Texture2D>("green_np");
            buttons[2].texture = Content.Load<Texture2D>("red_np");
            buttons[3].texture = Content.Load<Texture2D>("yellow_np");

            pushedButtons[0].texture = Content.Load<Texture2D>("blue_p");
            pushedButtons[1].texture = Content.Load<Texture2D>("green_p");
            pushedButtons[2].texture = Content.Load<Texture2D>("red_p");
            pushedButtons[3].texture = Content.Load<Texture2D>("yellow_p");

            guitarFail = Content.Load<SoundEffect>("Music/guitarFail");
            CurretSong = Content.Load<Song>("Music/AFI");

            ScoreBoard = Content.Load<Texture2D>("plank");
            ButtonWay = Content.Load<Texture2D>("ButtonBG");
            checkRectangle = Content.Load<Texture2D>("ButtonCB");
            font = Content.Load<SpriteFont>("Font/specifiedFont");
            boldFont = Content.Load<SpriteFont>("Font/boldSpecifiedFont");
            exit = Content.Load<Texture2D>("exit");
            play = Content.Load<Texture2D>("play");
            choosenExit = Content.Load<Texture2D>("exit_hl");
            choosenPlay = Content.Load<Texture2D>("play_hl");
            logo = Content.Load<Texture2D>("logo");
            backGround = Content.Load<Texture2D>("GHscoreboard");
            results = Content.Load<Texture2D>("wyniki");
            buttonA = Content.Load<Texture2D>("A");
            buttonS = Content.Load<Texture2D>("S");
            buttonD = Content.Load<Texture2D>("D");
            buttonF = Content.Load<Texture2D>("F");
            buttonP = Content.Load<Texture2D>("P");
            buttonEsc = Content.Load<Texture2D>("ESC");
        }

    }
}