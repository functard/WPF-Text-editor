using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestTextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load font size combo box
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            cmbFontSize.Text = "12";

            //load font family combo box
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontFamily.Text = "Segoe UI";
        }


        private Document document = new Document();

        #region ---Clicks----
        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            NewFile();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveAs();
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
        private void LeftAllignment_Click(object sender, RoutedEventArgs e)
        {
            BlockCollection MyBC = rtbBody.Document.Blocks;
            foreach (Block b in MyBC)
                b.TextAlignment = TextAlignment.Left;
        }

        private void CenterAllignment_Click(object sender, RoutedEventArgs e)
        {
            BlockCollection MyBC = rtbBody.Document.Blocks;
            foreach (Block b in MyBC)
                b.TextAlignment = TextAlignment.Center;

        }

        private void RightAllignment_Click(object sender, RoutedEventArgs e)
        {
            BlockCollection MyBC = rtbBody.Document.Blocks;
            foreach (Block b in MyBC)
                b.TextAlignment = TextAlignment.Right;
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectionTextRange = new TextRange(rtbBody.Selection.Start, rtbBody.Selection.End);

            FontWeight selectionFontWeigth;

            // helps "Unable to cast object of type 'MS.Internal.NamedObject" which has no impact
            // to do : find a better way
            try
            {
                selectionFontWeigth = (FontWeight)selectionTextRange.GetPropertyValue(Inline.FontWeightProperty);
            }
            catch
            {

            }


            if (selectionFontWeigth != FontWeights.Bold)
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.FontWeight = FontWeights.Bold;

                        newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }
                    // selection is not empty
                    else
                    {
                        // change the font weigth of the selection
                        rtbBody.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
                    }
            }
            else
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.FontWeight = FontWeights.Normal;

                        newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }
                // selection is not empty
                else
                {
                    // change the font weigth of the selection
                    rtbBody.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
                }
            }
            rtbBody.Focus();
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectionTextRange = new TextRange(rtbBody.Selection.Start, rtbBody.Selection.End);
            FontStyle selectionFontStyle;

            // helps "Unable to cast object of type 'MS.Internal.NamedObject" which has no impact
            // to do : find a better way
            try
            {
                selectionFontStyle = (FontStyle)selectionTextRange.GetPropertyValue(Inline.FontStyleProperty); 
            }
            catch
            {

            }


            if (selectionFontStyle != FontStyles.Italic)
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.FontStyle = FontStyles.Italic;

                        newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }
                // selection is not empty
                else
                {
                    // change the font style of selection
                    rtbBody.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
                }
            }
            else
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.FontStyle = FontStyles.Normal;

                        newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }

                // selection is not empty
                else
                {
                    // change the font style of selection
                    rtbBody.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
                }
            }
            rtbBody.Focus();

        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            TextRange selectionTextRange = new TextRange(rtbBody.Selection.Start, rtbBody.Selection.End);
            TextDecorationCollection selectionDecoration = new TextDecorationCollection();


            // helps "Unable to cast object of type 'MS.Internal.NamedObject" which has no impact
            // to do : find a better way
            try
            {
                selectionDecoration = (TextDecorationCollection)selectionTextRange.GetPropertyValue(Inline.TextDecorationsProperty);
            }
            catch
            {

            }


            if (selectionDecoration != TextDecorations.Underline)
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.TextDecorations = TextDecorations.Underline;

                        newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }
                // selection is not empty
                else
                {
                    // change the decoration of the selection
                    rtbBody.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                }
            }
            else
            {
                if (rtbBody.Selection.IsEmpty)
                {
                    // Get the  block that surronds the carret 
                    Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                    if (curBlock != null)
                    {
                        Paragraph curParagraph = curBlock as Paragraph;
                        // Create a new run object with the fontsize, and add it to the current block
                        Run newRun = new Run();
                        // if text doesn't contains elements with different size 
                        if (cmbFontSize.Text != "-")
                            newRun.FontSize = double.Parse(cmbFontSize.Text);

                        newRun.TextDecorations = null;

                        newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                        newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                        newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
                        newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                        //add the run object to current block
                        curParagraph.Inlines.Add(newRun);

                        // Reset the cursor into the new block.
                        rtbBody.CaretPosition = newRun.ElementStart;
                    }
                }
                // selection is not empty
                else
                {
                    // change the decoration of the selection
                    rtbBody.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
                }
            }
            rtbBody.Focus();
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            ColorPicker cp = new ColorPicker();

            // prevents the window from stealing the focus
            cp.ShowActivated = false;
            
            cp.Show();
            
        }


        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            rtbBody.Undo();
        }

        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            rtbBody.Redo();
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            rtbBody.SelectAll();
        }

        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            rtbBody.Copy();
        }

        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            rtbBody.Paste();
            rtbBody.Background = Brushes.Red;
        }

        #endregion

        private void FontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rtbBody.Selection.IsEmpty)
            {
                // Get the  block that surronds the carret 
                Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                if (curBlock != null)
                {
                    Paragraph curParagraph = curBlock as Paragraph;
                    // Create a new run object with the fontsize, and add it to the current block
                    Run newRun = new Run();

                    FontFamilyConverter f = new FontFamilyConverter();
                    FontFamily ft = (FontFamily)f.ConvertFromString(cmbFontFamily.SelectedItem.ToString());

                    newRun.FontFamily = ft;

                    if (cmbFontSize.Text != "-")
                        newRun.FontSize = double.Parse(cmbFontSize.Text);


                    newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                    newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                    newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                    newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);

                    //add the run object to current block
                    curParagraph.Inlines.Add(newRun);

                    // Reset the cursor into the new block.
                    rtbBody.CaretPosition = newRun.ElementStart;
                }
            }
            else
            {
                if (cmbFontFamily.SelectedItem != null)
                    rtbBody.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, cmbFontFamily.SelectedItem);

            }
            rtbBody.Focus();
        }

        private void FontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(rtbBody.Selection.IsEmpty)
            {
                // Get the  block that surronds the carret 
                Block curBlock = rtbBody.Document.Blocks.Where(x => x.ContentStart.CompareTo(rtbBody.CaretPosition) == -1 && x.ContentEnd.CompareTo(rtbBody.CaretPosition) == 1).FirstOrDefault();

                if (curBlock != null)
                {
                    Paragraph curParagraph = curBlock as Paragraph;
                    // Create a new run object with the fontsize, and add it to the current block
                    Run newRun = new Run();
                    // if text doesn't contains elements with different size 
                    if(cmbFontSize.Text != "-")
                        newRun.FontSize = double.Parse(cmbFontSize.Text);

                    newRun.FontWeight = (FontWeight)rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);
                    newRun.Foreground = (Brush)rtbBody.Selection.GetPropertyValue(Inline.ForegroundProperty);
                    newRun.TextDecorations = (TextDecorationCollection)rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
                    newRun.FontStyle = (FontStyle)rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
                    newRun.FontFamily = (FontFamily)rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
                    //add the run object to current block
                    curParagraph.Inlines.Add(newRun);

                    // Reset the cursor into the new block.
                    rtbBody.CaretPosition = newRun.ElementStart;
                }
            }
            else
            {
                TextRange selectionTextRange = new TextRange(rtbBody.Selection.Start, rtbBody.Selection.End);
                if (selectionTextRange.GetPropertyValue(Inline.FontSizeProperty) != DependencyProperty.UnsetValue)
                    selectionTextRange.ApplyPropertyValue(Inline.FontSizeProperty, cmbFontSize.Text);

            }
            rtbBody.Focus();
        }

        

        // updates the buttons according the font style of the selected text
        private void Font_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //get the selection
            object selection = rtbBody.Selection.GetPropertyValue(Inline.FontWeightProperty);

            //update if selected text not null and bold
            btnBold.IsChecked = (selection != DependencyProperty.UnsetValue) && (selection.Equals(FontWeights.Bold));


            //update if selected text not null and italic
            selection = rtbBody.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (selection != DependencyProperty.UnsetValue) && (selection.Equals(FontStyles.Italic));

            //update if selected text not null and underlined
            selection = rtbBody.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            if (selection == null)
                btnUnderline.IsChecked = false;
            else
                btnUnderline.IsChecked = (selection != DependencyProperty.UnsetValue) && (selection.Equals(TextDecorations.Underline));

            //update fontfamily combobox
            selection = rtbBody.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            cmbFontFamily.SelectedItem = selection;

            // update the fontsize text
            selection = rtbBody.Selection.GetPropertyValue(Inline.FontSizeProperty);
            if (rtbBody.Selection.GetPropertyValue(Inline.FontSizeProperty) == DependencyProperty.UnsetValue)
                cmbFontSize.Text = "-";
            else
                cmbFontSize.Text = selection.ToString();
        }

        #region --- My Functions ----

        private void Save()
        {
            // if a path already exists
            if (document.HasPath)
            {
                // save to existing path
                var range = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd);
                File.WriteAllText(document.FilePath, range.Text);
            }
            else
                SaveAs();
        }

        private void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                TextRange range = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd);
                range.Save(fileStream, DataFormats.Rtf);
            }
        }

        private void OpenFile()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Rtf);
            }
        }

        private void NewFile()
        {
            var range = new TextRange(rtbBody.Document.ContentStart, rtbBody.Document.ContentEnd);

            // if text box is empty
            if (!string.IsNullOrEmpty(range.Text))
            {
                MessageBoxResult mb = MessageBox.Show("Do you want to save your file first?", "Delete Confirmation", MessageBoxButton.YesNo);

                if (mb == MessageBoxResult.Yes)
                {
                    Save();
                    ClearDocument();
                }
                else
                    ClearDocument();
            }
        }

        private void ClearDocument()
        {
            rtbBody.FontStyle = FontStyles.Normal;
            rtbBody.Document.Blocks.Clear();
            document.FilePath = null;
            cmbFontSize.Text = "12";
            cmbFontFamily.Text = "Segoe UI";
        }
        public void ChangeColor(SolidColorBrush _brush)
        {
            rtbBody.Selection.ApplyPropertyValue(Inline.ForegroundProperty, _brush);
        }

    }
    #endregion
}
