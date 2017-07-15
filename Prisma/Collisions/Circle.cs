using System;
using Microsoft.Xna.Framework;

namespace Prisma
{
	public class Circle : Shape
	{
		public float Radius;

		public Circle(float raduis)
		{
			Radius = raduis;
		}

		public override bool CollidesWith(Entity e)
		{
			return this.Position.DistanceTo(e.Position) <= this.Radius;
		}

		public override bool CollidesWith(Circle c)
		{
			return c.Position.DistanceTo(this.Position) <= c.Radius + this.Radius;
		}

		public override bool CollidesWith(Rectangle r)
		{
			throw new NotImplementedException();
		}
	}
}
