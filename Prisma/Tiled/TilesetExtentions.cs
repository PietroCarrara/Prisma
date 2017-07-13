using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
namespace Prisma
{
	public static class TilesetExtentions
	{
		public static Rectangle GetTile(this TiledMapTileset self, int id)
		{
			// Id begins at 1, working it
			// beginning at 0 is easier
			id--;

			int y = id / self.Columns,
				x = id - y * self.Columns;

			return new Rectangle(x * self.TileWidth, y * self.TileHeight, self.TileWidth, self.TileHeight);
		}
	}
}
