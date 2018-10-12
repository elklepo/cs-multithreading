using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        private async void bCalculate_Click(object sender, RoutedEventArgs e)
        {
            bCalculate.IsEnabled = false;
            await CalculateAsync();
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
