using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace SAE_DEV
{
    internal class VoitureJoueur
    {
        //private const int LARGEUR_VOITURE = 78; //taille en px du sprite
        private const int DECOR_MAP = 448;// taille des tuiles ciel, herbe et barriere en px : x * 32 = taille px
        private const int ESPACE_LIGNE = 25;  //petit espace entre la barriere et la ligne continu
        private const int LARGEUR_ECRAN = 1600;

        private string nom;
        private int vitesse;
        private Vector2 position;
        private AnimatedSprite sprite;
        private int direction;
        private float  angle ;
        

        public VoitureJoueur(string nom, int vitesse, Vector2 position, AnimatedSprite typeVehicule)
        {
            this.Nom = nom;
            this.Vitesse = vitesse;
            this.Position = position;
            this.Sprite = typeVehicule;
            this.Angle = 0f;
            this.Direction = 1;
        }

        public VoitureJoueur(string nom, int vitesse, Vector2 position)
            : this(nom, vitesse, position, null)
        {

        }

        #region propriété
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

        public int Vitesse
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

        public Vector2 Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = value;
            }
        }

        public AnimatedSprite Sprite
        {
            get
            {
                return this.sprite;
            }

            set
            {
                this.sprite = value;
            }
        }

        public int Direction
        {
            get
            {
                return this.direction;
            }

            set
            {
                this.direction = value;
            }
        }

        public float Angle
        {
            get
            {
                return this.angle;
            }

            set
            {
                this.angle = value;
            }
        }
        #endregion
        public void DeplacementDroite(float deltaSeconds)
        {
            Sprite.Play("droite");
            if (Angle <= 0.3f)
            {
                Angle += 0.02f;
            }

            float nextX = Position.X + Direction * Vitesse * deltaSeconds;
            float maxPositionX = LARGEUR_ECRAN - DECOR_MAP - ESPACE_LIGNE;
            if (nextX < maxPositionX) 
            {
                Position = new Vector2(nextX, Position.Y);
            }
            else
            {
                Position = new Vector2(maxPositionX, Position.Y);
                Angle = 0f;
            }
        }

        public void DeplacementGauche(float deltaSeconds)
        {

            Sprite.Play("gauche");
            if (Angle >= -0.3f)
            {
                Angle -= 0.02f;
            }

            float nextX = Position.X - Direction * Vitesse * deltaSeconds;
            float maxPositionX = DECOR_MAP + ESPACE_LIGNE;
            if (nextX > maxPositionX) 
            {
                Position = new Vector2(nextX, Position.Y);
            }
            else
            {
                Position = new Vector2(maxPositionX, Position.Y);
                Angle = 0;
            }

        }

        public void Idle()
        {
            Sprite.Play("idle");
            if (Angle > 0f)
            {
                Angle -= 0.02f;
            }
            else if (Angle < 0f)
            {
                Angle += 0.02f;
            }
        }
    }
}