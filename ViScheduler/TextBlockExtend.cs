using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace ViScheduler
{
    public static class TextBlockExtend
    {
        public static bool IsTextTrimmed(this TextBlock textBlock)
        {
            Typeface typeface = new Typeface(textBlock.FontFamily,
                textBlock.FontStyle,
                textBlock.FontWeight,
                textBlock.FontStretch);

            FormattedText formmatedText = new FormattedText(
                textBlock.Text,
                System.Threading.Thread.CurrentThread.CurrentCulture,
                textBlock.FlowDirection,
                typeface,
                textBlock.FontSize,
                textBlock.Foreground);
            formmatedText.Trimming = textBlock.TextTrimming;

            var fontHeight = formmatedText.Height;
            var lineCount = Math.Ceiling(formmatedText.Width/textBlock.ActualWidth);

            return formmatedText.Height*lineCount*1.5 > textBlock.ActualHeight;
        }
    }
}
