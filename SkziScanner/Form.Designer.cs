namespace SkziScanner
{
    partial class Form
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
            this.targetPcsTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectionStringTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.certificatesDataGridView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.logsTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.certificatesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // targetPcsTextBox
            // 
            this.targetPcsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetPcsTextBox.Location = new System.Drawing.Point(292, 15);
            this.targetPcsTextBox.Name = "targetPcsTextBox";
            this.targetPcsTextBox.Size = new System.Drawing.Size(880, 22);
            this.targetPcsTextBox.TabIndex = 0;
            this.targetPcsTextBox.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Компьютеры в сети (через запятую):";
            // 
            // connectionStringTextBox
            // 
            this.connectionStringTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionStringTextBox.Location = new System.Drawing.Point(292, 52);
            this.connectionStringTextBox.Name = "connectionStringTextBox";
            this.connectionStringTextBox.Size = new System.Drawing.Size(880, 22);
            this.connectionStringTextBox.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(197, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Строка подключения MSSQL:";
            // 
            // scanButton
            // 
            this.scanButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.scanButton.Location = new System.Drawing.Point(832, 93);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(146, 23);
            this.scanButton.TabIndex = 4;
            this.scanButton.Text = "Сканировать ПК";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.Location = new System.Drawing.Point(1018, 93);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(154, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Обновить таблицу";
            this.refreshButton.UseVisualStyleBackColor = true;
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
            this.certificatesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.certificatesDataGridView.Location = new System.Drawing.Point(12, 134);
            this.certificatesDataGridView.Name = "certificatesDataGridView";
            this.certificatesDataGridView.ReadOnly = true;
            this.certificatesDataGridView.RowHeadersWidth = 51;
            this.certificatesDataGridView.RowTemplate.Height = 24;
            this.certificatesDataGridView.Size = new System.Drawing.Size(1160, 375);
            this.certificatesDataGridView.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 523);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Журнал событий (логи):";
            // 
            // logsTextBox
            // 
            this.logsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsTextBox.Location = new System.Drawing.Point(15, 542);
            this.logsTextBox.Multiline = true;
            this.logsTextBox.Name = "logsTextBox";
            this.logsTextBox.ReadOnly = true;
            this.logsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logsTextBox.Size = new System.Drawing.Size(1157, 153);
            this.logsTextBox.TabIndex = 9;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 707);
            this.Controls.Add(this.logsTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.certificatesDataGridView);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.connectionStringTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.targetPcsTextBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "Form";
            this.Text = "Модуль сбора данных о СКЗИ";
            ((System.ComponentModel.ISupportInitialize)(this.certificatesDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox targetPcsTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox connectionStringTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridView certificatesDataGridView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox logsTextBox;
    }
}

