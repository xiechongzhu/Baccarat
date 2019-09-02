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
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void AddLog(String logString)
        {
            if(logList.Items.Count == 100)
            {
                logList.Items.RemoveAt(0);
            }

            String[] strItem = new string[] {DateTime.Now.ToString(), logString };
            ListViewItem item = new ListViewItem(strItem);
            logList.Items.Add(item);
            logList.EnsureVisible(logList.Items.Count - 1);
        }
    }
}
