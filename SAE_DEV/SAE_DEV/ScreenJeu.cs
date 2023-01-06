using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Content;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using Microsoft.Xna.Framework.Audio;
using System.Threading; 

namespace SAE_DEV
{
    public class ScreenJeu : GameScreen
    {
        public const int HAUTEUR_VEHICULE_BASIQUE = 47;
        public const int LARGEUR_VEHICULE_BASIQUE = 85;
        //private GraphicsDeviceManager _graphics;
        //private SpriteBatch _spriteBatch;
        private TiledMap _tiledMap;
        private TiledMapRenderer _tiledMapRenderer;
        private Texture2D _textureVoiturePolice;
        private Texture2D _textureAmbulance;
        private Texture2D _textureMiniTruck;
        private Texture2D _textureAudi;
        private Texture2D _textureMinivan;
        private Texture2D _textureVoitureBolide;
        private Texture2D _textureCar;
        private Texture2D _textureTruck;
        private AnimatedSprite _voitureJoueur;
        private SoundEffect _radio;
        private SoundEffect _radioON;
        private SoundEffect _radioOFF;

        private KeyboardState _keyboardState;

        private Vector2 _positionVoiture;
        private VoitureEnnemi ambulance;
        private VoitureEnnemi miniTruck;
        private VoitureEnnemi audi;
        private VoitureEnnemi minivan;
        private VoitureEnnemi voitureBolide;
        private VoitureEnnemi car;
        private VoitureEnnemi truck;
        private VoitureEnnemi taxi;
        private VoitureJoueur _joueur;
        private Vector2 _positionInitialVoitureEnnemi;


        private float _mapYPosition = 0;
        private float _vitesseYMap = 300;
        private float _angleVehicule;


        private int _directionVoiture;
        private int _vitesseVehicule;
        private int _maxPositionsX = 0;

        private Game1 _myGame;
        // récup une ref à l'objet game qui permet d'accéder à ce qu'il y a dans Game1

        public ScreenJeu(Game1 game) : base(game)
        {         
            Content.RootDirectory = "Content";
            game.IsMouseVisible = true;
            _myGame = game;           
        }

        public override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _myGame._graphics.PreferredBackBufferWidth = 1920;
            _myGame._graphics.PreferredBackBufferHeight = 720;
            //_myGame._graphics.IsFullScreen = true;
            _myGame._graphics.ApplyChanges();
            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width - GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height - HAUTEUR_VEHICULE_BASIQUE);
            _positionInitialVoitureEnnemi = new Vector2(100, 100);
            _directionVoiture = 1;
            _vitesseVehicule = 100;
            _angleVehicule = 0f;

            VoitureEnnemi[] tabVoitureEnnemies = { ambulance, truck, audi, voitureBolide, car, miniTruck, minivan, taxi, truck };

            base.Initialize();
        }
        public override void LoadContent()
        {
           // _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureCar = Content.Load<Texture2D>("Car");
            _textureAmbulance = Content.Load<Texture2D>("Ambulance");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureMiniTruck = Content.Load<Texture2D>("MiniTruck");
            _textureTruck = Content.Load<Texture2D>("Truck");
            _textureMinivan = Content.Load<Texture2D>("Minivan");
            _textureVoitureBolide = Content.Load<Texture2D>("Blackviper");
            _textureVoiturePolice = Content.Load<Texture2D>("Police");
            _textureAudi = Content.Load<Texture2D>("Audi");
            ////_radio = Content.Load<SoundEffect>("Son radio");
            //_radioOFF = Content.Load<SoundEffect>("radioTurnOff");
            //_radioON = Content.Load<SoundEffect>("radioTurnON");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("CarSprite2.sf", new JsonContentLoader());
            _voitureJoueur = new AnimatedSprite(spriteSheet);


            //////////////ENNEMI///////////////////////////
            ambulance = new VoitureEnnemi("Ambulance", 100, _positionInitialVoitureEnnemi, _textureAmbulance);
            audi = new VoitureEnnemi("audi", 100, _positionInitialVoitureEnnemi, _textureAudi);
            voitureBolide = new VoitureEnnemi("Voiture de Course", 100, _positionInitialVoitureEnnemi, _textureVoitureBolide);
            car = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureCar);
            miniTruck = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureMiniTruck);
            minivan = new VoitureEnnemi("MiniVan", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            taxi = new VoitureEnnemi("Taxi", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            truck = new VoitureEnnemi("Truck", 100, _positionInitialVoitureEnnemi, _textureMinivan);

            /////////////////////////////JOUEUR/////////////////////////////////////

            _joueur = new VoitureJoueur("Voiture Basique", 100, _positionVoiture, _voitureJoueur, 0);

            base.LoadContent();

            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                _myGame.Exit();
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            _tiledMapRenderer.Update(gameTime);
            _voitureJoueur.Update(deltaSeconds);

            
            _joueur.DeplacementDroite(gameTime);
            _joueur.DeplacementGauche(gameTime);
          
            
            _mapYPosition += _vitesseYMap * deltaSeconds;
            _mapYPosition %= 1000;
        }

        public override void Draw(GameTime gameTime)
        {
            _tiledMapRenderer.Draw(viewMatrix: Matrix.CreateTranslation(0, _mapYPosition - 1000, 0));
             _myGame.SpriteBatch.Begin();
             _myGame.SpriteBatch.Draw(_textureVoiturePolice, _positionInitialVoitureEnnemi, Color.White);
             _myGame.SpriteBatch.Draw(_textureCar, _positionInitialVoitureEnnemi, Color.White);
             _myGame.SpriteBatch.Draw(_voitureJoueur, _positionVoiture, _angleVehicule);
             _myGame.SpriteBatch.End();

            // TODO: Add your drawing code here
        }

        /////////////////////////////////RADIO(Phase de test son dégeu jsp pk)/////////////////////////////////////////////////////
        //if (_keyboardState.IsKeyDown(Keys.K))
        //{
        //    _radioON.Play();
        //    Thread.Sleep(5000);


        //    //_radio.Play();
        //}
        //else if (_keyboardState.IsKeyDown(Keys.L))
        //{

        //    _radioOFF.Play();
        //}
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
