using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmi
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Déclaration des variables

            List<Sommet> listSommet = new List<Sommet>();
            List<Sommet> listCoord = new List<Sommet>();
            List<Arc> listArc = new List<Arc>();


            #endregion

            #region Récupération des données

            //Création de la liste des sommets et de leur noms
            StreamReader reader = new StreamReader(File.OpenRead("../../csv/sommets-noms.csv"));
            
            int count = 0;
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                
                if (count > 0)
                {
                    Sommet sommet = new Sommet(int.Parse(values[0]), values[1].ToString());
                    listSommet.Add(sommet);
                }

                count++;
            }

            //Ajout des coordonnées aux différents sommets
            StreamReader readerCoord = new StreamReader(File.OpenRead("../../csv/sommets-coordonnees.csv"));
            count = 0;
            while (!readerCoord.EndOfStream)
            {
                var line = readerCoord.ReadLine();
                var values = line.Split(';');

                if (count > 0)
                {

                    int index = listSommet.FindIndex(s => s.getId() == int.Parse(values[0]));
                    listSommet.ElementAt(index).SetCoordX(int.Parse(values[1]));
                    listSommet.ElementAt(index).SetCoordY(int.Parse(values[2]));
                }

                count++;
            }

            //Création de la liste des arcs
            Sommet sommet1;
            Sommet sommet2;
            Arc arc;

            StreamReader readerArc = new StreamReader(File.OpenRead("../../csv/valeurs-arcs.csv"));
            count = 0;
            while (!readerArc.EndOfStream)
            {
                var line = readerArc.ReadLine();
                var values = line.Split(';');

                if (count > 0)
                {
                    sommet1 = listSommet.Find(s => s.getId() == int.Parse(values[0]));
                    sommet2 = listSommet.Find(s => s.getId() == int.Parse(values[1]));
                    arc = new Arc(sommet1, sommet2, float.Parse(values[2]));
                    listArc.Add(arc);
                }

                count++;
            }

            //Ajout des temps de trajet à pied
            StreamReader readerArc2 = new StreamReader(File.OpenRead("../../csv/temps.csv"));
            count = 0;
            arc = null;
            while (!readerArc2.EndOfStream)
            {
                var line = readerArc2.ReadLine();
                var values = line.Split(';');

                if (count > 0)
                {
                    sommet1 = listSommet.Find(s => s.getId() == int.Parse(values[0]));
                    sommet2 = listSommet.Find(s => s.getId() == int.Parse(values[1]));
                    arc = new Arc(sommet1, sommet2, float.Parse(values[2]));
                    listArc.Add(arc);
                }

                count++;
            }



            //Affichage de la liste des sommets
            /*foreach (Sommet so in listSommet)
            {
                Console.WriteLine("id : " + so.getId() + " - nom : " + so.getNom() + " - X : " + so.getCoordX() + " - Y : " + so.getCoordY());
            }*/

            //Affichage de la liste des arcs
            /*foreach (Arc ar in listArc)
            {
                Console.WriteLine("s1: " + ar.getSommet1().getId() + ":" + ar.getSommet1().getNom() + " - s2: " + ar.getSommet2().getId() + ":" + ar.getSommet2().getNom() + " - tps: " + ar.getTemps());
            }*/

            #endregion

            #region Sélection du départ et de l'arrivée

            // Choisir 2 sommets au hasard, 375 sommets en tout
            Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));
            int idSommetDebut = rdm.Next(0, 375);
            int idSommetFin = rdm.Next(0, 375);

            #endregion

            //roue biaisée - sélection du chemin
            List<Fourmi> listFourmi = new List<Fourmi>();

            Fourmi fourmi = new Fourmi();
            arc = new Arc();
            List<Arc> listCheminPossible = new List<Arc>();
            
            Console.WriteLine("sommet de départ : " + idSommetDebut);
            Console.WriteLine("sommet d'arrivée : " + idSommetFin);

            int idSommetEnCours = idSommetDebut;
            int idRand = 0;
            do
            {
                listCheminPossible = listArc.FindAll(a => a.getSommet1().getId() == idSommetEnCours);
                // Affiche la liste des chemins possibles
                /*foreach (Arc ar in listCheminPossible)
                {
                    Console.WriteLine("s1: " + ar.getSommet1().getId() + ":" + ar.getSommet1().getNom() + " - s2: " + ar.getSommet2().getId() + ":" + ar.getSommet2().getNom() + " - tps: " + ar.getTemps());
                }*/
                idRand = rdm.Next(0, listCheminPossible.Count());
                idSommetEnCours = listCheminPossible.ElementAt(idRand).getSommet2().getId();
                fourmi.setChemin(listCheminPossible.ElementAt(idRand));
                //Console.WriteLine(listCheminPossible.ElementAt(idRand).getSommet1().getId() + " -> " + listCheminPossible.ElementAt(idRand).getSommet2().getId());
            }
            while (idSommetEnCours != idSommetFin);

                foreach (Arc ar in fourmi.getListChemin())
                {
                    Console.WriteLine("s1: " + ar.getSommet1().getId() + ":" + ar.getSommet1().getNom() + " - s2: " + ar.getSommet2().getId() + ":" + ar.getSommet2().getNom() + " - tps: " + ar.getTemps());
                }

        }

        
    }
}
