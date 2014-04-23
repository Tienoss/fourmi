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

        public Fourmi() {
            listChemin = new List<Arc>();
        }

        public List<Arc> getListChemin()
        {
            return listChemin;
        }

        public void setChemin(Arc arc)
        {
            listChemin.Add(arc);
        }

    }
}
