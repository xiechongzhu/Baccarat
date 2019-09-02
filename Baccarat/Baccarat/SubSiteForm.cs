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
            List<String> subNames = SiteInfo.Instance().GetSubSiteNames(mainSite);
            cbSubSite.Items.AddRange(subNames.ToArray());
            cbSubSite.SelectedIndex = 0;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public ESubSite GetSubSite()
        {
            return SiteInfo.Instance().GetSubSite(mainSite, cbSubSite.SelectedItem.ToString());
        }
    }
}
