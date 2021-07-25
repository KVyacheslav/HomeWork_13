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
    /// Логика взаимодействия для WindowAddBankAccount.xaml
    /// </summary>
    public partial class WindowAddBankAccount : Window
    {
        private MainWindow window;
        private Client client;
        private decimal balance;

        public WindowAddBankAccount(MainWindow window, Client client)
        {
            InitializeComponent();

            this.window = window;
            this.client = client;
            this.balance = (decimal) Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = balance.ToString();
        }

        /// <summary>
        /// Добавить расчётный счёт.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBankAccount(object sender, RoutedEventArgs e)
        {
            client.AddBankAccount(
                new BankAccount(MainWindow.Date, balance, (bool)chbCapitalization.IsChecked)
                );

            this.Close();
        }

        /// <summary>
        /// Отображать значение в форме при изменении.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.balance = (decimal)Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = balance.ToString();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void WindowAddBankAccount_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
