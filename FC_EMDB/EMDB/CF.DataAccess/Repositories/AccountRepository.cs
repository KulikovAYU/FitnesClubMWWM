using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.Data.Repositories;
using FC_EMDB.EMDB.CF.DataAccess.Context;
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
            //return DataBaseFcContext.Accounts.FirstOrDefault(account => account.NumberSubscription == nNumberSubscription);
            return null;
        }

        /// <summary>
        /// Получить аккаунты, записанные на конкретную тренировку
        /// </summary>
        /// <param name="nTrainingId">Id тренировки</param>
        /// <returns> Коллекция аккаунтов, записанных на конкретную тренировку</returns>
        public IEnumerable<Account> GetAccountsWritingOnTraining(int nTrainingId)
        {
            return null;
        }

        /// <summary>
        /// Получить аккаунты с истекшими абонементами
        /// </summary>
        /// <returns>Коллекция аккаунтов с истекшими абонементами</returns>
        public IEnumerable<Account> GetAccountsWichHasExpiredSubscription()
        {

            return null;
            //var query= DataBaseFcContext.Accounts.Where(acc => acc.TrainingCount == 0).ToList();

            //return query;
        }

        public Account FindAccountWithSameData(Account clientData)
        {
            var query = DataBaseFcContext.Accounts.FirstOrDefault(acc => ((acc.HumanFirstName == clientData.HumanFirstName)
                                                                          &&
                                                                          (acc.HumanLastName == clientData.HumanLastName)
                                                                          &&
                                                                          (acc.HumanFamilyName == clientData.HumanFamilyName)
                                                                          &&
                                                                          (acc.HumanPasportDataSeries == clientData.HumanPasportDataSeries)
                                                                          &&
                                                                          (acc.HumanPasportDataNumber == clientData.HumanPasportDataNumber)
                ));
            if (query != null && !query.bIsExistPerson)
            {
                query.StrPathPhoto = SqlTools.SavePhoto(query);
            }

            return query;
        }


        /// <summary>
        /// Найти аккаунт с определенными данными
        /// </summary>
        /// <param name="clientData"> Данные по которым будет производиться выборка</param>
        /// <returns></returns>
        //public NewClientData FindAccountWithSameData(NewClientData clientData)
        //{
        //    //var query = DataBaseFcContext.Accounts.FirstOrDefault(acc => acc.ClientFirstName == clientData.PersonFirstName && acc.ClientLastName ==
        //    //                                                                                                      clientData.PersonLastName
        //    //                                                                                                               && acc.ClientFamilyName ==
        //    //                                                                                                      clientData.PersonFamilyName &&
        //    //                                                                                                      acc.ClientDateOfBirdth ==
        //    //                                                                                                               clientData.PersonDateOfBirdth);

        //var query = DataBaseFcContext.Accounts.FirstOrDefault(acc => ((acc.HumanFirstName == clientData.PersonFirstName)
        //                                                                  &&
        //                                                                 (acc.HumanLastName == clientData.PersonLastName)
        //                                                                  &&
        //                                                                  (acc.HumanFamilyName == clientData.PersonFamilyName)
        //                                                                  &&
        //                                                                  (acc.HumanPasportDataSeries == clientData.ClientPasportDataSeries)
        //                                                                  &&
        //                                                                  (acc.HumanPasportDataNumber == clientData.ClientPasportDataNumber)
        //                                                                  ));


        //    return query != null
        //        ? new NewClientData(true)
        //        {
        //            PersonRole = "Клиент",
        //            PersonFirstName = query.HumanFirstName,
        //            PersonLastName =  query.HumanLastName,
        //            PersonFamilyName = query.HumanFamilyName,
        //            PersonDateOfBirdth = query.HumanDateOfBirdth,
        //            PersonGender = query.HumanGender,
        //            PersonId = query.HumanId,
        //            PersonAdress = query.HumanAdress,
        //            PersonPhoneNumber = query.HumanPhoneNumber,
        //            PersonMail = query.HumanMail,
        //            PersonPhoto =query.HumanPhoto,
        //            ClientPasportDataSeries = query.HumanPasportDataSeries,
        //            ClientPasportDataNumber = query.HumanPasportDataNumber,
        //            ClientPasportDataIssuedBy = query.HumanPasportDataIssuedBy,
        //            ClientPasportDatеOfIssue = query.HumanPasportDatеOfIssue,
        //            NumberSubscription = query.Abonement.NumberSubscription

        //        } : null;
        //}

        public void UpdateFields(Account clientData)
        {
            Account _clientData = null;
            if (clientData.HumanId == 0)
            {
                _clientData = DataBaseFcContext.Accounts.Find(clientData.HumanId);
                if (_clientData == null)
                    return;
            }
            else
            {
                _clientData = clientData;
            }
        

         
            SqlTools.UpdatePhoto(ref _clientData, clientData.StrPathPhoto);
            DataBaseFcContext.Accounts.AddOrUpdate(_clientData);
            DataBaseFcContext.SaveChanges();
        }

        /// <summary>
        /// Создать запись клиента в БД
        /// </summary>
        /// <param name="data">Регистрационные данные клиента</param>
        public void CreateRecord(Account data)
        {
            //проверим еще раз, что такой записи нет
            var clientData = DataBaseFcContext.Accounts.Find(data.HumanId);

            if (clientData == null)
            {
                //создадим статус
                var statuses = DataBaseFcContext.AbonementStatuses.FirstOrDefault(stat => stat.StatusName == "Не активен");
                //создадим абонемент
                data.Abonement = new Abonement(){ AbonmentStatus = statuses };//создаем абонемент
                data.HumanPhoto = SqlTools.ConvertImageToByteArray(data.StrPathPhoto);
            }
            DataBaseFcContext.Accounts.AddOrUpdate(data);
            DataBaseFcContext.SaveChanges();
        }

        public void AppentRecordToExistAccount(Account acc)
        {
            DataBaseFcContext.Accounts.AddOrUpdate(acc);
            DataBaseFcContext.SaveChanges();
        }

        public void AddTraining(Account account)
        {
         var res =   DataBaseFcContext.Accounts.Find(account.HumanId);

            //res.ArrServicesInSubscription.Add(account.ArrServicesInSubscription.First());
            //DataBaseFcContext.SaveChanges();
        }

        public DataBaseFcContext DataBaseFcContext => m_context as DataBaseFcContext;
    }
}
