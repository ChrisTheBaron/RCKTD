using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace RCKTD.Core
{
    public abstract class Style
    {

        public const int ScreenWidth = 800;
        public const int ScreenHeight = 600;

        public static Rectangle ScreenSize => new Rectangle(0, 0, ScreenWidth, ScreenHeight);

        private static ContentManager _content;

        public static Texture2D ShipTexture { get; private set; }
        public static Texture2D WallTexture { get; private set; }

        public static void Load(GraphicsDevice graphics, ContentManager content)
        {

            _content = content;

            ShipTexture = _content.Load<Texture2D>("ship");

            WallTexture = new Texture2D(graphics, 1, 1);
            WallTexture.SetData(new Color[] { Color.Green });

        }

        public static void Unload()
        {
            WallTexture.Dispose();
        }

    }

}
