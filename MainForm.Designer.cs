namespace PPCpuMon
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.timerMonitor = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cpuDisplayModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.totalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.physicalCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logicalCoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.slowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.stopAnimationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerAnimation
            // 
            this.timerAnimation.Interval = 1000;
            this.timerAnimation.Tick += new System.EventHandler(this.TimerAnimation_Tick);
            // 
            // timerMonitor
            // 
            this.timerMonitor.Interval = 1000;
            this.timerMonitor.Tick += new System.EventHandler(this.TimerMonitor_Tick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cpuDisplayModeToolStripMenuItem,
            this.speedToolStripMenuItem,
            this.stopAnimationToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(211, 134);
            // 
            // cpuDisplayModeToolStripMenuItem
            // 
            this.cpuDisplayModeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.totalToolStripMenuItem,
            this.physicalCoreToolStripMenuItem,
            this.logicalCoreToolStripMenuItem});
            this.cpuDisplayModeToolStripMenuItem.Name = "cpuDisplayModeToolStripMenuItem";
            this.cpuDisplayModeToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.cpuDisplayModeToolStripMenuItem.Text = "&CPU";
            // 
            // totalToolStripMenuItem
            // 
            this.totalToolStripMenuItem.Name = "totalToolStripMenuItem";
            this.totalToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.totalToolStripMenuItem.Text = "Total";
            this.totalToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_CheckedChanged);
            // 
            // physicalCoreToolStripMenuItem
            // 
            this.physicalCoreToolStripMenuItem.Name = "physicalCoreToolStripMenuItem";
            this.physicalCoreToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.physicalCoreToolStripMenuItem.Text = "Physical Core";
            this.physicalCoreToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_CheckedChanged);
            // 
            // logicalCoreToolStripMenuItem
            // 
            this.logicalCoreToolStripMenuItem.Name = "logicalCoreToolStripMenuItem";
            this.logicalCoreToolStripMenuItem.Size = new System.Drawing.Size(179, 26);
            this.logicalCoreToolStripMenuItem.Text = "Logical Core";
            this.logicalCoreToolStripMenuItem.Click += new System.EventHandler(this.DisplayModeToolStripMenuItem_CheckedChanged);
            // 
            // speedToolStripMenuItem
            // 
            this.speedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fastToolStripMenuItem,
            this.normalToolStripMenuItem,
            this.slowToolStripMenuItem,
            this.toolStripSeparator2});
            this.speedToolStripMenuItem.Name = "speedToolStripMenuItem";
            this.speedToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.speedToolStripMenuItem.Text = "&Animation speed";
            this.speedToolStripMenuItem.ToolTipText = "Animation speed";
            // 
            // fastToolStripMenuItem
            // 
            this.fastToolStripMenuItem.Name = "fastToolStripMenuItem";
            this.fastToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.fastToolStripMenuItem.Text = "&Fast";
            this.fastToolStripMenuItem.Click += new System.EventHandler(this.AnimationSpeedToolStripMenuItem_Click);
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.normalToolStripMenuItem.Text = "&Normal";
            this.normalToolStripMenuItem.Click += new System.EventHandler(this.AnimationSpeedToolStripMenuItem_Click);
            // 
            // slowToolStripMenuItem
            // 
            this.slowToolStripMenuItem.Name = "slowToolStripMenuItem";
            this.slowToolStripMenuItem.Size = new System.Drawing.Size(142, 26);
            this.slowToolStripMenuItem.Text = "&Slow";
            this.slowToolStripMenuItem.Click += new System.EventHandler(this.AnimationSpeedToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(139, 6);
            // 
            // stopAnimationToolStripMenuItem
            // 
            this.stopAnimationToolStripMenuItem.Name = "stopAnimationToolStripMenuItem";
            this.stopAnimationToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.stopAnimationToolStripMenuItem.Text = "&Stop animation";
            this.stopAnimationToolStripMenuItem.ToolTipText = "Stop animation when CPU load is low";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.ToolTipText = "Leave the perty";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 219);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.Timer timerMonitor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem slowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cpuDisplayModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem totalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem physicalCoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem logicalCoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem stopAnimationToolStripMenuItem;
    }
}

