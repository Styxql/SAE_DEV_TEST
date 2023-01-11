using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

namespace SAE_DEV
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch SpriteBatch { get; private set; }
        public readonly ScreenManager _screenManager;



        // on définit les différents états possibles du jeu ( à compléter) 
        public enum Etats { Play,Menu,MenuMap, Settings, Exit, Lose ,Classement};

        // on définit un champ pour stocker l'état en cours du jeu
        public Etats Etat;

        // on définit  2 écrans ( à compléter )
        public ScreenMenu _screenMenu;
        public ScreenJeu _screenJeu;
        public ScreenSettings _screenSettings;
        private ScreenRemerciement _screenRemerciement;
        public ScreenGameOver _screenGameOver;
        private ScreenChargement _screenChargement;
        private ScreenMenuMap _screenMenuMap;
        private ScreenClassement _screenClassement;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _screenManager = new ScreenManager();
            Components.Add(_screenManager);

            // Par défaut, le 1er état flèche l'écran de menu
            Etat = Etats.Menu;

            // on charge les 2 écrans 
            _screenMenu = new ScreenMenu(this);
            _screenJeu = new ScreenJeu(this);
            _screenSettings = new ScreenSettings(this);
            _screenGameOver = new ScreenGameOver(this);
            _screenChargement = new ScreenChargement(this);
            _screenMenuMap = new ScreenMenuMap(this);
            _screenClassement=new ScreenClassement(this);
        }

       

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            _graphics.PreferredBackBufferWidth = 720;
            _graphics.PreferredBackBufferHeight = 720;
            //_graphics.IsFullScreen = true;
            _graphics.ApplyChanges();



            base.Initialize();
        }

        protected override void LoadContent()
        {


            SpriteBatch = new SpriteBatch(GraphicsDevice);
            _screenManager.LoadScreen(_screenChargement, new FadeTransition(GraphicsDevice, Color.Black, 0));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            System.Console.WriteLine(Etat);


            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed)
            {
                if (this.Etat == Etats.Play)
                    _screenManager.LoadScreen(_screenJeu, new FadeTransition(GraphicsDevice, Color.Black));


                if (this.Etat == Etats.Menu)
                    _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));

                else if (this.Etat == Etats.Settings)
                    _screenManager.LoadScreen(_screenSettings, new FadeTransition(GraphicsDevice, Color.Black));

                else if (this.Etat == Etats.Exit)
                    Exit();
                else if (this.Etat == Etats.Lose)
                    _screenManager.LoadScreen(_screenGameOver, new FadeTransition(GraphicsDevice, Color.Black, 10));
                else if (this.Etat == Etats.MenuMap)
                    _screenManager.LoadScreen(_screenMenuMap, new FadeTransition(GraphicsDevice, Color.Black));
                else if (this.Etat == Etats.Classement) 
                    _screenManager.LoadScreen(_screenClassement, new FadeTransition(GraphicsDevice, Color.Black));
            }

            base.Update(gameTime);
        }
    
      
        
       
        public void LoadScreenPlay()
        {
        _screenManager.LoadScreen(_screenJeu,new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreenMenu()
        {
            _screenManager.LoadScreen(_screenMenu, new FadeTransition(GraphicsDevice, Color.Black));
        }
        public void LoadScreenSettings()
        {
            _screenManager.LoadScreen(_screenSettings, new FadeTransition(GraphicsDevice, Color.Black));
        }
        //public void LoadScreenLose()
        
            
        
            

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //SpriteBatch.Begin();

            //SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        

    }
    
}