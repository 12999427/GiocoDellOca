using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaLabirinto : CasellaSpeciale
    {
        MuovitiADelegato muovitiA;
        public CasellaLabirinto(int numero, MuovitiADelegato muovitiA) : base("labirinto", numero)
        {
            this.muovitiA = muovitiA;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            muovitiA?.Invoke(numGiocatore, 38);
        }
    }
}
