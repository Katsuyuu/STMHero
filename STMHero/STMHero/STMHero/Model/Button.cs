using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;


namespace STMHero.Model
{
    public class Button
    {
        List<float> test = new List<float>();
        public float timeSpawn { get; set; }  //milisec
        public bool active { get; set; }
        public Texture2D buttonImage { get; set; }
        public Vector2 buttonPosition { get; set; }
        public buttonEnum Type { get; set; }
        public float scale { get; set; }
        public bool CorrectClick { get; set; }

        public int width
        {
            get
            {
                return buttonImage.Width;
            }
        }
        public int height
        {
            get
            {
                return buttonImage.Height;
            }
        }

        public void Update(float time)
        {

            switch (Type)
            {
                case buttonEnum.blue:
                    {
                        float tempX = 362.5f - (150f * ((buttonPosition.Y - 40f) / 430f));
                        tempX = MathHelper.Clamp(tempX, 212.5f, 362.5f);
                        buttonPosition = new Vector2(tempX,40 +((time-timeSpawn)/1000)*205);
                        break;

						}
                case buttonEnum.red:
                    {
                        float tempX = 462.5f - (50f * ((buttonPosition.Y - 40f) / 430f));
                        tempX = MathHelper.Clamp(tempX, 412.5f, 462.5f);
                        buttonPosition = new Vector2(tempX, 40 + ((time - timeSpawn) / 1000) * 205); 
                        break;
                    }
                case buttonEnum.green:
                    {
                        float tempX = 562.5f + (50f * ((buttonPosition.Y - 40f) / 430f));
                        tempX = MathHelper.Clamp(tempX, 562.5f, 612.5f);
                        buttonPosition = new Vector2(tempX, 40 + ((time - timeSpawn) / 1000) * 205); 
                        break;
                    }
                case buttonEnum.yellow:
                    {

                        float tempX = 662.5f + (150f * ((buttonPosition.Y - 40f) / 430f));
                        tempX = MathHelper.Clamp(tempX, 662.5f, 812.5f);
                        buttonPosition = new Vector2(tempX, 40 + ((time - timeSpawn) / 1000) * 205);
                        break;
                    }
            }

            //ustalanie skali ze wzgledu na pionowa pozycje przycisku
            float tempScale = 0.5f * ((buttonPosition.Y - 40) / 360);
            scale = 0.5f + tempScale;
            //zeby skala nie byla wieksza niz 1
            scale = MathHelper.Clamp(scale, 0f, 1f);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active == true && CorrectClick == false)
            {

                spriteBatch.Draw(buttonImage, buttonPosition, null, Color.Red, 0f, new Vector2(buttonImage.Width / 2, buttonImage.Height / 2), scale, SpriteEffects.None, 0);
            }

            else
                spriteBatch.Draw(buttonImage, buttonPosition, null, Color.White, 0f, new Vector2(buttonImage.Width / 2, buttonImage.Height / 2), scale, SpriteEffects.None, 0);


        }

    }
}
