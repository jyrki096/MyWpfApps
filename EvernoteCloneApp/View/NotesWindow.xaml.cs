using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Controls.Primitives;

namespace EvernoteCloneApp.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

 

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void contentRichTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            int amountofCharacters = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd).Text.Length;

            statusTextBlock.Text = $"Document length: {amountofCharacters} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            bool isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
        }


        private async void speechRec_Click(object sender, RoutedEventArgs e)
        {
            string region = "southcentralus";
            string key = "87464598e855488dbe3f5c85b71005f2";

            var speechConfig = SpeechConfig.FromSubscription(key, region);
            using (var audioConfig = AudioConfig.FromDefaultMicrophoneInput())
            {
                using (var recognizer = new SpeechRecognizer(speechConfig, audioConfig))
                {
                    var result = await recognizer.RecognizeOnceAsync();
                    contentRichTextBox.Document.Blocks.Add(new Paragraph(new Run(result.Text)));
                }
            }
        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && selectedWeight.Equals(FontWeights.Bold);
        }
    }
}
