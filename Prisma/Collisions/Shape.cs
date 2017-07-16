using System;
namespace Prisma
{
	public abstract class Shape : Entity
	{
		internal Shape()
		{

		}

		public bool CollidesWith(Shape s)
		{
			if (s is Circle)
				return CollidesWith((Circle)s);

			if (s is Rectangle)
				return CollidesWith((Rectangle)s);

			return CollidesWith((Entity)s);
		}

		public abstract bool CollidesWith(Entity e);

		public abstract bool CollidesWith(Circle c);

		public abstract bool CollidesWith(Rectangle r);
	}
}
