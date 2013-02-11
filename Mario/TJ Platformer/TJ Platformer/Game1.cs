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

namespace Mario
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Mario mario = new Mario(new Vector2(8, 8));
        public static Random rand = new Random();
        public static List<Object> coins = new List<Object>();
        public static List<Object> powerUps = new List<Object>();
        public static List<Object> blocks = new List<Object>();
        public static SpriteFont timesNewRoman;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            timesNewRoman = Content.Load<SpriteFont>("Times New Roman");
            for (int a = 0; a < graphics.PreferredBackBufferWidth / 16; a++)
                blocks.Add(new ShineBlock(new Vector2(16 * a + 8, graphics.PreferredBackBufferHeight - 8)));
            for (int a = 0; a < 6; a++)
                //Ask Connor why the random is making one number, and how to fix it!
                blocks.Add(new PowerUpBlock(new Vector2(16 * 20 + 8 + (16 * a), graphics.PreferredBackBufferHeight - 56)));
            blocks.Add(new CoinShineBlock(new Vector2(16 * 20 + 8, graphics.PreferredBackBufferHeight - 16 * 7 - 8)));
            for (int a = 0; a < 50; a++)
                coins.Add(new Coin(new Vector2(0, 0)));
            for (int a = 0; a < 15; a++)
                powerUps.Add(new PowerUp(new Vector2(0, 0)));
            foreach (Object o in blocks)
                o.LoadContent(Content);
            foreach (Coin c in coins)
                c.LoadContent(Content);
            foreach (PowerUp c in powerUps)
                c.LoadContent(Content);
            mario.LoadContent(Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            InputDevice.Update();
            foreach (Object o in blocks)
                o.Update();
            foreach (Coin c in Game1.coins)
                c.Update();
            foreach (PowerUp c in Game1.powerUps)
                c.Update(mario);
            mario.Update(Content);
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            foreach (Object o in blocks)
                o.Draw(spriteBatch);
            foreach (Coin c in coins)
                c.Draw(spriteBatch);
            foreach (PowerUp c in powerUps)
                c.Draw(spriteBatch);
            mario.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
