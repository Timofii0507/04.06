using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _04._06
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private List<Process> processes;

        public MainWindow()
        {
            InitializeComponent();


            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();


            ProcessListView.SelectionChanged += ProcessListView_SelectionChanged;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            processes = Process.GetProcesses().ToList();
            ProcessListView.ItemsSource = processes;
        }

        private void ProcessListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessListView.SelectedItem is Process selectedProcess)
            {

                ProcessNameTextBlock.Text = selectedProcess.ProcessName;
                ProcessIdTextBlock.Text = selectedProcess.Id.ToString();
                StartTimeTextBlock.Text = selectedProcess.StartTime.ToString();
                TotalProcessorTimeTextBlock.Text = selectedProcess.TotalProcessorTime.ToString();
                ThreadsCountTextBlock.Text = selectedProcess.Threads.Count.ToString();
                InstancesCountTextBlock.Text = Process.GetProcessesByName(selectedProcess.ProcessName).Count().ToString();
            }
        }

        private void UpdateIntervalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int interval = int.Parse(UpdateIntervalTextBox.Text);
                if (interval <= 0)
                {
                    MessageBox.Show("Інтервал оновлення має бути додатнім числом.", "Помилка");
                    return;
                }
                timer.Interval = TimeSpan.FromSeconds(interval);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Неправильний формат інтервалу оновлення.", "Помилка");
            }
        }

        private void TerminateProcessButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessListView.SelectedItem is Process selectedProcess)
            {

                if (MessageBox.Show($"Ви впевнені, що хочете завершити процес '{selectedProcess.ProcessName}'?", "Підтвердження", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        selectedProcess.Kill();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не вдалося завершити процес: {ex.Message}", "Помилка");
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку виберіть процес у списку.", "Помилка");
            }
        }
    }
}