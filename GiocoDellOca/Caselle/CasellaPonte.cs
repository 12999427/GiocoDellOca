using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaPonte : CasellaSpeciale
    {
        MuovitiADelegato muovitiA;
        public CasellaPonte(int numero, MuovitiADelegato muovitiA) : base("ponte", numero)
        {
            this.muovitiA = muovitiA;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            muovitiA?.Invoke(numGiocatore, posAttuale + numMosse);
        }
    }
}
