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
            string firstNumber = FirstNumberTextBox.Text;
            string secondNumber = SecondNumberTextBox.Text;
            string operation = OperationTextBox.Text;

            if (string.IsNullOrWhiteSpace(firstNumber) || string.IsNullOrWhiteSpace(secondNumber) || string.IsNullOrWhiteSpace(operation))
            {
                ResultTextBlock.Text = "Please enter valid inputs.";
                return;
            }

            Process process = new Process();
            process.StartInfo.FileName = "ChildProcess.exe";
            process.StartInfo.Arguments = $"{firstNumber} {secondNumber} {operation}";
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