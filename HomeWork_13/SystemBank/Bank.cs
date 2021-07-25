using System;
using System.Collections.ObjectModel;
using HomeWork_13_new.SystemBank.Clients;

namespace HomeWork_13_new.SystemBank
{
    /// <summary>
    /// Класс, описывающий логику банка.
    /// </summary>
    public class Bank
    {
        /// <summary>
        /// Список клиентов - физические лица.
        /// </summary>
        public ObservableCollection<Individual> Individuals { get; set; }

        /// <summary>
        /// Список клиентов - юридические лица.
        /// </summary>
        public ObservableCollection<LegalEntity> LegalEntities { get; set; }




        /// <summary>
        /// Создаём банк.
        /// </summary>
        public Bank()
        {
            this.Individuals = new ObservableCollection<Individual>();
            this.LegalEntities = new ObservableCollection<LegalEntity>();
        }



        /// <summary>
        /// Добавляем клиента - физическое лицо.
        /// </summary>
        /// <param name="fullName">Полное имя (ФИО).</param>
        /// <param name="clientType">Тип клиента.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        /// <param name="open">Дата открытия счёта.</param>
        /// <param name="sum">Сумма на счету.</param>
        /// <param name="capitalization">С капитализацией?</param>
        public void AddIndividual(string fullName, ClientTypes clientType, bool isVip,
            DateTime open, decimal sum, bool capitalization)
        {
            this.Individuals.Add(
                new Individual(fullName, 
                    clientType, 
                    new BankAccount(open, sum, capitalization), 
                    isVip)
                );
        }

        /// <summary>
        /// Добавляем клиента - физическое лицо.
        /// </summary>
        /// <param name="individual"></param>
        public void AddIndividual(Individual individual)
        {
            this.Individuals.Add(individual);
        }

        /// <summary>
        /// Добавляем клиента - физическое лицо.
        /// </summary>
        /// <param name="fullName">Полное имя (ФИО).</param>
        /// <param name="clientType">Тип клиента.</param>
        /// <param name="isVip">Является ли клиент привилегированным?</param>
        /// <param name="open">Дата открытия счёта.</param>
        /// <param name="sum">Сумма на счету.</param>
        /// <param name="capitalization">С капитализацией?</param>
        public void AddLegalEntity(string fullName, ClientTypes clientType, bool isVip,
            DateTime open, decimal sum, bool capitalization)
        {
            this.LegalEntities.Add(
                new LegalEntity(fullName,
                    clientType,
                    new BankAccount(open, sum, capitalization),
                    isVip)
            );
        }

        /// <summary>
        /// Добавляем клиента - юридическое лицо.
        /// </summary>
        /// <param name="legalEntity"></param>
        public void AddLegalEntity(LegalEntity legalEntity)
        {
            this.LegalEntities.Add(legalEntity);
        }
    }
}
