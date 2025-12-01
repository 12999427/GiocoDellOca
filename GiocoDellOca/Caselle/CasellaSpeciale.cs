using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal abstract class CasellaSpeciale : Casella
    {
        private string nomeFigura;
        public string NomeFigura
        {
            get => nomeFigura;
            protected set
            {
                nomeFigura = value;
            }
        }

        protected CasellaSpeciale(string nomeFigura, int numero) : base(numero)
        {
            NomeFigura = nomeFigura;
        }
        public abstract void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale);

    }
}
