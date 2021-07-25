using System;
using System.Collections.Generic;
using System.Globalization;
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
using HomeWork_13.SystemBank;
using HomeWork_13.SystemBank.Clients;

namespace HomeWork_13
{
    /// <summary>
    /// Логика взаимодействия для WindowPutClient.xaml
    /// </summary>
    public partial class WindowPutClient : Window
    {
        private MainWindow window;          // Главное окно
        private Client client;              // Текущий клиент
        private BankAccount bankAccount;    // Текущий р/с
        private decimal amount;             // Сумма пополнения р/с

        public WindowPutClient(MainWindow window, Client client, BankAccount bankAccount)
        {
            InitializeComponent();

            this.window = window;
            this.client = client;
            this.bankAccount = bankAccount;
            this.amount = (decimal) Math.Round(slAmount.Value, 2);
            this.tbAmount.Text = Convert.ToString(amount, CultureInfo.InvariantCulture);
            this.tbBankAccount.Text = bankAccount.ToString();

        }

        /// <summary>
        /// Добавить в баланс р/с сумму.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPutBalance_OnClick(object sender, RoutedEventArgs e)
        {
            this.bankAccount.Sum += amount;
            MainWindow.Logs.Add(new Log(
                $"Клиент {client.FullName} пополнил баланс р/с на сумму {amount}."
                ));

            this.Close();
        }

        /// <summary>
        /// Задает значение и отображает на форме сумму пополнения.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SlAmount_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.amount = (decimal)Math.Round(slAmount.Value, 2);
            this.tbAmount.Text = Convert.ToString(amount, CultureInfo.InvariantCulture);
        }

        private void WindowPutClient_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
