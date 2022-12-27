using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static WatermarkGenerator.Generator;

namespace Watermark_Empower
{
    public partial class PresetDialog : Form
    {

       
       
        public Options thisOptions = new Options();
        public ProjectSettings thisprojectSettings = new ProjectSettings();
        public EffectSettings thiseffectSettings = new EffectSettings();
       
        public PresetDialog(Options options, ProjectSettings projectsettings, EffectSettings effectsettings)
        {
            
            InitializeComponent();
            thisOptions = options;
            thisprojectSettings = projectsettings;
            thiseffectSettings = effectsettings;
            
        }
        
        private void customButtons2_Click(object sender, EventArgs e)
        {
            XmlColor convert = new XmlColor();
            convert.ToColor(thisOptions.Color, thisOptions.Color2, thisOptions.Color3, thiseffectSettings.EffectColor1, thiseffectSettings.EffectColor2, thiseffectSettings.EffectColor3);
            Wrapper savepreset = new Wrapper { WrapedOptions = thisOptions, WrapedPrjSettings = thisprojectSettings, WrapedEffectSettings = thiseffectSettings,
                WrapedColor = convert};
          
            string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = Path.Combine(executableDirectory, PresetNameTxtBox.Text + ".xml");
            string oldfilename = fileName;
            int count = 1;
            if (!RewritePreset.Checked)
            {
                while (File.Exists(fileName))
                {
                    fileName = oldfilename;
                    string newFileName = fileName.Insert(fileName.LastIndexOf('.'), $" ({count})");
                    fileName = Path.Combine(executableDirectory, newFileName);
                    count++;
                }
               
              
                
            }
            
            XmlSerializer serializer = new XmlSerializer(typeof(Wrapper));

            using (FileStream stream = File.Create(fileName))
            {
                serializer.Serialize(stream, savepreset);
                
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshPresets_Click(object sender, EventArgs e)
        {
            string[] xmlpresets = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");
            // Add the file names to a combo box

            comboBox1.Items.Clear();


            foreach (string filePath in xmlpresets)
            {
                comboBox1.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        private void customButtons3_Click(object sender, EventArgs e)
        {
            try
            {
                string filename = comboBox1.Text + ".xml";
                         File.Delete(filename);
                RefreshPresets_Click(sender,e);
                comboBox1.Text = null;
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
         
        }

        private void PresetDialog_Load(object sender, EventArgs e)
        {
           RefreshPresets_Click(sender, e);
        }
    }
}
