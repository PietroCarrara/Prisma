using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prisma
{
	public abstract class Transition : Scene
	{
		protected Scene previous, next;

		protected RenderTarget2D prevRt, nextRt;

		public Action Modifier;

		public Transition(Scene previous, Scene next, Action modifier = null)
		{
			this.previous = previous;
			this.next = next;

			Modifier = modifier;

			if (!next.IsInitialized)
				next.Initialize();

			prevRt = new RenderTarget2D(Graphics.Device, Graphics.Device.PresentationParameters.BackBufferWidth,
											 Graphics.Device.PresentationParameters.BackBufferHeight);


			nextRt = new RenderTarget2D(Graphics.Device, Graphics.Device.PresentationParameters.BackBufferWidth,
													 Graphics.Device.PresentationParameters.BackBufferHeight);
		}

		public override void Initialize()
		{
			base.Initialize();

			var rt = Graphics.Device.GetRenderTargets();

			Graphics.Device.SetRenderTarget(prevRt);

			// Flush the backbuffer to our render target
			PrismaGame.SpriteBatch.Begin();
			previous.Draw();
			PrismaGame.SpriteBatch.End();

			Modifier?.Invoke();

			Graphics.Device.SetRenderTarget(nextRt);

			// Flush the backbuffer to our render target
			PrismaGame.SpriteBatch.Begin();
			next.Draw();
			PrismaGame.SpriteBatch.End();

			Graphics.Device.SetRenderTargets(rt);
		}

		protected void End()
		{
			PrismaGame.Scene = next;

			prevRt.Dispose();
			nextRt.Dispose();
		}
	}
}
