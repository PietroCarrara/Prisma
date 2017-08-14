using System;
using System.Reflection;

namespace Prisma
{
	public class Counter
	{
		private readonly float start;

		public float Step;

		private float current;

		public Counter(float start, float step)
		{
			this.start = start;

			current = start - step;

			Step = step;
		}

		public float Next()
		{
			return current += Step;
		}
	}
}
