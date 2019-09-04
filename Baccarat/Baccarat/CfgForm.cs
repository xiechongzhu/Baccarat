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
    public class GameResultInfo
    {
        public GameResult result { get; set; }
        public string Name { get; set; }
    }

    public partial class CfgForm : Form
    {
        private ESite mainSite;
        public CfgForm(ESite mainSite)
        {
            this.mainSite = mainSite;
            InitializeComponent();
            InitControlls();
            ReadConfig();
        }

        private Int32[] chips = new Int32[] { 10, 50, 100, 500, 1000, 5000, 10000, 50000, 100000, 200000};

        private void InitControlls()
        {
            foreach(Control control in groupBoxChip.Controls)
            {
                if(control is ComboBox && control.Name.StartsWith("JinShaCbChip"))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                    foreach(Int32 chip in chips)
                    {
                        cb.Items.Add(chip.ToString());
                    }
                    cb.SelectedIndex = 0;
                }
            }

            List<GameResultInfo> gameResultInfos = new List<GameResultInfo>();
            gameResultInfos.Add(new GameResultInfo { result = GameResult.BANKER_WIN, Name = "庄家赢" });
            gameResultInfos.Add(new GameResultInfo { result = GameResult.PLAYER_WIN, Name = "闲家赢" });
            gameResultInfos.Add(new GameResultInfo { result = GameResult.DRAW_GAME, Name = "平局" });
            foreach (Control control in groupBoxBet.Controls)
            {
                if(control is ComboBox && control.Name.StartsWith("comboBoxReuslt"))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DataSource = gameResultInfos;
                    cb.ValueMember = "result";
                    cb.DisplayMember = "Name";
                }
            }
        }

        private void BtnCancle_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            WriteConfig();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CfgForm_Load(object sender, EventArgs e)
        {

        }

        private void ReadConfig()
        {
            String configFileName = SiteInfo.Instance().GetConfigFileName(mainSite);
            StringBuilder sb = new StringBuilder();
            WinApi.GetPrivateProfileString("Chips", "Chip1", "10", sb, 255, configFileName);
            JinShaCbChip1.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip2", "10", sb, 255, configFileName);
            JinShaCbChip2.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip3", "10", sb, 255, configFileName);
            JinShaCbChip3.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip4", "10", sb, 255, configFileName);
            JinShaCbChip4.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip5", "10", sb, 255, configFileName);
            JinShaCbChip5.Text = sb.ToString();
        }

        private void WriteConfig()
        {
            String configFileName = SiteInfo.Instance().GetConfigFileName(mainSite);
            WinApi.WritePrivateProfileString("Chips", "Chip1", JinShaCbChip1.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip2", JinShaCbChip2.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip3", JinShaCbChip3.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip4", JinShaCbChip4.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip5", JinShaCbChip5.Text, configFileName);
        }

        private void CheckBoxWatch_CheckedChanged(object sender, EventArgs e)
        {
            foreach(Control control in groupBoxBet.Controls)
            {
                if(control is ComboBox && control.Name.StartsWith("comboBoxReuslt"))
                {
                    control.Enabled = checkBoxWatch.Checked;
                }
            }
        }
    }
}
