using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;
using System.Threading;

namespace SAE_DEV
{
    public class ScreenGameOver : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _textureBackgroundGameOver;
        private float _chronoChangementScene;
        

        public ScreenGameOver(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = 720;
            _myGame._graphics.PreferredBackBufferWidth = 925;
            _myGame._graphics.ApplyChanges();


        }
        public override void LoadContent()
        {
            _textureBackgroundGameOver = Content.Load<Texture2D>("BackgroundGameOver");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _chronoChangementScene += deltaSeconds;
            if(_chronoChangementScene > 3 )
            {
                _myGame.Etat = Game1.Etats.Menu;
                _myGame.LoadScreen(Game1.Etats.Menu);


            }
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureBackgroundGameOver, new Vector2(GraphicsDevice.Viewport.Width / 2 - (925 / 2), GraphicsDevice.Viewport.Height / 2 - (720 / 2)), Color.White);
            _myGame.GraphicsDevice.Clear(Color.Yellow);
            _myGame.SpriteBatch.End();
        }
    }
}
