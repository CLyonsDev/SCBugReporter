using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SC_Bug_Reporter
{
    /// <summary>
    /// Interaction logic for FinishedPopup.xaml
    /// </summary>
    public partial class FinishedPopup : Window
    {
        public string path;
        public string filePath;
        public MainWindow program;

        public FinishedPopup()
        {
            InitializeComponent();
            PopupCloseButton.Focus();
        }

        private void OnClick_CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
            program.ResetBoxes();
        }

        private void OnClick_ViewDirectory(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(path);
        }

        private void OnClick_ViewNewLog(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(filePath);
        }
    }
}
