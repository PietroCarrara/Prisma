using System;
using Microsoft.Xna.Framework.Graphics;

namespace Prisma
{
	public class MovingBackgroud : Entity
	{
		public float Speed;

		private readonly Sprite[] bg;

		public MovingBackgroud(Texture2D tex, int Width, int Height, float speed, int loopTimes = 2)
		{
			Speed = speed;

			bg = new Sprite[loopTimes];
			for (int i = 0; i < loopTimes; i++)
			{
				bg[i] = new Sprite(tex, Width, Height);
				bg[i].X = Width * i + Width / 2f;
				bg[i].Y += Height / 2f;

				AddChild(bg[i]);
			}
		}

		public override void Update()
		{
			base.Update();

			for (int i = 0; i < bg.Length; i++)
			{
				bg[i].X -= Speed * Time.DeltaTime;
				if (bg[i].X + bg[i].Width / 2f < 0)
				{
					// Get the previous image
					var index = (i > 0) ? i - 1 : bg.Length - 1;

					bg[i].X = bg[index].X + bg[index].Width;

					// If it is our first time running the loop,
					// we will be located by the side of the last
					// image, but it hasn't moved yet, so we move
					// ourselves that much
					if (i == 0)
						bg[i].X -= Speed * Time.DeltaTime;
				}
			}
		}
	}
}

