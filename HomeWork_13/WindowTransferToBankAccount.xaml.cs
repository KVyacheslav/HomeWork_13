using HomeWork_13.SystemBank;
using HomeWork_13.SystemBank.Clients;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для WindowTransferToBankAccount.xaml
    /// </summary>
    public partial class WindowTransferToBankAccount : Window
    {
        private MainWindow window;              // Главное окно
        private Client client;                  // Текущий клиент
        private BankAccount bankAccount;        // Текущий р/с
        private BankAccount toBankAccount;      // р/с на который будет перевод
        private decimal sum;                    // Сумма перевода

        public WindowTransferToBankAccount(MainWindow window, Client client, BankAccount bankAccount)
        {
            InitializeComponent();

            this.window = window;
            this.client = client;
            var tmpBankAccounts = new ObservableCollection<BankAccount>(client.BankAccounts);

            foreach (var ba in tmpBankAccounts)
            {
                if (ba.Number.Equals(bankAccount.Number))
                {
                    tmpBankAccounts.Remove(ba);
                    break;
                }
            }
            this.tbBankAccount.Text = bankAccount.ToString();
            this.cbBankAccounts.ItemsSource = tmpBankAccounts;
            this.cbBankAccounts.SelectedIndex = 0;
            this.bankAccount = bankAccount;
            this.slSum.DataContext = bankAccount;
            this.sum = (decimal)Math.Round(slSum.Value, 2);
            this.tbSum.Text = sum.ToString();
        }

        /// <summary>
        /// Перевод на другой р/с
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Transfer(object sender, RoutedEventArgs e)
        {
            bankAccount.Sum -= sum;
            toBankAccount.Sum += sum;

            MainWindow.Logs.Add(new Log(
                $"Клиент произвел произвел перевод внутри своих счетов на сумму {sum}"
                ));

            this.Close();
        }

        /// <summary>
        /// Задает и показывает значение суммы перевода.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slSum_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.sum = (decimal)Math.Round(slSum.Value, 2);
            this.tbSum.Text = sum.ToString();
        }

        /// <summary>
        /// Выбор р/с на который будет произведен перевод
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBankAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.toBankAccount = this.cbBankAccounts.SelectedItem as BankAccount;
            foreach (var ba in client.BankAccounts)
            {
                if (toBankAccount.Number.Equals(ba.Number))
                {
                    toBankAccount = ba;
                    break;
                }    
            }
        }

        private void WindowTransferToBankAccount_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
