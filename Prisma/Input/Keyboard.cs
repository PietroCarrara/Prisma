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
		private static KeyboardState
			currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState(),
			previousState;

		/// <summary>
		/// Update the keyboard state.
		/// </summary>
		internal static void Update()
		{
			previousState = currentState;

			currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
		}

		/// <summary>
		/// Is the key being held?
		/// </summary>
		/// <param name="key">The key to check for.</param>
		public static bool IsKeyDown(Keys key)
		{
			return currentState.IsKeyDown(key);
		}

		/// <summary>
		/// Is the key not being held?
		/// </summary>
		/// <param name="key">The key to check for.</param>
		public static bool IsKeyUp(Keys key)
		{
			return currentState.IsKeyUp(key);
		}

		/// <summary>
		/// Has the key just been pressed?
		/// </summary>
		/// <returns><c>true</c> the frame the key has been pressed, <c>false</c> otherwise.</returns>
		/// <param name="key">The key to check for.</param>
		public static bool IsKeyPressed(Keys key)
		{
			return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
		}

		/// <summary>
		/// Has the key just been released?
		/// </summary>
		/// <returns><c>true</c> the frame the key has been released, <c>false</c> otherwise.</returns>
		/// <param name="key">Key.</param>
		public static bool IsKeyReleased(Keys key)
		{
			return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
		}
	}
}
