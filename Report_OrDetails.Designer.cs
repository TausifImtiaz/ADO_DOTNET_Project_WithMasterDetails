namespace ProductAdo
{
    partial class Report_OrDetails
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
            this.rptViewerOrderDetails = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // rptViewerOrderDetails
            // 
            this.rptViewerOrderDetails.ActiveViewIndex = -1;
            this.rptViewerOrderDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rptViewerOrderDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.rptViewerOrderDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rptViewerOrderDetails.Location = new System.Drawing.Point(0, 0);
            this.rptViewerOrderDetails.Name = "rptViewerOrderDetails";
            this.rptViewerOrderDetails.Size = new System.Drawing.Size(800, 450);
            this.rptViewerOrderDetails.TabIndex = 0;
            // 
            // Report_OrDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rptViewerOrderDetails);
            this.Name = "Report_OrDetails";
            this.Text = "Report_OrDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer rptViewerOrderDetails;
    }
}