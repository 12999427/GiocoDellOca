using System.Numerics;

namespace GiocoDellOca
{
    public partial class Form1 : Form
    {
        const int pathLen = 63;
        public Form1()
        {
            InitializeComponent();
            InitializeField();
        }

        public void InitializeField()
        {
            dtg_Campo.Rows.Clear();
            for (int i = 0; i<2+Math.Ceiling((decimal)63/8)*2; i++)
            {
                dtg_Campo.Rows.Add("", "", "", "", "", "", "", "", "");
            }

            int colorate = 0;
            bool interrupt = false;

            for (int y = 1; y < (1 + Math.Ceiling((decimal)63 / 8) * 2) && !interrupt; y++)
            {
                if (y%2 == 1)
                {
                    for (int x = 1; x < 9; x++)
                    {
                        if (colorate++ < pathLen)
                        {
                            dtg_Campo.Rows[y].Cells[x].Style.BackColor = Color.Green;
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
                    if ((y-1)%4 == 1)
                    {
                        //colora
                        dtg_Campo.Rows[y].Cells[1].Style.BackColor = Color.Green;
                    }
                    else
                    {
                        dtg_Campo.Rows[y].Cells[8].Style.BackColor = Color.Green;
                    }
                }

            }
        }
    }
}
