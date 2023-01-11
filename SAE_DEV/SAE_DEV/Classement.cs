using System;
using System.IO;


namespace SAE_DEV;
internal class Classement
{
    private string pseudo;
    private float score;

    public Classement(string pseudo, float score)
    {
        this.pseudo = pseudo;
        this.score = score;
    }

    #region propriété
    public string Pseudo
    {
        get
        {
            return this.pseudo;
        }

        set
        {
            this.pseudo = value;
        }
    }
    public float Score
    {
        get
        {
            return this.score;
        }

        set
        {
            this.score = value;
        }
    }
    #endregion

    public void AddScore()
    {
        string chemin = @"P:\NO_BRAKE\SAE_DEV\SAE_DEV\classement\classement.txt";
        if (!File.Exists(chemin))
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(chemin))
            {
                sw.WriteLine("Pseudo");
                sw.WriteLine(" : ");
                sw.WriteLine("Score");
            }
        }

        // Open the file to read from.
        using (StreamReader sr = File.OpenText(chemin))
        {
            string chaine;
            while ((chaine = sr.ReadLine()) != null)
            {
                Console.WriteLine(chaine);
            }
        }
    }
    

}


