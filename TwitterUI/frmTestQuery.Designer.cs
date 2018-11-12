namespace TwitterUI
{
    partial class frmTestQuery
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
            this.label2 = new System.Windows.Forms.Label();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblEstimatedTweets = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblETASingle = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblThread = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblEstimatedThreads = new System.Windows.Forms.Label();
            this.lblPUtilization = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblFileSize = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFTDT = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblLTDT = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.lblLargestDiff = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Twitter Link";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(285, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Your Internet download and Scraper Speed:";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Location = new System.Drawing.Point(323, 86);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(50, 17);
            this.lblSpeed.TabIndex = 2;
            this.lblSpeed.Text = "0 kbps";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "1000 downloaded in:";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(323, 116);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(42, 17);
            this.lblTime.TabIndex = 2;
            this.lblTime.Text = "0 sec";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(32, 265);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(216, 17);
            this.label7.TabIndex = 4;
            this.label7.Text = "Estimated Tweets for your query:";
            // 
            // lblEstimatedTweets
            // 
            this.lblEstimatedTweets.AutoSize = true;
            this.lblEstimatedTweets.Location = new System.Drawing.Point(323, 265);
            this.lblEstimatedTweets.Name = "lblEstimatedTweets";
            this.lblEstimatedTweets.Size = new System.Drawing.Size(16, 17);
            this.lblEstimatedTweets.TabIndex = 3;
            this.lblEstimatedTweets.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(32, 295);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(137, 17);
            this.label8.TabIndex = 4;
            this.label8.Text = "ETA without Thread:";
            // 
            // lblETASingle
            // 
            this.lblETASingle.AutoSize = true;
            this.lblETASingle.Location = new System.Drawing.Point(323, 295);
            this.lblETASingle.Name = "lblETASingle";
            this.lblETASingle.Size = new System.Drawing.Size(175, 17);
            this.lblETASingle.TabIndex = 3;
            this.lblETASingle.Text = "00 Days 00 Hours 00 Mins";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(558, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(137, 17);
            this.label10.TabIndex = 4;
            this.label10.Text = "ETA without Thread:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(558, 265);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(185, 17);
            this.label12.TabIndex = 4;
            this.label12.Text = "Recommended # of Thread:";
            // 
            // lblThread
            // 
            this.lblThread.AutoSize = true;
            this.lblThread.Location = new System.Drawing.Point(849, 265);
            this.lblThread.Name = "lblThread";
            this.lblThread.Size = new System.Drawing.Size(16, 17);
            this.lblThread.TabIndex = 3;
            this.lblThread.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(558, 296);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(124, 17);
            this.label14.TabIndex = 4;
            this.label14.Text = "ETA with Threads:";
            // 
            // lblEstimatedThreads
            // 
            this.lblEstimatedThreads.AutoSize = true;
            this.lblEstimatedThreads.Location = new System.Drawing.Point(849, 296);
            this.lblEstimatedThreads.Name = "lblEstimatedThreads";
            this.lblEstimatedThreads.Size = new System.Drawing.Size(175, 17);
            this.lblEstimatedThreads.TabIndex = 3;
            this.lblEstimatedThreads.Text = "00 Days 00 Hours 00 Mins";
            // 
            // lblPUtilization
            // 
            this.lblPUtilization.AutoSize = true;
            this.lblPUtilization.Location = new System.Drawing.Point(323, 147);
            this.lblPUtilization.Name = "lblPUtilization";
            this.lblPUtilization.Size = new System.Drawing.Size(28, 17);
            this.lblPUtilization.TabIndex = 5;
            this.lblPUtilization.Text = "0%";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(32, 147);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(219, 17);
            this.label17.TabIndex = 6;
            this.label17.Text = "Processor Utilization for 1 thread:";
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Red;
            this.lblMessage.Location = new System.Drawing.Point(32, 368);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(992, 150);
            this.lblMessage.TabIndex = 8;
            this.lblMessage.Text = "Message";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(32, 173);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(97, 17);
            this.label19.TabIndex = 6;
            this.label19.Text = "File size (MB):";
            // 
            // lblFileSize
            // 
            this.lblFileSize.AutoSize = true;
            this.lblFileSize.Location = new System.Drawing.Point(323, 173);
            this.lblFileSize.Name = "lblFileSize";
            this.lblFileSize.Size = new System.Drawing.Size(40, 17);
            this.lblFileSize.TabIndex = 5;
            this.lblFileSize.Text = "0 MB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(558, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "First Tweet Timestamp:";
            // 
            // lblFTDT
            // 
            this.lblFTDT.AutoSize = true;
            this.lblFTDT.Location = new System.Drawing.Point(849, 86);
            this.lblFTDT.Name = "lblFTDT";
            this.lblFTDT.Size = new System.Drawing.Size(16, 17);
            this.lblFTDT.TabIndex = 3;
            this.lblFTDT.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(558, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Last Tweet Timestamp:";
            // 
            // lblLTDT
            // 
            this.lblLTDT.AutoSize = true;
            this.lblLTDT.Location = new System.Drawing.Point(849, 114);
            this.lblLTDT.Name = "lblLTDT";
            this.lblLTDT.Size = new System.Drawing.Size(16, 17);
            this.lblLTDT.TabIndex = 3;
            this.lblLTDT.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(558, 147);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(275, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Largest Time difference between 2 tweets:";
            // 
            // lblLargestDiff
            // 
            this.lblLargestDiff.AutoSize = true;
            this.lblLargestDiff.Location = new System.Drawing.Point(849, 147);
            this.lblLargestDiff.Name = "lblLargestDiff";
            this.lblLargestDiff.Size = new System.Drawing.Size(16, 17);
            this.lblLargestDiff.TabIndex = 3;
            this.lblLargestDiff.Text = "0";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Location = new System.Drawing.Point(326, 26);
            this.linkLabel1.Multiline = true;
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.ReadOnly = true;
            this.linkLabel1.Size = new System.Drawing.Size(698, 47);
            this.linkLabel1.TabIndex = 10;
            // 
            // frmTestQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 534);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.lblFileSize);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.lblPUtilization);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lblEstimatedThreads);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblThread);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.lblETASingle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblLargestDiff);
            this.Controls.Add(this.lblLTDT);
            this.Controls.Add(this.lblFTDT);
            this.Controls.Add(this.lblEstimatedTweets);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "frmTestQuery";
            this.Text = "frmTestQuery";
            this.Load += new System.EventHandler(this.frmTestQuery_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label lblMessage;
        public System.Windows.Forms.Label lblSpeed;
        public System.Windows.Forms.Label lblTime;
        public System.Windows.Forms.Label lblEstimatedTweets;
        public System.Windows.Forms.Label lblETASingle;
        public System.Windows.Forms.Label lblThread;
        public System.Windows.Forms.Label lblEstimatedThreads;
        public System.Windows.Forms.Label lblPUtilization;
        public System.Windows.Forms.Label lblFileSize;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lblFTDT;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblLTDT;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblLargestDiff;
        private System.Windows.Forms.TextBox linkLabel1;
    }
}