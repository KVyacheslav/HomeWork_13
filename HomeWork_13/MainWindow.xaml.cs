using System;
using System.Collections;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using HomeWork_13.SystemBank;
using HomeWork_13.SystemBank.Clients;

namespace HomeWork_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientTypes clientType;         // Используемый тип
        private Random rnd;


        public BankClients<Individual> Individuals { get; set; }    // Список клиентов - физических лиц.
        public BankClients<LegalEntity> LegalEntities { get; set; }   // Список клиентов - юридических лиц.
        public static ObservableCollection<Log> Logs { get; set; }
        public static DateTime Date { get; set; }                // Текущая дата

        public MainWindow()
        {
            Date = DateTime.Now;
            Logs = new ObservableCollection<Log>();
            this.Individuals = new BankClients<Individual>();
            this.LegalEntities = new BankClients<LegalEntity>();
            this.rnd = new Random();
            this.GenerateClients();

            InitializeComponent();

            this.tbDate.Text = $"Текущая дата: {Date:dd.MM.yyyy}";
            this.lvLogs.ItemsSource = Logs;
            this.lvLogs.ScrollIntoView(this.lvLogs.Items[this.lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// При изменении выбора списка клиентов.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var index = this.cbClients.SelectedIndex;
            // Присваиваем используемый тип.
            this.clientType = index == 0 
                ? ClientTypes.Individual 
                : ClientTypes.LegalEntity;

            lvClients.ItemsSource = index == 0
                ? (IEnumerable) this.Individuals.Clients
                :  this.LegalEntities.Clients;

            this.lvClients.ScrollIntoView(lvClients.Items[0]);

            this.tbName.Text = string.Empty;
            this.tbIsVip.Text = string.Empty;
            this.tbCountBankAcc.DataContext = lvClients.SelectedItem as Client;
            this.tbCountBankCredits.DataContext = lvClients.SelectedItem as Client;
            this.lvBankAccounts.Visibility = Visibility.Hidden;
            this.lvBankCredits.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Генерация клиентов.
        /// </summary>
        private void GenerateClients()
        {
            for (int i = 0; i < 150; i++)
            {
                var name = GetRandomFullName();
                var type = rnd.Next(2) == 0
                    ? ClientTypes.Individual
                    : ClientTypes.LegalEntity;
                var isVip = rnd.Next(5) == 0;
                var bankAccount = GetGenerateBankAccount();

                Client client;



                if (type == ClientTypes.Individual)
                {
                    client = new Individual(name, type, bankAccount, isVip);
                    this.Individuals.AddClient(client as Individual);
                }
                else
                {
                    client = new LegalEntity(name, type, bankAccount, isVip);
                    this.LegalEntities.AddClient(client as LegalEntity);
                }

                if (rnd.Next(5) == 0)
                    client.AddBankAccount(GetGenerateBankAccount());

                if (rnd.Next(3) != 2)
                    client.AddBankCredit(GetGenerateBankCredit(client, out decimal sum), sum);
            }
        }

        /// <summary>
        /// Сгенерировать клиенту кредит.
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private BankCredit GetGenerateBankCredit(Client client, out decimal sum)
        {
            sum = rnd.Next(500, (int)client.BankAccounts[0].Sum);
            var credit = sum + sum * (decimal)(client.IsVip ? 0.2 : 0.3);
            Logs.Add(new Log($"Клиент {client.FullName} взял кредит на сумму {sum} от {Date:dd.MM.yyyy}."));
            return new BankCredit(credit, Date, 3, client.BankAccounts[0], sum);
        }

        /// <summary>
        /// Сгенерировать расчётные счета.
        /// </summary>
        /// <param name="count">Количество счетов.</param>
        /// <returns></returns>
        private BankAccount GetGenerateBankAccount()
        {
            return new BankAccount(
                DateTime.Now, 
                rnd.Next(1000, 10001), 
                rnd.Next(2) == 0);
        }

        /// <summary>
        /// Сгенерировать полное имя.
        /// </summary>
        /// <returns>Полное имя.</returns>
        private string GetRandomFullName()
        {
            var data = @"Бичурин Алексей Платонович
Царёва Ева Якововна
Бочарова Оксана Виталиевна
Грефа Софья Филипповна
Гусева Роза Мефодиевна
Дёмшина Арина Елизаровна
Архипов Артем Ираклиевич
Цветкова Людмила Павеловна
Ямзин Леонид Филимонович
Плюхина Нина Емельяновна
Григорьева Инна Василиевна
Анисимова Полина Борисовна
Лешев Виктор Богданович
Бессуднов Станислав Евстафиевич
Топоров Анатолий Самсонович
Васильева Владлена Серафимовна
Бикулов Аскольд Капитонович
Головкина Алина Федоровна
Насонова Лада Мироновна
Островерха Ульяна Станиславовна
Шамякин Терентий Тихонович
Кидирбаева Валентина Анатолиевна
Булыгина Диана Никитевна
Беломестнов Фока Никанорович
Гайдученко Тимофей Зиновиевич
Казьмин Агафон Семенович
Соломахина Юлия Михеевна
Хорошилова Ярослава Романовна
Волынкина Валерия Леонидовна
Садовничий Алиса Петровна
Чичерин Кондратий Титович
Рыкова Зинаида Олеговна
Крутой Наталия Брониславовна
Есипов Герман Касьянович
Лачков Аркадий Назарович
Яикбаева Инга Фомевна
Семенов Иосиф Кондратиевич
Курушин Прокл Валериевич
Денисова Анна Кузьмевна
Рыжанов Богдан Моисеевич
Канадина Светлана Данииловна
Никаева Изольда Юлиевна
Кочинян Никон Феликсович
Бурмакина Элеонора Георгиевна
Висенина Ульяна Владиленовна
Валиев Вениамин Яковович
Ярилов Зиновий Епифанович
Гибазов Эдуард Сергеевич
Клокова Антонина Серафимовна
Волобуева Раиса Семеновна
Бабышев Гавриил Феликсович
Задков Филипп Миронович
Варфоломеева Варвара Феликсовна
Селиванов Герман Карлович
Томсин Аскольд Эрнестович
Енотова Евгения Юлиевна
Мандрыкин Владислав Богданович
Голубцов Аскольд Давидович
Рыжов Прокл Всеволодович
Кораблин Иннокентий Наумович
Черенчикова Светлана Несторовна
Арсеньева Римма Виталиевна
Громыко Лука Елизарович
Архаткин Леонид Евграфович
Дубинина Арина Леонидовна
Дуркина Надежда Фомевна
Шкиряк Аким Ипполитович
Солдатов Петр Вячеславович
Иванников Ефрем Григориевич
Липова Пелагея Казимировна
Янкин Модест Ираклиевич
Машлыкин Станислав Евгениевич
Погребной Прохор Сигизмундович
Кетов Лавр Иосифович
Степихова Мирослава Казимировна
Кучава Всеволод Касьянович
Кустов Вадим Назарович
Борзилов Макар Миронович
Блатова Светлана Олеговна
Лапотников Семён Мартьянович
Аронова Клара Никитевна
Кудяшова Розалия Никитевна
Киприянов Антип Вячеславович
Ягунова Дарья Геннадиевна
Ручкина Варвара Юлиевна
Малинина Ярослава Ростиславовна
Завражный Кондратий Эмилевич
Крымов Андрон Матвеевич
Голубов Тимур Андриянович
Клоков Нестор Кондратиевич
Гоминова Роза Евгениевна
Петухов Ефрем Савелиевич
Вьялицына Виктория Несторовна
Игнатенко Эвелина Иосифовна
Фернандес Аким Савелиевич
Блатова Эвелина Якововна
Любимцев Ярослав Мирославович
Уголева ﻿Агата Петровна
Саянов Виталий Адрианович
Якунова Зоя Леонидовна
Дорохова ﻿Агата Германовна
Журавлёв Евгений Игоревич
Цветков Игнатий Наумович
Дагина Эвелина Мироновна
Гика Алла Яновна
Дубровский Роман Александрович
Касатый Агафья Иларионовна
Березовский Артём Игнатиевич
Чекмарёв Никита Куприянович
Смотров Георгий Демьянович
Кошелева Элеонора Антониновна
Калашников Борислав Кондратиевич
Травкина Ангелина Леонидовна
Кочубей Роза Александровна
Шурдукова Антонина Родионовна
Голованова Полина Всеволодовна
Карчагина Каролина Святославовна
Золотухин Михей Гордеевич
Прокашева Анисья Павеловна
Кулешов Роман Георгиевич
Воронцов Яков Моисеевич
Рунов Марк Ульянович
Солодский Елизар Адамович
Васенин Фока Ерофеевич
Кидина Роза Данииловна
Кологреев Валерий Андреевич
Козариса Василиса Тимуровна
Тупицын Вацлав Святославович
Жариков Петр Модестович
Якубович Платон Иосифович
Нуряев Владилен Миронович
Бебчука Виктория Тимофеевна
Шамякин Гавриил Мартьянович
Елешев Аким Ираклиевич
Лагошина Каролина Яновна
Кантонистов Николай Куприянович
Мусин Михей Анатолиевич
Ямковой Анфиса Данииловна
Ажищенкова Инга Тимуровна
Окрокверцхов Иннокентий Яковович
Зууфина Ника Виталиевна
Янин Алексей Кондратович
Мацовкин Филипп Эдуардович
Камбарова Лада Марковна
Тимофеева Софья Мефодиевна
Зёмина Кристина Андрияновна
Сагунова Яна Яновна
Распутина Мария Геннадиевна
Стегнова Рада Трофимовна
Фанина Жанна Родионовна
Мосякова Инга Иосифовна
Шамякин Артемий Маркович
Драгомирова ﻿Агата Ефимовна
Ельченко Валерий Пахомович
Добролюбов Порфирий Севастьянович
Кругликова Елена Ростиславовна
Бабышев Осип Богданович
Дудника Ангелина Евгениевна
Бондарчука Агния Трофимовна
Кобелева Таисия Данииловна
Сапалёва Всеслава Игнатиевна
Дуболазов Всеволод Титович
Яшвили Агап Евграфович
Коллерова Анисья Василиевна
Палюлин Юрий Сигизмундович
Цыгвинцев Дмитрий Филимонович
Большаков Трофим Демьянович
Яндарбиева Софья Алексеевна
Валуев Лаврентий Адамович
Колотушкина Наталья Вячеславовна
Бруевича Жанна Казимировна
Масмеха Кира Несторовна
Меншикова Кира Василиевна
Шаршин Мстислав Сократович
Малафеев Харитон Кириллович
Завражина Виктория Брониславовна
Заболотный Самуил Семенович
Яскунов Фадей Макарович
Зимин Виссарион Григориевич
Крестьянинов Евгений Давидович
Земляков Клавдий Ростиславович
Соловьёв Андриян Прохорович
Якурин Илья Гаврилевич
Мещерякова Алина Кузьмевна
Катаева Бронислава Ираклиевна
Грязнов ﻿Август Матвеевич
Никулина Доминика Карповна
Марьин Геннадий Капитонович
Зюлёва Инга Игоревна
Лобана Ярослава Несторовна
Счастливцева Василиса Всеволодовна
Набоко Егор Капитонович
Аничков Всеслав Капитонович
Костюк Венедикт Святославович
Синдеева Ираида Яновна
Москвитина Ефросинья Феликсовна
Арданкина Оксана Мироновна
Казанцева Анастасия Игоревна
Якутин Виталий Брониславович
Безрукова Альбина Владленовна"
                .Split('\n');
            return data[rnd.Next(data.Length)];
        }

        /// <summary>
        /// При выборе клиента из списка...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LvClients_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client client;

            if (this.clientType == ClientTypes.Individual) 
                client = lvClients.SelectedItem as Individual;
            else
                client = lvClients.SelectedItem as LegalEntity;

            if (client != null)
            {
                this.tbName.Text = client.FullName;
                this.tbIsVip.Text = client.IsVip ? "Да" : "Нет";
                this.tbCountBankAcc.DataContext = client;
                this.tbCountBankCredits.DataContext = client;
                this.lvBankAccounts.ItemsSource = client.BankAccounts;
                this.lvBankCredits.ItemsSource = client.BankCredits;
                this.lvBankAccounts.Visibility = Visibility.Visible;
                this.lvBankCredits.Visibility = Visibility.Visible;
                this.tbName.Visibility = Visibility.Visible;
                this.tbIsVip.Visibility = Visibility.Visible;
            }
            else
            {
                this.tbCountBankAcc.DataContext = null;
                this.tbCountBankCredits.DataContext = null;
            }
        }

        /// <summary>
        /// Следующий месяц.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextMonth(object sender, RoutedEventArgs e)
        {
            Date = Date.AddMonths(1);
            this.tbDate.Text = $"Текущая дата: {Date:dd.MM.yyyy}";
            this.Individuals.CheckBalanceClients(Date);
            this.LegalEntities.CheckBalanceClients(Date);
        }

        /// <summary>
        /// Вывести окно добавления клиента.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowWindowAddClient(object sender, RoutedEventArgs e)
        {
            new WindowAddClient(this).ShowDialog();
            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Удаляем клиента.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveClient(object sender, RoutedEventArgs e)
        {
            if (clientType == ClientTypes.Individual)
            {
                var client = lvClients.SelectedItem as Individual;
                Individuals.RemoveClient(client);
            }
            else
            {
                var client = lvClients.SelectedItem as LegalEntity;
                LegalEntities.RemoveClient(client);
            }

            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
            tbName.Visibility = Visibility.Hidden;
            tbIsVip.Visibility = Visibility.Hidden;
            lvBankAccounts.Visibility = Visibility.Hidden;
            lvBankCredits.Visibility = Visibility.Hidden;

        }

        /// <summary>
        /// Добавить расчётный счёт.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBankAccount(object sender, RoutedEventArgs e)
        {
            Client client = lvClients.SelectedItem as Client;
            new WindowAddBankAccount(this, client).ShowDialog();
            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Удалить расчётный счёт
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveBankAccount(object sender, RoutedEventArgs e)
        {
            BankAccount bankAccount = lvBankAccounts.SelectedItem as BankAccount;

            if (bankAccount == null)
                return;

            Client client = lvClients.SelectedItem as Client;
            client.RemoveBankAccount(bankAccount);
            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Показать окно редактирования клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowWindowEditClient(object sender, RoutedEventArgs e)
        {
            var client = lvClients.SelectedItem as Client;

            if (client == null)
                return;

            new WindowEditClient(this, client).ShowDialog();
        }

        /// <summary>
        /// Добавить расчётный счёт.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBankCredit(object sender, RoutedEventArgs e)
        {
            Client client = lvClients.SelectedItem as Client;

            if (client == null)
                return;

            if (client.BankCredits.Count == 1)
            {
                MessageBox.Show("Нельзя одновременно открывать более 1 кредита.", "Ошибка");
                return;
            }

            var maxSum = client.BankAccounts.ToList().Max(ba => ba.Sum);

            if (maxSum <= 500)
            {
                MessageBox.Show("Невозможно взять кредит при максимальном\nбалансе 500 или менее.", "Ошибка");
                return;
            }


            new WindowAddBankCredit(this, client).ShowDialog();
            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);

        }

        /// <summary>
        /// При изменении размера логов, скролл показывает последний лог.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvLogs_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Перевод между своими счетами.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferToBankAccount(object sender, RoutedEventArgs e)
        {
            var client = lvClients.SelectedItem as Client;
            var bankAccount = lvBankAccounts.SelectedItem as BankAccount;

            if (bankAccount == null)
                return;

            if (lvBankAccounts.Items.Count <= 1)
                return;

            new WindowTransferToBankAccount(this, client, bankAccount).ShowDialog();

            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }
        
        /// <summary>
        /// Пополнение расчётного счёта.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PutBankAccount(object sender, RoutedEventArgs e)
        {
            Client client = lvClients.SelectedItem as Client;
            BankAccount bankAccount = lvBankAccounts.SelectedItem as BankAccount;
            
            if (bankAccount == null)
                return;

            new WindowPutClient(this, client, bankAccount).ShowDialog();

            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Перевод другому клиенту на р/с
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransferToClientBankAccount(object sender, RoutedEventArgs e)
        {
            if (this.lvClients.Items.Count <= 1)
                return;

            var client = lvClients.SelectedItem as Client;
            
            new WindowTransferToClient(this, client).ShowDialog();

            lvLogs.ScrollIntoView(lvLogs.Items[lvLogs.Items.Count - 1]);
        }

        /// <summary>
        /// Закрыть окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Перемещение окна 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
