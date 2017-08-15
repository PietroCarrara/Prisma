using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Prisma
{
	/// <summary>
	/// A scene in your game.
	/// </summary>
	public class Scene : IUpdateable, IDisposable
	{
		/// <summary>
		/// The <see cref="EntityGroup"/>s within this scene.
		/// </summary>
		public GroupList Groups { get; private set; } = new GroupList();

		/// <summary>
		/// This scene's camera.
		/// </summary>
		public Camera Camera;

		public Color ClearColor = Color.CornflowerBlue;

		public bool IsInitialized { get; private set; } = false;

		public ContentManager Content;

		/// <summary>
		/// The drawing layers of this scene.
		/// </summary>
		public readonly Dictionary<string, Counter> Layers = new Dictionary<string, Counter>();

		/// <summary>
		/// Automatically calculates the depth of each layer.
		/// </summary>
		/// <param name="layers">The names of the layers.</param>
		public void SetLayers(params string[] layers)
		{
			const float step = 0.00001f;

			Layers.Clear();

			// The value each layer must have
			float each = 1f / layers.Length;

			for (int i = 0; i < layers.Length; i++)
				Layers.Add(layers[i], new Counter(i * each, step));
		}

		public Scene()
		{
			Groups.Scene = this;
		}

		public virtual void Initialize()
		{
			IsInitialized = true;

			Camera = new Camera(1280, 720);

			Content = new ContentManager(PrismaGame.ContentManager.ServiceProvider, PrismaGame.ContentManager.RootDirectory);
		}

		public virtual void Update()
		{
			// If the game is paused, don't update
			if (PrismaGame.IsPaused)
				return;

			foreach (var group in Groups)
				foreach (var ent in group)
					ent.Update();

			Camera.Update();

			foreach (var group in Groups)
				while (group.DestroyQueue.Any())
				{
					group.RemoveEntity(group.DestroyQueue[0]);
					group.DestroyQueue.RemoveAt(0);
				}
		}

		public virtual void Draw()
		{
			Graphics.Device.Clear(ClearColor);

			foreach (var group in Groups)
				foreach (var ent in group)
					ent.Draw(Camera);
		}

		public void Dispose()
		{
			Content.Dispose();
		}
	}
}
