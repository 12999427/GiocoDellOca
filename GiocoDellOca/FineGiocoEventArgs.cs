using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiocoDellOca
{
    public class FineGiocoEventArgs : EventArgs
    {
        public int giocatoreVincitore;
        public FineGiocoEventArgs (int n)
        {
            giocatoreVincitore = n;
        }
    }
}
