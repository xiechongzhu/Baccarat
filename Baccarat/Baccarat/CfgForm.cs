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

            List<GameResultInfo> gameResultInfos = new List<GameResultInfo>();
            gameResultInfos.Add(new GameResultInfo { result = GameResult.BANKER_WIN, Name = "庄家赢" });
            gameResultInfos.Add(new GameResultInfo { result = GameResult.PLAYER_WIN, Name = "闲家赢" });
            gameResultInfos.Add(new GameResultInfo { result = GameResult.DRAW_GAME, Name = "平局" });
            foreach (Control control in groupBoxBet.Controls)
            {
                if (control is ComboBox && control.Name.StartsWith("comboBoxReuslt"))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DataSource = new List<GameResultInfo>(gameResultInfos);
                    cb.ValueMember = "result";
                    cb.DisplayMember = "Name";
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }

            List<Object> betList = new List<object> { new { Title = "庄家", Value = GameResult.BANKER_WIN},
                new { Title = "闲家", Value = GameResult.PLAYER_WIN},  new { Title = "平局", Value = GameResult.DRAW_GAME}};
            foreach (Control control in groupBoxBet.Controls)
            {
                if (control is ComboBox && control.Name.StartsWith("comboBoxBet"))
                {
                    ComboBox cb = (ComboBox)control;
                    cb.DataSource = new List<Object>(betList);
                    cb.ValueMember = "Value";
                    cb.DisplayMember = "Title";
                    cb.DropDownStyle = ComboBoxStyle.DropDownList;
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
            CbChip1.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip2", "10", sb, 255, configFileName);
            CbChip2.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip3", "10", sb, 255, configFileName);
            CbChip3.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip4", "10", sb, 255, configFileName);
            CbChip4.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Chips", "Chip5", "10", sb, 255, configFileName);
            CbChip5.Text = sb.ToString();
            WinApi.GetPrivateProfileString("Start", "Watch", "False", sb, 255, configFileName);
            checkBoxWatch.Checked = bool.Parse(sb.ToString());
            WinApi.GetPrivateProfileString("Start", "Round1", "BANKER_WIN", sb, 255, configFileName);
            comboBoxReuslt1.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Start", "Round2", "BANKER_WIN", sb, 255, configFileName);
            comboBoxReuslt2.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Start", "Round3", "BANKER_WIN", sb, 255, configFileName);
            comboBoxReuslt3.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "BankerWinBet", "PLAYER_WIN", sb, 255, configFileName);
            comboBoxBetBanker.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "PlayerWinBet", "BANKER_WIN", sb, 255, configFileName);
            comboBoxBetPlayer.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "DrawGameBet", "DRAW_GAME", sb, 255, configFileName);
            comboBoxBetDrawGame.SelectedValue = Enum.Parse(typeof(GameResult), sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "BankerWinValue", "0", sb, 255, configFileName);
            BetValueBanker.Value = Int32.Parse(sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "PlayerWinValue", "0", sb, 255, configFileName);
            BetValuePlayer.Value = Int32.Parse(sb.ToString());
            WinApi.GetPrivateProfileString("Bet", "DrawGameValue", "0", sb, 255, configFileName);
            BetValueDrawGame.Value = Int32.Parse(sb.ToString());
        }

        private void WriteConfig()
        {
            String configFileName = SiteInfo.Instance().GetConfigFileName(mainSite);
            WinApi.WritePrivateProfileString("Chips", "Chip1", CbChip1.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip2", CbChip2.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip3", CbChip3.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip4", CbChip4.Text, configFileName);
            WinApi.WritePrivateProfileString("Chips", "Chip5", CbChip5.Text, configFileName);
            WinApi.WritePrivateProfileString("Start", "Watch", checkBoxWatch.Checked.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Start", "Round1", comboBoxReuslt1.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Start", "Round2", comboBoxReuslt2.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Start", "Round3", comboBoxReuslt3.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "BankerWinBet", comboBoxBetBanker.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "BankerWinValue", BetValueBanker.Value.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "PlayerWinBet", comboBoxBetPlayer.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "PlayerWinValue", BetValuePlayer.Value.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "DrawGameBet", comboBoxBetDrawGame.SelectedValue.ToString(), configFileName);
            WinApi.WritePrivateProfileString("Bet", "DrawGameValue", BetValueDrawGame.Value.ToString(), configFileName);
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
