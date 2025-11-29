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
            button1 = new Button();
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
            // button1
            // 
            button1.Location = new Point(0, 0);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(846, 601);
            Controls.Add(button1);
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
        private Button button1;
    }
}
