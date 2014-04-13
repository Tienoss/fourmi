using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmi
{
    class Sommet
    {
        public List<Sommet> listSommet;
        
        private int id;
        private string nom;
        private int coordX;
        private int coordY;

        public int getId() { return id; }
        public string getNom() { return nom; }
        public int getCoordX() { return coordX; }
        public int getCoordY() { return coordY; }

        public void SetId(int id) { this.id = id; }
        public void SetNom(string nom) { this.nom = nom; }
        public void SetCoordX(int coordX) { this.coordX = coordX; }
        public void SetCoordY(int coordY) { this.coordY = coordY; }

        
        public Sommet() {}

        public Sommet(int id, string nom)
        {
            this.id = id;
            this.nom = nom;
        }

        public Sommet(int id, int coordX, int coordY)
        {
            this.id = id;
            this.coordX = coordX;
            this.coordY = coordY;
        }

        public List<Sommet> getListSommet()
        {
            return listSommet;
        }
    }
}
