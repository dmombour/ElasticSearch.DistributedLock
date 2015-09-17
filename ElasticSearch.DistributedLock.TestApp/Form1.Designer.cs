namespace ElasticSearch.DistributedLock.TestApp
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
            this.buttonAquire = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.buttonRelease = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxChaos = new System.Windows.Forms.TextBox();
            this.buttonChaos = new System.Windows.Forms.Button();
            this.textBoxChaosThreads = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLockTimeout = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxLockRetry = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonAquire
            // 
            this.buttonAquire.Location = new System.Drawing.Point(112, 95);
            this.buttonAquire.Name = "buttonAquire";
            this.buttonAquire.Size = new System.Drawing.Size(75, 23);
            this.buttonAquire.TabIndex = 0;
            this.buttonAquire.Text = "Aquire";
            this.buttonAquire.UseVisualStyleBackColor = true;
            this.buttonAquire.Click += new System.EventHandler(this.buttonAquire_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lock name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(112, 16);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(165, 20);
            this.textBoxName.TabIndex = 2;
            // 
            // buttonRelease
            // 
            this.buttonRelease.Location = new System.Drawing.Point(202, 94);
            this.buttonRelease.Name = "buttonRelease";
            this.buttonRelease.Size = new System.Drawing.Size(75, 23);
            this.buttonRelease.TabIndex = 3;
            this.buttonRelease.Text = "Release";
            this.buttonRelease.UseVisualStyleBackColor = true;
            this.buttonRelease.Click += new System.EventHandler(this.buttonRelease_Click);
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLog.Location = new System.Drawing.Point(12, 224);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.Size = new System.Drawing.Size(265, 198);
            this.textBoxLog.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Chaos Test Time:";
            // 
            // textBoxChaos
            // 
            this.textBoxChaos.Location = new System.Drawing.Point(112, 123);
            this.textBoxChaos.Name = "textBoxChaos";
            this.textBoxChaos.Size = new System.Drawing.Size(165, 20);
            this.textBoxChaos.TabIndex = 6;
            this.textBoxChaos.Text = "00:00:10";
            // 
            // buttonChaos
            // 
            this.buttonChaos.Location = new System.Drawing.Point(112, 175);
            this.buttonChaos.Name = "buttonChaos";
            this.buttonChaos.Size = new System.Drawing.Size(165, 23);
            this.buttonChaos.TabIndex = 7;
            this.buttonChaos.Text = "Chaos";
            this.buttonChaos.UseVisualStyleBackColor = true;
            this.buttonChaos.Click += new System.EventHandler(this.buttonChaos_Click);
            // 
            // textBoxChaosThreads
            // 
            this.textBoxChaosThreads.Location = new System.Drawing.Point(112, 149);
            this.textBoxChaosThreads.Name = "textBoxChaosThreads";
            this.textBoxChaosThreads.Size = new System.Drawing.Size(165, 20);
            this.textBoxChaosThreads.TabIndex = 9;
            this.textBoxChaosThreads.Text = "10";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Chaos Threads:";
            // 
            // textBoxLockTimeout
            // 
            this.textBoxLockTimeout.Location = new System.Drawing.Point(112, 40);
            this.textBoxLockTimeout.Name = "textBoxLockTimeout";
            this.textBoxLockTimeout.Size = new System.Drawing.Size(165, 20);
            this.textBoxLockTimeout.TabIndex = 11;
            this.textBoxLockTimeout.Text = "00:01:00";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Lock TTL:";
            // 
            // textBoxLockRetry
            // 
            this.textBoxLockRetry.Location = new System.Drawing.Point(112, 66);
            this.textBoxLockRetry.Name = "textBoxLockRetry";
            this.textBoxLockRetry.Size = new System.Drawing.Size(165, 20);
            this.textBoxLockRetry.TabIndex = 13;
            this.textBoxLockRetry.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Lock Retry Count:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 434);
            this.Controls.Add(this.textBoxLockRetry);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLockTimeout);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxChaosThreads);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonChaos);
            this.Controls.Add(this.textBoxChaos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.buttonRelease);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonAquire);
            this.Name = "Form1";
            this.Text = "Distributed Lock";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAquire;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Button buttonRelease;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxChaos;
        private System.Windows.Forms.Button buttonChaos;
        private System.Windows.Forms.TextBox textBoxChaosThreads;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLockTimeout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxLockRetry;
        private System.Windows.Forms.Label label5;
    }
}

