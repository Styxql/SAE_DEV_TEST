using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;


namespace SAE_DEV
{
    public class ScreenRemerciement : GameScreen
    {
        private Game1 _myGame;

        public ScreenRemerciement(Game1 game) : base(game)
        {
            _myGame = game;
        }
        public override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Yellow);
        }
    }
}
