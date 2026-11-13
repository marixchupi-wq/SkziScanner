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
            this.txtTargetPcs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.dgvCertificates = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLogs = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTargetPcs
            // 
            this.txtTargetPcs.Location = new System.Drawing.Point(292, 15);
            this.txtTargetPcs.Name = "txtTargetPcs";
            this.txtTargetPcs.Size = new System.Drawing.Size(678, 22);
            this.txtTargetPcs.TabIndex = 0;
            this.txtTargetPcs.Text = "localhost";
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
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(292, 52);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(678, 22);
            this.txtConnectionString.TabIndex = 2;
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
            this.scanButton.Location = new System.Drawing.Point(630, 93);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(146, 23);
            this.scanButton.TabIndex = 4;
            this.scanButton.Text = "Сканировать ПК";
            this.scanButton.UseVisualStyleBackColor = true;
            this.scanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(816, 93);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(154, 23);
            this.refreshButton.TabIndex = 5;
            this.refreshButton.Text = "Обновить таблицу";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // dgvCertificates
            // 
            this.dgvCertificates.AllowUserToAddRows = false;
            this.dgvCertificates.AllowUserToDeleteRows = false;
            this.dgvCertificates.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCertificates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificates.Location = new System.Drawing.Point(12, 134);
            this.dgvCertificates.Name = "dgvCertificates";
            this.dgvCertificates.ReadOnly = true;
            this.dgvCertificates.RowHeadersWidth = 51;
            this.dgvCertificates.RowTemplate.Height = 24;
            this.dgvCertificates.Size = new System.Drawing.Size(958, 375);
            this.dgvCertificates.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 523);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Журнал событий (логи):";
            // 
            // txtLogs
            // 
            this.txtLogs.Location = new System.Drawing.Point(15, 542);
            this.txtLogs.Multiline = true;
            this.txtLogs.Name = "txtLogs";
            this.txtLogs.ReadOnly = true;
            this.txtLogs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLogs.Size = new System.Drawing.Size(955, 153);
            this.txtLogs.TabIndex = 9;
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 707);
            this.Controls.Add(this.txtLogs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvCertificates);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.scanButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtConnectionString);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTargetPcs);
            this.Name = "Form";
            this.Text = "Модуль сбора данных о СКЗИ";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTargetPcs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.DataGridView dgvCertificates;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtLogs;
    }
}

