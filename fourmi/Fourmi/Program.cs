﻿using System;
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
            List<Sommet> listSommet = new List<Sommet>();
            List<Sommet> listCoord = new List<Sommet>();
            List<Arc> listArc = new List<Arc>();

            //Création de la liste des sommets et de leur noms
            StreamReader reader = new StreamReader(File.OpenRead("csv/sommets-noms.csv"));
            
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
            StreamReader readerCoord = new StreamReader(File.OpenRead("csv/sommets-coordonnees.csv"));
            count = 0;
            while (!readerCoord.EndOfStream)
            {
                var line = readerCoord.ReadLine();
                var values = line.Split(';');

                if (count > 0)
                {

                    int index = listSommet.FindIndex(s => s.getId() == int.Parse(values[0]));
                    Sommet sommet = new Sommet(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]));
                    sommet.SetId(listSommet.ElementAt(index).getId());
                    sommet.SetNom(listSommet.ElementAt(index).getNom());
                    listSommet.Add(sommet);
                    listSommet.RemoveAt(index);
                }

                count++;
            }

            //Création de la liste des arcs
            Sommet sommet1;
            Sommet sommet2;
            Arc arc;

            StreamReader readerArc = new StreamReader(File.OpenRead("csv/valeurs-arcs.csv"));
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
            StreamReader readerArc2 = new StreamReader(File.OpenRead("csv/temps.csv"));
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
            foreach (Arc ar in listArc)
            /*{
                Console.WriteLine("s1: " + ar.getSommet1().getId() + ":" + ar.getSommet1().getNom() + " - s2: " + ar.getSommet2().getId() + ":" + ar.getSommet2().getNom() + " - tps: " + ar.getTemps());
            }*/
        }
    }
}