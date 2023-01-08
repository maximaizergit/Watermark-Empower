using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WatermarkGenerator;
using System.Drawing.Imaging;
using System.Reflection;
using System.Threading;
using EffectDialog;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using static WatermarkGenerator.Generator;
using System.Xml.Serialization;

namespace Watermark_Empower
{
    public partial class Form1 : Form
    {
        //VISUAL AND FORM SETTINGS
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        private bool _isDragging = false;
        private Point _lastCursorPosition;
      
        public Form1()
        {
            InitializeComponent();
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
           

            timer1.Tick += timer1_Tick;

        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Preparing form
            DoubleBuffered = true;
            foreach (string temp in tempfiles)
            {
                if (File.Exists(temp))
                {

                    File.Delete(temp);

                }
            }


            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
            filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            this.AutoSize = false;
            this.Width = 1410;
            this.Height = 820;
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
            Rectangle r = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height);
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();

            int d = 50;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.X + r.Width - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Y + r.Height - d, d, d, 90, 90);
            pictureBox1.Region = new Region(gp);

            int hgt = Tools.Height;
            Tools.Height = this.Height + 60;
            panel1.Height = Tools.Height;
            if (hgt < Tools.Height)
            {
                scrollableControl1.Height = Tools.Height - 20;
                scrollableControl3.Height = Tools.Height - 20;

            }
            Tools.SelectTab("None");
            RefreshPresets_Click(sender, e);
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start dragging the form
                _isDragging = true;
                _lastCursorPosition = Cursor.Position;
            }
        }
        private void MainDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Start dragging the form
                _isDragging = true;
                _lastCursorPosition = Cursor.Position;
                timer1.Start();
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Stop dragging the form
                _isDragging = false;
             
            }
        }
        private void MainDisplay_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
              
                // Stop dragging the form
                _isDragging = false;
                timer1.Stop();
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the difference between the current and last cursor positions
                Point delta = new Point(Cursor.Position.X - _lastCursorPosition.X, Cursor.Position.Y - _lastCursorPosition.Y);

                // Update the position of the form
                this.Location = new Point(this.Location.X + delta.X, this.Location.Y + delta.Y);

                // Update the last cursor position
                _lastCursorPosition = Cursor.Position;
            }
        }
     
        private void MainDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                // Calculate the difference between the current and last cursor positions
                Point delta = new Point(Cursor.Position.X - _lastCursorPosition.X, Cursor.Position.Y - _lastCursorPosition.Y);

                // Update the position of the form
                if (options.Operation != "Point")
                {
                    settings.Xoffset = settings.Xoffset + delta.X;
                    settings.Yoffset = settings.Yoffset + delta.Y;
                }
         
                if (currentPointIndex != -1)
                {
                    points.AllPoints[currentPointIndex].X += delta.X;
                    points.AllPoints[currentPointIndex].Y += delta.Y;
                   
                }
                    
              
                // Update the last cursor position
                _lastCursorPosition = Cursor.Position;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (options.Operation)
            {
                case ("Fullfill"):

                    FullfillUpdate();
                    break;
                case ("Chess"):

                    ChessUpdate();
                    break;
                case ("Point"):
                    if (currentPointIndex != -1)
                    {
                        UpdatePointCords();
                    }
                    PointUpdate();
                    break;
            }

        }
        //making menu height always equal to form height
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int hgt = Tools.Height;
            Tools.Height = this.Height - 29;
            navpanelborder.Height = this.Height;
            if (hgt < Tools.Height)
            {
                scrollableControl1.Height = Tools.Height - 20;

            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MainDisplay.Image != null) { newoperation(); }


            foreach (string temp in tempfiles)
            {
                if (File.Exists(temp))
                {

                    File.Delete(temp);

                }
            }
        }

        private void NavMove(int top, int left)//animation for nav menu
        {

            //Nav.Height = height;
            Nav.Left = left;
            int topmove = (Nav.Top - top) / 10;
            for (int i = 0; i < 10; i++)
            {

                Nav.Top = Nav.Top - topmove;
                Nav.Refresh();
                Thread.Sleep(10);
            }
            Nav.Top = top;

        }
        private void SizingNavMove(int left)//animation for nav menu
        {



            int leftmove = (sizingnav.Left - left) / 10;
            for (int i = 0; i < 10; i++)
            {

                sizingnav.Left = sizingnav.Left - leftmove;
                Nav.Refresh();
                Thread.Sleep(10);
            }
            sizingnav.Left = left;

        }
        //DESCRIBING VARIABLES

        string fileName = "tempinput.jpg";
        List<string> tempfiles = new List<string>() { "input.jpg", "tempinput.jpg" };
        string filePath;
        Generator.Options options = new Generator.Options();
        Generator.ProjectSettings settings = new Generator.ProjectSettings();
        Generator.EffectSettings effectsettings = new Generator.EffectSettings();
        Generator gen = new Generator();
        bool isimported = false;
        //MAIN FORM FUNCTIONAL
        private void Import_Image_Click(object sender, EventArgs e)//import image and preparing for work with images
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";

            isimported = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(fileName))
                {
                    newoperation();
                    File.Delete(fileName);

                }
                // Open the selected image file
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open))
                {
                    Image image = Image.FromStream(fileStream);
                    // Load the image into the PictureBox control
                    NavMove(MenuButton.Top, 0);
                    Tools.SelectTab("none");
                    MainDisplay.Image = image;
                    string executableDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    // Save the image to the executable directory with the specified name

                    MainDisplay.Image.Save(Path.Combine(executableDirectory, "tempinput.jpg"), ImageFormat.Jpeg);
                    MainDisplay.Image.Save(Path.Combine(executableDirectory, "input.jpg"), ImageFormat.Jpeg);
                    isimported = true;

                }
            }
        }
        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void StretchButton_Click(object sender, EventArgs e)
        {

            MainDisplay.SizeMode = PictureBoxSizeMode.StretchImage;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(StretchButton.Left);
        }

        private void NormalSizeButton_Click(object sender, EventArgs e)
        {

            MainDisplay.SizeMode = PictureBoxSizeMode.Normal;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(NormalSizeButton.Left);
        }

        private void AutosizeButton_Click(object sender, EventArgs e)
        {
            MainDisplay.SizeMode = PictureBoxSizeMode.AutoSize;
            SizingNavMove(AutosizeButton.Left);
            if (MainDisplay.Width > 960 || MainDisplay.Height > 585)
            {

                MainDisplay.SizeMode = PictureBoxSizeMode.Normal;
                MainDisplay.Width = 960;
                MainDisplay.Height = 585;
                MessageBox.Show("Размер изображения превосходит возможный для работы размер, выберите другой способ размежения картинки");
                SizingNavMove(NormalSizeButton.Left);
            }

        }

        private void CenterImageButton_Click(object sender, EventArgs e)
        {

            MainDisplay.SizeMode = PictureBoxSizeMode.CenterImage;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(CenterImageButton.Left);
        }

        private void ZoomButton_Click(object sender, EventArgs e)
        {

            MainDisplay.SizeMode = PictureBoxSizeMode.Zoom;
            MainDisplay.Width = 960;
            MainDisplay.Height = 585;
            SizingNavMove(ZoomButton.Left);
        }
        private void ProjectOptions_Click(object sender, EventArgs e)
        {
            using (ProjectSettingsDialog prjset = new ProjectSettingsDialog(settings))
            {
                if (prjset.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    settings = prjset.CurSettings;

                    switch (options.Operation)
                    {
                        case ("Fullfill"):
                            FullfillUpdate();
                            break;
                        case ("Chess"):
                            ChessUpdate();
                            break;

                    }
                }
            }
        }


        private void RefreshPresets_Click(object sender, EventArgs e)
        {
            string[] xmlpresets = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");
            // Add the file names to a combo box

            PresetsComboBox.Items.Clear();


            foreach (string filePath in xmlpresets)
            {
                PresetsComboBox.Items.Add(Path.GetFileNameWithoutExtension(filePath));
            }
        }
      
        private void PresetsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeAlignColor("None");
            var selectedItem = PresetsComboBox.SelectedItem;
            string selectedPreset = AppDomain.CurrentDomain.BaseDirectory + selectedItem + ".xml";
            Wrapper wrapperLoaded = new Wrapper();
            XmlSerializer serializer = new XmlSerializer(typeof(Wrapper));
            string[] xmlpresets = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml");
            // Add the file names to a combo box
          
            try
            {

             
                using (FileStream stream = File.OpenRead(selectedPreset))
                {
                    wrapperLoaded = (Wrapper)serializer.Deserialize(stream);
                    
                }
                Console.WriteLine("Parameters loaded successfully.");

                // Get the instances of the three classes from the wrapper

                options = wrapperLoaded.WrapedOptions;
                XmlColor restorecolor = new XmlColor();
                restorecolor = wrapperLoaded.WrapedColor;
                options.Color = restorecolor.GetColor(restorecolor.Red1, restorecolor.Green1, restorecolor.Blue1);
                options.Color2 = restorecolor.GetColor(restorecolor.Red2, restorecolor.Green2, restorecolor.Blue2);
                options.Color3 = restorecolor.GetColor(restorecolor.Red3, restorecolor.Green3, restorecolor.Blue3);



                settings = wrapperLoaded.WrapedPrjSettings;

                effectsettings = wrapperLoaded.WrapedEffectSettings;
                effectsettings.EffectColor1 = restorecolor.GetColor(restorecolor.EffectRed1, restorecolor.EffectGreen1, restorecolor.EffectBlue1);
                effectsettings.EffectColor2 = restorecolor.GetColor(restorecolor.EffectRed2, restorecolor.EffectGreen2, restorecolor.EffectBlue2);
                effectsettings.EffectColor3 = restorecolor.GetColor(restorecolor.EffectRed3, restorecolor.EffectGreen3, restorecolor.EffectBlue3);
                points = wrapperLoaded.WrapedPoints;
                Console.WriteLine(wrapperLoaded.WrapedPoints.Count);
               
                PointsList.Items.Clear();
                foreach (PointOptions point in points)
                {
                    PointsList.Items.Add(point.Name);
                    point.OptionsForPoint.Color = Color.FromArgb(255,point.MaincolorR,point.MaincolorG,point.MaincolorB);
                    point.OptionsForPoint.Color2 = Color.FromArgb(255, point.FirstsubcolorR, point.FirstsubcolorG, point.FirstsubcolorB);
                    point.OptionsForPoint.Color3 = Color.FromArgb(255, point.SecondsubcolorR, point.SecondsubcolorG, point.SecondsubcolorB);
                }
                foreach (PointOptions point in wrapperLoaded.WrapedPoints)
                {
                   
                    Console.WriteLine(point.MaincolorR+" "+ point.MaincolorG+" "+ point.MaincolorB);
                }
            
                UpdateImage();
                UpdColorDisplays();
                
            }
           catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PresetDialogButton_Click(object sender, EventArgs e)
        {
            using (PresetDialog presetdialog = new PresetDialog(options, settings, effectsettings, points))
            {
                if (presetdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    RefreshPresets_Click(sender, e);
                }
            }
        }
    
    //nav buttons
    private void FullfillSelector_Click(object sender, EventArgs e)//opening Fullfill options menu and creating image with defoult options
        {
            if (isimported)
            {

                FullfillText.Text = options.Text;
                FullfillFont.Text = options.Fontname;
                FullfillFontSize.Text = options.Fontsize.ToString();
                FullfillAngle.Text = options.Angle.ToString();
                double transparancy = (Convert.ToDouble(options.Transparancy) / 255 * 100);

                FullfillTransparancy.Text = Math.Round(transparancy, 0).ToString();
                Fullfillwbtw.Text = options.Widthbetween.ToString();
                Fullfillhbtw.Text = options.Heightbetween.ToString();
                FullfillStyle.Text = options.Fontstyle.ToString();
                FullfillColorDisplay.BackColor = options.Color;
                FullfillGradientStart.Text = options.Colorstart.ToString();
                FullfillGradientEnd.Text = options.Colorend.ToString();
                FullfillGradientAngle.Text = options.Angle.ToString();
                FullfillEffectTextBox.Text = options.Effect.ToString();
                
                NavMove(FullfillSelector.Top, FullfillSelector.Left);
                options.Operation = "Fullfill";
                if (options.Isgradienton)
                {
                    FullfillGradientcheckbox.Checked = true;
                    LinearGradientBrush gradientBrush = ColorPreviewUpd(options.Color, options.Color2, options.Color3);
                    UpdColorDisplays(gradientBrush);
                }
                else
                {
                    FullfillGradientcheckbox.Checked = false;
                }
                newoperation();

                if (!FullfillGradientcheckbox.Checked)
                {

                    gen.GenPatternFullfill(options, settings, effectsettings);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
                else
                {
                    gen.GenPatternFullfillGradient(options, settings, effectsettings);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");

                }

                Tools.SelectTab("fullfill");
                UpdColorDisplays();
            }
            else
            {
                MessageBox.Show("Сначала импортируйте изображение!");
            }

        }

        private void ChessSelector_Click(object sender, EventArgs e)
        {

            if (isimported)
            {

              
                ChessText.Text = options.Text;
                ChessFont.Text = options.Fontname;
                ChessTextSize.Text = options.Fontsize.ToString();
                ChessAngle.Text = options.Angle.ToString();
                double transparancy = (Convert.ToDouble(options.Transparancy) / 255 * 100);
                ChessTransparancy.Text = Math.Round(transparancy, 0).ToString();
                ChessWbtw.Text = options.Widthbetween.ToString();
                ChessHbtw.Text = options.Heightbetween.ToString();
                ChessTextStyle.Text = options.Fontstyle.ToString();
                ChessColorDisplay.BackColor = options.Color;
                ChessGradientStart.Text = options.Colorstart.ToString();
                ChessGradientEnd.Text = options.Colorend.ToString();
                ChessGradientAngle.Text = options.Angle.ToString();
                ChessEffect.Text = options.Effect.ToString();
               
                options.Operation = "Chess";
                NavMove(ChessSelector.Top, ChessSelector.Left);
                if (options.Isgradienton)
                {
                    LinearGradientBrush gradientBrush = ColorPreviewUpd(options.Color, options.Color2, options.Color3);
                    UpdColorDisplays(gradientBrush);
                    ChessGradientCheckBox.Checked = true;
                }
                else
                {

                    ChessGradientCheckBox.Checked = false;

                }
                newoperation();
                if (!options.Isgradienton)
                {

                    gen.GenPatternChess(options, settings, effectsettings);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
                else
                {
                    gen.GenPatternChessGradient(options, settings, effectsettings);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");

                }
                Tools.SelectTab("chess");
                UpdColorDisplays();

            }
            else
            {
                MessageBox.Show("Сначала импортируйте изображение!");
            }
        }
        private void PointSelector_Click(object sender, EventArgs e)
        {
            
            if (isimported)
            {
               
                
                PointTextTxtBox.Text = options.Text;
                PointFontTxtBox.Text = options.Fontname;
                PointFontSizeTxtBox.Text = options.Fontsize.ToString();
                PointAngleTxtBox.Text = options.Angle.ToString();
                double transparancy = (Convert.ToDouble(options.Transparancy) / 255 * 100);
                PointOpacityTxtBox.Text = Math.Round(transparancy, 0).ToString();
                PointStyleTxtBox.Text = options.Fontstyle.ToString();
                PointColorDisplay.BackColor = options.Color;
                PointGradientStartTxtBox.Text = options.Colorstart.ToString();
                PointGradientEndTxtBox.Text = options.Colorend.ToString();
                PointGradientAngleTxtBox.Text = options.Angle.ToString();
                PointEffectTxtBox.Text = options.Effect.ToString();
                
                options.Operation = "Point";
                NavMove(PointSelector.Top, PointSelector.Left);
                if (options.Isgradienton)
                {
                    
                   
                    PointGradientChkBox.Checked = true;
                }
                else
                {

                    PointGradientChkBox.Checked = false;

                }
              
                newoperation();
                if (!options.Isgradienton)
                {
                    gen.DrawPoint(points);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");
                }
                else
                {
                    gen.DrawPoint(points);
                    MainDisplay.Image = Image.FromFile("tempinput.jpg");

                }

                Tools.SelectTab("point");
                
                UpdColorDisplays();
               
               

            }
            else
            {
                MessageBox.Show("Сначала импортируйте изображение!");
            }
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            NavMove(button2.Top, button2.Left);
            button2.BackColor = Color.FromArgb(46, 51, 73);
        }
        //TODO remove color change
        private void FullfillSelector_Leave(object sender, EventArgs e)
        {
            FullfillSelector.BackColor = Color.FromArgb(24, 30, 53);
        }

        private void ChessSelector_Leave(object sender, EventArgs e)
        {
            ChessSelector.BackColor = Color.FromArgb(24, 30, 53);
        }
        //switch for selecting unused image
        private void newoperation()
        {
            if (MainDisplay.Image != null)
            {
               
                MainDisplay.Image.Dispose();
                MainDisplay.Image = null;
            }
            else
            {
                MessageBox.Show("noop");
                
            }
              
            
            
        }
        private Color colorupd()
        {
            ColorDialog colorDialog = new ColorDialog();

            // Allow the user to enter custom colors
            colorDialog.AllowFullOpen = true;

            // Set the initially selected color to red

            Color selectedColor = colorDialog.Color;

            // Only allow the user to select solid colors
            colorDialog.SolidColorOnly = true;

            // Set the custom colors that the user can select from
            colorDialog.CustomColors = new int[] { ColorTranslator.ToOle(Color.AliceBlue), ColorTranslator.ToOle(Color.AntiqueWhite) };

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // The user selected a color and clicked OK
                selectedColor = colorDialog.Color;

            }
            return selectedColor;
        }



        //WORK WITH FULLFILL PATTERN
        private void UpdateImage()
        {
         
            switch (options.Operation)
            {
                case ("Fullfill"):
                    FullfillUpdate();
                    break;
                case ("Chess"):
                    
                    ChessUpdate();
                    break;
                case ("Point"):
                    PointUpdate();
                    break;
            }
        }
        //Updating unused image. Disposing used image. Opening updated image 
        private void FullfillUpdate()
        {
            newoperation();
            if (!FullfillGradientcheckbox.Checked)
            {

              
                
                gen.GenPatternFullfill(options, settings, effectsettings);
                MainDisplay.Image = Image.FromFile("tempinput.jpg");

            }
            else
            {

                gen.GenPatternFullfillGradient(options, settings, effectsettings);

                MainDisplay.Image = Image.FromFile("tempinput.jpg");

            }

        }

        //Update image on fields change
        private void TextChange_TextChanged(object sender, EventArgs e)
        {

            TextBox textBox = sender as TextBox;
            options.Text = textBox.Text;
            UpdateImage();

        }
        private void FontChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            options.Fontname = textBox.Text;
            UpdateImage();
        }
        private void FontSizeChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || Convert.ToInt32(textBox.Text) <= 7))
            {
                try
                {
                    if (Convert.ToInt32(textBox.Text) < 0 || Convert.ToInt32(textBox.Text) > 10000)
                    {
                        throw new Exception("Размер шрифта вышел за диапазон");
                    }
                    options.Fontsize = Convert.ToInt32(textBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateImage();
            }
        }
        private void OpacityChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == ""))
            {
                try
                {
                    if (Convert.ToInt32(textBox.Text) < 0 || Convert.ToInt32(textBox.Text) > 100)
                    {
                        throw new Exception("Прозрачность вышла за пределы диапазона");
                    }
                    options.Transparancy = Convert.ToInt32(Convert.ToDouble(textBox.Text) / 100 * 255);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                UpdateImage();

            }
        }
        private void AngleChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {

                    options.Angle = Convert.ToInt32(textBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateImage();
            }
        }
        private void WidthbtwChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float width = size.Width - 5;



                        if (Convert.ToInt32(textBox.Text) < -width)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(width, 0) + " недопустимы для данного размера текста");
                        }
                        options.Widthbetween = Convert.ToInt32(textBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateImage();
            }
        }
        private void HeightbtwChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {
                    using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                    {
                        Font font = new Font(options.Fontname, options.Fontsize);
                        SizeF size = g.MeasureString(options.Text, font);
                        float height = size.Height - 5;



                        if (Convert.ToInt32(textBox.Text) < -height)
                        {
                            throw new Exception("Значения меньше -" + Math.Round(height, 0) + " недопустимы для данного размера текста");
                        }
                        options.Heightbetween = Convert.ToInt32(textBox.Text);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                UpdateImage();
            }
        }
        private void StyleChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            switch (textBox.Text.ToLower())
            {

                case "regular":
                    options.Fontstyle = FontStyle.Regular;
                    break;

                case "bold":
                    options.Fontstyle = FontStyle.Bold;
                    break;

                case "italic":
                    options.Fontstyle = FontStyle.Italic;
                    break;

                case "underline":
                    options.Fontstyle = FontStyle.Underline;
                    break;
                default:

                    break;
            }
            UpdateImage();
        }
        private void ColorDisplay_Click(object sender, EventArgs e)
        {

            if (!options.Isgradienton)
            {
                Color col = colorupd();
                FullfillColorDisplay.BackColor = col;
                ChessColorDisplay.BackColor = col;
                PointColorDisplay.BackColor = col;
                ColorUpdate(FullfillColorDisplay.BackColor);
                UpdColorDisplays();
            }
            else
            {
                Color color1 = colorupd();
                Color color2 = colorupd();
                Color color3 = colorupd();
                LinearGradientBrush gradientBrush = ColorPreviewUpd(color1, color2, color3);
               UpdColorDisplays(gradientBrush);
                GradientUpdate(color1, color2, color3, options.Colorstart, options.Colorend, options.Colorangle);
            }
        }
        private void UpdColorDisplays(LinearGradientBrush gradientBrush)
        {

            FullfillColorDisplay.CreateGraphics().FillRectangle(gradientBrush, FullfillColorDisplay.ClientRectangle);
            ChessColorDisplay.CreateGraphics().FillRectangle(gradientBrush, ChessColorDisplay.ClientRectangle);
            PointColorDisplay.CreateGraphics().FillRectangle(gradientBrush, PointColorDisplay.ClientRectangle);
         
        }
        private void UpdColorDisplays()
        {

            LinearGradientBrush gradientBrush = ColorPreviewUpd(options.Color, options.Color2, options.Color3);
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            // Get the Graphics object for the Bitmap
            Graphics g = Graphics.FromImage(bmp);
            if (options.Isgradienton)
            {


                // Draw on the Bitmap
                g.FillRectangle(gradientBrush, PointColorDisplay.ClientRectangle);

                FullfillColorDisplay.CreateGraphics().FillRectangle(gradientBrush, FullfillColorDisplay.ClientRectangle);
                ChessColorDisplay.CreateGraphics().FillRectangle(gradientBrush, ChessColorDisplay.ClientRectangle);
                PointColorDisplay.CreateGraphics().FillRectangle(gradientBrush, PointColorDisplay.ClientRectangle);
                FullfillColorDisplay.Image = bmp;
                ChessColorDisplay.Image = bmp;
                PointColorDisplay.Image = bmp;
            }
            else
            {
                Brush brush = new SolidBrush(options.Color);
                g.FillRectangle(brush, PointColorDisplay.ClientRectangle);
                FullfillColorDisplay.Image = bmp;
                ChessColorDisplay.Image = bmp;
                PointColorDisplay.Image = bmp;
            }
       
        }
        private void Gradientcheckbox_CheckedChanged(object sender, EventArgs e)
        {
            MyCheckBox checkBox = sender as MyCheckBox;
            
            if (checkBox.Checked)
            {
                options.Isgradienton = true;
                UpdColorDisplays();
                UpdateImage();
               
            }
            else
            {
                options.Isgradienton = false;
                UpdColorDisplays();
                UpdateImage();
                
            }
        }
        private void GradientStartChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(textBox.Text);
                    UpdateImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void GradientEndChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {
                    options.Colorend = Convert.ToInt32(textBox.Text);
                    UpdateImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void GradientAngleChange_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!(textBox.Text == "" || textBox.Text == "-"))
            {
                try
                {
                    options.Colorstart = Convert.ToInt32(textBox.Text);
                    UpdateImage();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void FontSelectBtn_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            // Set the font selection options
            fontDialog.AllowScriptChange = true;
            fontDialog.ShowEffects = true;
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                // Update the font of the selected control
                FullfillFont.Text = fontDialog.Font.Name.ToString();
                ChessFont.Text = fontDialog.Font.Name.ToString();
                PointFontTxtBox.Text = fontDialog.Font.Name.ToString();
                options.Fontname = fontDialog.Font.Name.ToString();
            }
        }
           
        private LinearGradientBrush ColorPreviewUpd(Color color1, Color color2, Color color3)
        {

            ColorBlend blend = new ColorBlend();

            blend.Positions = new float[] { 0, 0.5f, 1 };
            blend.Colors = new Color[] { color1, color2, color3 };
            LinearGradientBrush gradientBrush = new LinearGradientBrush(new Point(0, 0), new Point(40, 67), Color.Red, Color.Blue);
            gradientBrush.InterpolationColors = blend;

            return gradientBrush;
        }

       
        private void ColorUpdate(Color color)
        {

            options.Color = color;
            UpdateImage();
        }
        private void GradientUpdate(Color color, Color color1, Color color2, int colorstart, int colorend, int colorangle)
        {
            options.Color = color;
            options.Color2 = color1;
            options.Color3 = color2;
            options.Colorstart = colorstart;
            options.Colorend = colorend;
            options.Colorangle = colorangle;
            UpdateImage();
        }

        private void EffectDialogButton_Click(object sender, EventArgs e)
        {
            using (Form2 effectdialog = new Form2(effectsettings))
            {
                if (effectdialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    effectsettings = effectdialog.EffectSettings;
                    FullfillEffectTextBox.Text = effectsettings.SelectedEffect;
                    ChessEffect.Text = effectsettings.SelectedEffect;
                    PointEffectTxtBox.Text = effectsettings.SelectedEffect;
                    options.Effect = effectsettings.SelectedEffect;
                    UpdateImage();
                }
            }
        }

     

        //WORK WITH CHESS PATTERN
        private void ChessUpdate()
        {
            
            newoperation();
            if (!options.Isgradienton)
            {

                gen.GenPatternChess(options, settings, effectsettings);
                MainDisplay.Image = Image.FromFile("tempinput.jpg");

            }
            else
            {
                gen.GenPatternChessGradient(options, settings, effectsettings);

                MainDisplay.Image = Image.FromFile("tempinput.jpg");

            }

        }
        private void PointUpdate()
        {

            newoperation();
           changePointOptions();
            gen.DrawPoint(points);
                MainDisplay.Image = Image.FromFile("tempinput.jpg");

            

        }
        PointList points = new PointList();
        private void AddPointBtn_Click(object sender, EventArgs e)
        {
            string NameForPoint = options.Text;
            string OldNameForPoint = options.Text;
            int count = 0;
            foreach(PointOptions chkpoint in points)
            {
                foreach(PointOptions pointforcheck in points)
                {
                   
                        while (NameForPoint == pointforcheck.Name)
                        {
                            NameForPoint = GenPointName(NameForPoint, OldNameForPoint, count);
                            count++;
                        }
                    
                }
            }
                PointOptions point = new PointOptions(options)
                {
                    Name = NameForPoint,
                    X = MainDisplay.Image.Width / 2,
                    Y = MainDisplay.Image.Height / 2,
                    EffectsettingsForPoint = effectsettings
                };
                points.Add(point);
                PointUpdate();
                PointsList.Items.Add(point.Name);
                PointsList.Text=(point.Name);
                ChangeAlignColor("None");
           
        }
        private string GenPointName(string Name, string oldname, int count)
        {
            Name = oldname;
            string newName = Name.Insert(Name.Length, $" ({count})");
            

            return newName;
        }
        private void DelPointBtn_Click(object sender, EventArgs e)
        {
            if (currentPointIndex != -1)
            {
                points.RemoveAt(currentPointIndex);
                PointsList.Items.RemoveAt(currentPointIndex);
                currentPointIndex = -1;
                PointLocationXTxtBox.Text = "";
                PointLocationYTxtBox.Text = "";
                PointUpdate();
            }
          
        }
        int currentPointIndex =-1;
        private void PointsList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                  
                    Options tempoptions = new Options(point.OptionsForPoint);
                
                    currentPointIndex = points.AllPoints.IndexOf(point);
                    UpdatePointCords();
                    options = tempoptions;
                    
                    UpdatePointTxtBoxes();
                    ChangeAlignColor("None");
                    if (point.Center)
                    {
                        ChangeAlignColor("Center");
                    }
                    else
                    {
                        if (point.Aligntopbot == "Top")
                        {
                            ChangeAlignColor("Top");
                        }
                        else if (point.Aligntopbot=="Bottom")
                        { ChangeAlignColor("Bottom"); }
                        if (point.Alignrightleft == "Left")
                        {
                            ChangeAlignColor("Left");
                        }
                        else if (point.Alignrightleft=="Right")
                        { ChangeAlignColor("Right"); }
                    }
                    PointOffsetTxtBox.Text = point.Offset.ToString();
                    if (point.OptionsForPoint.Isgradienton)
                    {
                        PointGradientChkBox.Checked = true;
                    }
                    else
                    {
                        PointGradientChkBox.Checked = false;
                    }

                }
            }
       

        }
        public void UpdatePointCords()
        {
            if(currentPointIndex != -1)
            {
                PointLocationXTxtBox.Text = points.AllPoints[currentPointIndex].X.ToString();
                PointLocationYTxtBox.Text = points.AllPoints[currentPointIndex].Y.ToString();
            }
       
        }
        public void UpdatePointTxtBoxes()
        {
            PointTextTxtBox.Text = options.Text;
            PointFontTxtBox.Text = options.Fontname;
            PointFontSizeTxtBox.Text = options.Fontsize.ToString();
            double transparancy = (Convert.ToDouble(options.Transparancy) / 255 * 100);
            PointOpacityTxtBox.Text = Math.Round(transparancy, 0).ToString();
            PointStyleTxtBox.Text = options.Fontstyle.ToString();
            PointColorDisplay.BackColor = options.Color;
            PointGradientStartTxtBox.Text = options.Colorstart.ToString();
            PointGradientEndTxtBox.Text = options.Colorend.ToString();
            PointGradientAngleTxtBox.Text = options.Angle.ToString();
            PointEffectTxtBox.Text = options.Effect.ToString();
            
        }
        public void changePointOptions()
        {
            if (!settings.Syncpoints)
            {
                if (currentPointIndex != -1)
                {
                    points.AllPoints[currentPointIndex].UpdateOptions(options,effectsettings);
                }
            }
            else
            {
                foreach(PointOptions point in points)
                {
                    point.UpdateOptions(options, effectsettings);
                }
            }
           
           
        }
       

        private void PointLocationXTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentPointIndex != -1)
                {
                    points.AllPoints[currentPointIndex].X = Convert.ToInt32(PointLocationXTxtBox.Text);
                    PointUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private void PointLocationYTxtBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (currentPointIndex != -1)
                {
                    points.AllPoints[currentPointIndex].Y = Convert.ToInt32(PointLocationYTxtBox.Text);
                    PointUpdate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

     

        private void SyncPointsChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SyncPointsChkBox.Checked)
            {
                settings.Syncpoints = true;

            }
            else
            {
                settings.Syncpoints = false;
            }
        }

        private void customButtons3_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {

                    PointsList.Items[currentPointIndex] = PointName.Text;
                    PointsList.Text = PointName.Text;
                    point.Name = PointName.Text;

                }
            }
        }

        private void ChangeAlignColor(string selectedElement)
        {
            switch (selectedElement)
            {
                case ("Top"):
                    AlignTopBtn.FlatAppearance.BorderColor = Color.Green;
                    AlignBotBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
                case ("Bottom"):
                    AlignTopBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignBotBtn.FlatAppearance.BorderColor = Color.Green;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
                case ("Left"):
                    AlignRightBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignLeftBtn.FlatAppearance.BorderColor = Color.Green;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
                case ("Right"):
                    AlignRightBtn.FlatAppearance.BorderColor = Color.Green;
                    AlignLeftBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
                case ("Center"):
                    AlignTopBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignBotBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Green;
                    AlignLeftBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignRightBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
                case ("NoneTopBot"):
                    AlignTopBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignBotBtn.FlatAppearance.BorderColor = Color.Red;
                        
                    break;
                case ("NoneRightLeft"):
                    AlignLeftBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignRightBtn.FlatAppearance.BorderColor = Color.Red;
                    
                    break;
                case ("None"):
                    AlignLeftBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignRightBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignTopBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignBotBtn.FlatAppearance.BorderColor = Color.Red;
                    AlignCenterBtn.FlatAppearance.BorderColor = Color.Red;
                    break;
            }
            PointUpdate();
        }

        private void AlignTopBtn_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    if (point.Aligntopbot != "Top")
                    {
                        point.Aligntopbot = "Top";
                        point.Center = false;
                        ChangeAlignColor("Top");
                    }
                    else
                    {
                        point.Aligntopbot = "None";
                        ChangeAlignColor("NoneTopBot");
                    }
                 

                }
            }
        }

        private void AlignBotBtn_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    if (point.Aligntopbot != "Bottom")
                    {
                        point.Aligntopbot = "Bottom";
                        point.Center = false;
                        ChangeAlignColor("Bottom");
                    }
                    else
                    {
                        point.Aligntopbot = "None";
                        ChangeAlignColor("NoneTopBot");
                    }
                }
            }
        }

        private void AlignLeftBtn_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    if (point.Alignrightleft != "Left")
                    {
                        point.Alignrightleft = "Left";
                        point.Center = false;
                        ChangeAlignColor("Left");
                    }
                    else
                    {
                        point.Alignrightleft= "None";
                        ChangeAlignColor("NoneRightLeft");
                    }

                }
            }
        }

        private void AlignRightBtn_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    if (point.Alignrightleft != "Right")
                    {
                        point.Alignrightleft = "Right";
                    point.Center = false;
                    ChangeAlignColor("Right");
                    }
                    else
                    {
                        point.Alignrightleft = "None";
                        ChangeAlignColor("NoneRightLeft");
                    }
                }
            }
        }

        private void AlignCenterBtn_Click(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    if (point.Center == false)
                    {
                        point.Aligntopbot = "None";
                        point.Alignrightleft = "None";
                        point.Center = true;
                        ChangeAlignColor("Center");
                    }
                    else
                    {
                        point.Center = false;
                        ChangeAlignColor("None");
                    }
             

                }
            }
        }

        private void PointOffsetTxtBox_TextChanged(object sender, EventArgs e)
        {
            foreach (PointOptions point in points)
            {
                if (point.Name == PointsList.Text)
                {
                    try
                    {
                        point.Offset = Convert.ToInt32(PointOffsetTxtBox.Text);
                        PointUpdate();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                  
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newoperation();
            options.Operation = "Cross";
            gen.GenPatternCross(options, settings, effectsettings);
            MainDisplay.Image = Image.FromFile("tempinput.jpg");
        }
    }



}
