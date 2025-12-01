using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal abstract class Casella
    {
        private int numero;
        public int Numero
        {
            get => numero;
            protected set
            {
                numero = value;
            }
        }

        protected Casella (int numero)
        {
            Numero = numero;
        }

        public virtual bool PuoLasciareCasella (int numGiocatore)
        {
            return true;
        }
    }
}
