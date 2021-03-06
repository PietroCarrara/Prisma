﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	public static class FloatMath
	{
		public static float Sin(float radians)
		{
			return (float)Math.Sin(radians);
		}

		public static float Cos(float radians)
		{
			return (float)Math.Cos(radians);
		}

		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2(y, x);
		}
	}
}
