using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ButtonAndProgressBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bCalculate_Click(object sender, RoutedEventArgs e)
        {
            bCalculate.IsEnabled = false;
            Calculate();
            bCalculate.IsEnabled = true;
        }

        private void Calculate()
        {
            for (int i = 0; i <= 100; i++)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(20));

                progressBar.Dispatcher.Invoke(
                    () => progressBar.Value = i
                    );
            }
        }
        private Task CalculateAsync()
        {
            return Task.Run(() => Calculate());
        }

    }
}
