using System;
using System.Collections.Generic;
using System.IO;


namespace SAE_DEV;
internal class Classement : IComparable<Classement>
{
    private static string CHEMIN = Directory.GetCurrentDirectory() + "\\classement.txt";
    private string pseudo;
    private float score;

    public Classement(string pseudo, float score)
    {
        this.Pseudo = pseudo;
        this.Score = score;
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

    public static List <Classement> ReadAll()
    {
        if (!File.Exists(CHEMIN))
        { 
            File.Create(CHEMIN).Close();
        }
            List<Classement> _lesClassements= new List<Classement>();
        // Open the file to read from.
        using (StreamReader sr = File.OpenText(CHEMIN))
        {
            string chaine;
            while ((chaine = sr.ReadLine()) != null)
            {
                string[] subs = chaine.Split(':');
                float.TryParse(subs[1], out float score);
                Classement c = new Classement(subs[0], score);
                _lesClassements.Add(c);
            }
            return _lesClassements;
        }
    }

    public static void WriteAll(List<Classement> _lesClassements)
    {
        using (StreamWriter sw = File.CreateText(CHEMIN))
        {
            foreach (Classement classement in _lesClassements)
            {
                sw.Write(classement.Pseudo);
                sw.Write(" : ");
                sw.WriteLine(classement.Score);

            }
        }
    }

    public int CompareTo(Classement other)
    {
        if (this.Score < other.Score) return 1;
        if (this.Score == other.Score) return 0;
        return -1;
    }

}



