using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	/// <summary>
	/// Object that draws on screen based on it's own position.
	/// </summary>
	public class Camera : IUpdateable
	{
		public Vector2 Position = Vector2.Zero;

		/// <summary>
		/// The right edge of the camera.
		/// </summary>
		public float Right
		{
			get
			{
				return PrismaGame.ScreenWidth + Position.X;
			}
			set
			{
				Position.X = value - PrismaGame.ScreenWidth;
			}
		}

		/// <summary>
		/// The left edge of the camera.
		/// </summary>
		public float Left
		{
			get
			{
				return Position.X;
			}
			set
			{
				Position.X = value;
			}
		}

		/// <summary>
		/// The top edge of the camera.
		/// </summary>
		public float Top
		{
			get
			{
				return Position.Y;
			}
			set
			{
				Position.Y = value;
			}
		}

		/// <summary>
		/// The bottom edge of the camera.
		/// </summary>
		public float Bottom
		{
			get
			{
				return PrismaGame.ScreenHeight + Position.Y;
			}
			set
			{
				Position.Y = value - PrismaGame.ScreenHeight;
			}
		}

		/// <summary>
		/// Draw something on the screen.
		/// </summary>
		/// <param name="texture">The texture to be drawn.</param>
		/// <param name="position">Where to draw the texture.</param>
		/// <param name="destinationRectangle">Squash your sprite to fit this rectangle.</param>
		/// <param name="sourceRectangle">Cut a rectangle from the sprite and only draw that portion of it.</param>
		/// <param name="origin">The origin of the texture.</param>
		/// <param name="scale">Enlarge or shrink the texture.</param>
		/// <param name="color">Which color to apply to the texture.</param>
		/// <param name="rotation">Rotate the texture with the center on the origin.</param>
		/// <param name="effects">Flip the texture.</param>
		/// <param name="layerDepth">How much behind the things should this texture be?</param>
		public virtual void Draw(
			Texture2D texture,
			Vector2? position = default(Vector2?),
			Microsoft.Xna.Framework.Rectangle? destinationRectangle = default(Microsoft.Xna.Framework.Rectangle?),
			Microsoft.Xna.Framework.Rectangle? sourceRectangle = default(Microsoft.Xna.Framework.Rectangle?),
			Vector2? origin = default(Vector2?),
			Vector2? scale = default(Vector2?),
			Color? color = default(Color?),
			float rotation = 0,
			SpriteEffects effects = SpriteEffects.None,
			float layerDepth = 0)
		{
			// Draw the object relatively to the camera's position
			if (position != null)
				position -= Position;

			PrismaGame.SpriteBatch.Draw(texture,
				position,
				destinationRectangle,
				sourceRectangle,
				origin,
				rotation,
				scale,
				color,
				effects,
				layerDepth);
		}

		public virtual void Update()
		{

		}
	}
}
