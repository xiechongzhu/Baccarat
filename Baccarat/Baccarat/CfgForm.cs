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

namespace Baccarat
{
    public partial class CfgForm : Form
    {
        private ESite mainSite;
        List<Object> betModeList = new List<object> { new { Title = "一局结果下注", Value = 1},
                new { Title = "二局结果下注", Value = 2},  new { Title = "三局结果下注", Value = 3}};
        public CfgForm(ESite mainSite)
        {
            this.mainSite = mainSite;
            InitializeComponent();
            InitControlls();
        }

        private Int32[] chips = new Int32[] { 10, 50, 100, 500, 1000, 5000, 10000, 50000, 100000, 200000};

        private void InitControlls()
        {
            BetListView1.ItemSelectionChanged += BetListViewSelectedIndexChanged;
            BetListView2.ItemSelectionChanged += BetListViewSelectedIndexChanged;
            BetListView3.ItemSelectionChanged += BetListViewSelectedIndexChanged;

            foreach (Control control in groupBoxChip.Controls)
            {
                if (control is ComboBox && control.Name.StartsWith("CbChip"))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    foreach (Int32 chip in chips)
                    {
                        cb.Items.Add(chip.ToString());
                    }
                    cb.SelectedIndex = 0;
                }
            }

            foreach (Control control in groupBoxBet.Controls)
            {
                if (control is ComboBox && (control.Name.StartsWith("comboBoxReuslt") || control.Name.Equals("cbDefault")))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DataSource = new List<GameResultInfo>(CommonFunction.gameResultInfos);
                    cb.ValueMember = "result";
                    cb.DisplayMember = "Name";
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }

            comboBoxMode.DataSource = betModeList;
            comboBoxMode.DisplayMember = "Title";
            comboBoxMode.ValueMember = "Value";
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            Config.Instance().chips[0] = Int32.Parse(CbChip1.Text);
            Config.Instance().chips[1] = Int32.Parse(CbChip2.Text);
            Config.Instance().chips[2] = Int32.Parse(CbChip3.Text);
            Config.Instance().chips[3] = Int32.Parse(CbChip4.Text);
            Config.Instance().chips[4] = Int32.Parse(CbChip5.Text);
            Config.Instance().betMode = (Int32)comboBoxMode.SelectedValue;
            Config.Instance().gameRule1.Clear();
            Config.Instance().gameRule2.Clear();
            Config.Instance().gameRule3.Clear();
            foreach (ListViewItem item in BetListView1.Items)
            {
                GameRule1 rule = new GameRule1();
                rule.result = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                rule.rules = CommonFunction.Clone((List<BetRule>)item.Tag);
                Config.Instance().gameRule1.Add(rule);
            }
            foreach (ListViewItem item in BetListView2.Items)
            {
                GameRule2 rule = new GameRule2();
                rule.result2 = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                rule.result1 = CommonFunction.StringToGameResult(item.SubItems[1].Text);
                rule.rules = CommonFunction.Clone((List<BetRule>)item.Tag);
                Config.Instance().gameRule2.Add(rule);
            }
            foreach (ListViewItem item in BetListView3.Items)
            {
                GameRule3 rule = new GameRule3();
                rule.result3 = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                rule.result2 = CommonFunction.StringToGameResult(item.SubItems[1].Text);
                rule.result1 = CommonFunction.StringToGameResult(item.SubItems[2].Text);
                rule.rules = CommonFunction.Clone((List<BetRule>)item.Tag);
                Config.Instance().gameRule3.Add(rule);
            }

            WriteConfig();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CfgForm_Load(object sender, EventArgs e)
        {
            ReadConfig();
        }

        private void ReadConfig()
        {
            String configFileName = SiteInfo.Instance().GetConfigFileName(mainSite);
            Config.Instance().Read(configFileName);
            CbChip1.Text = Config.Instance().chips[0].ToString();
            CbChip2.Text = Config.Instance().chips[1].ToString();
            CbChip3.Text = Config.Instance().chips[2].ToString();
            CbChip4.Text = Config.Instance().chips[3].ToString();
            CbChip5.Text = Config.Instance().chips[4].ToString();
            comboBoxMode.SelectedValue = Config.Instance().betMode;
            BetListView1.Items.Clear();
            BetListView2.Items.Clear();
            BetListView3.Items.Clear();
            foreach (GameRule1 rule in Config.Instance().gameRule1)
            {
                String[] strList = new String[] { CommonFunction.GameResultToString(rule.result), rule .rules.Count.ToString()};
                ListViewItem item = new ListViewItem(strList);
                item.Tag = rule.rules;
                BetListView1.Items.Add(item);
            }
            foreach (GameRule2 rule in Config.Instance().gameRule2)
            {
                String[] strList = new String[] { CommonFunction.GameResultToString(rule.result2), CommonFunction.GameResultToString(rule.result1),
                    rule.rules.Count.ToString()};
                ListViewItem item = new ListViewItem(strList);
                item.Tag = rule.rules;
                BetListView2.Items.Add(item);
            }
            foreach (GameRule3 rule in Config.Instance().gameRule3)
            {
                String[] strList = new String[] { CommonFunction.GameResultToString(rule.result3), CommonFunction.GameResultToString(rule.result2),
                    CommonFunction.GameResultToString(rule.result1), rule.rules.Count.ToString()};
                ListViewItem item = new ListViewItem(strList);
                item.Tag = rule.rules;
                BetListView3.Items.Add(item);
            }
        }

        private void WriteConfig()
        {
            String configFileName = SiteInfo.Instance().GetConfigFileName(mainSite);
            Config.Instance().Write(configFileName);
        }

        private void BtnAdd1_Click(object sender, EventArgs e)
        {
            BetRule1AddForm dialog = new BetRule1AddForm();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                if (Rule1Exits(dialog.result))
                {
                    MessageBox.Show("规则已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String[] strItem = new string[] { CommonFunction.GameResultToString(dialog.result) ,dialog.GetBetRules().Count.ToString()};
                ListViewItem item = new ListViewItem(strItem);
                item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                BetListView1.Items.Add(item);
            }
        }

        private void BtnMod1_Click(object sender, EventArgs e)
        {
            if (BetListView1.SelectedItems.Count > 0)
            {
                ListViewItem item = BetListView1.SelectedItems[0];
                BetRule1AddForm dialog = new BetRule1AddForm();
                dialog.SetEditMode();
                dialog.result = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                dialog.SetBetRules((List<BetRule>)item.Tag);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[0].Text = CommonFunction.GameResultToString(dialog.result);
                    item.SubItems[1].Text = dialog.GetBetRules().Count.ToString();
                    item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                }
            }
        }

        private void BtnDel1_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in BetListView1.SelectedItems)
            {
                BetListView1.Items.Remove(item);
            }
        }

        private void BetListViewSelectedIndexChanged(object sender, EventArgs e)
        {
            ListView listView = (ListView)sender;
            foreach(ListViewItem item in listView.Items)
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

        private bool Rule1Exits(GameResult result)
        {
            foreach(ListViewItem item in BetListView1.Items)
            {
                if(CommonFunction.StringToGameResult(item.SubItems[0].Text) == result)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Rule2Exits(GameResult result2, GameResult result1)
        {
            foreach (ListViewItem item in BetListView2.Items)
            {
                if (CommonFunction.StringToGameResult(item.SubItems[0].Text) == result2 && CommonFunction.StringToGameResult(item.SubItems[1].Text) == result1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Rule2Exits(GameResult result3, GameResult result2, GameResult result1)
        {
            foreach (ListViewItem item in BetListView3.Items)
            {
                if (CommonFunction.StringToGameResult(item.SubItems[0].Text) == result3 && CommonFunction.StringToGameResult(item.SubItems[1].Text) == result2
                    && CommonFunction.StringToGameResult(item.SubItems[2].Text) == result1)
                {
                    return true;
                }
            }
            return false;
        }

        private void BtnAdd2_Click(object sender, EventArgs e)
        {
            BetRule2AddForm dialog = new BetRule2AddForm();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                if (Rule2Exits(dialog.result2, dialog.result1))
                {
                    MessageBox.Show("规则已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String[] strItem = new string[] { CommonFunction.GameResultToString(dialog.result2), CommonFunction.GameResultToString(dialog.result1),
                    dialog.GetBetRules().Count.ToString() };
                ListViewItem item = new ListViewItem(strItem);
                item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                BetListView2.Items.Add(item);
            }
        }

        private void BtnMod2_Click(object sender, EventArgs e)
        {
            if (BetListView2.SelectedItems.Count > 0)
            {
                ListViewItem item = BetListView2.SelectedItems[0];
                BetRule2AddForm dialog = new BetRule2AddForm();
                dialog.SetEditMode();
                dialog.result2 = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                dialog.result1 = CommonFunction.StringToGameResult(item.SubItems[1].Text);
                dialog.SetBetRules((List<BetRule>)item.Tag);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[0].Text = CommonFunction.GameResultToString(dialog.result2);
                    item.SubItems[1].Text = CommonFunction.GameResultToString(dialog.result1);
                    item.SubItems[2].Text = dialog.GetBetRules().Count.ToString();
                    item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                }
            }
        }

        private void BtnDel2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in BetListView2.SelectedItems)
            {
                BetListView2.Items.Remove(item);
            }
        }

        private void BtnAdd3_Click(object sender, EventArgs e)
        {
            BetRule3AddForm dialog = new BetRule3AddForm();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (Rule2Exits(dialog.result3, dialog.result2, dialog.result1))
                {
                    MessageBox.Show("规则已存在", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                String[] strItem = new string[] { CommonFunction.GameResultToString(dialog.result3), CommonFunction.GameResultToString(dialog.result2),
                    CommonFunction.GameResultToString(dialog.result1), dialog.GetBetRules().Count.ToString() };
                ListViewItem item = new ListViewItem(strItem);
                item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                BetListView3.Items.Add(item);
            }
        }

        private void BtnMod3_Click(object sender, EventArgs e)
        {
            if (BetListView3.SelectedItems.Count > 0)
            {
                ListViewItem item = BetListView3.SelectedItems[0];
                BetRule3AddForm dialog = new BetRule3AddForm();
                dialog.SetEditMode();
                dialog.result3 = CommonFunction.StringToGameResult(item.SubItems[0].Text);
                dialog.result2 = CommonFunction.StringToGameResult(item.SubItems[1].Text);
                dialog.result1 = CommonFunction.StringToGameResult(item.SubItems[2].Text);
                dialog.SetBetRules((List<BetRule>)item.Tag);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    item.SubItems[0].Text = CommonFunction.GameResultToString(dialog.result3);
                    item.SubItems[1].Text = CommonFunction.GameResultToString(dialog.result2);
                    item.SubItems[2].Text = CommonFunction.GameResultToString(dialog.result1);
                    item.SubItems[3].Text = dialog.GetBetRules().Count.ToString();
                    item.Tag = CommonFunction.Clone(dialog.GetBetRules());
                }
            }
        }

        private void BtnDel3_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in BetListView3.SelectedItems)
            {
                BetListView3.Items.Remove(item);
            }
        }

        private void BetListView1_DoubleClick(object sender, EventArgs e)
        {
            BtnMod1_Click(sender, e);
        }

        private void BetListView2_DoubleClick(object sender, EventArgs e)
        {
            BtnMod2_Click(sender, e);
        }

        private void BetListView3_DoubleClick(object sender, EventArgs e)
        {
            BtnMod3_Click(sender, e);
        }
    }
}
