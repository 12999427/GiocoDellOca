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

        public Form1()
        {
            InitializeComponent();

            int[] caselleOca = new int[] { 5, 9, 18, 27, 36, 45, 54 };
            int[] casellePonte = new int[] { 6 };
            int[] caselleCasa = new int[] { 19 };
            int[] casellePrigione = new int[] { 31 };
            int[] caselleLabirinto = new int[] { 42 };
            int[] caselleScheletro = new int[] { 58 };
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
            int dimensioneX = 2 + (int)Math.Ceiling(pathLen / 8f) * 2;
            //dtg_Campo.ColumnCount = dimensioneX;

            for (int i = 0; i < dimensioneX; i++)
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn();
                imgColumn.ImageLayout = DataGridViewImageCellLayout.Zoom; // o Stretch, Normal, ecc.
                dtg_Campo.Columns.Add(imgColumn);
            }

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
                            bool coloreScacchiera = (x + y) % 2 == 0;
                            dtg_Campo.Rows[y].Cells[x].Style.BackColor = coloreScacchiera ? Color.Green : Color.LightGreen;
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
                        bool coloreScacchiera = (x + y) % 2 == 0;

                        dtg_Campo.Rows[y].Cells[x].Style.BackColor = coloreScacchiera ? Color.Green : Color.LightGreen;
                    }
                }

            }

            setGridSize();
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
