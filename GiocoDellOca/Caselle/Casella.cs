using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal abstract class Casella
    {
        private Bitmap figura;
        public Bitmap Figura
        {
            get => figura;
            protected set
            {
                figura = value;
            }
        }


    }
}
