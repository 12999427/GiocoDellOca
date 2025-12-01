using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GiocoDellOca.Caselle;

namespace GiocoDellOca
{
    public delegate void AumentaAttesaDelegato(int numGiocatore, int numAttese);
    public delegate void MuovitiADelegato(int numGiocatore, int posizioneNuova);
    internal class GestoreGioco
    {
        Random random;
        public List<Casella> Caselle { get; protected set; }
        public List<int> PosizioniGiocatori { get; protected set; }
        private List<int> TurniAttesaGiocatori;
        int TotPathLen;
        event EventHandler<FineGiocoEventArgs> FineGioco;

        public GestoreGioco(int totLen, int[] caselleOca, int[] casellePonte, int[] caselleCasa, int[] casellePrigione, int[] caselleLabirinto, int[] caselleScheletro, EventHandler<FineGiocoEventArgs> funzioneFinePartita)
        {
            TotPathLen = totLen;
            FineGioco += funzioneFinePartita;
            random = new Random(Environment.TickCount);
            PosizioniGiocatori = new List<int>() { 0, 0 };
            TurniAttesaGiocatori = new List<int> { 0, 0 };
            GeneraCaselle(totLen, caselleOca, casellePonte, caselleCasa, casellePrigione, caselleLabirinto, caselleScheletro);
        }

        public void GeneraCaselle(int len, int[] caselleOca, int[] casellePonte, int[] caselleCasa, int[] casellePrigione, int[] caselleLabirinto, int[] caselleScheletro)
        {
            Caselle = new List<Casella>();

            for (int i = 0; i<len; i++)
            {
                if (caselleOca.Contains(i))
                    Caselle.Add(new CasellaOca(i, MuovitiA));
                else if (casellePonte.Contains(i))
                    Caselle.Add(new CasellaPonte(i, MuovitiA));
                else if (caselleCasa.Contains(i))
                    Caselle.Add(new CasellaCasa(i, AumentaAttesa));
                else if (casellePrigione.Contains(i))
                    Caselle.Add(new CasellaPrigione(i));
                else if (caselleLabirinto.Contains(i))
                    Caselle.Add(new CasellaLabirinto(i, MuovitiA));
                else if (caselleScheletro.Contains(i))
                    Caselle.Add(new CasellaScheletro(i, MuovitiA));
                else
                    Caselle.Add(new CasellaGenerica(i));
            }
        }

        public int AvanzaRandom (int numGiocatore)
        {
            int mosse = random.Next(0, 7) + random.Next(0, 7);
            return Avanza(numGiocatore, mosse);
        }

        public void AumentaAttesa(int numGiocatore, int numAttese=1)
        {
            TurniAttesaGiocatori[numGiocatore] += numAttese;
        }

        public void MuovitiA(int numGiocatore, int indiceCellaNuovo)
        {
            Avanza(numGiocatore, indiceCellaNuovo - PosizioniGiocatori[numGiocatore]);
        }

        public int Avanza(int numGiocatore, int numMosse)
        {
            if (TurniAttesaGiocatori[numGiocatore] != 0 && Caselle[PosizioniGiocatori[numGiocatore]].PuoLasciareCasella(numGiocatore))
            {
                TurniAttesaGiocatori[numGiocatore]--;
                return PosizioniGiocatori[numGiocatore];
            }
            else
            {
                int delta = Math.Min(0, TotPathLen - (PosizioniGiocatori[numGiocatore] + numMosse));

                PosizioniGiocatori[numGiocatore] += numMosse;
                PosizioniGiocatori[numGiocatore] -= delta;

                if (PosizioniGiocatori[numGiocatore] > TotPathLen)
                {
                    PosizioniGiocatori[numGiocatore] -= numMosse;
                }
                else
                {
                    PosizioniGiocatori[numGiocatore] += numMosse;
                }

                //una volta arrivato sulla cella giusta:

                if (PosizioniGiocatori[numGiocatore] == TotPathLen)
                {
                    //vittoria
                }
                else
                {
                    if (Caselle[PosizioniGiocatori[numGiocatore]] is CasellaSpeciale cs)
                    {
                        cs.ArrivaSuCella(numGiocatore, numMosse, PosizioniGiocatori[numGiocatore]);
                    }
                }

                return PosizioniGiocatori[numGiocatore];
            }
        }
    }
}
