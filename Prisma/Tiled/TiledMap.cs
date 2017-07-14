﻿using System;
using System.Diagnostics;
using MonoGame.Extended.Tiled;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

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

			// Adding and centralizing the layers
			foreach (var layer in map.TileLayers)
				AddChild(new TiledLayer(map, layer))
					.RelativePosition = -new Vector2(map.WidthInPixels / 2f, map.HeightInPixels / 2f);
		}

		public void SetDepth(Dictionary<string, float> dic)
		{
			var layers = GetChildren<TiledLayer>();

			foreach (var layer in layers)
				layer.Depth = dic[layer.Name];
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