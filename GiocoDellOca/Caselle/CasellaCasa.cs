using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca.Caselle
{
    internal class CasellaCasa : CasellaSpeciale
    {
        AumentaAttesaDelegato aumentaAttesa;
        public CasellaCasa(int numero, AumentaAttesaDelegato aumentaAttesa) : base("casa", numero)
        {
            this.aumentaAttesa += aumentaAttesa;
        }

        public override void ArrivaSuCella(int numGiocatore, int numMosse, int posAttuale)
        {
            aumentaAttesa?.Invoke(numGiocatore, 3);
        }
    }
}
