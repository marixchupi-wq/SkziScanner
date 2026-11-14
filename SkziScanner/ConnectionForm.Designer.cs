namespace SkziScanner
{
    partial class ConnectionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionForm));
            this.serverTextBox = new System.Windows.Forms.TextBox();
            this.databaseTextBox = new System.Windows.Forms.TextBox();
            this.userTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.trustCertificateCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // serverTextBox
            // 
            this.serverTextBox.Location = new System.Drawing.Point(156, 94);
            this.serverTextBox.Name = "serverTextBox";
            this.serverTextBox.Size = new System.Drawing.Size(255, 22);
            this.serverTextBox.TabIndex = 0;
            // 
            // databaseTextBox
            // 
            this.databaseTextBox.Location = new System.Drawing.Point(156, 140);
            this.databaseTextBox.Name = "databaseTextBox";
            this.databaseTextBox.Size = new System.Drawing.Size(255, 22);
            this.databaseTextBox.TabIndex = 1;
            // 
            // userTextBox
            // 
            this.userTextBox.Location = new System.Drawing.Point(156, 188);
            this.userTextBox.Name = "userTextBox";
            this.userTextBox.Size = new System.Drawing.Size(255, 22);
            this.userTextBox.TabIndex = 2;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(156, 240);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(255, 22);
            this.passwordTextBox.TabIndex = 3;
            this.passwordTextBox.UseSystemPasswordChar = true;
            // 
            // trustCertificateCheckBox
            // 
            this.trustCertificateCheckBox.AutoSize = true;
            this.trustCertificateCheckBox.Checked = true;
            this.trustCertificateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.trustCertificateCheckBox.Location = new System.Drawing.Point(156, 283);
            this.trustCertificateCheckBox.Name = "trustCertificateCheckBox";
            this.trustCertificateCheckBox.Size = new System.Drawing.Size(251, 20);
            this.trustCertificateCheckBox.TabIndex = 4;
            this.trustCertificateCheckBox.Text = "Доверенный сертификат сервера";
            this.trustCertificateCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Имя сервера";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Имя базы данных";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "Имя пользователя";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 246);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 8;
            this.label4.Text = "Пароль";
            // 
            // connectButton
            // 
            this.connectButton.BackColor = System.Drawing.Color.DodgerBlue;
            this.connectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.connectButton.FlatAppearance.BorderSize = 0;
            this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectButton.ForeColor = System.Drawing.Color.White;
            this.connectButton.Location = new System.Drawing.Point(109, 348);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(140, 23);
            this.connectButton.TabIndex = 9;
            this.connectButton.Text = "Подключиться";
            this.connectButton.UseVisualStyleBackColor = false;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.BackColor = System.Drawing.Color.White;
            this.CancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CancelButton.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.CancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CancelButton.ForeColor = System.Drawing.Color.SteelBlue;
            this.CancelButton.Location = new System.Drawing.Point(271, 348);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(140, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Отмена";
            this.CancelButton.UseVisualStyleBackColor = false;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(75, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(311, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Подключение к базе данных";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Location = new System.Drawing.Point(12, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(414, 3);
            this.panel1.TabIndex = 12;
            // 
            // ConnectionForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(438, 406);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trustCertificateCheckBox);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.userTextBox);
            this.Controls.Add(this.databaseTextBox);
            this.Controls.Add(this.serverTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConnectionForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverTextBox;
        private System.Windows.Forms.TextBox databaseTextBox;
        private System.Windows.Forms.TextBox userTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.CheckBox trustCertificateCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
    }
}