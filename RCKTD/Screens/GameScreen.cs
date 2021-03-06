using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RCKTD.Core;
using System;
using System.Collections.Generic;

namespace RCKTD.Screens
{
    public class GameScreen : Screen
    {

        private const float gravity = 0.1f;
        private const float thrust = 0.2f;
        private const float rotation = 0.01f;
        private const float groundResistance = 0.5f;
        private const float airResistance = 0.995f;
        private const float bouncyness = 0.2f;

        private const float allowedRotationCosine = 0.9f;
        private const int allowedAmountOffSurface = 5;

        private Vector2 shipPos;
        private Vector2 shipVel;
        private float shipRot;

        private List<Rectangle> surfaces = new List<Rectangle>();

        private Rectangle ShipBounds => new Rectangle((int)Math.Round(shipPos.X), (int)Math.Round(shipPos.Y), Style.ShipTexture.Width, Style.ShipTexture.Height);

        public GameScreen()
        {

            surfaces.Add(new Rectangle(0, Style.ScreenHeight - 10, Style.ScreenWidth, 10));
            surfaces.Add(new Rectangle(Style.ScreenWidth / 2, Style.ScreenHeight / 2 - 10, 100, 10));

        }

        private void InitShip()
        {
            shipPos = new Vector2(Style.ScreenWidth / 3, Style.ScreenHeight / 2);
            shipVel = Vector2.Zero;
            shipRot = 0;
        }

        public override void ControlPressed(Controls controls)
        {
            // TODO: Implement this!
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            var shipCenter = new Vector2(Style.ShipTexture.Width / 2, Style.ShipTexture.Height / 2);

            spriteBatch.Draw(
                Style.ShipTexture,
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
                spriteBatch.Draw(Style.WallTexture, sur, Color.White);
            }

        }

        public override void ProcessHide()
        {

        }

        public override void ProcessShow(Dictionary<string, object> data)
        {
            InitShip();
        }

        public override void Update(GameTime gameTime)
        {

            if (!Style.ScreenSize.Intersects(ShipBounds))
            {
                InitShip();
                return;
            }

            var shipAcc = new Vector2(0, gravity);

            if (Program.Game.InputManager.IsControlPressed(Controls.Up))
            {
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust;
            }
            else if (Program.Game.InputManager.IsControlPressed(Controls.Left))
            {
                shipRot -= rotation;
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust * 0.8f;
            }
            else if (Program.Game.InputManager.IsControlPressed(Controls.Right))
            {
                shipRot += rotation;
                shipAcc += new Vector2((float)Math.Sin(shipRot), -(float)Math.Cos(shipRot)) * thrust * 0.8f;
            }

            shipVel += shipAcc;

            shipVel *= airResistance;

            shipPos += shipVel;

            foreach (var sur in surfaces)
            {
                if (!ShipBounds.Intersects(sur))
                    continue;

                if (ShipBounds.Center.Y > sur.Center.Y ||
                    ShipBounds.Left < sur.Left - allowedAmountOffSurface ||
                    ShipBounds.Right > sur.Right + allowedAmountOffSurface ||
                    Math.Cos(shipRot) <= allowedRotationCosine)
                {
                    InitShip();
                    return;
                }

                shipVel.Y *= -bouncyness;

                shipVel.X *= groundResistance;

                shipPos.Y = sur.Top - Style.ShipTexture.Height;

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

        }

    }

}
