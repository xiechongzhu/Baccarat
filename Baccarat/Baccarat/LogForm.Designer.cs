namespace Baccarat
{
    partial class LogForm
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
            this.logList = new System.Windows.Forms.ListView();
            this.headerTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerContent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // logList
            // 
            this.logList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerTime,
            this.headerContent});
            this.logList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.logList.Location = new System.Drawing.Point(0, 0);
            this.logList.MultiSelect = false;
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(985, 766);
            this.logList.TabIndex = 0;
            this.logList.UseCompatibleStateImageBehavior = false;
            this.logList.View = System.Windows.Forms.View.Details;
            // 
            // headerTime
            // 
            this.headerTime.Text = "时间";
            this.headerTime.Width = 130;
            // 
            // headerContent
            // 
            this.headerContent.Text = "内容";
            this.headerContent.Width = 340;
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 766);
            this.Controls.Add(this.logList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LogForm";
            this.Text = "日志";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView logList;
        private System.Windows.Forms.ColumnHeader headerTime;
        private System.Windows.Forms.ColumnHeader headerContent;
    }
}