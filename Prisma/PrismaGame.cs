﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prisma
{
    public class PrismaGame : Game
    {
        protected Scene CurrentScene;
        private Scene previousScene;

        SpriteBatch spriteBatch;

        public PrismaGame()
        {
            Graphics.Setup(new GraphicsDeviceManager(this));

            Content.RootDirectory = "Content"; 
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(Graphics.Device);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Update Systems

            //Update Current Scene
            if (CurrentScene != previousScene)
                CurrentScene.Initialize();

            previousScene = CurrentScene;

            CurrentScene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            CurrentScene.Draw(spriteBatch);
            
            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
