using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Prisma
{
	public class Sprite : Entity
	{
		private Texture2D texture;

		private Vector2 scale;

		public Sprite(Texture2D texture)
		{
			this.texture = texture;
		}

		public override void Initialize()
		{
			base.Initialize();

			Width = Parent.Width;
			Height = Parent.Height;

			scale = new Vector2(((float)Width) / texture.Width, ((float)Height) / texture.Height);

			OriginPoint = OriginEnum.Center;
		}

		public override void Update()
		{
			base.Update();

			scale.X = ((float)Width) / texture.Width;
			scale.Y = ((float)Height) / texture.Height;
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);

			camera.Draw(texture: texture,
						position: Position,
						rotation: RotationRadians,
						scale: scale,
						origin: Origin / scale);
		}
	}
}
