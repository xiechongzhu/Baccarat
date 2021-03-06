﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baccarat.Tools;
using CefSharp;
using CefSharp.WinForms;

namespace Baccarat
{
    public partial class MainForm : Form
    {
        ESite mainSite;
        ESubSite subSite;
        AbstractOperator Operator;
        LogForm logForm = new LogForm();
        public MainForm()
        {
            InitializeComponent();
            CefSettings settings = new CefSettings();
            settings.Locale = "zh_CN";
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            Cef.Initialize(settings);
            browser.Dock = DockStyle.Fill;
            browser.LifeSpanHandler = new OpenPageSelf();
            panelWeb.Controls.Add(browser);

            SiteForm siteForm = new SiteForm();
            siteForm.ShowDialog();
            mainSite = siteForm.mainSite;
            browser.Load(siteForm.siteUrl);
            Text = "百家乐";
            List<SubSiteInfo> subSiteInfos = SiteInfo.Instance().GetSubSites(mainSite);
            cbSubSite.DisplayMember = "siteName";
            cbSubSite.ValueMember = "site";
            cbSubSite.DataSource = subSiteInfos;
        }

        private ChromiumWebBrowser browser = new ChromiumWebBrowser();

        private void MenuItemCapture_Click(object sender, EventArgs e)
        {
            Image image = BitmapCapture.GetWindowCapture(panelWeb);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JPEG图片(*.jpg)|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName);
            }
            image.Dispose();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Size = new Size(800, 600);
            if (!Config.Instance().Read(SiteInfo.Instance().GetConfigFileName(mainSite, subSite)))
            {
                MessageBox.Show("请先设置参数", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            switch (mainSite)
            {
                case ESite.SIET_JINSHA:
                    switch(subSite)
                    {
                        case ESubSite.AG_SITE:
                            Operator = new JinShaAgOperator(this);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (Operator != null)
            {
                Operator.Start();
                ImageCapTimer.Start();
                btnStart.Enabled = false;
                btnStop.Enabled = true;
                btnSetting.Enabled = false;
                cbSubSite.Enabled = false;
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            Operator.Reset();
            ImageCapTimer.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnSetting.Enabled = true;
            cbSubSite.Enabled = true;
        }

        private void ImageCapTimer_Tick(object sender, EventArgs e)
        {
            Image image = BitmapCapture.GetWindowCapture(panelWeb);
            Operator.ParseImage(image);
        }

        delegate void SendLogDelegate(String logString);

        public void addLog(String logString)
        {
            if(InvokeRequired)
            {
                Invoke(new SendLogDelegate(addLog), logString);
            }
            else
            {
                logForm.AddLog(logString);
            }
        }

        delegate void StopDelegate();

        public void Stop()
        {
            if(InvokeRequired)
            {
                Invoke(new StopDelegate(Stop));
            }
            else
            {
                addLog("自动停止");
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnSetting.Enabled = true;
                cbSubSite.Enabled = true;
                MessageBox.Show("程序自动停止", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void BrowserClick(Int32 x, Int32 y)
        {
            InternalBrowserClickDelegate d = new InternalBrowserClickDelegate(InternalBrowserClick);
            d.BeginInvoke(x, y, InternalBrowserClickCallBack, null);
        }

        protected void InternalBrowserClickCallBack(IAsyncResult result)
        {
            AsyncResult _result = (AsyncResult)result;
            InternalBrowserClickDelegate d = (InternalBrowserClickDelegate)_result.AsyncDelegate;
            d.EndInvoke(result);
        }

        delegate void InternalBrowserClickDelegate(Int32 x, Int32 y);
        protected void InternalBrowserClick(Int32 x, Int32 y)
        {
            var host = browser.GetBrowser().GetHost();
            host.SendMouseClickEvent(x, y, MouseButtonType.Left, false, 1, CefEventFlags.None);
            host.SendMouseClickEvent(x, y, MouseButtonType.Left, true, 1, CefEventFlags.None);
        }

        private void BtnLog_Click(object sender, EventArgs e)
        {
            logForm.Visible = true;
            logForm.BringToFront();
        }

        private void BtnSetting_Click(object sender, EventArgs e)
        {
            CfgForm cfgForm = new CfgForm(mainSite, subSite);
            cfgForm.ShowDialog();
        }

        private void CbSubSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            subSite = (ESubSite)cbSubSite.SelectedValue;
        }
    }
}
