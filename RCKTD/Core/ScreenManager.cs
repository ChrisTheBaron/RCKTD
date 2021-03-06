using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RCKTD.Screens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RCKTD.Core
{

    public class ScreenManager : IUpdatable, IControllable, IDrawable
    {

        private Dictionary<Type, Screen> AvailableScreens = new Dictionary<Type, Screen>();

        private Stack<Screen> ScreenStack = new Stack<Screen>();

        public Screen ActiveScreen => ScreenStack.Peek();

        public ScreenManager(ICollection<Screen> screens, Type root)
        {
            foreach (var screen in screens)
            {
                if (AvailableScreens.ContainsKey(screen.GetType()))
                {
                    throw new Exception($"Trying to add duplicate screen type '{screen.GetType().Name}'.");
                }
                AvailableScreens.Add(screen.GetType(), screen);
            }
            PushScreen(root);
        }

        public void PushScreen(Type screen, Dictionary<string, object> data = null)
        {
            if (!AvailableScreens.TryGetValue(screen, out var screenInst))
            {
                throw new Exception($"Trying to show an invalid screen type '{screen.Name}'.");
            }
            if (ScreenStack.Any())
            {
                ActiveScreen.ProcessHide();
            }
            screenInst.ProcessShow(data ?? new Dictionary<string, object>());
            ScreenStack.Push(screenInst);
        }

        public void PopScreen(Type screen = null, Dictionary<string, object> data = null)
        {
            if (screen != null && !AvailableScreens.ContainsKey(screen))
            {
                throw new Exception($"'{screen.Name}' isn't currently on the stack.");
            }
            if (ScreenStack.Count <= 1)
            {
                throw new Exception($"Trying to pop root screen off stack.");
            }
            var oldScreen = ActiveScreen;
            if (screen != null)
            {
                while (ActiveScreen.GetType() != screen)
                {
                    ScreenStack.Pop();
                }
            }
            else
            {
                ScreenStack.Pop();
            }
            ActiveScreen.ProcessShow(data ?? new Dictionary<string, object>());
            oldScreen.ProcessHide();
        }

        public void PopToRoot(Dictionary<string, object> data = null)
        {
            if (ScreenStack.Count <= 1)
            {
                throw new Exception($"Trying to pop root screen off stack.");
            }
            var oldScreen = ActiveScreen;
            while (ScreenStack.Count > 1)
            {
                ScreenStack.Pop();
            }
            ActiveScreen.ProcessShow(data ?? new Dictionary<string, object>());
            oldScreen.ProcessHide();
        }

        public void ControlPressed(Controls controls)
        {
            ActiveScreen.ControlPressed(controls);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            ActiveScreen.Draw(gameTime, spriteBatch);
        }

        public void Update(GameTime gameTime)
        {
            ActiveScreen.Update(gameTime);
        }

    }

}
