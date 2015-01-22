using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lvr_Land_Maker.Extension
{
    public static class RickTextBoxExtension
    {
        public static void AppendText(this RichTextBox richTextBox, string message, Color txtColor = default(Color))
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = txtColor == default(Color) ? richTextBox.ForeColor : txtColor;
            richTextBox.AppendText(message);
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }

        public static void Text(this RichTextBox richTextBox, string message, Color txtColor = default(Color))
        {
            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;

            richTextBox.SelectionColor = txtColor == default(Color) ? richTextBox.ForeColor : txtColor;
            richTextBox.Text = message;
            richTextBox.SelectionColor = richTextBox.ForeColor;
        }

        /// <summary>
        /// Async
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="message"></param>
        /// <param name="txtColor"></param>
        public static void AppendTextAsync(this RichTextBox richTextBox, string message, Color txtColor = default(Color))
        {
            richTextBox.Invoke(new Action(() =>
            {
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.SelectionLength = 0;

                richTextBox.SelectionColor = txtColor == default(Color) ? richTextBox.ForeColor : txtColor;
                richTextBox.AppendText(message);
                richTextBox.SelectionColor = richTextBox.ForeColor;
            }));
        }

        /// <summary>
        /// Async
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="message"></param>
        /// <param name="txtColor"></param>
        public static void TextAsync(this RichTextBox richTextBox, string message, Color txtColor = default(Color))
        {
            richTextBox.Invoke(new Action(() =>
            {
                richTextBox.SelectionStart = richTextBox.TextLength;
                richTextBox.SelectionLength = 0;

                richTextBox.SelectionColor = txtColor == default(Color) ? richTextBox.ForeColor : txtColor;
                richTextBox.Text = message;
                richTextBox.SelectionColor = richTextBox.ForeColor;
            }));
        }
    }
}
