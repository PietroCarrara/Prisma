using System;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace Prisma
{
	public class Rectangle : Shape
	{
		public float Width, Height;

		public Rectangle(float width, float height)
		{
			Width = width;
			Height = height;
		}

		public override bool CollidesWith(Entity e)
		{
			return inRange(e.X, this.Position.X, this.Position.X + Width) &&
					inRange(e.Y, this.Position.Y, this.Position.Y + Height);
		}

		public override bool CollidesWith(Circle c)
		{
			throw new NotImplementedException();
		}

		public override bool CollidesWith(Rectangle r)
		{
			return rangeOverlap(this.Position.X, this.Position.X + Width,
								r.Position.X, r.Position.X + r.Width)
					&&
					rangeOverlap(this.Position.Y, this.Position.Y + Height,
								 r.Position.Y, r.Position.Y + r.Height);
		}

		bool inRange(float check, float val1, float val2)
		{
			return check <= Math.Min(val1, val2) && check >= Math.Max(val1, val2);
		}

		bool rangeOverlap(float a1, float a2, float b1, float b2)
		{
			return Math.Max(a1, a2) >= Math.Min(b1, b2) &&
				   Math.Min(a1, a2) <= Math.Max(b1, b2);
		}
	}
}
