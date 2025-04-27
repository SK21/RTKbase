namespace RTKbaseMonitor
{
    partial class frmMonitor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitor));
            this.label1 = new System.Windows.Forms.Label();
            this.ckConnected = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbInoID = new System.Windows.Forms.Label();
            this.lbSent = new System.Windows.Forms.Label();
            this.lbHangUps = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lbIP = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ckBase = new System.Windows.Forms.CheckBox();
            this.lbBadPacket = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbSpeed = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bytes Sent";
            // 
            // ckConnected
            // 
            this.ckConnected.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckConnected.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.ckConnected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckConnected.Location = new System.Drawing.Point(18, 12);
            this.ckConnected.Name = "ckConnected";
            this.ckConnected.Size = new System.Drawing.Size(117, 34);
            this.ckConnected.TabIndex = 170;
            this.ckConnected.Text = "Server Connected";
            this.ckConnected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckConnected.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 20);
            this.label2.TabIndex = 172;
            this.label2.Text = "Hangups";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(156, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 20);
            this.label3.TabIndex = 174;
            this.label3.Text = "INO ID";
            // 
            // lbInoID
            // 
            this.lbInoID.Location = new System.Drawing.Point(239, 67);
            this.lbInoID.Name = "lbInoID";
            this.lbInoID.Size = new System.Drawing.Size(46, 20);
            this.lbInoID.TabIndex = 175;
            this.lbInoID.Text = "0";
            // 
            // lbSent
            // 
            this.lbSent.Location = new System.Drawing.Point(67, 147);
            this.lbSent.Name = "lbSent";
            this.lbSent.Size = new System.Drawing.Size(88, 20);
            this.lbSent.TabIndex = 176;
            this.lbSent.Text = "0";
            // 
            // lbHangUps
            // 
            this.lbHangUps.Location = new System.Drawing.Point(98, 108);
            this.lbHangUps.Name = "lbHangUps";
            this.lbHangUps.Size = new System.Drawing.Size(46, 20);
            this.lbHangUps.TabIndex = 177;
            this.lbHangUps.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lbIP
            // 
            this.lbIP.Location = new System.Drawing.Point(185, 108);
            this.lbIP.Name = "lbIP";
            this.lbIP.Size = new System.Drawing.Size(87, 20);
            this.lbIP.TabIndex = 179;
            this.lbIP.Text = "255.255.255.255";
            this.lbIP.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(156, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 20);
            this.label5.TabIndex = 178;
            this.label5.Text = "IP address";
            // 
            // ckBase
            // 
            this.ckBase.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckBase.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.ckBase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckBase.Location = new System.Drawing.Point(155, 12);
            this.ckBase.Name = "ckBase";
            this.ckBase.Size = new System.Drawing.Size(117, 34);
            this.ckBase.TabIndex = 180;
            this.ckBase.Text = "RTKbase Connected";
            this.ckBase.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckBase.UseVisualStyleBackColor = true;
            // 
            // lbBadPacket
            // 
            this.lbBadPacket.Location = new System.Drawing.Point(98, 67);
            this.lbBadPacket.Name = "lbBadPacket";
            this.lbBadPacket.Size = new System.Drawing.Size(46, 20);
            this.lbBadPacket.TabIndex = 182;
            this.lbBadPacket.Text = "0";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 20);
            this.label6.TabIndex = 181;
            this.label6.Text = "Bad Messages";
            // 
            // lbSpeed
            // 
            this.lbSpeed.Location = new System.Drawing.Point(216, 147);
            this.lbSpeed.Name = "lbSpeed";
            this.lbSpeed.Size = new System.Drawing.Size(56, 20);
            this.lbSpeed.TabIndex = 184;
            this.lbSpeed.Text = "0.0";
            this.lbSpeed.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(156, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 20);
            this.label7.TabIndex = 183;
            this.label7.Text = "Kbps";
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 176);
            this.Controls.Add(this.lbSpeed);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbBadPacket);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ckBase);
            this.Controls.Add(this.lbIP);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbHangUps);
            this.Controls.Add(this.lbSent);
            this.Controls.Add(this.lbInoID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ckConnected);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMonitor";
            this.Text = "RTKbase Monitor";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ckConnected;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbInoID;
        private System.Windows.Forms.Label lbSent;
        private System.Windows.Forms.Label lbHangUps;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbIP;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckBase;
        private System.Windows.Forms.Label lbBadPacket;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbSpeed;
        private System.Windows.Forms.Label label7;
    }
}

