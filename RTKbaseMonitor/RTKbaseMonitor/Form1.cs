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
        public UDPComm BaseComm;
        public PGN5000 BaseStatus;

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

            lbIP.Text = "";
            for (int i = 0; i < 3; i++)
            {
                lbIP.Text += BaseStatus.Address[i].ToString("N0") + ".";
            }
            lbIP.Text += BaseStatus.Address[3].ToString("N0");

            ckBase.Checked = BaseStatus.BaseConnected;
            lbBadPacket.Text = BaseStatus.BadPacket.ToString("N0");
            lbSpeed.Text = BaseStatus.Speed.ToString(("N2"));
        }
    }
}