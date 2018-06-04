using System;
using System.Collections.ObjectModel;
using FC_EMDB;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL.Annotations;
using FitnesClubCL.Classes;
using GalaSoft.MvvmLight;

namespace FitnesClubCL
{
    /// <summary>
    /// Класс представляет интерфейс для работы с моделью данных
    /// </summary>
    public sealed class ModelManager : ViewModelBase, ISystemUser
    {
    
       private ModelManager()
       {
         
        }

       private static ModelManager _mManager;

        public static ModelManager GetInstance => _mManager ?? (_mManager = new ModelManager());

        public void CreateDB()
       {
           DbManager.GetInstance().InitializeDatabase();
       }


        #region Методы и поля, связанные с пользователями информационной системы 
        /// <summary>
        /// Аутентификация пользоватля в системе
        /// </summary>
        /// <param name="datAutorizationUserData">Данные пользователя для аутентификации</param>
        public void Autontefication([CanBeNull] AutorizationUserData datAutorizationUserData)
        {
            if (string.IsNullOrEmpty(datAutorizationUserData?.UserLoginName) ||
                string.IsNullOrEmpty(datAutorizationUserData.PasswordString))
            {
                return;
            }

            AutorizationUserData = datAutorizationUserData;
            //При авторизации также получим данные пользователя
            PasswordController.GetSystemUserData(AutorizationUserData, out Employee userData);
            //Вернем данные пользователя
            WorkingUserData = userData;
        }

        /// <summary>
        /// Указатель на данные пользователя
        /// </summary>
        [CanBeNull] public AutorizationUserData AutorizationUserData { get; private set; }

        /// <summary>
        /// Экземпляр данных работника
        /// </summary>
        [CanBeNull] public Employee WorkingUserData { get; private set; }
        #endregion


    

        /// <summary>
        /// Проверить запись на существование
        /// </summary>
        /// <typeparam name="T">Шаблон параметра</typeparam>
        /// <param name="recordData">данные записи</param>
        public T CheckRecord<T>(T recordData) where T : class
        {
            if (recordData == null)
                return null;
            return ClientsHelper.CheckRecord<T>(recordData);
        }

        /// <summary>
        /// Метод обновляет запись либо создает новую запись в БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientData"></param>
        public void CreateOrUpdateRecord<T>(T clientData) where T : class
        {
            if (clientData == null)
                return;
            
            ClientsHelper.UpdateFields<T>(clientData);
        }


        public Account FindPersonForNumberSubsription(int numberSubscription) 
        {
            if (string.IsNullOrEmpty(numberSubscription.ToString()) )
            {
                return null;
            }

           return ClientsHelper.FindPersonForNumberSubsription(numberSubscription);
        }

        /// <summary>
        /// Предварительно зарегистрировать трениртовку
        /// </summary>
        /// <param name="currentItem">Предварительная запись на тренировку</param>
        public void CreatePriorRegistration(Account account,ServicesInSubscription service,UpcomingTraining currentItem)
        {
            ClientsHelper.CreatePriorRegistration(account,service, currentItem);
        }


        /// <summary>
        /// Метод возвращает коллекцию людей (работников, клиентов...)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>коллекция данных</returns>
        public static ObservableCollection<T> GetAllPersons<T>() where T : class
        {
            if (typeof(T) == typeof(Account)) //если запросили коллекцию работников
            {
              return  ClientsHelper.GetAllClients<T>();
            }

            return null;
        }


        /// <summary>
        /// Метод предоставляет спрачоные данны из бд
        /// </summary>
        /// <typeparam name="Template">тип сущности</typeparam>
        /// <returns>коллекцию сущноситей конкретного типа</returns>
        public static ObservableCollection<Template> GetReferenceData<Template>() where Template : class 
        {
            return Assistiant.GetReferenceData<Template>();
        }

        public static void SaveData<T>(T data) where T : class
        {
            Assistiant.SaveData<T>(data);
        }

        public static void AddData<T1, T2>(T1 data1, T2 data2) where T1 : class where T2 : class
        {
            Assistiant.AddData<T1, T2>(data1, data2);
        }

        /// <summary>
        /// Получить дотупные для пользователя расписание занятий по конкретной тренировке
        /// </summary>
        /// <param name="servicesInSubscription">доступные услуги</param>
        public ObservableCollection<UpcomingTraining> GetAvailableTrainings(ServicesInSubscription servicesInSubscription)
        {
           return Assistiant.GetAvailableTrainings(servicesInSubscription);
        }

        /// <summary>
        /// Проверить тренировку на возможность записи
        /// </summary>
        /// <param name="account">аккаунт</param>
        /// <param name="item">выбранная тренировка</param>
        /// <returns></returns>
        public bool CheckTrainingOnAvailable(Account account, UpcomingTraining item)
        {
            if (item == null || account == null) return false;
            return Assistiant.CheckTrainingOnAvailable(account, item);
        }

        /// <summary>
        /// Возвратить доступные тренировки в абонементе
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns>Коллекция  тренировок в абонементе </returns>
        public ObservableCollection<ServicesInSubscription> GetServicesInSubscription(Account account)
        {
            if (account?.Abonement != null)
                return new ObservableCollection<ServicesInSubscription>(account?.Abonement
                    ?.ArrServicesInSubscription);
            return null;
        }

        /// <summary>
        /// Возвратить те тренировки, на которые записан человек
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns>Коллекция  тренировок, на которые записан человек </returns>
        public ObservableCollection<UpcomingTraining> GetUpcomingTraining(Account account)
        {
            if (account?.Abonement != null)
                return new ObservableCollection<UpcomingTraining>(account?.Abonement
                    ?.ArrUpcomingTrainings);

            return null;
        }

        /// <summary>
        /// Звафиксировыать посещение тренировки
        /// </summary>
        /// <param name="account">Учетная запись</param>
        /// <param name="upcomingTraining">Предстоящая тренировка</param>
        public void FixTheVisit(Account account, UpcomingTraining upcomingTraining)
        {
            if (account == null || upcomingTraining== null)
            {
                return;
            }

            ClientsHelper.FixTheVisit(account, upcomingTraining);
        }

        /// <summary>
        /// Отказ о посещении тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="upcTraining">предстоящая тренировка</param>
        public void RefusalOfVisit(Account account, UpcomingTraining upcTraining)
        {
            if (account == null || upcTraining == null)
            {
                return;
            }

            ClientsHelper.RefusalOfVisit(account, upcTraining);

        }

        /// <summary>
        /// Обновить 
        /// </summary>
        /// <param name="accountAbonement"></param>
        public void ActivateAbonement(Abonement accountAbonement)
        {
            if (accountAbonement == null)
            {
                return;
            }
            accountAbonement.AbonmentStatus.StatusName = "Активен";

            CreateOrUpdateRecord(accountAbonement);
        }

        /// <summary>
        /// Продлить абонемент
        /// </summary>
        /// <param name="accountAbonement"></param>
        /// <param name="timeToLong"></param>
        public void LongAbonement(Abonement accountAbonement, DateTime timeToLong)
        {
            if (accountAbonement == null)
            {
                return;
            }

            accountAbonement.TimeToLong = timeToLong;
            CreateOrUpdateRecord(accountAbonement);
        }

        public void FreezeAbonement(Abonement accountAbonement)
        {
            if (accountAbonement == null)
            {
                return;
            }
            accountAbonement.AbonmentStatus.StatusName = "Заморожен";

            CreateOrUpdateRecord(accountAbonement);
        }

        /// <summary>
        /// Получить цены и виды тренировок
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<PriceTrainingList> GetPriceTrainingList()
        {
            return Assistiant.GetPriceTrainingList();
        }

        /// <summary>
        /// Получить список тарифов
        /// </summary>
        /// <returns></returns>
        public new ObservableCollection<Tarif> GetTarifs()
        {
            return Assistiant.GetTarifs();
        }
    }
}
