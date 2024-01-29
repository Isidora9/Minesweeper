namespace WindowsFormsApp1
{
    partial class Form1
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
            this.pnlBody = new System.Windows.Forms.Panel();
            this.BtnRestart = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.txtMineCount = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtTimer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pnlBody
            // 
            this.pnlBody.Location = new System.Drawing.Point(0, 63);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(381, 337);
            this.pnlBody.TabIndex = 10;
            // 
            // BtnRestart
            // 
            this.BtnRestart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnRestart.Location = new System.Drawing.Point(153, 22);
            this.BtnRestart.Name = "BtnRestart";
            this.BtnRestart.Size = new System.Drawing.Size(75, 23);
            this.BtnRestart.TabIndex = 11;
            this.BtnRestart.Text = "Restart";
            this.BtnRestart.UseVisualStyleBackColor = true;
            this.BtnRestart.Click += new System.EventHandler(this.BtnRestart_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(295, 22);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(72, 20);
            this.txtResult.TabIndex = 12;
            this.txtResult.Text = "0";
            this.txtResult.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMineCount
            // 
            this.txtMineCount.Location = new System.Drawing.Point(12, 35);
            this.txtMineCount.Name = "txtMineCount";
            this.txtMineCount.Size = new System.Drawing.Size(72, 20);
            this.txtMineCount.TabIndex = 13;
            this.txtMineCount.Text = "0";
            this.txtMineCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtTimer
            // 
            this.txtTimer.Location = new System.Drawing.Point(12, 7);
            this.txtTimer.Name = "txtTimer";
            this.txtTimer.Size = new System.Drawing.Size(72, 20);
            this.txtTimer.TabIndex = 14;
            this.txtTimer.Text = "0";
            this.txtTimer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(379, 392);
            this.Controls.Add(this.txtTimer);
            this.Controls.Add(this.txtMineCount);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.BtnRestart);
            this.Controls.Add(this.pnlBody);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MineSweeper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Button BtnRestart;
        private System.Windows.Forms.TextBox txtResult;
        private System.Windows.Forms.TextBox txtMineCount;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtTimer;
    }
}

