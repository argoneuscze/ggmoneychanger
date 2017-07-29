using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MemoryManager;

namespace GGMoneyChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Regex NumRegex;
        private MemoryHandler MemoryHandler;

        public MainWindow()
        {
            NumRegex = new Regex(@"^[0-9]+$");
            MemoryHandler = new MemoryHandler();

            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string strValue = MoneyValue.Text;
            try
            {
                int value = Convert.ToInt32(strValue);
                WriteMoneyValue(value);
            }
            catch (FormatException)
            {
                Console.Out.WriteLine("Invalid value.");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !NumRegex.IsMatch(e.Text);
        }

        private void WriteMoneyValue(int value)
        {
            MemoryHandler.OpenProcess("GuiltyGearXrd.exe");
            MemoryHandler.CloseProcess();
        }
    }
}