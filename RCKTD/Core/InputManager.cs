using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RCKTD.Core
{

    public enum Controls
    {
        Up,
        Down,
        Left,
        Right,

        Select,
        Back
    }

    public class KeyMap
    {
        public Keys Key;
        public Controls Control;
    }

    public delegate void ControlPressed(Controls control);

    public class InputManager : IUpdatable
    {

        public event ControlPressed ControlPressed;

        protected List<KeyMap> KeyMaps;

        protected KeyboardState LastKeyboardState;

        public InputManager()
        {

            KeyMaps = new List<KeyMap> {
                new KeyMap{ Key = Keys.Up, Control = Controls.Up },
                new KeyMap{ Key = Keys.Down, Control = Controls.Down },
                new KeyMap{ Key = Keys.Left, Control = Controls.Left },
                new KeyMap{ Key = Keys.Right, Control = Controls.Right },

                new KeyMap{ Key = Keys.Enter, Control = Controls.Select },
                new KeyMap{ Key = Keys.Back, Control = Controls.Back },
            };

            LastKeyboardState = Keyboard.GetState();

        }

        public bool IsControlPressed(Controls control)
        {
            foreach (var keyMap in KeyMaps)
            {
                if (keyMap.Control == control && LastKeyboardState.IsKeyDown(keyMap.Key))
                {
                    return true;
                }
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            foreach (var keyMap in KeyMaps)
            {
                if (keyState.IsKeyDown(keyMap.Key) && LastKeyboardState.IsKeyUp(keyMap.Key))
                {
                    ControlPressed(keyMap.Control);
                }
            }
            LastKeyboardState = keyState;
        }

    }

}
