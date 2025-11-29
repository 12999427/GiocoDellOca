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
        List<Casella> Caselle;
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
                    Caselle[i] = new CasellaOca();
                else if (casellePonte.Contains(i))
                    Caselle[i] = new CasellaPonte();
                else if (caselleCasa.Contains(i))
                    Caselle[i] = new CasellaCasa();
                else if (casellePrigione.Contains(i))
                    Caselle[i] = new CasellaPrigione();
                else if (caselleLabirinto.Contains(i))
                    Caselle[i] = new CasellaLabirinto();
                else if (caselleScheletro.Contains(i))
                    Caselle[i] = new CasellaScheletro();
                else
                    Caselle[i] = new CasellaGenerica();
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
