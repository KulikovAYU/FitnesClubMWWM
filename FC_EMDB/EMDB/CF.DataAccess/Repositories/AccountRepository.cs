using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;
using FC_EMDB.Interfaces;
using FC_EMDB.Utils;

namespace FC_EMDB.EMDB.CF.DataAccess.Repositories
{
    /// <summary>
    /// Этот класс не допилен
    /// </summary>
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DataBaseFcContext context) : base(context)
        {
        }

        /// <summary>
        /// Получить аккаунт по номеру абонемента
        /// </summary>
        /// <param name="nNumberSubscription">Номер абонемента</param>
        /// <returns>Аккаунт</returns>
        public Account GetAccountForNumberSubscription(int nNumberSubscription)
        {
            //var queryGetAccountForNumberSubscription = from account in DataBaseFcContext.Accounts
            //    where account.NumberSubscription == nNumberSubscription
            //    select account;
            var query = DataBaseFcContext.Accounts.FirstOrDefault(account => account.NumberSubscription == nNumberSubscription);

            return query;
        }

        /// <summary>
        /// Получить аккаунты, записанные на конкретную тренировку
        /// </summary>
        /// <param name="nTrainingId">Id тренировки</param>
        /// <returns> Коллекция аккаунтов, записанных на конкретную тренировку</returns>
        public IEnumerable<Account> GetAccountsWritingOnTraining(int nTrainingId)
        {
            var query = DataBaseFcContext.Trainings.Where(training => training.TrainingId == nTrainingId).Select(training=> training.ArrAccounts).FirstOrDefault();

            //var queryGetAccountsWritingOnTraining = from training in DataBaseFcContext.Trainings
            //    where training.TrainingId == nTrainingId
            //    select training.ArrAccounts.FirstOrDefault();
            return query;
        }

        /// <summary>
        /// Получить аккаунты с истекшими абонементами
        /// </summary>
        /// <returns>Коллекция аккаунтов с истекшими абонементами</returns>
        public IEnumerable<Account> GetAccountsWichHasExpiredSubscription()
        {
            var query= DataBaseFcContext.Accounts.Where(acc => acc.TrainingCount == 0).ToList();
            //var queryGetAccountsWichHasExpiredSubscription = from account in DataBaseFcContext.Accounts
            //    where account.TrainingCount == 0
            //    select account;
            return query;
        }

        public Account FindAccountWithSameData(Account clientData)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Найти аккаунт с определенными данными
        /// </summary>
        /// <param name="clientData"> Данные по которым будет производиться выборка</param>
        /// <returns></returns>
        public NewClientData FindAccountWithSameData(NewClientData clientData)
        {
            var query = DataBaseFcContext.Accounts.FirstOrDefault(acc => acc.ClientFirstName == clientData.PersonFirstName && acc.ClientLastName ==
                                                                                                                  clientData.PersonLastName
                                                                                                                           && acc.ClientFamilyName ==
                                                                                                                  clientData.PersonFamilyName &&
                                                                                                                  acc.ClientDateOfBirdth ==
                                                                                                                           clientData.PersonDateOfBirdth);
            return query != null
                ? new NewClientData(true)
                {
                    PersonRole = "Клиент",
                    PersonFirstName = query.ClientFirstName,
                    PersonLastName =  query.ClientLastName,
                    PersonFamilyName = query.ClientFamilyName,
                    PersonDateOfBirdth = query.ClientDateOfBirdth,
                    PersonGender = query.ClientGender,
                    PersonId = query.ClientId,
                    PersonAdress = query.ClientAdress,
                    PersonPhoneNumber = query.ClientPhoneNumber,
                    PersonMail = query.ClientMail,
                    PersonPhoto =query.ClientPhoto,
                    ClientPasportDataSeries = query.ClientPasportDataSeries,
                    ClientPasportDataNumber = query.ClientPasportDataNumber,
                    ClientPasportDataIssuedBy = query.ClientPasportDataIssuedBy,
                    ClientPasportDatеOfIssue = query.ClientPasportDatеOfIssue

                } : null;
        }

        public void UpdateFields(NewClientData clientData)
        {
           var _clientData = DataBaseFcContext.Accounts.Find(clientData.PersonId);

            if (_clientData == null)
                return;

            SqlTools.Convert(ref _clientData, clientData);
            DataBaseFcContext.Accounts.AddOrUpdate(_clientData);
            DataBaseFcContext.SaveChanges();
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
