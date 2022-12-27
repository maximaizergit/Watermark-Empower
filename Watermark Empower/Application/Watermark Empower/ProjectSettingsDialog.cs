using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static WatermarkGenerator.Generator;

namespace Watermark_Empower
{
   
    public partial class ProjectSettingsDialog : Form
    {
        public ProjectSettings CurSettings = new ProjectSettings();
      

        public ProjectSettingsDialog(ProjectSettings settings)
        {
            InitializeComponent();
            CurSettings = settings;
            RowsTxtBx.Text = CurSettings.Rows.ToString();
            ColsTxtBx.Text = CurSettings.Columns.ToString();
            XOffsettTxtBx.Text = CurSettings.Xoffset.ToString();
            YOffsetTxtBx.Text= CurSettings.Yoffset.ToString();
            
        }

        private void customButtons2_Click(object sender, EventArgs e)
        {
            try
            {
                CurSettings.Rows = Convert.ToInt32(RowsTxtBx.Text);
                CurSettings.Columns = Convert.ToInt32(ColsTxtBx.Text);
                CurSettings.Xoffset = Convert.ToInt32(XOffsettTxtBx.Text);
                CurSettings.Yoffset = Convert.ToInt32(YOffsetTxtBx.Text);
               
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
