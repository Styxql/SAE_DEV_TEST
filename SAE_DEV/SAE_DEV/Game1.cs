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
    public class Game1 : Game
    {
        public const int HAUTEUR_VEHICULE_BASIQUE = 47;
        public const int LARGEUR_VEHICULE_BASIQUE = 85;
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
        private VoitureJoueur joueur;
        private Vector2 _positionInitialVoitureEnnemi;


        private float _mapYPosition = 0;
        private float _vitesseYMap = 300;
        private float _angleVehicule;


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


            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - HAUTEUR_VEHICULE_BASIQUE);
            _positionInitialVoitureEnnemi=new Vector2(100,100);
            _directionVoiture = 1;
            _vitesseVehicule = 100;
            _angleVehicule=0f;

            VoitureEnnemi[] tabVoitureEnnemies = { ambulance, truck, audi, voitureBolide, car, miniTruck, minivan, taxi, truck };

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
            audi = new VoitureEnnemi("audi", 100,_positionInitialVoitureEnnemi, _textureAudi);
            voitureBolide = new VoitureEnnemi("Voiture de Course", 100, _positionInitialVoitureEnnemi,_textureVoitureBolide);
            car = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureCar);
            miniTruck = new VoitureEnnemi("Car", 100, _positionInitialVoitureEnnemi, _textureMiniTruck);
            minivan= new VoitureEnnemi("MiniVan",100,_positionInitialVoitureEnnemi,_textureMinivan);
            taxi = new VoitureEnnemi("Taxi", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            truck = new VoitureEnnemi("Truck", 100, _positionInitialVoitureEnnemi, _textureMinivan);
            
            /////////////////////////////JOUEUR/////////////////////////////////////
            
            joueur = new VoitureJoueur("Voiture Basique",100,_positionVoiture,_voitureJoueur,0);



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
                      

            _mapYPosition += _vitesseYMap * deltaSeconds;
            _mapYPosition %= 1003;            

            if (_keyboardState.IsKeyDown(Keys.Right))
            {
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;

                _voitureJoueur.Play("droite");
                if (_angleVehicule <= 0.3f)
                {
                    _angleVehicule += 0.02f;
                }

            }
            else if (_keyboardState.IsKeyDown(Keys.Left))
            {
                _positionVoiture.X -= _directionVoiture * _vitesseVehicule * deltaSeconds;

                _voitureJoueur.Play("gauche");
                if(_angleVehicule >= -0.3f)
                {
                    _angleVehicule -= 0.02f;
                }
            }

            else
            {
                _voitureJoueur.Play("idle");
                if (_angleVehicule > 0f)
                {
                    _angleVehicule -= 0.02f;
                }
                else if (_angleVehicule < 0f)
                {
                    _angleVehicule += 0.02f;
                }
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
            _mapYPosition += _vitesseYMap * deltaSeconds;
            _mapYPosition %= 1000;



            //foreach(VoitureEnnemie voitureEnnemie in tabVoitureEnnemies )
            //{
              
            //}

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Yellow);
            _tiledMapRenderer.Draw(viewMatrix: Matrix.CreateTranslation(0,_mapYPosition - 1000,0));
            _spriteBatch.Begin();
            _spriteBatch.Draw(_textureVoiturePolice, _positionInitialVoitureEnnemi, Color.White);
            _spriteBatch.Draw(_textureCar, _positionInitialVoitureEnnemi, Color.White);
            _spriteBatch.Draw(_voitureJoueur, _positionVoiture, _angleVehicule);
            
            _spriteBatch.End();

            
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}