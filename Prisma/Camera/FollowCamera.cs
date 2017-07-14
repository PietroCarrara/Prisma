using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma
{
	public class FollowCamera : Camera
	{
		public Entity Entity;

		public bool UseBounds;

		public float MinHeight, MinWidth, MaxHeight, MaxWidth;

		public FollowCamera(Entity e)
		{
			this.Entity = e;
		}

		public override void Update()
		{
			Position = Entity.Position;

			Position.X -= PrismaGame.ScreenWidth / 2;
			Position.Y -= PrismaGame.ScreenHeight / 2;

			if (!UseBounds)
				return;

			if (Position.X < MinWidth)
				Position.X = MinWidth;
			else if (Position.X > MaxWidth)
				Position.X = MaxWidth;

			if (Position.Y < MinHeight)
				Position.Y = MinHeight;
			else if (Position.Y > MaxHeight)
				Position.Y = MaxHeight;
		}
	}
}
