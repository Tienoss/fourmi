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
            List<Fourmi> listFourmi = new List<Fourmi>();
            List<Arc> listCheminPossible;
            List<Arc> listProbaChemin;
            Fourmi fourmi;
            float dureeMin;

            int nbFourmi = 1000;

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

            //idSommetDebut = 48;
            //idSommetFin = 16;
            #endregion

            #region 

            //sélection du chemin

            Console.WriteLine("sommet de départ : " + idSommetDebut + " - " + listSommet.Find(s => s.getId() == idSommetDebut).getNom());
            Console.WriteLine("sommet d'arrivée : " + idSommetFin + " - " + listSommet.Find(s => s.getId() == idSommetFin).getNom());

            dureeMin = -1;

            // boucle sur le nombre de Fourmi
            for (int i = 0; i < nbFourmi; i++)
            {
                fourmi = new Fourmi();
                listCheminPossible = new List<Arc>();

                int idSommetEnCours = idSommetDebut;
                int idRand = 0;

                #region Sélection du parcours
                //choisir un parcours jusqu'à ce que le sommet en cours soit le sommet d'arrivée
                do
                {
                    //récupérer la liste des arcs depuis le sommet actuel et suppression des arcs déjà parcouru de cette liste
                    listCheminPossible = listArc.FindAll(a => a.getSommet1().getId() == idSommetEnCours);
                    listCheminPossible.RemoveAll(c => fourmi.getListChemin().Contains(c));

                    listProbaChemin = new List<Arc>();

                    //si la liste des chemins possible est > 0 alors on choisi le prochain sommet
                    if (listCheminPossible.Count() != 0)
                    {
                        foreach(Arc chemin in listCheminPossible)
                        {
                            for(int j = 0; j < chemin.getPheromone(); j++)
                            {
                                listProbaChemin.Add(chemin);
                            }
                        }

                        idRand = rdm.Next(0, listProbaChemin.Count());
                        idSommetEnCours = listProbaChemin.ElementAt(idRand).getSommet2().getId();
                        //on garde l'historique du parcours
                        fourmi.setChemin(listProbaChemin.ElementAt(idRand));
                    }
                    else
                    {
                        //sinon on réinitialise le parcours de la fourmi
                        idSommetEnCours = idSommetDebut;
                        fourmi.removeListChemin();
                    }
                }
                while (idSommetEnCours != idSommetFin);

                listFourmi.Add(fourmi);

                #endregion

                // ajouter les phéromones a listArc
                //récupérer la liste des arc de listChemin et ajouter des phéromones aux arcs correspondants dans listArc
                foreach (Arc ar in fourmi.getListChemin())
                {
                    listArc.Find(a => a == ar).augmentePheromone();
                }

                foreach (Arc ar in fourmi.getListChemin())
                {
                    listArc.Find(a => a == ar).diminuePheromone();
                }

                if (dureeMin == -1 || fourmi.getDuree() < dureeMin)
                {
                    dureeMin = fourmi.getDuree();
                }
            }
            
            #endregion

            Fourmi fourmiMin;
            fourmiMin = listFourmi.Find(f => f.getDuree() == listFourmi.Min(fo => fo.getDuree()));

            foreach (Arc ar in fourmiMin.getListChemin())
            {
                Console.WriteLine("s1: " + ar.getSommet1().getId() + ":" + ar.getSommet1().getNom() + " - s2: " + ar.getSommet2().getId() + ":" + ar.getSommet2().getNom() + " - tps: " + ar.getTemps());
            }

            foreach (Fourmi f in listFourmi)
            {
                Console.WriteLine(listFourmi.FindIndex(fo => fo == f).ToString() +
                    " - Durée min : " + f.getDuree() +
                    " - Nombre de chemin : " + f.getListChemin().Count());
            }

            Console.WriteLine(listFourmi.FindIndex(fo => fo == fourmiMin).ToString() +
                    " - Durée min : " + fourmiMin.getDuree() +
                    " - Nombre de chemin : " + fourmiMin.getListChemin().Count());
            /*foreach (Fourmi fourm in listFourmi)
            {
                Console.WriteLine("\nDurée : " +
                    "\nsecondes : " + fourmi.getDuree() +
                    "\nminutes : " + fourmi.getDuree() / 60 +
                    "\nheures : " + fourmi.getDuree() / 60 / 60);
            }*/

        }
    }
}
