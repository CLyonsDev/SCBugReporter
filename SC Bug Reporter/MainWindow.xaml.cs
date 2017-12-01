using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SC_Bug_Reporter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        string logInputStr;

        string exeDir;
        string subDir;

        public MainWindow()
        {
            InitializeComponent();
            LogInputTextBox.Focus();

            exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            subDir = System.IO.Path.Combine(exeDir, "Logs");

            if (!System.IO.Directory.Exists(subDir))
            {
                // Dir does not exist. Create it.
                System.IO.Directory.CreateDirectory(subDir);
            }
        }

        private void KeyClicked(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return)
            {
                string time = System.DateTime.UtcNow.ToShortTimeString();

                string timeFormatted = String.Format("[{0} UTC] ", time);

                logInputStr = LogInputTextBox.Text;

                string inputStrFinal = timeFormatted + logInputStr;

                if (!string.IsNullOrWhiteSpace(logInputStr))
                {
                    LogPreviewTextBox.Text += inputStrFinal + "\n";
                    LogPreviewTextBox.ScrollToEnd();
                }
                LogInputTextBox.Text = "";
                e.Handled = true;
            }
        }

        public void ResetBoxes()
        {
            LogInputTextBox.Text = "";
            LogPreviewTextBox.ScrollToHome();
            LogPreviewTextBox.Text = "";
            LogInputTextBox.Focus();
        }

        private void OnClick_ExportLog(object sender, RoutedEventArgs e)
        {
            Point coords = this.PointToScreen(new Point(0, 0));
            Console.WriteLine(coords.ToString());

            string[] output = LogPreviewTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (output.Length == 0)
                return;

            string outputName = string.Format("SCLOG - {0}.txt", System.DateTime.Now.ToString("ss_mm_hh_dd_MM_yyyy"));

            string finalPath = System.IO.Path.Combine(subDir, outputName);
 
            using(StreamWriter sw = new StreamWriter(finalPath))
            {
                //sw.WriteLine("Hello World!");
                for (int i = 0; i < output.Length; i++)
                {
                    sw.WriteLine(output[i]);
                }
            }

            Console.WriteLine(finalPath);
            
            ResetBoxes();

            FinishedPopup popup = new FinishedPopup();
            popup.path = subDir;
            popup.filePath = finalPath;
            popup.program = this;
            popup.Left = coords.X + (this.Width / 2) - (popup.Width / 2);
            popup.Top = coords.Y + (this.Height / 2) - (popup.Height / 2);
            popup.Show();          
        }
    }
}
