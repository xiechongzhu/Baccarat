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
    public partial class BetRuleAddForm : Form
    {
        public BetRuleAddForm()
        {
            InitializeComponent();
            foreach (Control contrl in Controls)
            {
                if (contrl is ComboBox)
                {
                    ComboBox cb = (ComboBox)contrl;
                    cb.DataSource = CommonFunction.Clone(CommonFunction.gameResultInfos);
                    cb.DisplayMember = "Name";
                    cb.ValueMember = "result";
                    cb.SelectedIndex = 0;
                }
            }
        }

        public Int32 GetBetValue()
        {
            return (Int32)editValue.Value;
        }

        public void SetBetValue(Int32 value)
        {
            editValue.Value = value;
        }

        public GameResult GetGameResult()
        {
            return (GameResult)cbResult.SelectedValue;
        }

        public void SetGameResult(GameResult result)
        {
            cbResult.SelectedValue = result;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
