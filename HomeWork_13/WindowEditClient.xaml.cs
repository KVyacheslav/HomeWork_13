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
    /// Логика взаимодействия для WindowEditClient.xaml
    /// </summary>
    public partial class WindowEditClient : Window
    {
        private Client client;              // Текущий клиент
        private MainWindow window;          // Главное окно
        private ClientTypes firstType;      // Изначальный тип клиента
        private bool firstValueIsVip;       // Изначальное значение ВИП?

        public WindowEditClient(MainWindow window, Client client)
        {
            InitializeComponent();

            this.client = client;
            this.window = window;
            this.tbName.Text = client.FullName;
            this.firstValueIsVip = client.IsVip;
            this.firstType = client.ClientType;
            if (client.ClientType == ClientTypes.Individual)
                cbTypes.SelectedIndex = 0;
            else
                cbTypes.SelectedIndex = 1;

            this.chbIsVip.IsChecked = client.IsVip;
        }

        /// <summary>
        /// Изменить клиента.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditClient(object sender, RoutedEventArgs e)
        {
            this.client.IsVip = (bool)chbIsVip.IsChecked;

            var currentTypeStr = cbTypes.SelectedItem.ToString();
            var currentType = currentTypeStr.Equals("Физическое лицо")
                ? ClientTypes.Individual : ClientTypes.LegalEntity;

            if (currentType != this.firstType)
            {
                if (this.firstType == ClientTypes.Individual)
                {
                    this.window.Individuals.RemoveClient(client as Individual);
                    var tmp = new LegalEntity(client.FullName, currentType, client.BankAccounts, client.IsVip);
                    tmp.BankCredits = client.BankCredits;
                    this.window.LegalEntities.AddClient(tmp);
                }
                else
                {
                    this.window.LegalEntities.RemoveClient(client as LegalEntity);
                    var tmp = new Individual(client.FullName, currentType, client.BankAccounts, client.IsVip);
                    tmp.BankCredits = client.BankCredits;
                    this.window.Individuals.AddClient(tmp);
                }
            }

            if (client.IsVip != firstValueIsVip)
            {
                MainWindow.Logs.Add(new Log(
                      $"Клиенту {client.FullName} изменили привилегии на значение \"{(client.IsVip ? "Да" : "Нет")}\""));
                this.window.lvLogs.ScrollIntoView(this.window.lvLogs.Items[this.window.lvLogs.Items.Count - 1]);
            }

            window.tbIsVip.Text = "Привилегированный: " + (client.IsVip ? "Да" : "Нет");
            this.Close();
        }

        private void WindowEditClient_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
