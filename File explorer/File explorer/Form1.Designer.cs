namespace File_explorer
{
	partial class Form1
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
			this.btnLeft = new System.Windows.Forms.Button();
			this.btnRight = new System.Windows.Forms.Button();
			this.lblPath = new System.Windows.Forms.Label();
			this.btnOpen = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.SuspendLayout();
			// 
			// btnLeft
			// 
			this.btnLeft.Location = new System.Drawing.Point(12, 21);
			this.btnLeft.Name = "btnLeft";
			this.btnLeft.Size = new System.Drawing.Size(37, 23);
			this.btnLeft.TabIndex = 0;
			this.btnLeft.Text = "<<";
			this.btnLeft.UseVisualStyleBackColor = true;
			this.btnLeft.Click += new System.EventHandler(this.btnLeft_Click);
			// 
			// btnRight
			// 
			this.btnRight.Location = new System.Drawing.Point(55, 21);
			this.btnRight.Name = "btnRight";
			this.btnRight.Size = new System.Drawing.Size(37, 23);
			this.btnRight.TabIndex = 1;
			this.btnRight.Text = ">>";
			this.btnRight.UseVisualStyleBackColor = true;
			this.btnRight.Click += new System.EventHandler(this.btnRight_Click);
			// 
			// lblPath
			// 
			this.lblPath.AutoSize = true;
			this.lblPath.Location = new System.Drawing.Point(110, 24);
			this.lblPath.Name = "lblPath";
			this.lblPath.Size = new System.Drawing.Size(37, 16);
			this.lblPath.TabIndex = 2;
			this.lblPath.Text = "Path:";
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(692, 21);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(85, 23);
			this.btnOpen.TabIndex = 4;
			this.btnOpen.Text = "Open";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(153, 21);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(533, 22);
			this.txtPath.TabIndex = 5;
			this.txtPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// listView1
			// 
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(12, 60);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(765, 378);
			this.listView1.TabIndex = 6;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.btnOpen);
			this.Controls.Add(this.lblPath);
			this.Controls.Add(this.btnRight);
			this.Controls.Add(this.btnLeft);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLeft;
		private System.Windows.Forms.Button btnRight;
		private System.Windows.Forms.Label lblPath;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.ListView listView1;
	}
}

