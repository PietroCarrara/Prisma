﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	public enum MouseButton
	{
		Left,
		Right,
		Middle
	}

	public static class Mouse
	{
		// Start currentState at a value, so 
		// previous state is not null in the
		// first frame
		private static MouseState
			currentState = Microsoft.Xna.Framework.Input.Mouse.GetState(),
			previousState;

		internal static void Update()
		{
			previousState = currentState;
			currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
		}

		/// <summary>
		/// Is the button being held?
		/// </summary>
		/// <param name="button">The button to check for.</param>
		public static bool IsButtonDown(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
					return currentState.LeftButton == ButtonState.Pressed;
				case MouseButton.Right:
					return currentState.RightButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return currentState.MiddleButton == ButtonState.Pressed;
				default:
					throw new IndexOutOfRangeException("Invalid mouse button detected!");
			}
		}

		/// <summary>
		/// Is the button being held?
		/// </summary>
		/// <param name="button">The button to check for.</param>
		public static bool IsButtonUp(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
					return currentState.LeftButton == ButtonState.Released;
				case MouseButton.Right:
					return currentState.RightButton == ButtonState.Released;
				case MouseButton.Middle:
					return currentState.MiddleButton == ButtonState.Released;
				default:
					throw new IndexOutOfRangeException("Invalid mouse button detected!");
			}
		}

		/// <summary>
		/// Has the button just been pressed?
		/// </summary>
		/// <returns><c>true</c> the frame the button has been pressed, <c>false</c> otherwise.</returns>
		/// <param name="button">The button to check for.</param>
		public static bool IsButtonPressed(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
					return currentState.LeftButton == ButtonState.Pressed && previousState.LeftButton == ButtonState.Released;
				case MouseButton.Right:
					return currentState.RightButton == ButtonState.Pressed && previousState.RightButton == ButtonState.Released;
				case MouseButton.Middle:
					return currentState.MiddleButton == ButtonState.Pressed && previousState.MiddleButton == ButtonState.Released;
				default:
					throw new IndexOutOfRangeException("Invalid mouse button detected!");
			}
		}

		/// <summary>
		/// Has the button just been released?
		/// </summary>
		/// <returns><c>true</c> the frame the key has been pressed, <c>false</c> otherwise.</returns>
		/// <param name="button">The button to check for.</param>
		public static bool IsButtonReleased(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:
					return currentState.LeftButton == ButtonState.Released && previousState.LeftButton == ButtonState.Pressed;
				case MouseButton.Right:
					return currentState.RightButton == ButtonState.Released && previousState.RightButton == ButtonState.Pressed;
				case MouseButton.Middle:
					return currentState.MiddleButton == ButtonState.Released && previousState.MiddleButton == ButtonState.Pressed;
				default:
					throw new IndexOutOfRangeException("Invalid mouse button detected!");
			}
		}

		/// <summary>
		/// The cursor relative to the camera position.
		/// </summary>
		public static Vector2 Position
		{
			get
			{
				var pos = currentState.Position.ToVector2();

				return pos + PrismaGame.Scene.Camera.Position;
			}
		}

		public static Point PositionPoint
		{
			get
			{
				return currentState.Position;
			}
		}
	}
}
