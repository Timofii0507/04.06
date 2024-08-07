using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace system1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void StartProcessButton_Click(object sender, RoutedEventArgs e)
        {
            
            Process process = new Process();
            process.StartInfo.FileName = "notepad.exe"; 
            process.Start();

            
            await Task.Run(() => process.WaitForExit());

            
            int exitCode = process.ExitCode;

            
            ResultTextBlock.Text = $"Process exited with code: {exitCode}";
        }
    }
}