using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma
{
	/// <summary>
	/// A camera that smoothly follows a entity. 
	/// </summary>
	public class DelayFollowCamera : Camera
	{
		public Entity Entity;

		/// <summary>
		/// The camera's speed.
		/// </summary>
		private float speed;

		/// <summary>
		/// Should the camera respect some boundaries?
		/// </summary>
		public bool UseBounds;

		/// <summary>
		/// The boundaries.
		/// </summary>
		public float MinHeight, MinWidth, MaxHeight, MaxWidth;

		public DelayFollowCamera(Entity e, float speed)
		{
			this.Entity = e;

			this.speed = speed;

			Position = e.Position;

			Position.X -= PrismaGame.ScreenWidth / 2;
			Position.Y -= PrismaGame.ScreenHeight / 2;
		}

		public override void Update()
		{
			if (Entity == null)
				return;

			// Centralizes the camera
			var pos = Position;
			pos.X += PrismaGame.ScreenWidth / 2;
			pos.Y += PrismaGame.ScreenHeight / 2;

			// Distance to our target
			var dist = (Entity.Position - pos);
			// If the distance is smaller than 5 pixels, don't move
			dist.X = Math.Abs(dist.X) > 5 ? dist.X : 0;
			dist.Y = Math.Abs(dist.Y) > 5 ? dist.Y : 0;

			// The closer we are, the slower we move
			Position.X += speed * Time.DeltaTime * dist.X;
			Position.Y += speed * Time.DeltaTime * dist.Y;

			// Don't let the camera escape it's boundings
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
