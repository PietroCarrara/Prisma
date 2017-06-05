using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
    public static class Keyboard
    {
        // Start currentState at a value, so 
        // previous state is not null in the
        // first frame
        private static KeyboardState currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState(), previousState;

        internal static void Update()
        {
            previousState = currentState;

            currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return currentState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
        }
    }
}
