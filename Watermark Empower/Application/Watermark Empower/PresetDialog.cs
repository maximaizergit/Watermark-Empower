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
       public PointList thispoints = new PointList();
      
        public PresetDialog(Options options, ProjectSettings projectsettings, EffectSettings effectsettings, PointList points)
        {
            
            InitializeComponent();
            thisOptions = options;
            thisprojectSettings = projectsettings;
            thiseffectSettings = effectsettings;
            thispoints = points;
            
        }
        
        
        private void customButtons2_Click(object sender, EventArgs e)
        {
            XmlColor convert = new XmlColor();
            foreach(PointOptions point in thispoints)
            {
                point.MaincolorR = point.OptionsForPoint.Color.R;
                point.MaincolorG = point.OptionsForPoint.Color.G;
                point.MaincolorB = point.OptionsForPoint.Color.B;
                point.FirstsubcolorR = point.OptionsForPoint.Color2.R;
                point.FirstsubcolorG = point.OptionsForPoint.Color2.G;
                point.FirstsubcolorB = point.OptionsForPoint.Color2.B;
                point.SecondsubcolorR = point.OptionsForPoint.Color3.R;
                point.SecondsubcolorG = point.OptionsForPoint.Color3.G;
                point.SecondsubcolorB = point.OptionsForPoint.Color3.B;
            }
            convert.ToColor(thisOptions.Color, thisOptions.Color2, thisOptions.Color3, thiseffectSettings.EffectColor1, thiseffectSettings.EffectColor2, thiseffectSettings.EffectColor3);
            Wrapper savepreset = new Wrapper { WrapedOptions = thisOptions, WrapedPrjSettings = thisprojectSettings, WrapedEffectSettings = thiseffectSettings,
                WrapedColor = convert, WrapedPoints = thispoints};
          
            string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = Path.Combine(executableDirectory, PresetNameTxtBox.Text + ".xml");
            string oldfilename = fileName;
            int count = 1;
            if (!RewritePreset.Checked)
            {
                while (File.Exists(fileName))
                {
                    fileName = savePreset(fileName,oldfilename,executableDirectory,count);
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
        private string savePreset(string fileName, string oldfilename, string executableDirectory, int count)
        {
            fileName = oldfilename;
            string newFileName = fileName.Insert(fileName.LastIndexOf('.'), $" ({count})");
            fileName = Path.Combine(executableDirectory, newFileName);
           
            return fileName;
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

        private void ImportPresetButton_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog openFileDialog = new OpenFileDialog();

                // Set the filter options and filter index
                openFileDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;

                // Call the ShowDialog method to show the dialog box
                DialogResult result = openFileDialog.ShowDialog();

                // Process input if the user clicked OK
                if (result == DialogResult.OK)
                {
                    // Open the selected file to read
                    string filePath = openFileDialog.FileName;
                    // Copy the file to the execution directory
                    System.IO.File.Copy(filePath, System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), System.IO.Path.GetFileName(filePath)));
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
