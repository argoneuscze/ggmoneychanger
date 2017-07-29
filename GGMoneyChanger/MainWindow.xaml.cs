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
        private readonly Regex _numRegex;
        private readonly MemoryHandler _memoryHandler;

        private static IntPtr xrdPointer = new IntPtr(0x02D91228);

        public MainWindow()
        {
            _numRegex = new Regex(@"^[0-9]+$");
            _memoryHandler = new MemoryHandler();

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
            e.Handled = !_numRegex.IsMatch(e.Text);
        }

        private void WriteMoneyValue(int value)
        {
            try
            {
                _memoryHandler.OpenProcess("GuiltyGearXrd");
            }
            catch (IndexOutOfRangeException)
            {
                Console.Out.WriteLine("Process not found.");
            }
            _memoryHandler.WriteInt32(xrdPointer, value);
            _memoryHandler.CloseProcess();
        }
    }
}