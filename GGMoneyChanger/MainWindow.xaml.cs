using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using MemoryInjector;

namespace GGMoneyChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Regex numRegex;
        private readonly MemoryHandler memoryHandler;

        private static IntPtr xrdPointer = new IntPtr(0x1AD1228);

        public MainWindow()
        {
            numRegex = new Regex(@"^[0-9]+$");
            memoryHandler = new MemoryHandler();

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
            e.Handled = !numRegex.IsMatch(e.Text);
        }

        private void WriteMoneyValue(int value)
        {
            memoryHandler.OpenProcess("GuiltyGearXrd.exe");
            memoryHandler.CloseProcess();
        }
    }
}