using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RCKTD.Core;
using RCKTD.Screens;
using System.Collections.Generic;

namespace RCKTD
{
    public class Game1 : Game
    {

        public InputManager InputManager { get; private set; }

        private GameScreen gameScreen;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            InputManager = new InputManager();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = Style.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Style.ScreenHeight;
            _graphics.ApplyChanges();

            gameScreen = new GameScreen();
            gameScreen.ProcessShow(new Dictionary<string, object>());

            InputManager.ControlPressed += gameScreen.ControlPressed;

            base.Initialize();
        }

        // TODO: use this.Content to load your game content here
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Style.Load(GraphicsDevice, Content);

        }

        protected override void UnloadContent()
        {
            Style.Unload();
            base.UnloadContent();
        }

        // TODO: Add your update logic here
        protected override void Update(GameTime gameTime)
        {
            InputManager.Update(gameTime);
            gameScreen.Update(gameTime);
            base.Update(gameTime);
        }

        // TODO: Add your drawing code here
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            gameScreen.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
