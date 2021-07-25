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
    /// Логика взаимодействия для WindowAddClient.xaml
    /// </summary>
    public partial class WindowAddClient : Window
    {
        private MainWindow window;          // Главное окно
        private decimal balance;            // Баланс р/с

        public WindowAddClient(MainWindow window)
        {
            InitializeComponent();

            this.window = window;
        }

        /// <summary>
        /// Задает баланс при изменении положения ползунка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void slBalance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.balance = (decimal)Math.Round(slBalance.Value, 2);
            this.tbBalance.Text = this.balance.ToString();
        }

        /// <summary>
        /// Добавляем клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddClient(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbName.Text))
            {
                MessageBox.Show("Поле ФИО не может быть пустым!", "Ошибка");
                return;
            }

            var name = this.tbName.Text;
            var type = this.cbTypes.SelectedItem.ToString().Equals("Физическое лицо")
                ? ClientTypes.Individual : ClientTypes.LegalEntity;
            var isVip = (bool)this.chbIsVip.IsChecked;
            var capitalization = (bool)this.chbCapitalization.IsChecked;
            var date = MainWindow.Date;
            BankAccount bankAccount = new BankAccount(date, this.balance, capitalization);

            if (type == ClientTypes.Individual)
            {
                var client = new Individual(name, type, bankAccount, isVip);
                this.window.Individuals.AddClient(client);
            }
            else
            {
                var client = new LegalEntity(name, type, bankAccount, isVip);
                this.window.LegalEntities.AddClient(client);
            }

            this.Close();
        }

        private void WindowAddClient_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
