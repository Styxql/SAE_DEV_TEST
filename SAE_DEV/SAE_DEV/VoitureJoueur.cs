using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Content;

namespace SAE_DEV
{
    internal class VoitureJoueur
    {
        private string nom;
        private double vitesse;
        private Vector2 positionInitial;
        private int prix;
        private AnimatedSprite _voitureJoueur;
        private AnimatedSprite _typeVehicule;
        private KeyboardState _keyboardState;
        private Vector2 _positionVoiture;
        private int _directionVoiture;
        private int _vitesseVehicule;
        private float _angleVehicule;
        private int _maxPositionsX = 0;
        private GraphicsDeviceManager _graphics;
        public VoitureJoueur _joueur;

        public VoitureJoueur(string nom, double vitesse, Vector2 positionInitial, AnimatedSprite typeVehicule, int prix)
        {
            this.Nom = nom;
            this.Vitesse = vitesse;
            this._positionVoiture = positionInitial;
            this._voitureJoueur = typeVehicule;
            this.Prix = prix;
        }

        public string Nom
        {
            get
            {
                return this.nom;
            }

            set
            {
                this.nom = value;
            }
        }

        public double Vitesse
        {
            get
            {
                return this.vitesse;
            }

            set
            {
                this.vitesse = value;
            }
        }

        public Vector2 PositionInitialEnnemie
        {
            get
            {
                return this.positionInitial;
            }

            set
            {
                this.positionInitial = value;
            }
        }

        public AnimatedSprite TypeVehicule
        {
            get
            {
                return this._typeVehicule;
            }

            set
            {
                this._typeVehicule = value;
            }
        }

        public int Prix
        {
            get
            {
                return this.prix;
            }

            set
            {
                this.prix = value;
            }
        }

        


        public void DeplacementDroite(GameTime gameTime)
        {            

            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_keyboardState.IsKeyDown(Keys.Right) && !(_keyboardState.IsKeyDown(Keys.Left)))
            {
                System.Console.WriteLine(_positionVoiture.X);
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;

                _voitureJoueur.Play("droite");
                if (_angleVehicule <= 0.3f)
                {
                    _angleVehicule += 0.02f;
                }

                float nextX = _positionVoiture.X;
                _maxPositionsX = _graphics.PreferredBackBufferWidth - 32 - 78 - 420;
                if (nextX < _maxPositionsX) //32 : barriere , 78 : width voiture , 420 : decor.width pos barriere : 1390
                {
                    _positionVoiture.X = nextX;
                }
                else
                {
                    _positionVoiture.X = _maxPositionsX;
                    _angleVehicule = 0f;
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

            if (_positionVoiture.X < 490 || _positionVoiture.X > 1390)
            {
                //_directionVoiture = -_directionVoiture;
                _vitesseVehicule = 0;
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;
            }                   
        }

        public void DeplacementGauche(GameTime gameTime)
        {
            float deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_keyboardState.IsKeyDown(Keys.Left) && !(_keyboardState.IsKeyDown(Keys.Right)))
            {
                System.Console.WriteLine(_positionVoiture.X);
                _positionVoiture.X -= _directionVoiture * _vitesseVehicule * deltaSeconds;

                _voitureJoueur.Play("gauche");
                if (_angleVehicule >= -0.3f)
                {
                    _angleVehicule -= 0.02f;
                }

                float nextX = _positionVoiture.X;
                _maxPositionsX = 32 + 390 + 78;
                if (nextX > _maxPositionsX) //32 : barriere , 390 : decor , 78 :voiture.width
                {
                    _positionVoiture.X = nextX;
                }
                else
                {
                    _positionVoiture.X = _maxPositionsX;
                    _angleVehicule = 0;
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

            if (_positionVoiture.X < 490 || _positionVoiture.X > 1390)
            {
                //_directionVoiture = -_directionVoiture;
                _vitesseVehicule = 0;
                _positionVoiture.X += _directionVoiture * _vitesseVehicule * deltaSeconds;

            }
        }
    }
}

            



