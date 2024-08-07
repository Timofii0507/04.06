using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace _04._06
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProcessButton_Click(object sender, RoutedEventArgs e)
        {
            string filePath = FilePathTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(searchWord))
            {
                ResultTextBlock.Text = "Please enter valid inputs.";
                return;
            }


            Process process = new Process();
            process.StartInfo.FileName = "ChildProcess.exe";
            process.StartInfo.Arguments = $"{filePath} {searchWord}";
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;


            process.Start();


            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();


            ResultTextBlock.Text = result;
        }
    }
}