using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;
using MonoGame.Extended;

namespace SAE_DEV
{
    internal class VoitureJoueur
    {
        private string nom;
        private int vitesse;
        private Vector2 position;
        private AnimatedSprite sprite;
        private int direction;
        private float angle;

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
            float maxPositionX = 1920 - 32 - 78 - 420;
            if (nextX < maxPositionX) //32 : barriere , 78 : width voiture , 420 : decor.width pos barriere : 1390
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
            float maxPositionX = 32 + 390 + 78;
            if (nextX > maxPositionX) //32 : barriere , 390 : decor , 78 :voiture.width
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