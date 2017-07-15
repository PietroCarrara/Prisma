using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma
{
	/// <summary>
	/// A colored circle sprite. Useful for prototyping.
	/// </summary>
	public class CirclePrototypeSprite : Sprite
	{
		Color color;
		public Color Color
		{
			get
			{
				return color;
			}
			set
			{
				Texture.SetData(makeData(value, radius));
				color = value;
			}
		}
		int radius;

		public CirclePrototypeSprite(Color color, int radius) :
		base(makeCircle(color, radius), radius * 2, radius * 2)
		{
			this.color = color;
			this.radius = radius;
		}

		private static Texture2D makeCircle(Color c, int radius)
		{
			var circle = new Texture2D(Graphics.Device, radius * 2, radius * 2);

			circle.SetData(makeData(c, radius));

			return circle;
		}

		private static Color[] makeData(Color c, int radius)
		{
			int width = radius * 2;
			int height = radius * 2;

			var center = new Vector2(radius, radius);

			var data = new Color[width * height];

			for (int y = 0; y < height; y++)
			{
				for (int x = 0; x < width; x++)
				{
					// If we are within the radius, we have a color
					if (center.DistanceTo(new Vector2(x, y)) < radius)
						data[y * width + x] = c;
					else
						data[y * width + x] = Color.Transparent;
				}
			}

			return data;
		}
	}
}
