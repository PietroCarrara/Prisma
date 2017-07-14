using System;
using System.Diagnostics;
using MonoGame.Extended.Tiled;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;

namespace Prisma
{
	public class TiledMap : Entity
	{
		readonly MonoGame.Extended.Tiled.TiledMap map;

		public TiledMap(MonoGame.Extended.Tiled.TiledMap map)
		{
			this.map = map;
		}

		public override void Initialize()
		{
			base.Initialize();

			foreach (var layer in map.TileLayers)
				AddChild(new TiledLayer(map, layer));
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);
		}
	}
}