using GiocoDellOca.Caselle;
using GiocoDellOca.Properties;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GiocoDellOca
{
    public partial class Form1 : Form
    {
        const int pathLen = 63;
        System.Windows.Forms.Timer resizeTimer;
        private const int RESIZE_TICK_TIME_MS = 500;
        private bool isInitialized = false;
        GestoreGioco gestore;

        Bitmap[,] tavolaImgCaselle = new Bitmap[7, 4];

        public Form1()
        {
            InitializeComponent();
            gestore = new(pathLen, new int[] { 5, 9, 18, 27, 36, 45, 54 }, new int[] { 6 }, new int[] { 19 }, new int[] { 31 }, new int[] { 42 }, new int[] { 58 }, FinePartita);
            ProduciTutteImmagini();
            CreateDynamicGrid();

            // Timer for dynamic resizing
            resizeTimer = new System.Windows.Forms.Timer();
            resizeTimer.Interval = RESIZE_TICK_TIME_MS;
            resizeTimer.Tick += ResizeTimer_Tick;

            isInitialized = true;
        }

        private void ResizeTimer_Tick(object? sender, EventArgs e)
        {
            resizeTimer.Stop();
            setGridSize();
        }

        public void FinePartita (object? sender, FineGiocoEventArgs fgea)
        {

        }

        private void setGridSize()
        {
            const int BORDER_VALUE = 3;

            int size = Math.Min((pnl_gamePanel.ClientSize.Width - BORDER_VALUE) / dtg_Campo.ColumnCount, (pnl_gamePanel.ClientSize.Height - BORDER_VALUE) / dtg_Campo.RowCount);

            foreach (DataGridViewColumn column in dtg_Campo.Columns)
            {
                column.Width = size;
            }
            foreach (DataGridViewRow row in dtg_Campo.Rows)
            {
                row.Height = size;
            }

            dtg_Campo.Width = size * dtg_Campo.ColumnCount + BORDER_VALUE;
            dtg_Campo.Height = size * dtg_Campo.RowCount + BORDER_VALUE;

        }

        private void CreateDynamicGrid()
        {
            if (pathLen == 0)
                return;

            int dimensioneY = 7 + 2;
            int dimensioneX = 2 + 16;

            // 2+(int)Math.Ceiling(pathLen / 8f) * 2 

            dtg_Campo.ColumnCount = dimensioneX;
            dtg_Campo.RowCount = dimensioneY;

            int colorate = 0;
            bool interrupt = false;
            bool rigaOrizzontalePari = true;

            for (int y = 1; y < (dimensioneY - 1) && !interrupt; y++)
            {
                if (y % 2 == 1)
                {
                    for (int x = (rigaOrizzontalePari ? 1 : dimensioneX - 2); (rigaOrizzontalePari ? x < (dimensioneX - 1) : x >= 1); x += (rigaOrizzontalePari ? 1 : -1))
                    {
                        if (colorate++ < pathLen)
                        {
                            ImpostaSingolaCella(x, y, colorate - 1);
                        }
                        else
                        {
                            interrupt = true;
                            break;
                        }
                    }
                    rigaOrizzontalePari = !rigaOrizzontalePari;
                }
                else
                {
                    if (colorate++ >= pathLen)
                    {
                        interrupt = true;
                    }
                    else
                    {
                        int x = (y - 1) % 4 == 3 ? 1 : dimensioneX - 2;
                        ImpostaSingolaCella(x, y, colorate - 1);
                    }
                }

            }

            setGridSize();
        }

        private void ImpostaSingolaCella(int x, int y, int num)
        {
            bool p1 = gestore.PosizioniGiocatori[0] == num;
            bool p2 = gestore.PosizioniGiocatori[1] == num;
            if (gestore.Caselle[num] is CasellaGenerica && !p1 && !p2)
            {
                var cella = new DataGridViewTextBoxCell();
                cella.Value = num + 1;
                dtg_Campo[x, y] = cella;
            }
            else
            {
                var cella = new DataGridViewImageCell();
                if (gestore.Caselle[num] is CasellaGenerica)
                    cella.Value = OttieniImmagine("", p1, p2);
                else if (gestore.Caselle[num] is CasellaSpeciale cs)
                    cella.Value = OttieniImmagine(cs.NomeFigura, p1, p2);
                //cella.Value = Resources.oca;
                cella.ImageLayout = DataGridViewImageCellLayout.Stretch;
                dtg_Campo[x, y] = cella; //sostituisce
            }

            dtg_Campo[x, y].Tag = num;

            bool coloreScacchiera = (x + y) % 2 == 0;
            dtg_Campo.Rows[y].Cells[x].Style.BackColor = coloreScacchiera ? Color.Green : Color.LightGreen;
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            if (!isInitialized)
                return;
            resizeTimer.Stop();
            resizeTimer.Start();
        }

        private void AggiornaPosizioniGiocatori()
        {
            CreateDynamicGrid();
        }

        private Bitmap CombinaImmagini(Bitmap img1, Bitmap img2, (int x, int y) posSeconda)
        {
            Bitmap result = new Bitmap(img1.Width, img1.Height);

            // Uso Graphics per disegnare
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(img1, 0, 0);

                g.DrawImage(img2, posSeconda.x, posSeconda.y);
            }

            return result;
        }

        private void ProduciTutteImmagini()
        {
            Bitmap[] risorse = new Bitmap[]
            {
                Resources.casa,
                Resources.labirinto,
                Resources.oca,
                Resources.ponte,
                Resources.prigione,
                Resources.scheletro
            };

            // Posizioni delle pedine
            (int x, int y) posBlu = (50, 100);
            (int x, int y) posRossa = (300, 100);

            // Normali
            for (int i = 0; i < 6; i++)
            {
                tavolaImgCaselle[i, 0] = risorse[i];
            }

            // Blu
            for (int i = 0; i < 6; i++)
            {
                tavolaImgCaselle[i, 1] = CombinaImmagini(risorse[i], Resources.pedina_blu, posBlu);
            }

            // Rossa
            for (int i = 0; i < 6; i++)
            {
                tavolaImgCaselle[i, 2] = CombinaImmagini(risorse[i], Resources.pedina_rossa, posRossa);
            }

            // Entrambe
            for (int i = 0; i < 6; i++)
            {
                Bitmap temp = CombinaImmagini(risorse[i], Resources.pedina_blu, posBlu);
                tavolaImgCaselle[i, 3] = CombinaImmagini(temp, Resources.pedina_rossa, posRossa);
            }

            // Solo P1, solo P2, P1+P2
            // [7, 0] è vuoto e mai usato perchè ci sarebbe il numero e non l'imamgine
            tavolaImgCaselle[6, 1] = CombinaImmagini(new Bitmap(500, 500), Resources.pedina_blu, posBlu);
            tavolaImgCaselle[6, 2] = CombinaImmagini(new Bitmap(500, 500), Resources.pedina_rossa, posRossa);
            tavolaImgCaselle[6, 3] = CombinaImmagini(CombinaImmagini(new Bitmap(500, 500), Resources.pedina_blu, posBlu), Resources.pedina_rossa, posRossa);

        }


        private void btn_Giocatore_1_Dadi_Click(object sender, EventArgs e)
        {
            gestore.AvanzaRandom(0);
            AggiornaPosizioniGiocatori();
            btn_Giocatore_1_Dadi.Enabled = false;
            btn_Giocatore_2_Dadi.Enabled = true;
        }

        private void btn_Giocatore_2_Dadi_Click(object sender, EventArgs e)
        {
            gestore.AvanzaRandom(1);
            AggiornaPosizioniGiocatori();
            btn_Giocatore_1_Dadi.Enabled = true;
            btn_Giocatore_2_Dadi.Enabled = false;
        }

        private Bitmap OttieniImmagine(string nomeCasella, bool p1, bool p2)
        {
            string[] nomiRisorse = { "casa", "labirinto", "oca", "ponte", "prigione", "scheletro" };

            int indice = Array.IndexOf(nomiRisorse, nomeCasella.ToLower());

            // Determina la colonna
            int colonna = 0;
            if (p1 && p2)
                colonna = 3;
            else if (p1)
                colonna = 1;
            else if (p2)
                colonna = 2;


            if (indice == -1)
            {
                if (colonna == 0)
                    return null; //non dovrebbe succedere
                else
                    return tavolaImgCaselle[6, colonna];

            }

            return tavolaImgCaselle[indice, colonna];
        }


    }
}
