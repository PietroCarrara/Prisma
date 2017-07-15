using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma.Prototyping
{
	/// <summary>
	/// A sprite that is just a solid color. Great for prototyping.
	/// </summary>
	public class PrototypeSprite : Sprite
	{
		private Color color;
		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				Texture.SetData(makeData(value, Width, Height));
				color = value;
			}
		}

		private static Texture2D makeRect(Color c, int w, int h)
		{
			var rect = new Texture2D(Graphics.Device, w, h);
			rect.SetData(makeData(c, w, h));

			return rect;
		}

		private static Color[] makeData(Color c, int w, int h)
		{
			var color = new Color[w * h];
			for (int i = 0; i < w * h; i++)
				color[i] = c;

			return color;
		}

		public PrototypeSprite(Color color, int width, int height) :
		base(makeRect(color, width, height), width, height)
		{
			Color = color;
		}
	}
}
