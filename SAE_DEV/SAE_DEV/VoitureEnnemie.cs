using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SAE_DEV
{
    internal class VoitureEnnemie
    {
        private string nom;
        private int vitesse;
        private Vector2 position;
        private Texture2D texture;
        private float sens;

        public VoitureEnnemie(string nom, int vitesse, Vector2 position, float sens, Texture2D texture)
        {
            this.Nom = nom;
            this.Vitesse = vitesse;
            this.Position = position;
            this.Texture = texture;
            this.Sens = sens;
        }
        public VoitureEnnemie(string nom, int vitesse, Vector2 position, float sens)
            : this(nom, vitesse, position, sens, null)
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

        public float Sens
        {
            get
            {
                return this.sens;
            }

            set 
            { 
                this.sens = value; 
            }

        }

        public Texture2D Texture
        {
            get
            {
                return this.texture;
            }

            set
            {
                this.texture = value;
            }
        }
        #endregion

    }

}
