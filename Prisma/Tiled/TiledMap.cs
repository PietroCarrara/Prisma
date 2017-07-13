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

			var _mapRenderer = (Graphics.Device);
		}

		public override void Draw(Camera camera)
		{
			base.Draw(camera);

			foreach (var layer in map.TileLayers)
			{
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

								Debug.WriteLineIf(id != 0, id.ToString());

								var rect = map.Tilesets[0].GetTile(tile.GlobalIdentifier);

								camera.Draw(map.Tilesets[0].Texture,
											Position + new Vector2(x * map.TileWidth, y * map.TileHeight),
											sourceRectangle: rect);
							}
						}
					}
				}
			}
		}
	}
}