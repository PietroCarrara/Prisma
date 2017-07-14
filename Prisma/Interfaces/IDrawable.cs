﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	public interface IDrawable
	{
		float Depth { get; set; }

		void Draw(Camera camera);
	}
}
