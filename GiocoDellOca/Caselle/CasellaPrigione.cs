using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaPrigione : CasellaSpeciale
    {
        public int numGiocatoreIntrappolato = -1;
        public CasellaPrigione(int numero) : base("prigione", numero) { }

        public override bool PuoLasciareCasella(int numGiocatore)
        {
            return numGiocatoreIntrappolato != numGiocatore;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            numGiocatoreIntrappolato = numGiocatore;
        }
    }
}
