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
    public partial class SubSiteForm : Form
    {
        private ESite mainSite;
        public SubSiteForm()
        {
            InitializeComponent();
            cbSubSite.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public void SetMainSite(ESite mainSite)
        {
            this.mainSite = mainSite;
            List<SubSiteInfo> subSiteInfos = SiteInfo.Instance().GetSubSites(mainSite);
            cbSubSite.DisplayMember = "siteName";
            cbSubSite.ValueMember = "site";
            cbSubSite.DataSource = subSiteInfos;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public ESubSite GetSubSite()
        {
            return (ESubSite)cbSubSite.SelectedValue;
        }
    }
}
