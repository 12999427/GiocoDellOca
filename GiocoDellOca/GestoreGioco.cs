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
        List<List<int>> buffer;

        public GestoreGioco(int totLen, int[] caselleOca, int[] casellePonte, int[] caselleCasa, int[] casellePrigione, int[] caselleLabirinto, int[] caselleScheletro, EventHandler<FineGiocoEventArgs> funzioneFinePartita)
        {
            TotPathLen = totLen;
            FineGioco += funzioneFinePartita;
            random = new Random(Environment.TickCount);
            PosizioniGiocatori = new List<int>() { 0, 0 };
            TurniAttesaGiocatori = new List<int> { 0, 0 };
            buffer = new();
            for (int i = 0; i<2; i++)
            {
                buffer.Add(new List<int>());
            }
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

        public List<List<int>> OttieniBufferMosse ()
        {
            List<List<int>> copy = buffer.ToList();
            return copy;
        }

        public int Avanza(int numGiocatore, int numMosse)
        {
            if (TurniAttesaGiocatori[numGiocatore] != 0 && Caselle[PosizioniGiocatori[numGiocatore]].PuoLasciareCasella(numGiocatore))
            {
                TurniAttesaGiocatori[numGiocatore]--;
                MessageBox.Show("Attesa");
                return PosizioniGiocatori[numGiocatore];
            }
            else
            {
                //MessageBox.Show($"{PosizioniGiocatori[numGiocatore]} {numMosse.ToString()}");
                int downDelta = Math.Max(0, (PosizioniGiocatori[numGiocatore] + numMosse) - TotPathLen);
                int upDelta = Math.Min(PosizioniGiocatori[numGiocatore] + numMosse, TotPathLen);

                //MessageBox.Show($"Posizione attuale: {PosizioniGiocatori[numGiocatore]}, cresciuto a {upDelta}, sceso di {downDelta}");

                
                for (int i = 1; i<=upDelta; i++)
                {
                    buffer[numGiocatore].Add(PosizioniGiocatori[numGiocatore] + i);
                }

                PosizioniGiocatori[numGiocatore] = upDelta;

                for (int i = 1; i <= downDelta; i++)
                {
                    buffer[numGiocatore].Add(PosizioniGiocatori[numGiocatore] - i);
                }

                PosizioniGiocatori[numGiocatore] -= downDelta;


                //una volta arrivato sulla cella giusta:

                if (PosizioniGiocatori[numGiocatore] == TotPathLen-1)
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
