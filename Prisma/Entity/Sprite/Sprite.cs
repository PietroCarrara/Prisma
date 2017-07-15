using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prisma
{
	/// <summary>
	/// A entity that contains a texture.
	/// </summary>
	public class Sprite : Entity
	{
		/// <summary>
		/// The texture.
		/// </summary>
		protected Texture2D Texture;

		private Vector2 scale, middle;

		private int width, height;
		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width = value;
				scale.X = ((float)this.width) / Texture.Width;
				middle.X = width / 2f;
			}
		}
		public int Height
		{
			get
			{
				return height;
			}
			set
			{
				height = value;
				scale.Y = ((float)this.height) / Texture.Height;
				middle.Y = height / 2f;
			}
		}

		public Sprite(Texture2D texture, int width, int height)
		{
			this.Texture = texture;

			this.width = width;
			this.height = height;

			middle = new Vector2(this.width / 2f, this.height / 2f);

			scale = new Vector2(((float)this.width) / Texture.Width, ((float)this.height) / Texture.Height);
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);

			camera.Draw(texture: Texture,
						position: Position,
						rotation: Rotation.ToRadians(),
						scale: scale,
						origin: middle / scale,
						layerDepth: Parent.Depth);
		}
	}
}
