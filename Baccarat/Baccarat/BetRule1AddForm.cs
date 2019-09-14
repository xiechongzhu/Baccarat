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
    public partial class BetRule1AddForm : Form
    {
        public void SetEditMode()
        {
            cbResult.Enabled = false;
        }

        public BetRule1AddForm()
        {
            result = GameResult.BANKER_WIN;
            InitializeComponent();
            foreach(Control contrl in Controls)
            {
                if(contrl is ComboBox)
                {
                    ComboBox cb = (ComboBox)contrl;
                    cb.DataSource = CommonFunction.Clone(CommonFunction.gameResultInfos);
                    cb.DisplayMember = "Name";
                    cb.ValueMember = "result";
                    cb.SelectedIndex = 0;
                }
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            result = (GameResult)cbResult.SelectedValue;
            DialogResult = DialogResult.OK;
            Close();
        }

        public GameResult result { get; set; }

        private void BetRule1AddForm_Shown(object sender, EventArgs e)
        {
            cbResult.SelectedValue = result;
        }

        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            foreach (ListViewItem item in listView.Items)
            {
                item.BackColor = SystemColors.Window;
                item.ForeColor = Color.Black;
            }

            foreach (ListViewItem item in listView.SelectedItems)
            {
                item.BackColor = SystemColors.MenuHighlight;
                item.ForeColor = Color.White;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            BetRuleAddForm betRuleAddForm = new BetRuleAddForm();
            if(betRuleAddForm.ShowDialog() == DialogResult.OK)
            {
                String[] strItem = new String[] { CommonFunction.GameResultToString(betRuleAddForm.GetGameResult()),
                    betRuleAddForm.GetBetValue().ToString()};
                ListViewItem  item = new ListViewItem(strItem);
                listView.Items.Add(item);
            }
        }

        public List<BetRule> GetBetRules()
        {
            List<BetRule> betRules = new List<BetRule>();
            foreach(ListViewItem item in listView.Items)
            {
                BetRule rule = new BetRule();
                rule.result = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                rule.betValue = Int32.Parse(item.SubItems[1].Text);
                betRules.Add(rule);
            }
            return betRules;
        }

        public void SetBetRules(List<BetRule> rules)
        {
            foreach(BetRule rule in rules)
            {
                String[] strItem = new string[] { CommonFunction.GameResultToString(rule.result), rule.betValue.ToString() };
                listView.Items.Add(new ListViewItem(strItem));
            }
        }

        private void BtnMod_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count > 0)
            {
                BetRuleAddForm betRuleAddForm = new BetRuleAddForm();
                ListViewItem item = listView.SelectedItems[0];
                betRuleAddForm.SetGameResult(CommonFunction.StringToGameResult(item.SubItems[0].Text));
                betRuleAddForm.SetBetValue(Int32.Parse(item.SubItems[1].Text));
                if(betRuleAddForm.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[0].Text = CommonFunction.GameResultToString(betRuleAddForm.GetGameResult());
                    item.SubItems[1].Text = betRuleAddForm.GetBetValue().ToString();
                }
            }
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView.SelectedItems)
            {
                listView.Items.Remove(item);
            }
        }

        private void ListView_DoubleClick(object sender, EventArgs e)
        {
            BtnMod_Click(sender, e);
        }
    }
}
