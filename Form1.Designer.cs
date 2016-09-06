namespace AutoBrowser
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip_mainStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_新建指令 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_保存指令 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_另存指令 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_文件_打开指令文件 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_指令 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_指令_运行指令 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_指令_停止运行 = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox_CommandWindow = new System.Windows.Forms.RichTextBox();
            this.richTextBox_ApplicationInfo = new System.Windows.Forms.RichTextBox();
            this.webBrowser_MainWeb = new System.Windows.Forms.WebBrowser();
            this.textBox_url = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_goWebPage = new System.Windows.Forms.Button();
            this.openFileDialog_Command = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog_Command = new System.Windows.Forms.SaveFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_webPanel = new System.Windows.Forms.Panel();
            this.menuStrip_mainStrip.SuspendLayout();
            this.panel_webPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_mainStrip
            // 
            this.menuStrip_mainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件,
            this.toolStripMenuItem_指令});
            this.menuStrip_mainStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_mainStrip.Name = "menuStrip_mainStrip";
            this.menuStrip_mainStrip.Size = new System.Drawing.Size(1184, 25);
            this.menuStrip_mainStrip.TabIndex = 0;
            this.menuStrip_mainStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItem_文件
            // 
            this.toolStripMenuItem_文件.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_文件_新建指令,
            this.toolStripMenuItem_文件_保存指令,
            this.toolStripMenuItem_文件_另存指令,
            this.toolStripMenuItem_文件_打开指令文件});
            this.toolStripMenuItem_文件.Name = "toolStripMenuItem_文件";
            this.toolStripMenuItem_文件.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_文件.Text = "文件";
            // 
            // toolStripMenuItem_文件_新建指令
            // 
            this.toolStripMenuItem_文件_新建指令.Name = "toolStripMenuItem_文件_新建指令";
            this.toolStripMenuItem_文件_新建指令.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_新建指令.Text = "新建指令...";
            // 
            // toolStripMenuItem_文件_保存指令
            // 
            this.toolStripMenuItem_文件_保存指令.Name = "toolStripMenuItem_文件_保存指令";
            this.toolStripMenuItem_文件_保存指令.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_保存指令.Text = "保存指令";
            // 
            // toolStripMenuItem_文件_另存指令
            // 
            this.toolStripMenuItem_文件_另存指令.Name = "toolStripMenuItem_文件_另存指令";
            this.toolStripMenuItem_文件_另存指令.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_另存指令.Text = "另存指令...";
            // 
            // toolStripMenuItem_文件_打开指令文件
            // 
            this.toolStripMenuItem_文件_打开指令文件.Name = "toolStripMenuItem_文件_打开指令文件";
            this.toolStripMenuItem_文件_打开指令文件.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_文件_打开指令文件.Text = "打开指令文件...";
            // 
            // toolStripMenuItem_指令
            // 
            this.toolStripMenuItem_指令.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_指令_运行指令,
            this.toolStripMenuItem_指令_停止运行});
            this.toolStripMenuItem_指令.Name = "toolStripMenuItem_指令";
            this.toolStripMenuItem_指令.Size = new System.Drawing.Size(44, 21);
            this.toolStripMenuItem_指令.Text = "指令";
            // 
            // toolStripMenuItem_指令_运行指令
            // 
            this.toolStripMenuItem_指令_运行指令.Name = "toolStripMenuItem_指令_运行指令";
            this.toolStripMenuItem_指令_运行指令.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_指令_运行指令.Text = "运行指令";
            // 
            // toolStripMenuItem_指令_停止运行
            // 
            this.toolStripMenuItem_指令_停止运行.Name = "toolStripMenuItem_指令_停止运行";
            this.toolStripMenuItem_指令_停止运行.Size = new System.Drawing.Size(124, 22);
            this.toolStripMenuItem_指令_停止运行.Text = "停止运行";
            // 
            // richTextBox_CommandWindow
            // 
            this.richTextBox_CommandWindow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.richTextBox_CommandWindow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox_CommandWindow.ForeColor = System.Drawing.Color.Lime;
            this.richTextBox_CommandWindow.Location = new System.Drawing.Point(758, 62);
            this.richTextBox_CommandWindow.Name = "richTextBox_CommandWindow";
            this.richTextBox_CommandWindow.Size = new System.Drawing.Size(414, 487);
            this.richTextBox_CommandWindow.TabIndex = 1;
            this.richTextBox_CommandWindow.Text = "";
            // 
            // richTextBox_ApplicationInfo
            // 
            this.richTextBox_ApplicationInfo.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.richTextBox_ApplicationInfo.ForeColor = System.Drawing.Color.Red;
            this.richTextBox_ApplicationInfo.Location = new System.Drawing.Point(14, 378);
            this.richTextBox_ApplicationInfo.Name = "richTextBox_ApplicationInfo";
            this.richTextBox_ApplicationInfo.Size = new System.Drawing.Size(730, 171);
            this.richTextBox_ApplicationInfo.TabIndex = 2;
            this.richTextBox_ApplicationInfo.Text = "";
            // 
            // webBrowser_MainWeb
            // 
            this.webBrowser_MainWeb.Location = new System.Drawing.Point(-2, 0);
            this.webBrowser_MainWeb.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_MainWeb.Name = "webBrowser_MainWeb";
            this.webBrowser_MainWeb.Size = new System.Drawing.Size(732, 288);
            this.webBrowser_MainWeb.TabIndex = 3;
            this.webBrowser_MainWeb.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_MainWeb_DocumentCompleted);
            // 
            // textBox_url
            // 
            this.textBox_url.Location = new System.Drawing.Point(46, 28);
            this.textBox_url.Name = "textBox_url";
            this.textBox_url.Size = new System.Drawing.Size(657, 21);
            this.textBox_url.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("华文琥珀", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 21);
            this.label1.TabIndex = 5;
            this.label1.Text = "URL:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_goWebPage
            // 
            this.button_goWebPage.Location = new System.Drawing.Point(709, 26);
            this.button_goWebPage.Name = "button_goWebPage";
            this.button_goWebPage.Size = new System.Drawing.Size(35, 23);
            this.button_goWebPage.TabIndex = 6;
            this.button_goWebPage.Text = "GO";
            this.button_goWebPage.UseVisualStyleBackColor = true;
            // 
            // openFileDialog_Command
            // 
            this.openFileDialog_Command.Filter = "(*.cm)|*.cm";
            // 
            // saveFileDialog_Command
            // 
            this.saveFileDialog_Command.Filter = "(*.cm)|*.cm";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("华文琥珀", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(758, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(414, 27);
            this.label2.TabIndex = 7;
            this.label2.Text = "指令框";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("华文琥珀", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(14, 356);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(730, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "输出信息框";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_webPanel
            // 
            this.panel_webPanel.Controls.Add(this.webBrowser_MainWeb);
            this.panel_webPanel.Location = new System.Drawing.Point(14, 62);
            this.panel_webPanel.Name = "panel_webPanel";
            this.panel_webPanel.Size = new System.Drawing.Size(730, 288);
            this.panel_webPanel.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_goWebPage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_url);
            this.Controls.Add(this.richTextBox_ApplicationInfo);
            this.Controls.Add(this.richTextBox_CommandWindow);
            this.Controls.Add(this.menuStrip_mainStrip);
            this.Controls.Add(this.panel_webPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip_mainStrip;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Browser - Beta v1.0";
            this.menuStrip_mainStrip.ResumeLayout(false);
            this.menuStrip_mainStrip.PerformLayout();
            this.panel_webPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_mainStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件;
        private System.Windows.Forms.RichTextBox richTextBox_CommandWindow;
        private System.Windows.Forms.RichTextBox richTextBox_ApplicationInfo;
        private System.Windows.Forms.WebBrowser webBrowser_MainWeb;
        private System.Windows.Forms.TextBox textBox_url;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_goWebPage;
        private System.Windows.Forms.OpenFileDialog openFileDialog_Command;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_Command;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_新建指令;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_保存指令;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_另存指令;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_文件_打开指令文件;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_指令;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_指令_运行指令;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_指令_停止运行;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_webPanel;
    }
}

