using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microsoft.Win32;
using System.Windows.Forms;

namespace CrossPlanGenerator
{
    public static class StaticMethods
    {
        //public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        //{
        //    //Taxes: Remote Desktop Connection and painting
        //    //hhttp://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
        //    if (System.Windows.Forms.SystemInformation.TerminalServerSession)
        //        return;

        //    System.Reflection.PropertyInfo aProp =
        //          typeof(System.Windows.Forms.Control).GetProperty(
        //                "DoubleBuffered",
        //                System.Reflection.BindingFlags.NonPublic |
        //                System.Reflection.BindingFlags.Instance);

        //    aProp.SetValue(c, true, null);
        //}

        public static string GetFontString(Font f)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append(f.FontFamily.Name);
            sb.Append(", ");
            sb.Append(f.SizeInPoints);
            sb.Append("pt");
            sb.Append(", ");
            sb.Append(f.Style.ToString());

            return sb.ToString();
        }

        public static DialogResult ShowDocumentNotSavedAlert()
        {
            FormMessageBox msg = new FormMessageBox();
            msg.MessageType = FormMessageBox.MessageTypeEnum.YesNo;
            msg.Message = "آیا مایل به ذخیره تغییرات می باشید؟";
            return msg.ShowDialog();
        }

        public static void ShowErrorMessage(string errorMessage)
        {
            FormMessageBox msg = new FormMessageBox();
            msg.MessageType = FormMessageBox.MessageTypeEnum.Error;
            msg.Message = errorMessage;
            msg.ShowDialog();
        }

        public static void ShowMessage(string message)
        {
            FormMessageBox msg = new FormMessageBox();
            msg.MessageType = FormMessageBox.MessageTypeEnum.Alert;
            msg.Message = message;
            msg.ShowDialog();
        }

        public static void ShowErrorMessage(string errorMessage, Exception e)
        {
            FormMessageBox msg = new FormMessageBox();
            msg.MessageType = FormMessageBox.MessageTypeEnum.Error;

            // Create error message text
            StringBuilder errMsg = new StringBuilder();
            errMsg.AppendLine(errorMessage);
            errMsg.AppendLine();
            errMsg.AppendLine("جزئیات فنی خطا:");
            errMsg.Append(e.Message);
            msg.Message = errMsg.ToString();

            msg.ShowDialog();
        }

        public static void ShowImageProccessingFailedErrorMessage(Exception e)
        {
            const string PROCCESSING_OUT_OF_MEMORY_EXCEPTION = "به علت کمبود حافظه، امکان ایجاد تصویر خروجی وجود ندارد." +
                "لطفا از یک فایل ورودی با ابعاد کوچکتر استفاده کنید یا برنامه را روی کامپیوتری باحافظه RAM بیشتر نصب کنید.";
            const string IMAGE_PROCCESSING_FAILED = "هنگام پردازش تصویر خطایی رخ داد.";

            if (e is OutOfMemoryException)
                ShowErrorMessage(PROCCESSING_OUT_OF_MEMORY_EXCEPTION, e);
            else
                ShowErrorMessage(IMAGE_PROCCESSING_FAILED, e);
        }

        public static Color ColorFromHsv(float h, float s, float v)
        {
            int i;
            float f, p, q, t, r, g, b;

            if (s == 0) {
                // achromatic (grey)
                return Color.FromArgb((int)(v * 255), (int)(v * 255), (int)(v * 255));
            }

            h /= 60;			// sector 0 to 5
            i = (int)(h);
            f = h - i;			// factorial part of h
            p = v * (1 - s);
            q = v * (1 - s * f);
            t = v * (1 - s * (1 - f));
            switch (i) {
                case 0:
                    r = v;
                    g = t;
                    b = p;
                    break;
                case 1:
                    r = q;
                    g = v;
                    b = p;
                    break;
                case 2:
                    r = p;
                    g = v;
                    b = t;
                    break;
                case 3:
                    r = p;
                    g = q;
                    b = v;
                    break;
                case 4:
                    r = t;
                    g = p;
                    b = v;
                    break;
                default:		// case 5:
                    r = v;
                    g = p;
                    b = q;
                    break;
            }

            Color clr;
            clr = Color.FromArgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));

            return clr;
        }

        public static Color InvertColor(Color color)
        {

            return ColorFromHsv(
                ((int)color.GetHue() + 180) % 360,
                1.0f,
                1.0f);

            //const int GRAY = 127;
            //const int DELTA = 30;
            //const int GRAY_UPPER_BOUND = GRAY + DELTA;
            //const int GRAY_LOWER_BOUND = GRAY - DELTA;
            //int r, g, b;

            //// R
            //if (color.R <= GRAY_LOWER_BOUND)
            //    r = 255 - color.R;
            //else if (color.R <= GRAY)
            //    r = GRAY_UPPER_BOUND;
            //else if (color.R <= GRAY_UPPER_BOUND)
            //    r = GRAY_LOWER_BOUND;
            //else
            //    r = 255 - color.R;


            //// G
            //if (color.G <= GRAY_LOWER_BOUND)
            //    g = 255 - color.G;
            //else if (color.G <= GRAY)
            //    g = GRAY_UPPER_BOUND;
            //else if (color.G <= GRAY_UPPER_BOUND)
            //    g = GRAY_LOWER_BOUND;
            //else
            //    g = 255 - color.G;

            //// B
            //if (color.B <= GRAY_LOWER_BOUND)
            //    b = 255 - color.B;
            //else if (color.B <= GRAY)
            //    b = GRAY_UPPER_BOUND;
            //else if (color.B <= GRAY_UPPER_BOUND)
            //    b = GRAY_LOWER_BOUND;
            //else
            //    b = 255 - color.B;


            //return Color.FromArgb(r, g, b);
        }

    }
}
