using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RCKTD
{
    public class Game1 : Game
    {

        private Vector2 shipPos;

        private Texture2D shipTexture;
        private Texture2D wallTexture;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Rectangle ScreenSize => _graphics.GraphicsDevice.Viewport.Bounds;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            InitShip();

            base.Initialize();
        }

        private void InitShip()
        {
            shipPos = new Vector2(ScreenSize.Width / 3, ScreenSize.Height / 2);
        }

        // TODO: use this.Content to load your game content here
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            shipTexture = Content.Load<Texture2D>("ship");

            wallTexture = new Texture2D(GraphicsDevice, 1, 1);
            wallTexture.SetData(new Color[] { Color.Green });

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        // TODO: Add your drawing code here
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(shipTexture, shipPos, Color.White);

            _spriteBatch.Draw(wallTexture, new Rectangle(0, ScreenSize.Height - 10, ScreenSize.Width, 10), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
