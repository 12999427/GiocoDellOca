using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaOca : CasellaSpeciale
    {
        MuovitiADelegato muovitiA;
        public CasellaOca(int numero, MuovitiADelegato muovitiA) : base("oca", numero)
        {
            this.muovitiA = muovitiA;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            muovitiA?.Invoke(numGiocatore, posAttuale + numMosse);
        }
    }
}
