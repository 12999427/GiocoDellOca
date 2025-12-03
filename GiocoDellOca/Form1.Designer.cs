namespace GiocoDellOca
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dtg_Campo = new DataGridView();
            pnl_gamePanel = new Panel();
            btn_Giocatore_1_Dadi = new Button();
            btn_Giocatore_2_Dadi = new Button();
            lbl_Stato = new Label();
            ((System.ComponentModel.ISupportInitialize)dtg_Campo).BeginInit();
            pnl_gamePanel.SuspendLayout();
            SuspendLayout();
            // 
            // dtg_Campo
            // 
            dtg_Campo.AllowUserToAddRows = false;
            dtg_Campo.AllowUserToDeleteRows = false;
            dtg_Campo.AllowUserToResizeColumns = false;
            dtg_Campo.AllowUserToResizeRows = false;
            dtg_Campo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dtg_Campo.ColumnHeadersVisible = false;
            dtg_Campo.Location = new Point(0, 0);
            dtg_Campo.MultiSelect = false;
            dtg_Campo.Name = "dtg_Campo";
            dtg_Campo.ReadOnly = true;
            dtg_Campo.RowHeadersVisible = false;
            dtg_Campo.RowTemplate.ReadOnly = true;
            dtg_Campo.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dtg_Campo.Size = new Size(556, 426);
            dtg_Campo.TabIndex = 0;
            // 
            // pnl_gamePanel
            // 
            pnl_gamePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnl_gamePanel.BackColor = Color.Gainsboro;
            pnl_gamePanel.Controls.Add(dtg_Campo);
            pnl_gamePanel.Location = new Point(12, 79);
            pnl_gamePanel.Name = "pnl_gamePanel";
            pnl_gamePanel.Size = new Size(822, 510);
            pnl_gamePanel.TabIndex = 1;
            // 
            // btn_Giocatore_1_Dadi
            // 
            btn_Giocatore_1_Dadi.Font = new Font("Sylfaen", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            btn_Giocatore_1_Dadi.Location = new Point(12, 12);
            btn_Giocatore_1_Dadi.Name = "btn_Giocatore_1_Dadi";
            btn_Giocatore_1_Dadi.Size = new Size(145, 61);
            btn_Giocatore_1_Dadi.TabIndex = 1;
            btn_Giocatore_1_Dadi.Text = "Giocatore 1 Dadi";
            btn_Giocatore_1_Dadi.UseVisualStyleBackColor = true;
            btn_Giocatore_1_Dadi.Click += btn_Giocatore_1_Dadi_Click;
            // 
            // btn_Giocatore_2_Dadi
            // 
            btn_Giocatore_2_Dadi.Enabled = false;
            btn_Giocatore_2_Dadi.Font = new Font("Sylfaen", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            btn_Giocatore_2_Dadi.Location = new Point(558, 12);
            btn_Giocatore_2_Dadi.Name = "btn_Giocatore_2_Dadi";
            btn_Giocatore_2_Dadi.Size = new Size(145, 61);
            btn_Giocatore_2_Dadi.TabIndex = 2;
            btn_Giocatore_2_Dadi.Text = "Giocatore 2 Dadi";
            btn_Giocatore_2_Dadi.UseVisualStyleBackColor = true;
            btn_Giocatore_2_Dadi.Click += btn_Giocatore_2_Dadi_Click;
            // 
            // lbl_Stato
            // 
            lbl_Stato.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_Stato.Location = new Point(197, 12);
            lbl_Stato.Name = "lbl_Stato";
            lbl_Stato.Size = new Size(324, 61);
            lbl_Stato.TabIndex = 3;
            lbl_Stato.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 601);
            Controls.Add(lbl_Stato);
            Controls.Add(btn_Giocatore_2_Dadi);
            Controls.Add(btn_Giocatore_1_Dadi);
            Controls.Add(pnl_gamePanel);
            Name = "Form1";
            Text = "Form1";
            Resize += Form1_Resize;
            ((System.ComponentModel.ISupportInitialize)dtg_Campo).EndInit();
            pnl_gamePanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dtg_Campo;
        private Panel pnl_gamePanel;
        private Button btn_Giocatore_1_Dadi;
        private Button btn_Giocatore_2_Dadi;
        private Label lbl_Stato;
    }
}
