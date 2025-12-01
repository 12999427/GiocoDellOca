using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaScheletro : CasellaSpeciale
    {
        MuovitiADelegato muovitiA;
        public CasellaScheletro(int numero, MuovitiADelegato muovitiA) : base("scheletro", numero)
        {
            this.muovitiA = muovitiA;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            muovitiA?.Invoke(numGiocatore, 1);
        }
    }
}
