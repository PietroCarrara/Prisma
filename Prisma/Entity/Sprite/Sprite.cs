using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prisma
{
	public class Sprite : Entity
	{
		protected Texture2D Texture;

		private Vector2 scale, middle;

		public int Width, Height;

		public Sprite(Texture2D texture, int width, int height)
		{
			this.Texture = texture;

			Width = width;
			Height = height;

			middle = new Vector2(Width / 2f, Height / 2f);
		}

		public override void Initialize()
		{
			base.Initialize();

			scale = new Vector2(((float)Width) / Texture.Width, ((float)Height) / Texture.Height);
		}

		public override void Update()
		{
			base.Update();

			scale.X = ((float)Width) / Texture.Width;
			scale.Y = ((float)Height) / Texture.Height;
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
