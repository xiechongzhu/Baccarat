using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Baccarat.Tools;
using CefSharp;
using CefSharp.WinForms;

namespace Baccarat
{
    public partial class MainForm : Form
    {
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
            browser.Load(siteForm.GetUrl());
            Text = "百家乐-" + siteForm.GetUrl();
        }

        private ChromiumWebBrowser browser = new ChromiumWebBrowser();

        private void PanelTool_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void MenuItemCapture_Click(object sender, EventArgs e)
        {
            Image image = BitmapCapture.GetWindowCapture(panelWeb);
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "位图(*.bmp)|*.bmp";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image.Save(dialog.FileName);
            }
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            ImageCapTimer.Start();
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            ImageCapTimer.Stop();
        }

        private void ImageCapTimer_Tick(object sender, EventArgs e)
        {

        }

        delegate void SendLogHandler(String logString);

        public void addLog(String logString)
        {
            if(InvokeRequired)
            {
                Invoke(new SendLogHandler(addLog), logString);
            }
            else
            {
                Console.WriteLine(logString);
            }
        }

        delegate void BrowserClickHandler(Int32 x, Int32 y);

        public void BrowserClick(Int32 x, Int32 y)
        {
            if(InvokeRequired)
            {
                Invoke(new BrowserClickHandler(BrowserClick), x, y);
            }
            else
            {
                var host = browser.GetBrowser().GetHost();
                host.SendMouseClickEvent(x, y, MouseButtonType.Left, false, 1, CefEventFlags.None);
                host.SendMouseClickEvent(x, y, MouseButtonType.Left, true, 1, CefEventFlags.None);
            }
        }
    }
}
