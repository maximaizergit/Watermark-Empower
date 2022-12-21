using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watermark_Empower
{
   
    public partial class ProjectSettingsDialog : Form
    {
        public int rows { get; set; }
        public int columns { get; set; }
        public int xoffset { get; set; }
        public int yoffset { get; set; }

        public ProjectSettingsDialog(int srows, int scolumns, int sxoffset, int syoffset)
        {
            InitializeComponent();
            RowsTxtBx.Text = srows.ToString();
            ColsTxtBx.Text = scolumns.ToString();
            XOffsettTxtBx.Text = sxoffset.ToString();
            YOffsetTxtBx.Text=syoffset.ToString();
            rows = srows;
            columns = scolumns;
            xoffset = sxoffset;
            yoffset = syoffset;
        }

        private void customButtons2_Click(object sender, EventArgs e)
        {
            try
            {
                rows = Convert.ToInt32(RowsTxtBx.Text);
                columns = Convert.ToInt32(ColsTxtBx.Text);
                xoffset = Convert.ToInt32(XOffsettTxtBx.Text);
                yoffset = Convert.ToInt32(YOffsetTxtBx.Text);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void ProjectSettingsDialog_Load(object sender, EventArgs e)
        {
            
        }
    }
}
