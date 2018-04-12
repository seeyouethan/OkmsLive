using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OkmsLive.HelpersLib
{
    static class GetTextDisplayWidthHelper
    {
        public static Double GetTextDisplayWidth(Label label)
        {
            return GetTextDisplayWidth(label.Content.ToString(), label.FontFamily, label.FontStyle, label.FontWeight, label.FontStretch, label.FontSize);
        }

        public static Double GetTextDisplayWidth(string str, FontFamily fontFamily, FontStyle fontStyle, FontWeight fontWeight, FontStretch fontStretch, double FontSize)
        {
            var formattedText = new FormattedText(
                                str,
                                CultureInfo.CurrentUICulture,
                                FlowDirection.LeftToRight,
                                new Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                                FontSize,
                                Brushes.Black
                                );
            Size size = new Size(formattedText.Width, formattedText.Height);
            return size.Width;
        }
    }
}
