using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
namespace Prisma
{
	public class TiledLayer : Entity
	{
		readonly MonoGame.Extended.Tiled.TiledMap map;
		readonly TiledMapTileLayer layer;

		internal TiledLayer(MonoGame.Extended.Tiled.TiledMap m, TiledMapTileLayer l)
		{
			map = m;
			layer = l;
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);

			for (int y = 0; y < layer.Height; y++)
			{
				for (int x = 0; x < layer.Width; x++)
				{
					TiledMapTile? t;

					if (layer.TryGetTile(x, y, out t))
					{
						var tile = (TiledMapTile)t;

						if (tile.GlobalIdentifier != 0)
						{

							// The point used in the tileset
							int id = tile.GlobalIdentifier;

							var data = map.GetTile(id);

							camera.Draw(data.Item1,
										Position + new Vector2(x * map.TileWidth, y * map.TileHeight),
										sourceRectangle: data.Item2);
						}
					}
				}
			}
		}
	}
}
