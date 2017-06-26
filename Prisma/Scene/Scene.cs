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
	public class Scene : IUpdateable, IDisposable
	{
		public GroupList Groups { get; private set; } = new GroupList();

		public Camera Camera = new Camera();

		public Color ClearColor = Color.CornflowerBlue;

		public bool IsInitialized { get; private set; } = false;

		public ContentManager Content;

		public Scene()
		{
			Groups.Scene = this;
		}

		public virtual void Initialize()
		{
			IsInitialized = true;

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
