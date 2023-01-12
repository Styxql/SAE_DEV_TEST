using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;


namespace SAE_DEV
{
    public class ScreenChargement : GameScreen
    {
        public const int HAUTEUR_BARRE = 30;
        public const int LARGEUR_BARRE = 600;
        public const int HAUTEUR_VOITURE = 59;
        public const int LARGEUR_VOITURE = 94;
        public const int VITESSE_VOITURE = 50;

        private Game1 _myGame;
        private Texture2D _textureBackground;
        Rectangle _rectangleJaugeChargement;
        private Vector2 _positionVoiture;
        private int _largeurBarreChargement;
        private float _barreChargement;
       
        private Texture2D _textureJaugeChargement;
        private Texture2D _textureVoitureChargement;


        public ScreenChargement(Game1 game) : base(game)
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
            _barreChargement = 0;

        }
        public override void LoadContent()
        {
            _textureBackground = Content.Load<Texture2D>("BackgroundChargement");
            _textureJaugeChargement = Content.Load<Texture2D>("BarreChargement");
            _textureVoitureChargement = Content.Load<Texture2D>("VoitureChargement");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            
            _largeurBarreChargement = (int)(_barreChargement/ 25 * _textureJaugeChargement.Width);
            

            _rectangleJaugeChargement = new Rectangle(-LARGEUR_VOITURE, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_BARRE * 2, _largeurBarreChargement, HAUTEUR_BARRE);

            if (!(_largeurBarreChargement-LARGEUR_VOITURE*2>= _myGame._graphics.PreferredBackBufferWidth)) 
            {
                _positionVoiture = new Vector2(_largeurBarreChargement-LARGEUR_VOITURE, _myGame._graphics.PreferredBackBufferHeight - HAUTEUR_VOITURE - HAUTEUR_BARRE /2);

                _positionVoiture.X += _positionVoiture.X * deltaSeconds - VITESSE_VOITURE;


                _barreChargement += 10 * deltaSeconds;

            }
            else
            {
                _myGame.Etat = Game1.Etats.Menu;
                _myGame.LoadScreen(Game1.Etats.Menu);
            }





        }
        public override void Draw(GameTime gameTime)
        {
            _myGame.SpriteBatch.Begin();
            _myGame.SpriteBatch.Draw(_textureBackground, new Vector2(0,0), Color.White);
            _myGame.SpriteBatch.Draw(_textureJaugeChargement, _rectangleJaugeChargement, Color.White);
            _myGame.SpriteBatch.Draw(_textureVoitureChargement, _positionVoiture, Color.White);

            _myGame.GraphicsDevice.Clear(Color.Yellow);
            _myGame.SpriteBatch.End();
        }
    }
}
