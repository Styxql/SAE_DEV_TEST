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


namespace SAE_DEV
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
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
        private Texture2D _textureVoitureJoueur;
        private AnimatedSprite _voitureJoueur;

        private KeyboardState _keyboardState;

        private Vector2 _positionVoiture;
        private Voiture ambulance;
        private Voiture miniTruck;
        private Voiture audi;
        private Voiture minivan;
        private Voiture voitureBolide;
        private Voiture car;
        private Voiture truck;
        private Voiture taxi;
        private Vector2 _positionInitialVoitureEnnemie;
        private readonly ScreenManager _screenManager;
        public SpriteBatch SpriteBatch { get; set; }


        private int directionVoiture;
        private int _vitesseVehicule;
        

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 1980;
            _graphics.PreferredBackBufferHeight = 820;
            _graphics.ApplyChanges();
            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 45);
            _positionInitialVoitureEnnemie=new Vector2(100,100);
             directionVoiture = 1;
            _vitesseVehicule = 10;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            System.Console.WriteLine("un truc");

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureCar = Content.Load<Texture2D>("Car");
            _textureAmbulance = Content.Load<Texture2D>("Ambulance");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureMiniTruck = Content.Load<Texture2D>("Truck");
            _textureTruck = Content.Load<Texture2D>("Minitruck");
            _textureMinivan = Content.Load<Texture2D>("Minivan");
            _textureVoitureBolide = Content.Load<Texture2D>("Black_viper");
            _textureVoiturePolice = Content.Load<Texture2D>("Police");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureVoitureJoueur = Content.Load<Texture2D>("VoitureJoueur");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("CarSprite2.sf", new JsonContentLoader());
            _voitureJoueur = new AnimatedSprite(spriteSheet);

            System.Console.WriteLine("milieu");

            ambulance = new Voiture("Ambulance", 100, _positionInitialVoitureEnnemie, _textureAmbulance);
            audi = new Voiture("audi", 100,_positionInitialVoitureEnnemie, _textureAudi);
            voitureBolide = new Voiture("Voiture de Course", 100, _positionInitialVoitureEnnemie,_textureVoitureBolide);
            car = new Voiture("Car", 100, _positionInitialVoitureEnnemie, _textureCar);
            miniTruck = new Voiture("Car", 100, _positionInitialVoitureEnnemie, _textureMiniTruck);
            minivan=new Voiture("MiniVan",100,_positionInitialVoitureEnnemie,_textureMinivan);
            taxi = new Voiture("Taxi", 100, _positionInitialVoitureEnnemie, _textureMinivan);
            truck = new Voiture("Truck", 100, _positionInitialVoitureEnnemie, _textureMinivan);

            System.Console.WriteLine("un truc different ");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _tiledMapRenderer.Update(gameTime);
            // TODO: Add your update logic here
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _keyboardState = Keyboard.GetState();

            _tiledMapRenderer.Update(gameTime);
            _voitureJoueur.Play("idle");
            _voitureJoueur.Update(deltaSeconds);

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                directionVoiture = 1;
                _voitureJoueur.Play("walkEast");
                _positionVoiture.X += directionVoiture * _vitesseVehicule * deltaSeconds;
            }
            else if (_keyboardState.IsKeyDown(Keys.Up))
            {
                directionVoiture = -1;
                _voitureJoueur.Play("walkNorth");
                _positionVoiture.Y += directionVoiture * _vitesseVehicule * deltaSeconds;
            }
            else if (_keyboardState.IsKeyDown(Keys.Down))
            {
                directionVoiture = 1;
                _voitureJoueur.Play("walkSouth");
                _positionVoiture.Y += directionVoiture * _vitesseVehicule * deltaSeconds;
            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                directionVoiture = -1;
                _voitureJoueur.Play("walkWest");
               _positionVoiture.X += directionVoiture * _vitesseVehicule * deltaSeconds;
            }
            else
            {
                _voitureJoueur.Play("idle");
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);
            _tiledMapRenderer.Draw();
            _spriteBatch.Begin();

            _spriteBatch.Draw(_textureVoiturePolice, _positionInitialVoitureEnnemie, Color.White);
            _spriteBatch.Draw(_textureCar, _positionInitialVoitureEnnemie, Color.White);
            _spriteBatch.Draw(_textureVoitureJoueur, _positionInitialVoitureEnnemie, Color.White);





            _spriteBatch.End();

            
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}