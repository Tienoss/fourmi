using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmi
{
    class Arc
    {
        private Sommet sommet1;
        private Sommet sommet2;
        private float temps;
        private int pheromone;

        public Sommet getSommet1() { return sommet1; }
        public Sommet getSommet2() { return sommet2; }
        public float getTemps() { return temps; }
        public int getPheromone() { return pheromone; }

        public Arc() { }

        public Arc(Sommet sommet1, Sommet sommet2, float temps)
        {
            this.sommet1 = sommet1;
            this.sommet2 = sommet2;
            this.temps = temps;
        }



    }
}
