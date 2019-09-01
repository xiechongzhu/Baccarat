using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baccarat
{
    public partial class SiteForm : Form
    {
        public SiteForm()
        {
            InitializeComponent();
            cbSite.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSite.Items.AddRange(SiteInfo.Instance().GetSiteNames().ToArray());
            cbSite.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            siteUrl = cbSite.SelectedItem.ToString();
            Close();
        }

        public String GetUrl()
        {
            return siteUrl;
        }

        private String siteUrl;
    }
}
