using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Tiled.Serialization;

namespace SAE_DEV
{
    internal class VoitureJoueur
    {
        private string nom;
        private double vitesse;
        private Microsoft.Xna.Framework.Vector2 positionInitial;
        private AnimatedSprite _typeVehicule;
        private int prix;

        public VoitureJoueur(string nom, double vitesse, Vector2 positionInitial, AnimatedSprite typeVehicule, int prix)
        {
            this.Nom = nom;
            this.Vitesse = vitesse;
            this.PositionInitialEnnemie = positionInitial;
            this.TypeVehicule = typeVehicule;
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
    }
}

            



