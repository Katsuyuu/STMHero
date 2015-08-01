using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using STMHero.View;
using STMHero.Controler;
using STMHero.Model;

namespace STMHero
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GMClass GM;
        KeyboardState lastKeyboardState;

        public Game1()
        {
            lastKeyboardState = Keyboard.GetState();
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";


            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f); //  metoda mowiaca ile razy na sec musi wykonac sie update


            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
        }

        protected override void Initialize()
        {
            GM = new GMClass();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GM.gameGraphic.LoadContent(spriteBatch, Content);
            GM.songResources.LoadContent(spriteBatch, Content);
            Credits.LoadContent(spriteBatch, Content);
            NameManager.LoadContent(spriteBatch, Content);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (lastKeyboardState != Keyboard.GetState())
            {
                lastKeyboardState = Keyboard.GetState();
                if (GM.CheckButton(lastKeyboardState))
                    this.Exit();
            }

            GM.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            GM.Draw();

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}