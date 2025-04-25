using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTKbaseMonitor
{
    public partial class frmMonitor : Form
    {
        public PGN5000 BaseStatus;
        public UDPComm BaseComm;
        public frmMonitor()
        {
            InitializeComponent();
            BaseStatus = new PGN5000(this);
            BaseComm = new UDPComm(this, 5350, 5710, 6550, "UDPbase");

            BaseComm.StartUDPServer();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ckConnected.Checked = BaseStatus.Connected;
            lbSent.Text = BaseStatus.BytesSent.ToString("N0");
            lbHangUps.Text = BaseStatus.HangupCount.ToString("N0");
            lbInoID.Text = BaseStatus.InoID.ToString("D");
        }
    }
}
