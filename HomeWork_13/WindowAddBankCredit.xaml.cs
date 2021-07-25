using HomeWork_13.SystemBank;
using HomeWork_13.SystemBank.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HomeWork_13
{
    /// <summary>
    /// Логика взаимодействия для WindowAddBankCredit.xaml
    /// </summary>
    public partial class WindowAddBankCredit : Window
    {
        private decimal sum;            // Сумма пополнения р/с при взятии кредита
        private Client client;          // Текущий клиент
        private MainWindow window;      // Главное окно
        private int countYears;         // Срок кредита
        private decimal percent;        // Процент кредита

        public WindowAddBankCredit(MainWindow window, Client client)
        {
            InitializeComponent();
            cbPercent.IsEnabled = false;

            this.window = window;
            this.client = client;
            if (client.IsVip)
            {
                cbPercent.IsEnabled = true;
                cbPercent.SelectedIndex = 1;
            }

            cbBankAccounts.ItemsSource = client.BankAccounts;
            cbBankAccounts.SelectedIndex = 0;
            slBalance.DataContext = client.BankAccounts[cbBankAccounts.SelectedIndex];

            sum = (decimal)Math.Round(slBalance.Value, 2);
            tbBalance.Text = sum.ToString();

        }

        /// <summary>
        /// Добавить кредит клиенту.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBankCredit(object sender, RoutedEventArgs e)
        {
            var credit = sum + sum * percent;
            var bankAccount = cbBankAccounts.SelectedItem as BankAccount;
            client.AddBankCredit(new BankCredit(credit, MainWindow.Date, countYears, bankAccount, sum), sum);

            this.Close();
        }

        /// <summary>
        /// Присваивание максимального значения, равной сумме выбранного р/с
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBankAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            slBalance.DataContext = client.BankAccounts[cbBankAccounts.SelectedIndex];
        }

        /// <summary>
        /// Отображение значения суммы.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.sum = (decimal)Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = sum.ToString();
        }

        /// <summary>
        /// Задает значение переменной "Срок кредита"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTimeCredit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(cbTimeCredit.SelectedIndex)
            {
                case 0:
                    countYears = 1;
                    break;
                case 1:
                    countYears = 3;
                    break;
                default:
                    countYears = 5;
                    break;
            }
        }

        /// <summary>
        /// Задает значение процента кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbPercent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            percent = cbPercent.SelectedIndex == 0 ? 0.3m : 0.2m;
        }

        private void WindowAddBankCredit_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
