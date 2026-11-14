namespace SkziScanner
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.targetPcsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.certificatesDataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.logsTextBox = new System.Windows.Forms.TextBox();
            this.dbSettingsButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.editComputersButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.certificatesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // targetPcsTextBox
            // 
            this.targetPcsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPcsTextBox.Location = new System.Drawing.Point(310, 22);
            this.targetPcsTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.targetPcsTextBox.Name = "targetPcsTextBox";
            this.targetPcsTextBox.Size = new System.Drawing.Size(1042, 29);
            this.targetPcsTextBox.TabIndex = 0;
            this.targetPcsTextBox.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Компьютеры в сети (через запятую):";
            // 
            // scanButton
            // 
            this.scanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.scanButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.scanButton.FlatAppearance.BorderSize = 0;
            this.scanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanButton.ForeColor = System.Drawing.Color.White;
            this.scanButton.Location = new System.Drawing.Point(781, 61);
            this.scanButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(249, 40);
            this.scanButton.TabIndex = 4;
            this.scanButton.Text = "Сканировать ПК";
            this.scanButton.UseVisualStyleBackColor = false;
            this.scanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.BackColor = System.Drawing.Color.White;
            this.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.refreshButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.refreshButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.refreshButton.Location = new System.Drawing.Point(1103, 62);
            this.refreshButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(249, 40);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Обновить таблицу";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // certificatesDataGridView
            // 
            this.certificatesDataGridView.AllowUserToAddRows = false;
            this.certificatesDataGridView.AllowUserToDeleteRows = false;
            this.certificatesDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.certificatesDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.certificatesDataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.certificatesDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.certificatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.certificatesDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.certificatesDataGridView.EnableHeadersVisualStyles = false;
            this.certificatesDataGridView.Location = new System.Drawing.Point(13, 141);
            this.certificatesDataGridView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.certificatesDataGridView.Name = "certificatesDataGridView";
            this.certificatesDataGridView.ReadOnly = true;
            this.certificatesDataGridView.RowHeadersVisible = false;
            this.certificatesDataGridView.RowHeadersWidth = 51;
            this.certificatesDataGridView.RowTemplate.Height = 24;
            this.certificatesDataGridView.Size = new System.Drawing.Size(1339, 370);
            this.certificatesDataGridView.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 516);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(180, 21);
            this.label3.TabIndex = 8;
            this.label3.Text = "Журнал событий (логи):";
            // 
            // logsTextBox
            // 
            this.logsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsTextBox.BackColor = System.Drawing.Color.AliceBlue;
            this.logsTextBox.Location = new System.Drawing.Point(17, 542);
            this.logsTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.logsTextBox.Multiline = true;
            this.logsTextBox.Name = "logsTextBox";
            this.logsTextBox.ReadOnly = true;
            this.logsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logsTextBox.Size = new System.Drawing.Size(1335, 149);
            this.logsTextBox.TabIndex = 9;
            // 
            // dbSettingsButton
            // 
            this.dbSettingsButton.BackColor = System.Drawing.Color.White;
            this.dbSettingsButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dbSettingsButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.dbSettingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.dbSettingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dbSettingsButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.dbSettingsButton.Location = new System.Drawing.Point(339, 62);
            this.dbSettingsButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.dbSettingsButton.Name = "dbSettingsButton";
            this.dbSettingsButton.Size = new System.Drawing.Size(249, 40);
            this.dbSettingsButton.TabIndex = 11;
            this.dbSettingsButton.Text = "Настроить БД";
            this.dbSettingsButton.UseVisualStyleBackColor = false;
            this.dbSettingsButton.Click += new System.EventHandler(this.DbSettingsButton_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Location = new System.Drawing.Point(13, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1339, 3);
            this.panel1.TabIndex = 12;
            // 
            // editComputersButton
            // 
            this.editComputersButton.BackColor = System.Drawing.Color.White;
            this.editComputersButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.editComputersButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.editComputersButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.editComputersButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editComputersButton.ForeColor = System.Drawing.Color.DodgerBlue;
            this.editComputersButton.Location = new System.Drawing.Point(12, 61);
            this.editComputersButton.Name = "editComputersButton";
            this.editComputersButton.Size = new System.Drawing.Size(250, 41);
            this.editComputersButton.TabIndex = 13;
            this.editComputersButton.Text = "Добавить компьютеры в сети";
            this.editComputersButton.UseVisualStyleBackColor = false;
            this.editComputersButton.Click += new System.EventHandler(this.EditComputersButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1366, 709);
            this.Controls.Add(this.editComputersButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dbSettingsButton);
            this.Controls.Add(this.logsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.certificatesDataGridView);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetPcsTextBox);
            this.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(1250, 700);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Модуль сбора данных о СКЗИ";
            ((System.ComponentModel.ISupportInitialize)(this.certificatesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox targetPcsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridView certificatesDataGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logsTextBox;
        private System.Windows.Forms.Button dbSettingsButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button editComputersButton;
    }
}

