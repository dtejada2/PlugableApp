namespace PlugableApp
{
    partial class Menu
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
            this.btnPlugins = new System.Windows.Forms.Button();
            this.btnSystem = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnPlugins
            // 
            this.btnPlugins.Location = new System.Drawing.Point(35, 46);
            this.btnPlugins.Name = "btnPlugins";
            this.btnPlugins.Size = new System.Drawing.Size(191, 61);
            this.btnPlugins.TabIndex = 0;
            this.btnPlugins.Text = "Plugins";
            this.btnPlugins.UseVisualStyleBackColor = true;
            this.btnPlugins.Click += new System.EventHandler(this.btnPlugins_Click);
            // 
            // btnSystem
            // 
            this.btnSystem.Location = new System.Drawing.Point(35, 113);
            this.btnSystem.Name = "btnSystem";
            this.btnSystem.Size = new System.Drawing.Size(191, 62);
            this.btnSystem.TabIndex = 0;
            this.btnSystem.Text = "System";
            this.btnSystem.UseVisualStyleBackColor = true;
            this.btnSystem.Click += new System.EventHandler(this.btnHostForm_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnSystem);
            this.Controls.Add(this.btnPlugins);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPlugins;
        private System.Windows.Forms.Button btnSystem;
    }
}