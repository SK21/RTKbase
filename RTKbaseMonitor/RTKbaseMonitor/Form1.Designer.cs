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
            this.label1 = new System.Windows.Forms.Label();
            this.ckConnected = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbInoID = new System.Windows.Forms.Label();
            this.lbSent = new System.Windows.Forms.Label();
            this.lbHangUps = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bytes Sent";
            // 
            // ckConnected
            // 
            this.ckConnected.Appearance = System.Windows.Forms.Appearance.Button;
            this.ckConnected.FlatAppearance.CheckedBackColor = System.Drawing.Color.LightGreen;
            this.ckConnected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ckConnected.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckConnected.Location = new System.Drawing.Point(81, 12);
            this.ckConnected.Name = "ckConnected";
            this.ckConnected.Size = new System.Drawing.Size(117, 34);
            this.ckConnected.TabIndex = 170;
            this.ckConnected.Text = "Server Connected";
            this.ckConnected.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ckConnected.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 172;
            this.label2.Text = "Hangups";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(78, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 174;
            this.label3.Text = "INO ID";
            // 
            // lbInoID
            // 
            this.lbInoID.Location = new System.Drawing.Point(152, 136);
            this.lbInoID.Name = "lbInoID";
            this.lbInoID.Size = new System.Drawing.Size(46, 20);
            this.lbInoID.TabIndex = 175;
            this.lbInoID.Text = "0";
            // 
            // lbSent
            // 
            this.lbSent.Location = new System.Drawing.Point(152, 59);
            this.lbSent.Name = "lbSent";
            this.lbSent.Size = new System.Drawing.Size(46, 20);
            this.lbSent.TabIndex = 176;
            this.lbSent.Text = "0";
            // 
            // lbHangUps
            // 
            this.lbHangUps.Location = new System.Drawing.Point(152, 97);
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
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 168);
            this.Controls.Add(this.lbHangUps);
            this.Controls.Add(this.lbSent);
            this.Controls.Add(this.lbInoID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ckConnected);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "frmMonitor";
            this.Text = "RTKbase Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}

