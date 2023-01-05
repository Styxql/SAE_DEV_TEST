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

        private float _mapYPosition = 0;
        private float _vitesseYMap = 300;


        private int _directionVoiture;
        private int _vitesseVehicule; 


                                    //"donc une classe pour les voitures enemies et une autre pour le joueur "
                                  //je le note là donc pour voiture je dois avoir position, deplacement ? je vais oas les controler ouiok
                                  // les collisions ok joueur deplacement avec contraintes oui !
                                  // une classe pour ma map ? avec la possibilité de changer les maps pour la suite
                                  // une de jour et une nuit pour debuter et ca va draw des voitures au hasard ? j'ai 4 positions que je dois
                                  // donc un tableau uni = 4 positions au hasard pour choisir la voie ok oui
                                  // il y a une gestion de scene qui s'apparente à des classes 
                                  // je vais devoir me documenter sur la creation du menu et des boutons monogame.extended oui bon on va pas céder à la tentation direct
                                  // jeudi... de la semaine pro le 12 on va faire ca demain avec les collegues le fonctionnement change pas trop mais il est vite fait different qd meme
                                  // avec les initialize les uploads les draw rien d'autre change apres si l'utilisation du "_" 
                                 
                                  // gauche : decor + barriere je vais y aller j'ai mes proprios qui vont passer mdrr
                                  // demain soir je t'envoie un petit compte rendu de ce que j'ai pu oui les cs des classes 

        
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
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();
            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height);
            _positionInitialVoitureEnnemie=new Vector2(100,100);
            _directionVoiture = 1;
            _vitesseVehicule = 25;


            base.Initialize();
        }

        protected override void LoadContent()
        {


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _tiledMap = Content.Load<TiledMap>("map");
            _tiledMapRenderer = new TiledMapRenderer(GraphicsDevice, _tiledMap);
            _textureCar = Content.Load<Texture2D>("Car");
            _textureAmbulance = Content.Load<Texture2D>("Ambulance");
            _textureAudi = Content.Load<Texture2D>("Audi");
            _textureMiniTruck = Content.Load<Texture2D>("Truck");
            _textureTruck = Content.Load<Texture2D>("Minitruck");
            _textureMinivan = Content.Load<Texture2D>("Minivan");
            _textureVoitureBolide = Content.Load<Texture2D>("Blackviper");
            _textureVoiturePolice = Content.Load<Texture2D>("Police");
            _textureAudi = Content.Load<Texture2D>("Audi");
            SpriteSheet spriteSheet = Content.Load<SpriteSheet>("CarSprite2.sf", new JsonContentLoader());
            _voitureJoueur = new AnimatedSprite(spriteSheet);



            ambulance = new Voiture("Ambulance", 100, _positionInitialVoitureEnnemie, _textureAmbulance);
            audi = new Voiture("audi", 100,_positionInitialVoitureEnnemie, _textureAudi);
            voitureBolide = new Voiture("Voiture de Course", 100, _positionInitialVoitureEnnemie,_textureVoitureBolide);
            car = new Voiture("Car", 100, _positionInitialVoitureEnnemie, _textureCar);
            miniTruck = new Voiture("Car", 100, _positionInitialVoitureEnnemie, _textureMiniTruck);
            minivan=new Voiture("MiniVan",100,_positionInitialVoitureEnnemie,_textureMinivan);
            taxi = new Voiture("Taxi", 100, _positionInitialVoitureEnnemie, _textureMinivan);
            truck = new Voiture("Truck", 100, _positionInitialVoitureEnnemie, _textureMinivan);



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
            _voitureJoueur.Update(deltaSeconds);
            // le calcul de ma barriere à droite c'est : taille fenetre.width - taille barriere - taille decor - taille voiture
            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _voitureJoueur.Play("droite"); 
                _directionVoiture = _vitesseVehicule;
                float nextX = _positionVoiture.X + _directionVoiture * _vitesseVehicule * deltaSeconds;
                if (nextX < _graphics.PreferredBackBufferWidth - 32 - 78 - 415) //32 : barriere , 78 : width voiture , 410 : decor.width
                {
                    _positionVoiture.X = nextX;
                    
                }
                
            }
            else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {               
                _voitureJoueur.Play("gauche");
                _directionVoiture = -_vitesseVehicule;
                float nextX = _positionVoiture.X + _directionVoiture * _vitesseVehicule * deltaSeconds;
                if(nextX > 32 + 390 + 78) //32 : barriere , 410 : decor
                {
                    _positionVoiture.X = nextX;
                }               
                
                
            }
            else if (_keyboardState.IsKeyDown(Keys.Up) && !(_keyboardState.IsKeyDown(Keys.Down)))
            {
                _directionVoiture = -_vitesseVehicule;
                float nextX = _positionVoiture.Y += _directionVoiture * _vitesseVehicule * deltaSeconds;
                if(nextX > _graphics.PreferredBackBufferHeight + 85)
                {
                    _positionVoiture.Y = nextX;
                }
            }
            else if (_keyboardState.IsKeyDown(Keys.Down) && !(_keyboardState.IsKeyDown(Keys.Up)))
            {
                _directionVoiture = 10;
                _positionVoiture.Y += _directionVoiture * _vitesseVehicule * deltaSeconds;
            }
            else
            {
                _directionVoiture = 0;
                _voitureJoueur.Play("idle");
            }

            if(_positionVoiture.X < 480 || _positionVoiture.X > 1400)
            {
                System.Console.WriteLine("ici");
                _directionVoiture = 0;
                _positionVoiture.Y += _directionVoiture * _vitesseVehicule * deltaSeconds;
            }           

            _mapYPosition += _vitesseYMap * deltaSeconds;
            _mapYPosition %= 1003;            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);
            _tiledMapRenderer.Draw(viewMatrix: Matrix.CreateTranslation(0,_mapYPosition - 1000,0));
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureVoiturePolice, _positionInitialVoitureEnnemie, Color.White);
            _spriteBatch.Draw(_textureCar, _positionInitialVoitureEnnemie, Color.White);
            _spriteBatch.Draw(_voitureJoueur,_positionVoiture);
            _spriteBatch.End();

            
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}