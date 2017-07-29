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

        private static readonly IntPtr XrdPointer1 = new IntPtr(0x1AD1228);
        private static readonly IntPtr XrdPointer2 = new IntPtr(0x1BD7310);

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
                MessageBox.Show("Invalid value.");
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
                MessageBox.Show("The game isn't running.");
                return;
            }
            _memoryHandler.WriteInt32Ptr(XrdPointer1, value);
            _memoryHandler.WriteInt32Ptr(XrdPointer2, value);
            _memoryHandler.CloseProcess();
            MessageBox.Show("Successfully changed money.");
        }
    }
}