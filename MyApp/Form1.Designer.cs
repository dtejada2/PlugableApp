namespace MyApp
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
			this.label1 = new System.Windows.Forms.Label();
			this.locationLabel = new System.Windows.Forms.Label();
			this.versionLabel = new System.Windows.Forms.Label();
			this.locationTextBox = new System.Windows.Forms.TextBox();
			this.versionTextBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(419, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Welcome to MyApp, an example application for Squirrel.Windows.";
			// 
			// locationLabel
			// 
			this.locationLabel.AutoSize = true;
			this.locationLabel.Location = new System.Drawing.Point(9, 42);
			this.locationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.locationLabel.Name = "locationLabel";
			this.locationLabel.Size = new System.Drawing.Size(191, 17);
			this.locationLabel.TabIndex = 1;
			this.locationLabel.Text = "Executing Assembly Location";
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(9, 88);
			this.versionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(129, 17);
			this.versionLabel.TabIndex = 2;
			this.versionLabel.Text = "Application Version";
			// 
			// locationTextBox
			// 
			this.locationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.locationTextBox.Location = new System.Drawing.Point(12, 62);
			this.locationTextBox.Name = "locationTextBox";
			this.locationTextBox.ReadOnly = true;
			this.locationTextBox.Size = new System.Drawing.Size(436, 23);
			this.locationTextBox.TabIndex = 3;
			this.locationTextBox.TabStop = false;
			// 
			// versionTextBox
			// 
			this.versionTextBox.Location = new System.Drawing.Point(12, 108);
			this.versionTextBox.Name = "versionTextBox";
			this.versionTextBox.ReadOnly = true;
			this.versionTextBox.Size = new System.Drawing.Size(139, 23);
			this.versionTextBox.TabIndex = 4;
			this.versionTextBox.TabStop = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(460, 155);
			this.Controls.Add(this.versionTextBox);
			this.Controls.Add(this.locationTextBox);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.locationLabel);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Form1";
			this.Text = "MyApp";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label locationLabel;
		private System.Windows.Forms.Label versionLabel;
		private System.Windows.Forms.TextBox locationTextBox;
		private System.Windows.Forms.TextBox versionTextBox;
	}
}

