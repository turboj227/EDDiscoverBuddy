namespace EDDiscoverBuddy.Forms
{
    partial class frmSettings
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
            this.txtMinPlanetValue = new System.Windows.Forms.TextBox();
            this.lblMinPlanetValue = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMinPlanetValue
            // 
            this.txtMinPlanetValue.Location = new System.Drawing.Point(145, 12);
            this.txtMinPlanetValue.Name = "txtMinPlanetValue";
            this.txtMinPlanetValue.Size = new System.Drawing.Size(217, 23);
            this.txtMinPlanetValue.TabIndex = 0;
            // 
            // lblMinPlanetValue
            // 
            this.lblMinPlanetValue.AutoSize = true;
            this.lblMinPlanetValue.Location = new System.Drawing.Point(12, 15);
            this.lblMinPlanetValue.Name = "lblMinPlanetValue";
            this.lblMinPlanetValue.Size = new System.Drawing.Size(127, 15);
            this.lblMinPlanetValue.TabIndex = 1;
            this.lblMinPlanetValue.Text = "Minimum Planet value";
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(12, 390);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(112, 48);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(676, 390);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(112, 48);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.lblMinPlanetValue);
            this.Controls.Add(this.txtMinPlanetValue);
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ED Discover Buddy - Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox txtMinPlanetValue;
        private Label lblMinPlanetValue;
        private Button cmdSave;
        private Button cmdCancel;
    }
}