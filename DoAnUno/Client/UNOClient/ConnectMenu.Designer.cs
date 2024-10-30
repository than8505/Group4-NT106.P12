
namespace UNOClient
{
    partial class ConnectMenu
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
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnJoin = new System.Windows.Forms.Button();
            this.btnRules = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxName
            // 
            this.textBoxName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxName.Font = new System.Drawing.Font("Arial Narrow", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(48, 44);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxName.Multiline = true;
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(309, 47);
            this.textBoxName.TabIndex = 0;
            // 
            // textBoxIP
            // 
            this.textBoxIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxIP.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIP.Location = new System.Drawing.Point(48, 132);
            this.textBoxIP.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxIP.Multiline = true;
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(309, 47);
            this.textBoxIP.TabIndex = 1;
            this.textBoxIP.Text = "127.0.0.1";
            this.textBoxIP.TextChanged += new System.EventHandler(this.textBoxIP_TextChanged);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(48, 206);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(89, 80);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "CREATE";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // btnJoin
            // 
            this.btnJoin.Location = new System.Drawing.Point(158, 206);
            this.btnJoin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(89, 80);
            this.btnJoin.TabIndex = 3;
            this.btnJoin.Text = "JOIN";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // btnRules
            // 
            this.btnRules.Location = new System.Drawing.Point(269, 206);
            this.btnRules.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRules.Name = "btnRules";
            this.btnRules.Size = new System.Drawing.Size(89, 80);
            this.btnRules.TabIndex = 4;
            this.btnRules.Text = "How 2 Play";
            this.btnRules.UseVisualStyleBackColor = true;
            this.btnRules.Click += new System.EventHandler(this.btnRules_Click);
            // 
            // ConnectMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HotTrack;
            this.ClientSize = new System.Drawing.Size(407, 322);
            this.Controls.Add(this.btnRules);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.textBoxName);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(429, 378);
            this.MinimumSize = new System.Drawing.Size(429, 378);
            this.Name = "ConnectMenu";
            this.Text = "Welcome to UNO Game !";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.Button btnRules;
    }
}

