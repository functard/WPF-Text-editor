using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestTextEditor
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        public ColorPicker()
        {
            InitializeComponent();
        }

        private void PickColor_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = (MainWindow)Application.Current.MainWindow;

            Button button = sender as Button;
            SolidColorBrush bg = button.Background as SolidColorBrush;
            this.Close();
            m.ChangeColor(bg);
        }

    }
}
