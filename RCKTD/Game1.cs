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
        private const float rotation = 0.01f;

        private const float allowedRotationCosine = 0.9f;

        private Vector2 shipPos;
        private Vector2 shipVel;
        private float shipRot;

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
            shipRot = 0;
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
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                shipRot -= rotation;
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust * 0.8f;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                shipRot += rotation;
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust * 0.8f;
            }

            shipVel += shipAcc;
            shipPos += shipVel;

            foreach (var sur in surfaces)
            {
                if (!ShipBounds.Intersects(sur))
                    continue;

                if (Math.Cos(shipRot) <= allowedRotationCosine)
                {
                    InitShip();
                    return;
                }

                shipVel = Vector2.Zero;
                shipPos.Y = sur.Top - shipTexture.Height;

                shipRot %= (float)(Math.PI * 2.0f);
                if (shipRot < 0)
                {
                    shipRot += (float)(Math.PI * 2.0f);
                }
                // 0 <= shipRot < 2PI

                if (shipRot > Math.PI)
                {
                    shipRot = (float)Math.Min(Math.PI * 2.0f, shipRot + rotation);
                }
                else if (shipRot > 0)
                {
                    shipRot = Math.Max(0.0f, shipRot - rotation);
                }

            }

            base.Update(gameTime);
        }

        // TODO: Add your drawing code here
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            var shipCenter = new Vector2(shipTexture.Width / 2, shipTexture.Height / 2);

            _spriteBatch.Draw(
                shipTexture,
                shipPos + shipCenter,
                null,
                Color.White,
                shipRot,
                shipCenter,
                Vector2.One,
                SpriteEffects.None,
                0f
            );

            foreach (var sur in surfaces)
            {
                _spriteBatch.Draw(wallTexture, sur, Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
