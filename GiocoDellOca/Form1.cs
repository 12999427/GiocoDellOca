using GiocoDellOca.Caselle;
using GiocoDellOca.Properties;
using System.Numerics;
using System.Windows.Forms;

namespace GiocoDellOca
{
    public partial class Form1 : Form
    {
        const int pathLen = 63;
        System.Windows.Forms.Timer resizeTimer;
        private const int RESIZE_TICK_TIME_MS = 500;
        private bool isInitialized = false;
        GestoreGioco gestore;

        public Form1()
        {
            InitializeComponent();
            gestore = new(pathLen, new int[] { 5, 9, 18, 27, 36, 45, 54 }, new int[] { 6 }, new int[] { 19 }, new int[] { 31 }, new int[] { 42 }, new int[] { 58 });

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
            int dimensioneX = 2 + /*(int)Math.Ceiling(pathLen / 8f) * 2*/ 15;

            dtg_Campo.ColumnCount = dimensioneX;
            dtg_Campo.RowCount = dimensioneY;

            int colorate = 0;
            bool interrupt = false;

            for (int y = 1; y < (dimensioneY - 1) && !interrupt; y++)
            {
                if (y % 2 == 1)
                {
                    bool normalDir = (pathLen % ((dimensioneX - 1) * 2)) < (dimensioneX - 1);
                    for (int x = (normalDir ? 1 : dimensioneX - 2); (normalDir ? x < (dimensioneX - 1) : x >= 1); x += (normalDir ? 1 : -1))
                    {
                        if (colorate++ < pathLen)
                        {
                            ImpostaSingolaCella(x, y, dimensioneX, dimensioneY);
                        }
                        else
                        {
                            interrupt = true;
                            break;
                        }
                    }
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
                        ImpostaSingolaCella(x, y, dimensioneX, dimensioneY);
                    }
                }

            }

            setGridSize();
        }

        private int GetIndexFromXYCell(int x, int y, int dimX, int dimY)
        {
            // Numero di blocchi completi di 2 righe (una lunga + una corta)
            int block = (y - 1) / 2;

            // Celle già assegnate nelle righe precedenti
            int prev = block * (dimX - 1);

            // Se siamo in una riga pari -> è sempre la cella singola verticale
            if (y % 2 == 0)
            {
                return prev + (dimX - 2);  // sempre l’ultima del blocco precedente
            }

            // --- Riga dispari: riga orizzontale del serpente ---

            bool leftToRight = (block % 2 == 0);

            if (leftToRight)
                return prev + (x - 1);        // va da 1 → dimX-2
            else
                return prev + ((dimX - 2) - (x - 1));  // va da dimX-2 → 1
        }


        private void ImpostaSingolaCella(int x, int y, int dimX, int dimY)
        {
            int num = GetIndexFromXYCell(x, y, dimX, dimY);
            if (gestore.Caselle[num] is CasellaGenerica)
            {
                dtg_Campo[x, y].Value = num;
            }
            else
            {
                var cella = new DataGridViewImageCell();
                cella.Value = Resources.labirinto;  //mette immagine dentro
                cella.ImageLayout = DataGridViewImageCellLayout.Stretch;    //la proporziona alla grandezza
                dtg_Campo[x, y] = cella;   //assegna al datagridview che la sostiruice;
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
    }
}
