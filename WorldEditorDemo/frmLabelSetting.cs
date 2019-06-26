using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WEMapDemo
{
    public partial class frmLabelSetting : Form
    {
        public frmLabelSetting()
        {
            InitializeComponent();
        }

        public frmLabelSetting(Font source, StyleSetting _father)
        {
            InitializeComponent();
            LabelFont = source;
            LabelFontText.Text = source.Name;
            father = _father;
            for(int i = 0; i < father.Items.Count; i++)
                cmbFieldName.Items.Add(father.Items[i]);
            cmbFieldName.SelectedIndex = 0;
        }

        public Font LabelFont = new Font("宋体", 5);
        public string LabelField = "FID";
        public Color LabelColor = Color.Black;
        StyleSetting father = null;

        private void btnLabelFont_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            //fontDialog1.ShowDialog();
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                //SetLabel.Font = fontDialog1.Font;
                LabelFont = fontDialog1.Font;
                LabelFontText.Text = fontDialog1.Font.Name;
            }
        }

        private void OKbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            LabelField = cmbFieldName.Items[(cmbFieldName.SelectedIndex)].ToString();
            this.Close();
        }

        private void Clsbtn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog1 = new ColorDialog();
            colorDialog1.Color = btnColor.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                btnColor.BackColor = colorDialog1.Color;
        }
    }
}
