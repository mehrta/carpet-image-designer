using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CrossPlanGenerator
{
    public class Document
    {
        // Private fields
        MemoryStream _inputImageStream;
        bool _isDocumentChanged;

        // Events
        public event EventHandler DocumentChanged;
        public event EventHandler DocumentSaved;

        // Properties
        public Image InputImage { set; get; }
        public List<Label> Labels { set; get; }
        public ushort OutputImageZoom { set; get; }
        public string FileName { private set; get; }  // Document file name (.cpg)
        public bool IsCpgDocument { set; get; }
        public bool IsDocumentChanged
        {
            set
            {
                if(_isDocumentChanged == value)
                    return;

                _isDocumentChanged = value;

                if (_isDocumentChanged)
                    DocumentChanged(null, null);
            }

            get
            {
                return _isDocumentChanged;

            }
        }

        public Document()
        {
            _inputImageStream = new MemoryStream();
            this.InputImage = null;
            this.Labels = new List<Label>(10);
            this.FileName = "";
            this.IsDocumentChanged = false;
        }

        public void LoadFromFile(string fileName)
        {
            string fileExtension;

            _inputImageStream.Position = 0;
            this.InputImage = null;
            this.Labels.Clear();

            fileExtension = Path.GetExtension(fileName).ToLower();

            if (fileExtension == ".cpg") {
                // Load Document (.cpg file)
                LoadCpgFile(fileName);
                this.IsCpgDocument = true;
                this.FileName = fileName; // document file name 
            }
            else {
                // Load an image
                FileStream fs = File.Open(fileName, FileMode.Open);
                //CopyStream(fs, fs.Length, _inputImageStream);
                fs.CopyTo(_inputImageStream);
                fs.Dispose();

                this.InputImage = Bitmap.FromStream(_inputImageStream);
                this.IsCpgDocument = false;
                this.FileName = "";
            }

            //
            this.IsDocumentChanged = false;
        }

        private void LoadCpgFile(string fileName)
        {
            using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open))) {
                int inputImageSize;
                ushort numberOfLabels;

                // ---- Read Heder of file
                // Input Image Size (Int32)
                inputImageSize = br.ReadInt32();
                // Number of labels (UInt16)
                numberOfLabels = br.ReadUInt16();
                // OutputImageZoom (UInt16)
                this.OutputImageZoom = br.ReadUInt16();

                // ---- Read Input Image
                CopyStream(br.BaseStream, inputImageSize, _inputImageStream);
                this.InputImage = new Bitmap(Image.FromStream(_inputImageStream));

                //----- Read Labels Properties and create them
                for (int i = 0; i < numberOfLabels; i++) {
                    int left, top, foreColor;
                    string text, fontFamily;
                    float fontSize;
                    bool isBold, isItalic;

                    // Read label properties
                    left = br.ReadInt32();
                    top = br.ReadInt32();
                    foreColor = br.ReadInt32();
                    text = br.ReadString();
                    fontFamily = br.ReadString();
                    fontSize = br.ReadSingle();
                    isBold = br.ReadBoolean();
                    isItalic = br.ReadBoolean();

                    // Create label
                    Label l = new Label();
                    l.AutoSize = true;
                    l.Left = left;
                    l.Top = top;
                    l.ForeColor = Color.FromArgb(foreColor);
                    l.Text = text;

                    FontStyle fontStyle;
                    if (!isBold && !isItalic)
                        fontStyle = FontStyle.Regular;
                    else if (isBold && !isItalic)
                        fontStyle = FontStyle.Bold;
                    else if (!isBold && isItalic)
                        fontStyle = FontStyle.Italic;
                    else
                        fontStyle = FontStyle.Bold | FontStyle.Italic;

                    Font font = new Font(fontFamily, fontSize, fontStyle);
                    l.Font = font;

                    this.Labels.Add(l);
                }
            }
        }

        public void SaveToFile(string fileName)
        {
            // ---- Write _inputImageStream to a MemoryStream (Format: PNG)
            if (_inputImageStream.Length > Int32.MaxValue)
                throw new Exception("Image Too Big.");

            // Writing data to files
            using (BinaryWriter bw = new BinaryWriter(File.Open(fileName, FileMode.Create))) {

                // ---- Write Heder of file
                // Size of input image (Int32)
                bw.Write((int)_inputImageStream.Length);
                // Number of labels (UShort)
                bw.Write((ushort)((Labels == null) ? 0 : Labels.Count));
                // OutputImageZoom (UShort)
                bw.Write(this.OutputImageZoom);
           
                // ---- Write Input Image
                //bw.Write(_inputImageStream.ToArray());
                _inputImageStream.Position = 0;
                _inputImageStream.CopyTo(bw.BaseStream);

                //----- Write Labels Properties
                if (this.Labels != null)
                    foreach (Label l in Labels) {
                        bw.Write((l.Tag as LabelManager.LabelTag).Left);
                        bw.Write((l.Tag as LabelManager.LabelTag).Top);
                        bw.Write(l.ForeColor.ToArgb());
                        bw.Write(l.Text);
                        bw.Write(l.Font.FontFamily.Name);
                        bw.Write((l.Tag as LabelManager.LabelTag).FontSize);
                        bw.Write(l.Font.Bold);
                        bw.Write(l.Font.Italic);
                    }
            }

            //
            this.FileName = fileName;
            this.IsDocumentChanged = false;

            // Fire DocumentSaved() event
            DocumentSaved(null, null);
        }

        private static void CopyStream(Stream source, long count, Stream destination)
        {
            const int BUFFER_SIZE = 1024 * 4; // 4 KB

            byte[] buffer = new byte[BUFFER_SIZE];
            long writenBytes = 0; // Bytes that are writen so far
            int readBytes;

            do {
                readBytes = source.Read(buffer, 0, (int)Math.Min(BUFFER_SIZE, count - writenBytes));

                writenBytes += readBytes;
                destination.Write(buffer, 0, readBytes);
            } while ((writenBytes < count) && (readBytes > 0));
        }
    }
}
