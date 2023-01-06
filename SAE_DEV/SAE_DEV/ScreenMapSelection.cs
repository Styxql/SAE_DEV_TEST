using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_DEV
{
    public class ScreenMapSelection : GameScreen
    {
        private Game1 _myGame;

        public ScreenMapSelection(Game1 game) : base(game)
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
            _myGame.GraphicsDevice.Clear(Color.Red);




        }
    }
}
