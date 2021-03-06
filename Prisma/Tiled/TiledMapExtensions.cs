﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using System.Diagnostics;
namespace Prisma
{
	public static class TiledMapExtensions
	{
		public static Tuple<Texture2D, Microsoft.Xna.Framework.Rectangle> GetTile(this MonoGame.Extended.Tiled.TiledMap map, int id)
		{
			int index = 0;
			TiledMapTileset tileset = map.Tilesets[index];

			while (tileset.TileCount < id)
			{
				id -= tileset.TileCount;
				index++;
				tileset = map.Tilesets[index];
			}

			// Id begins at 1, working it
			// beginning at 0 is easier
			id--;

			int y = id / tileset.Columns,
				x = id - y * tileset.Columns;

			var rect = new Microsoft.Xna.Framework.Rectangle(x * tileset.TileWidth + tileset.Margin + tileset.Spacing * x,
									 y * tileset.TileHeight + tileset.Margin + tileset.Spacing * y,

									 tileset.TileWidth,
									 tileset.TileHeight);

			return new Tuple<Texture2D, Microsoft.Xna.Framework.Rectangle>(tileset.Texture, rect);
		}
	}
}
