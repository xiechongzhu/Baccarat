﻿namespace Baccarat
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelTool = new System.Windows.Forms.Panel();
            this.ContexMenuTool = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.panelWeb = new System.Windows.Forms.Panel();
            this.ImageCapTimer = new System.Windows.Forms.Timer(this.components);
            this.panelTool.SuspendLayout();
            this.ContexMenuTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTool
            // 
            this.panelTool.ContextMenuStrip = this.ContexMenuTool;
            this.panelTool.Controls.Add(this.btnLog);
            this.panelTool.Controls.Add(this.btnStop);
            this.panelTool.Controls.Add(this.btnStart);
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTool.Location = new System.Drawing.Point(0, 0);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(787, 28);
            this.panelTool.TabIndex = 0;
            // 
            // ContexMenuTool
            // 
            this.ContexMenuTool.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ContexMenuTool.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemCapture});
            this.ContexMenuTool.Name = "ContexMenuTool";
            this.ContexMenuTool.Size = new System.Drawing.Size(101, 26);
            // 
            // MenuItemCapture
            // 
            this.MenuItemCapture.Name = "MenuItemCapture";
            this.MenuItemCapture.Size = new System.Drawing.Size(100, 22);
            this.MenuItemCapture.Text = "截图";
            this.MenuItemCapture.Click += new System.EventHandler(this.MenuItemCapture_Click);
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(726, 4);
            this.btnLog.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(56, 22);
            this.btnLog.TabIndex = 2;
            this.btnLog.Text = "日志";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.BtnLog_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(85, 2);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "开始";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // panelWeb
            // 
            this.panelWeb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelWeb.Location = new System.Drawing.Point(0, 28);
            this.panelWeb.Margin = new System.Windows.Forms.Padding(0);
            this.panelWeb.Name = "panelWeb";
            this.panelWeb.Size = new System.Drawing.Size(787, 502);
            this.panelWeb.TabIndex = 1;
            // 
            // ImageCapTimer
            // 
            this.ImageCapTimer.Tick += new System.EventHandler(this.ImageCapTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 530);
            this.Controls.Add(this.panelWeb);
            this.Controls.Add(this.panelTool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "百家乐";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelTool.ResumeLayout(false);
            this.ContexMenuTool.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.Panel panelWeb;
        private System.Windows.Forms.ContextMenuStrip ContexMenuTool;
        private System.Windows.Forms.ToolStripMenuItem MenuItemCapture;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Timer ImageCapTimer;
        private System.Windows.Forms.Button btnLog;
    }
}

