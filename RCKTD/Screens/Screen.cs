using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RCKTD.Core;
using System.Collections.Generic;

namespace RCKTD.Screens
{
    public abstract class Screen : IScreen
    {
        public abstract void ControlPressed(Controls controls);
        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void ProcessHide();
        public abstract void ProcessShow(Dictionary<string, object> data);
        public abstract void Update(GameTime gameTime);

    }

}
