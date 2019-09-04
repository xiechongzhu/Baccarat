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
            cbSite.DisplayMember = "siteName";
            cbSite.ValueMember = "site";
            cbSite.DataSource = SiteInfo.Instance().GetMainSites();
            
            //cbSite.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            mainSite = (ESite)cbSite.SelectedValue;
            siteUrl = SiteInfo.Instance().mainSiteUrl(mainSite);
            Close();
        }

        public String siteUrl { get; set; }

        public ESite mainSite { get; set; }
    }
}
