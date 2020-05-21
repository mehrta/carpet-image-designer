using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace CrossPlanGenerator
{
    class SettingsManager
    {
        private readonly string FILE_NAME;
        private ImageAnalyzer _imgAnalyzer;
        private LabelManager _lblManager;

        public SettingsManager(ImageAnalyzer imgAnalyzer, LabelManager lblManager)
        {
            _imgAnalyzer = imgAnalyzer;
            _lblManager = lblManager;

            FILE_NAME = System.Windows.Forms.Application.StartupPath + "\\CrossPlanGenerator.config";
        }

        public void LoadSettings()
        {
            StreamReader sr = null;

            try {
                sr = new StreamReader(FILE_NAME);

                // -------- Load ImageAnalyzer Properties

                // ImageAnalyzer.BorderSize
                _imgAnalyzer.BorderSize = Int32.Parse(sr.ReadLine());

                // ImageAnalyzer.HighlightRegions
                _imgAnalyzer.HighlightRegions = ((sr.ReadLine() == "True") ? true : false);

                // ImageAnalyzer.Grid.MainGridColor1
                _imgAnalyzer.Grid.MainGridColor1 = Color.FromArgb(Int32.Parse(sr.ReadLine()));

                // ImageAnalyzer.Grid.MainGridColor2
                _imgAnalyzer.Grid.MainGridColor2 = Color.FromArgb(Int32.Parse(sr.ReadLine()));

                // ImageAnalyzer.Grid.MainGridSize
                _imgAnalyzer.Grid.MainGridSize = Int32.Parse(sr.ReadLine());

                // ImageAnalyzer.Grid.ShowMainGrid
                _imgAnalyzer.Grid.ShowMainGrid = ((sr.ReadLine() == "True") ? true : false);

                // ImageAnalyzer.Grid.ShowSubGrid
                _imgAnalyzer.Grid.ShowSubGrid = ((sr.ReadLine() == "True") ? true : false);

                // ImageAnalyzer.Grid.SubGridCorrectiveValue
                _imgAnalyzer.Grid.SubGridCorrectiveValue = Int32.Parse(sr.ReadLine());


                // -------- Load LabelManager Properties

                // LabelManager.DarkBackgroundColorUpperBound
                _lblManager.DarkBackgroundColorUpperBound = Color.FromArgb(Int32.Parse(sr.ReadLine()));

                // LabelManager.DefaultLabelFont
                string fontFamily;
                float fontSize;
                FontStyle fontStyle;

                fontFamily = sr.ReadLine();
                fontSize = float.Parse(sr.ReadLine());
                fontStyle = ((sr.ReadLine() == "True") ? FontStyle.Bold : FontStyle.Regular);
                _lblManager.DefaultLabelFont = new Font(fontFamily, fontSize, fontStyle);

                // LabelManager.DefaultLabelForeColor
                _lblManager.DefaultLabelForeColor = Color.FromArgb(Int32.Parse(sr.ReadLine()));

                // LabelManager.DefaultLabelForeColorDark
                _lblManager.DefaultLabelForeColorDark = Color.FromArgb(Int32.Parse(sr.ReadLine()));

                // LabelManager.InvertLabelColorOfDarkRegions
                _lblManager.InvertLabelColorOfDarkRegions = ((sr.ReadLine() == "True") ? true : false);
            }
            catch {
                // Do nothing.
            }
            finally {
                if (sr != null)
                    sr.Close();
            }

        }

        public void SaveSettings()
        {
            StreamWriter sw = null;

            try {
                sw = new StreamWriter(FILE_NAME);

                // -------- Save ImageAnalyzer Properties

                // ImageAnalyzer.BorderSize
                sw.WriteLine(_imgAnalyzer.BorderSize);

                // ImageAnalyzer.HighlightRegions
                sw.WriteLine(_imgAnalyzer.HighlightRegions);

                // ImageAnalyzer.Grid.MainGridColor1
                sw.WriteLine(_imgAnalyzer.Grid.MainGridColor1.ToArgb());

                // ImageAnalyzer.Grid.MainGridColor2
                sw.WriteLine(_imgAnalyzer.Grid.MainGridColor2.ToArgb());

                // ImageAnalyzer.Grid.MainGridSize
                sw.WriteLine(_imgAnalyzer.Grid.MainGridSize);

                // ImageAnalyzer.Grid.ShowMainGrid
                sw.WriteLine(_imgAnalyzer.Grid.ShowMainGrid.ToString());

                // ImageAnalyzer.Grid.ShowSubGrid
                sw.WriteLine(_imgAnalyzer.Grid.ShowSubGrid.ToString());

                // ImageAnalyzer.Grid.SubGridCorrectiveValue
                sw.WriteLine(_imgAnalyzer.Grid.SubGridCorrectiveValue);


                // -------- Save LabelManager Properties

                // LabelManager.DarkBackgroundColorUpperBound
                sw.WriteLine(_lblManager.DarkBackgroundColorUpperBound.ToArgb());

                // LabelManager.DefaultLabelFont.FontFamily.Name
                sw.WriteLine(_lblManager.DefaultLabelFont.FontFamily.Name);

                // LabelManager.DefaultLabelFont.SizeInPoints
                sw.WriteLine(_lblManager.DefaultLabelFont.SizeInPoints);

                // LabelManager.DefaultLabelFont.Bold
                sw.WriteLine(_lblManager.DefaultLabelFont.Bold);

                // LabelManager.DefaultLabelForeColor
                sw.WriteLine(_lblManager.DefaultLabelForeColor.ToArgb());

                // LabelManager.DefaultLabelForeColorDark
                sw.WriteLine(_lblManager.DefaultLabelForeColorDark.ToArgb());

                // LabelManager.InvertLabelColorOfDarkRegions
                sw.WriteLine(_lblManager.InvertLabelColorOfDarkRegions);
            }
            catch { }
            finally {
                if (sw != null)
                    sw.Close();
            }
        }
    }
}
