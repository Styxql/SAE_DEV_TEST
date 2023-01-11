using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SAE_DEV
{
    internal class Items
    {
        private string nom;
        private int vitesse;
        private Vector2 position;
        private Texture2D texture;

        public Items(string nom, int vitesse, Vector2 position, Texture2D texture)
        {
            this.nom = nom;
            this.vitesse = vitesse;
            this.position = position;
            this.texture = texture;
        }

        public Items(string nom, int vitesse, Vector2 position)
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