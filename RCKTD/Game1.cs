using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace RCKTD
{
    public class Game1 : Game
    {

        private const float gravity = 0.1f;
        private const float thrust = 0.2f;

        private Vector2 shipPos;
        private Vector2 shipVel;

        private Texture2D shipTexture;
        private Texture2D wallTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Rectangle> surfaces = new List<Rectangle>();

        private Rectangle ScreenSize => _graphics.GraphicsDevice.Viewport.Bounds;

        private Rectangle ShipBounds => new Rectangle((int)Math.Round(shipPos.X), (int)Math.Round(shipPos.Y), shipTexture.Width, shipTexture.Height);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            surfaces.Add(new Rectangle(0, ScreenSize.Height - 10, ScreenSize.Width, 10));

            InitShip();

            base.Initialize();
        }

        private void InitShip()
        {
            shipPos = new Vector2(ScreenSize.Width / 3, ScreenSize.Height / 2);
            shipVel = Vector2.Zero;
        }

        // TODO: use this.Content to load your game content here
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipTexture = Content.Load<Texture2D>("ship");

            wallTexture = new Texture2D(GraphicsDevice, 1, 1);
            wallTexture.SetData(new Color[] { Color.Green });

        }

        // TODO: Add your update logic here
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (!ScreenSize.Intersects(ShipBounds))
            {
                InitShip();
                return;
            }

            var shipAcc = new Vector2(0, gravity);

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                shipAcc.Y -= thrust;
            }

            shipVel += shipAcc;
            shipPos += shipVel;

            foreach (var sur in surfaces)
            {
                if (ShipBounds.Intersects(sur))
                {
                    shipVel = Vector2.Zero;
                    shipPos.Y = sur.Top - shipTexture.Height;
                }
            }

            base.Update(gameTime);
        }

        // TODO: Add your drawing code here
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(shipTexture, shipPos, Color.White);

            foreach (var sur in surfaces)
            {
                _spriteBatch.Draw(wallTexture, sur, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
