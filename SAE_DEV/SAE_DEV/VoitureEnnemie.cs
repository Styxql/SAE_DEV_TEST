using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.Tiled.Serialization;

namespace SAE_DEV
{
    internal class VoitureEnnemie
    {
        private string nom;
        private double vitesse;
        private Microsoft.Xna.Framework.Vector2 _positionInitialEnnemie;
        private Texture2D _typeVehicule;

        public VoitureEnnemie (string nom, double vitesse, Vector2 positionInitialEnnemie, Texture2D typeVehicule)
        {
            this.Nom = nom;
            this.Vitesse = vitesse;
            this.PositionInitialEnnemie = positionInitialEnnemie;
            this.TypeVehicule = typeVehicule;
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
                return this._positionInitialEnnemie;
            }

            set
            {
                this._positionInitialEnnemie = value;
            }
        }

        public Texture2D TypeVehicule
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
    }
}
