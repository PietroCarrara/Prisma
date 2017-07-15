using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	/// <summary>
	/// Graphics Manager Class
	/// </summary>
	public static class Graphics
	{
		/// <summary>
		/// The device manager.
		/// </summary>
		public static GraphicsDeviceManager Manager { get; internal set; }

		/// <summary>
		/// The Graphics Device.
		/// </summary>
		/// <value>The device.</value>
		public static GraphicsDevice Device
		{
			get
			{
				return Manager.GraphicsDevice;
			}
		}
	}
}
