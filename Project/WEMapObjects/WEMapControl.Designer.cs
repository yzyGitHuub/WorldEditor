﻿namespace WEMapObjects
{
    partial class WEMapControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.finishPartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finishSketchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.finishPartToolStripMenuItem,
            this.finishSketchToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(147, 48);
            // 
            // finishPartToolStripMenuItem
            // 
            this.finishPartToolStripMenuItem.Name = "finishPartToolStripMenuItem";
            this.finishPartToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.finishPartToolStripMenuItem.Text = "FinishPart";
            this.finishPartToolStripMenuItem.Click += new System.EventHandler(this.finishPartToolStripMenuItem_Click);
            // 
            // finishSketchToolStripMenuItem
            // 
            this.finishSketchToolStripMenuItem.Name = "finishSketchToolStripMenuItem";
            this.finishSketchToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.finishSketchToolStripMenuItem.Text = "FinishSketch";
            this.finishSketchToolStripMenuItem.Click += new System.EventHandler(this.finishSketchToolStripMenuItem_Click);
            // 
            // WEMapControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.DoubleBuffered = true;
            this.Name = "WEMapControl";
            this.Size = new System.Drawing.Size(290, 365);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WEMapControl_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WEMapControl_KeyDown);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WEMapControl_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WEMapControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WEMapControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WEMapControl_MouseUp);
            this.Resize += new System.EventHandler(this.WEMapControl_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem finishPartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finishSketchToolStripMenuItem;
    }
}
