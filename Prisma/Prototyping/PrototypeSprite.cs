using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma.Prototyping
{
	public class PrototypeSprite : Entity
	{
		public Color Color;

		private Texture2D rect;

		public PrototypeSprite(Color color)
		{
			Color = color;
		}

		public override void Initialize()
		{
			var colorData = new Color[Parent.Width * Parent.Height];
			for (int i = 0; i < Parent.Width * Parent.Height; i++)
				colorData[i] = Color;

			rect = new Texture2D(Graphics.Device, Parent.Width, Parent.Height);
			rect.SetData(colorData);

			base.Initialize();
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);

			camera.Draw(
				texture: rect,
				position: Position,
				rotation: RotationRadians,
				origin: Parent.Origin);
		}

		public override void OnDestroy()
		{
			rect.Dispose();
		}
	}
}
