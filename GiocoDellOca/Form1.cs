using GiocoDellOca.Caselle;
using GiocoDellOca.Properties;
using System.Numerics;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GiocoDellOca
{
    public partial class Form1 : Form
    {
        const int pathLen = 64;
        System.Windows.Forms.Timer resizeTimer;
        System.Windows.Forms.Timer moveTimer;
        private const int RESIZE_TICK_TIME_MS = 500;
        private bool isInitialized = false;
        GestoreGioco gestore;

        Bitmap[,] tavolaImgCaselle = new Bitmap[7, 4];
        int CurrentPlayer = 0;

        public Form1()
        {
            InitializeComponent();
            gestore = new(pathLen,
                new int[] { 4, 8, 17, 26, 35, 44, 53 },
                new int[] { 5 },
                new int[] { 18 },
                new int[] { 30 },
                new int[] { 41 },
                new int[] { 57 },
                FinePartita
            );
            ProduciTutteImmagini();

            CreateDynamicGrid(0, 0);

            // Timer for dynamic resizing
            resizeTimer = new System.Windows.Forms.Timer();
            resizeTimer.Interval = RESIZE_TICK_TIME_MS;
            resizeTimer.Tick += ResizeTimer_Tick;

            moveTimer = new System.Windows.Forms.Timer();
            moveTimer.Interval = 100;
            moveTimer.Tick += AnimFrame;

            isInitialized = true;
        }

        private void ResizeTimer_Tick(object? sender, EventArgs e)
        {
            resizeTimer.Stop();
            setGridSize();
        }

        private void AnimFrame(object? sender, EventArgs e)
        {

            int len = gestore.buffer.Count;
            if (len == 0)
            {
                moveTimer.Stop();
                CurrentPlayer = -CurrentPlayer - 1 ; //il giocatore di cui si stava svolgendo l'anim
                CurrentPlayer = 1 - CurrentPlayer; //passa turno all'altro
                btn_Giocatore_1_Dadi.Enabled = CurrentPlayer == 0;
                btn_Giocatore_2_Dadi.Enabled = CurrentPlayer == 1;
                lbl_Stato.Text = $"Posizione giocatore 1: {gestore.PosizioniGiocatori[0] + 1}\n" +
                    $"Posizione giocatore 2: {gestore.PosizioniGiocatori[1] + 1}";
                return;
            }

            var currentPlacement = gestore.buffer[0];
            gestore.buffer.RemoveAt(0);

            lbl_Stato.Text = $"Posizione giocatore 1: {currentPlacement[0] + 1}\n" +
                $"Posizione giocatore 2: {currentPlacement[1] + 1}";

            CreateDynamicGrid(currentPlacement[0], currentPlacement[1]);
        }

        public void FinePartita (object? sender, FineGiocoEventArgs fgea)
        {
            MessageBox.Show("Ha vinto giocatore: " + fgea.giocatoreVincitore);
            btn_Giocatore_1_Dadi.Enabled = false;
            btn_Giocatore_2_Dadi.Enabled = false;
            CreateDynamicGrid(gestore.PosizioniGiocatori[0], gestore.PosizioniGiocatori[0]);
            moveTimer.Stop();
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

        private void CreateDynamicGrid(int p1Pos, int p2Pos)
        {
            if (pathLen == 0)
                return;

            int dimensioneY = 7 + 2;
            int dimensioneX = 2 + 16;

            // 2+(int)Math.Ceiling(pathLen / 8f) * 2 

            dtg_Campo.ColumnCount = dimensioneX;
            dtg_Campo.RowCount = dimensioneY;

            //correggi prima cella
            /*var cella = new DataGridViewTextBoxCell();
            dtg_Campo[0, 0] = cella;
            dtg_Campo[0, 0].Style.BackColor = Color.Green;
            */

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
                            ImpostaSingolaCella(x, y, colorate - 1, p1Pos, p2Pos);
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
                        ImpostaSingolaCella(x, y, colorate - 1, p1Pos, p2Pos);
                    }
                }

            }

            setGridSize();
        }

        private void ImpostaSingolaCella(int x, int y, int num, int p1Pos, int p2Pos)
        {
            bool p1 = p1Pos == num;
            bool p2 = p2Pos == num;

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
                {
                    cella.Value = OttieniImmagine("", p1, p2);
                    dtg_Campo.Rows[y].Cells[x].Style.BackColor = Color.Green;
                }
                else if (gestore.Caselle[num] is CasellaSpeciale cs)
                {
                    cella.Value = OttieniImmagine(cs.NomeFigura, p1, p2);
                    //cella.Value = Resources.oca;
                }
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
            // [6, 0] è vuoto e mai usato perchè ci sarebbe il numero e non l'imamgine
            tavolaImgCaselle[6, 1] = CombinaImmagini(new Bitmap(500, 500), Resources.pedina_blu, posBlu);
            tavolaImgCaselle[6, 2] = CombinaImmagini(new Bitmap(500, 500), Resources.pedina_rossa, posRossa);
            tavolaImgCaselle[6, 3] = CombinaImmagini(CombinaImmagini(new Bitmap(500, 500), Resources.pedina_blu, posBlu), Resources.pedina_rossa, posRossa);

        }


        private void btn_Giocatore_1_Dadi_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer != 0)
                return;

            //MessageBox.Show(gestore.AvanzaRandom(0).ToString());
            gestore.AvanzaRandom(0);
            CurrentPlayer = -1 - CurrentPlayer; //-1 per giocatore 0 in anim, -2 per giocatore 1 in anim
            moveTimer.Start();
            btn_Giocatore_1_Dadi.Enabled = false;
            btn_Giocatore_2_Dadi.Enabled = false;


        }

        private void btn_Giocatore_2_Dadi_Click(object sender, EventArgs e)
        {
            if (CurrentPlayer != 1)
                return;

            //MessageBox.Show(gestore.AvanzaRandom(1).ToString());
            gestore.AvanzaRandom(1);
            CurrentPlayer = -1 - CurrentPlayer; //-1 per giocatore 0 in anim, -2 per giocatore 1 in anim
            moveTimer.Start();
            btn_Giocatore_1_Dadi.Enabled = false;
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
