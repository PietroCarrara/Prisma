using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Prisma
{
    public class PrismaGame : Game
    {
        protected Scene scene;

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
            scene.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            scene.Draw(spriteBatch);
            
            base.Draw(gameTime);

            spriteBatch.End();
        }
    }
}
