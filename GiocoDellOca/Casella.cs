using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca
{
    internal class Casella
    {
        private int numero;
        public int Numero
        {
            get => numero;
            set
            {
                if (value < 0)
                    throw new ArgumentException();

                numero = value;
            }
        }
    }
}
