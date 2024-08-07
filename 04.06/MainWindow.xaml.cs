using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace _04._06
{
    public partial class MainWindow : Window
    {
        private Process _process;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProcessButton_Click(object sender, RoutedEventArgs e)
        {

            _process = new Process();
            _process.StartInfo.FileName = "notepad.exe";
            _process.Start();


            WaitForExitButton.IsEnabled = true;
            KillProcessButton.IsEnabled = true;
            ResultTextBlock.Text = string.Empty; 
        }

        private async void WaitForExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null)
            {

                await Task.Run(() => _process.WaitForExit());


                int exitCode = _process.ExitCode;
                ResultTextBlock.Text = $"Process exited with code: {exitCode}";


                WaitForExitButton.IsEnabled = false;
                KillProcessButton.IsEnabled = false;
            }
        }

        private void KillProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (_process != null && !_process.HasExited)
            {

                _process.Kill();
                _process.WaitForExit();


                int exitCode = _process.ExitCode;
                ResultTextBlock.Text = $"Process killed with code: {exitCode}";


                WaitForExitButton.IsEnabled = false;
                KillProcessButton.IsEnabled = false;
            }
        }
    }
}