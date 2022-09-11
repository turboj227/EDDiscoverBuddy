using EXControls;

namespace EDScoutBuddy
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Icons",
            "Position",
            "Distance",
            "Value",
            "Type"}, -1);
            this.JourneyTimer = new System.Windows.Forms.Timer(this.components);
            this.lsvSystemInfo = new EXControls.EXListView();
            this.hdrIcons1 = new EXControls.EXColumnHeader();
            this.hdrIcons2 = new System.Windows.Forms.ColumnHeader();
            this.hdrBodies1 = new System.Windows.Forms.ColumnHeader();
            this.hdrJumps1 = new System.Windows.Forms.ColumnHeader();
            this.hdrSystem1 = new System.Windows.Forms.ColumnHeader();
            this.lsvHighValuePlanets = new EXControls.EXListView();
            this.excolumnHeader1 = new EXControls.EXColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // JourneyTimer
            // 
            this.JourneyTimer.Interval = 250;
            this.JourneyTimer.Tick += new System.EventHandler(this.JourneyTimer_Tick);
            // 
            // lsvSystemInfo
            // 
            this.lsvSystemInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lsvSystemInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvSystemInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.hdrIcons1,
            this.hdrIcons2,
            this.hdrBodies1,
            this.hdrJumps1,
            this.hdrSystem1});
            this.lsvSystemInfo.ControlPadding = 4;
            this.lsvSystemInfo.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lsvSystemInfo.ForeColor = System.Drawing.Color.White;
            this.lsvSystemInfo.FullRowSelect = true;
            this.lsvSystemInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvSystemInfo.HideSelection = true;
            this.lsvSystemInfo.Location = new System.Drawing.Point(12, 12);
            this.lsvSystemInfo.Name = "lsvSystemInfo";
            this.lsvSystemInfo.OwnerDraw = true;
            this.lsvSystemInfo.Size = new System.Drawing.Size(1075, 111);
            this.lsvSystemInfo.TabIndex = 1;
            this.lsvSystemInfo.TabStop = false;
            this.lsvSystemInfo.UseCompatibleStateImageBehavior = false;
            this.lsvSystemInfo.View = System.Windows.Forms.View.Details;
            this.lsvSystemInfo.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lsvSystemInfo_ItemSelectionChanged);
            // 
            // hdrIcons1
            // 
            this.hdrIcons1.Text = "Icons";
            this.hdrIcons1.Width = 32;
            // 
            // hdrIcons2
            // 
            this.hdrIcons2.Text = "OtherIcons";
            this.hdrIcons2.Width = 90;
            // 
            // hdrBodies1
            // 
            this.hdrBodies1.Text = "Bodies";
            this.hdrBodies1.Width = 100;
            // 
            // hdrJumps1
            // 
            this.hdrJumps1.Text = "Jumps";
            // 
            // hdrSystem1
            // 
            this.hdrSystem1.Text = "System";
            this.hdrSystem1.Width = 750;
            // 
            // lsvHighValuePlanets
            // 
            this.lsvHighValuePlanets.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.lsvHighValuePlanets.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvHighValuePlanets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.excolumnHeader1,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lsvHighValuePlanets.ControlPadding = 4;
            this.lsvHighValuePlanets.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lsvHighValuePlanets.ForeColor = System.Drawing.Color.White;
            this.lsvHighValuePlanets.FullRowSelect = true;
            this.lsvHighValuePlanets.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvHighValuePlanets.HideSelection = true;
            this.lsvHighValuePlanets.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.lsvHighValuePlanets.Location = new System.Drawing.Point(12, 149);
            this.lsvHighValuePlanets.Name = "lsvHighValuePlanets";
            this.lsvHighValuePlanets.OwnerDraw = true;
            this.lsvHighValuePlanets.Size = new System.Drawing.Size(1075, 617);
            this.lsvHighValuePlanets.TabIndex = 3;
            this.lsvHighValuePlanets.TabStop = false;
            this.lsvHighValuePlanets.UseCompatibleStateImageBehavior = false;
            this.lsvHighValuePlanets.View = System.Windows.Forms.View.Details;
            this.lsvHighValuePlanets.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.exListView2_ItemSelectionChanged);
            // 
            // excolumnHeader1
            // 
            this.excolumnHeader1.Text = "Icons";
            this.excolumnHeader1.Width = 100;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Position";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Distance";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Value";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 550;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1099, 778);
            this.Controls.Add(this.lsvHighValuePlanets);
            this.Controls.Add(this.lsvSystemInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(0, 250);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer JourneyTimer;
        private EXListView lsvSystemInfo;
        private ColumnHeader hdrBodies1;
        private ColumnHeader hdrJumps1;
        private ColumnHeader hdrSystem1;
        private EXListView lsvHighValuePlanets;
        private EXColumnHeader excolumnHeader1;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader hdrIcons2;
        private EXColumnHeader hdrIcons1;
    }
}