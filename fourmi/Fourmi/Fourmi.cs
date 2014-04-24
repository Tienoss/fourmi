using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fourmi
{
    class Fourmi
    {
        private List<Arc> listChemin;
        private float duree;

        
        public Fourmi() {
            listChemin = new List<Arc>();
            duree = 0;
        }

        public float getDuree()
        {
            return duree;
        }

        public void removeListChemin()
        {
            listChemin = new List<Arc>();
            duree = 0;
        }
        public List<Arc> getListChemin()
        {
            return listChemin;
        }

        public void setChemin(Arc arc)
        {
            listChemin.Add(arc);
            duree += arc.getTemps();
        }

    }
}
