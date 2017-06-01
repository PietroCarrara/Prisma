using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prisma
{
    public class PrismaGame : Game
    {
        protected Scene CurrentScene;

        SpriteBatch spriteBatch;

        private static PrismaGame instance;

        public static Scene Scene
        {
            get
            {
                return instance.CurrentScene;
            }

            set
            {
                instance.CurrentScene = value;
            }
        }

        public PrismaGame()
        {
            instance = this;

            Graphics.Manager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content"; 
        }

        public static void End()
        {
            instance.Exit();
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
            Time.Update(gameTime);
            Input.Update();

            //Update Current Scene
            if (!CurrentScene.IsInitialized)
            {
                CurrentScene.Initialize();
                CurrentScene.IsInitialized = true;
            }

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
