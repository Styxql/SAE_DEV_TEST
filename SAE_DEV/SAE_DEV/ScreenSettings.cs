using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SAE_DEV
{
    public class ScreenSettings : GameScreen
    {
        private Game1 _myGame;
        private Texture2D _settingsBackground;

        

        public ScreenSettings(Game1 game) : base(game)
        {
            _myGame = game;

        }
        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;

            _myGame._graphics.PreferredBackBufferHeight = 435;
            _myGame._graphics.PreferredBackBufferWidth = 697;
            _myGame._graphics.ApplyChanges();


        }
        public override void LoadContent()
        {
            _settingsBackground = Content.Load<Texture2D>("SettingsBackground");

            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.GraphicsDevice.Clear(Color.Black);
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_settingsBackground, new Vector2(GraphicsDevice.Viewport.Width / 2 - (697 / 2), GraphicsDevice.Viewport.Height / 2 - (435 / 2)), Color.White);
            _myGame.SpriteBatch.End();




        }
    }
}
