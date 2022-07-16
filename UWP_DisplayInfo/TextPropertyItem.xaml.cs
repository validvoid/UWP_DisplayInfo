using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace UWP_DisplayInfo
{
    public sealed partial class TextPropertyItem : UserControl
    {
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof(string), typeof(TextPropertyItem), new PropertyMetadata(default(string)));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(TextPropertyItem), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                CopyButton.IsEnabled = !(string.IsNullOrWhiteSpace(Text) || Text == "-");
            }
        }

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
            "Glyph", typeof(string), typeof(TextPropertyItem), new PropertyMetadata(default(string)));

        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public TextPropertyItem()
        {
            this.InitializeComponent();
        }

        private void OnClickCopy(object sender, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(Text) && Text != "-")
            {
                DataPackage dp = new DataPackage();
                dp.SetText(Text);

                Clipboard.SetContent(dp);
            }
        }
    }
}
