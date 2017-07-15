using System;
namespace Prisma
{
	public abstract class Shape : Entity
	{
		internal Shape()
		{

		}

		public abstract bool CollidesWith(Entity e);

		public abstract bool CollidesWith(Circle c);

		public abstract bool CollidesWith(Rectangle r);
	}
}
