using System;
using System.Diagnostics;
using MonoGame.Extended.Tiled;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Prisma
{
	/// <summary>
	/// A tiled map.
	/// </summary>
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

			// Adding and centralizing the layers
			foreach (var layer in map.TileLayers)
				AddChild(new TiledLayer(map, layer))
					.RelativePosition = -new Vector2(map.WidthInPixels / 2f, map.HeightInPixels / 2f);
		}

		/// <summary>
		/// Tries to set the tiled layers on the specified draw layers.
		/// </summary>
		public void SetDepth(Dictionary<string, Counter> dic)
		{
			var layers = GetChildren<TiledLayer>();

			foreach (var layer in layers)
				layer.Depth = dic[layer.Name].Next();
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);
		}

		public override void OnDestroy()
		{
			base.OnDestroy();

			map.Dispose();
		}
	}
}