using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiocoDellOca.Caselle;

namespace GiocoDellOca
{
    internal class GestoreGioco
    {
        Random random;
        public List<Casella> Caselle { get; protected set; }
        List<int> PosizioniGiocatori;
        int TotPathLen;

        public GestoreGioco(int totLen, int[] caselleOca, int[] casellePonte, int[] caselleCasa, int[] casellePrigione, int[] caselleLabirinto, int[] caselleScheletro)
        {
            TotPathLen = totLen;
            random = new Random(Environment.TickCount);
            GeneraCaselle(totLen, caselleOca, casellePonte, caselleCasa, casellePrigione, caselleLabirinto, caselleScheletro);
        }

        public void GeneraCaselle(int len, int[] caselleOca, int[] casellePonte, int[] caselleCasa, int[] casellePrigione, int[] caselleLabirinto, int[] caselleScheletro)
        {
            Caselle = new List<Casella>();

            for (int i = 0; i<len; i++)
            {
                if (caselleOca.Contains(i))
                    Caselle.Add(new CasellaOca());
                else if (casellePonte.Contains(i))
                    Caselle.Add(new CasellaPonte());
                else if (caselleCasa.Contains(i))
                    Caselle.Add(new CasellaCasa());
                else if (casellePrigione.Contains(i))
                    Caselle.Add(new CasellaPrigione());
                else if (caselleLabirinto.Contains(i))
                    Caselle.Add(new CasellaLabirinto());
                else if (caselleScheletro.Contains(i))
                    Caselle.Add(new CasellaScheletro());
                else
                    Caselle.Add(new CasellaGenerica());
            }
        }

        public void Avanza(int numGiocatore)
        {
            int mosse = random.Next(0, 7) + random.Next(0, 7);

            int delta = Math.Min(0, TotPathLen - (PosizioniGiocatori[numGiocatore] + mosse));

            PosizioniGiocatori[numGiocatore] += mosse;
            PosizioniGiocatori[numGiocatore] -= delta;

            if (PosizioniGiocatori[numGiocatore] > TotPathLen)
            {
                PosizioniGiocatori[numGiocatore] -= mosse;
            }
            else
            {
                PosizioniGiocatori[numGiocatore] += mosse;
            }

            if (PosizioniGiocatori[numGiocatore] == TotPathLen)
            {
                //vittoria
            }
            else
            {
                //scatena situazioni
            }
        }
    }
}
