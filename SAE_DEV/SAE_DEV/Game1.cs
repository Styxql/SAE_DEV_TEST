﻿using Microsoft.Xna.Framework;
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
        public SpriteBatch SpriteBatch { get; set; }


        private int _directionVoiture;
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
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            _positionVoiture = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height - 45);
            _positionInitialVoitureEnnemie=new Vector2(100,100);
            _directionVoiture = 1;
            _vitesseVehicule = 10;

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
            _voitureJoueur.Play("idle");
            _voitureJoueur.Update(deltaSeconds);            

            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                _directionVoiture = 25;
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;
                //_voitureJoueur.Play("animation0");

            }            
            else if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                _directionVoiture = -25;
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;
            } 
            else
            {
                _directionVoiture = 0;
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
            _spriteBatch.Draw(_voitureJoueur,_positionVoiture);





            _spriteBatch.End();

            
            
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}