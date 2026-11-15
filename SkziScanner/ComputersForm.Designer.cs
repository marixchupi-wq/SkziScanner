namespace SkziScanner
{
    partial class ComputersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComputersForm));
            this.computersDataGridView = new System.Windows.Forms.DataGridView();
            this.saveButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.scanNetworkButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.computersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // computersDataGridView
            // 
            this.computersDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.computersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.computersDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.computersDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.computersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.computersDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.computersDataGridView.EnableHeadersVisualStyles = false;
            this.computersDataGridView.Location = new System.Drawing.Point(14, 22);
            this.computersDataGridView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.computersDataGridView.Name = "computersDataGridView";
            this.computersDataGridView.RowHeadersVisible = false;
            this.computersDataGridView.RowHeadersWidth = 51;
            this.computersDataGridView.RowTemplate.Height = 24;
            this.computersDataGridView.Size = new System.Drawing.Size(615, 561);
            this.computersDataGridView.TabIndex = 0;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FlatAppearance.BorderSize = 0;
            this.saveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveButton.ForeColor = System.Drawing.Color.White;
            this.saveButton.Location = new System.Drawing.Point(424, 591);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(205, 52);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Сохранить";
            this.saveButton.UseVisualStyleBackColor = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Location = new System.Drawing.Point(14, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(616, 3);
            this.panel1.TabIndex = 13;
            // 
            // scanNetworkButton
            // 
            this.scanNetworkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scanNetworkButton.BackColor = System.Drawing.Color.White;
            this.scanNetworkButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scanNetworkButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanNetworkButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.scanNetworkButton.Location = new System.Drawing.Point(177, 591);
            this.scanNetworkButton.Name = "scanNetworkButton";
            this.scanNetworkButton.Size = new System.Drawing.Size(205, 52);
            this.scanNetworkButton.TabIndex = 14;
            this.scanNetworkButton.Text = "Найти в сети";
            this.scanNetworkButton.UseVisualStyleBackColor = false;
            this.scanNetworkButton.Click += new System.EventHandler(this.ScanNetworkButton_Click);
            // 
            // ComputersForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(641, 659);
            this.Controls.Add(this.scanNetworkButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.computersDataGridView);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ComputersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список компьютеров для сканирования";
            ((System.ComponentModel.ISupportInitialize)(this.computersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView computersDataGridView;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button scanNetworkButton;
    }
}