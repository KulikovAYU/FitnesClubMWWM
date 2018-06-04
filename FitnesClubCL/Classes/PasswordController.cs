using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using FC_EMDB;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FitnesClubCL.Annotations;

namespace FitnesClubCL.Classes
{
    /// <summary>
    /// класс для управления авторизацией пользователя в инфосистеме
    /// </summary>
    static class PasswordController
    {

        /// <summary>
        /// Метод генерирует хэш пароля
        /// </summary>
        /// <param name="password">строка(хэш) пароля</param>
        private static string GeneratePasswordHash(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
              return  GetMd5Hash(md5Hash, password);
            }
        }

        /// <summary>
        /// Преобразование хэша пароля в строку
        /// </summary>
        /// <param name="md5Hash">Ссылка на экземпляр класса MD5 </param>
        /// <param name="input">Имя пользователя</param>
        /// <returns></returns>
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {
            if (input == null)
                return null;

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
       
        /// <summary>
        /// Проверка хэш пароля
        /// </summary>
        /// <param name="md5Hash"></param>
        /// <param name="input"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        private static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            // Hash the input.
            string hashOfInput = GetMd5Hash(md5Hash, input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Метод подгружает данные пользователя из БД
        /// </summary>
        /// <param name="datAutorizationUserData">Данные для авторизации</param>
        /// <param name="userData">Данные пользователя, который прошел авторизацию</param>
        public static void GetSystemUserData([CanBeNull]AutorizationUserData datAutorizationUserData, out Employee userData)
        {
            if (string.IsNullOrEmpty(datAutorizationUserData?.UserLoginName) ||
                string.IsNullOrEmpty(datAutorizationUserData.PasswordString))
            {
                userData = null;
                return;
            }
            //устанавливаем хэш пароля
            datAutorizationUserData.SetPasswordHash (GeneratePasswordHash(datAutorizationUserData.PasswordString));
          
            DbManager.GetInstance().GetSystemUserData(datAutorizationUserData, out userData);
        }
    }

    /// <summary>
    /// Класс, который осуществляет все вспомогательные операции при работе с клиентами
    /// </summary>
    static class ClientsHelper
    {
        /// <summary>
        /// Метод проверяет запись на признак существования
        /// </summary>
        /// <param name="recordData">Запись</param>
        public static T CheckRecord<T>(T recordData) where T : class
        {
            if (recordData == null)
                return null;

           return DbManager.GetInstance().GetRecord(recordData);
        }

        /// <summary>
        /// Метод обновляет запись в бд
        /// </summary>
        /// <param name="data">Данные с формы</param>
        public static void UpdateFields<T>(T data) where T : class
        {
            if (data == null)
                return;

             DbManager.GetInstance().UpdateFields<T>(data);
        }

        public static ObservableCollection<T> GetAllClients<T>() where T : class
        {
            return  DbManager.GetInstance().GetAllClients<T>();
        }

        public static Account FindPersonForNumberSubsription(int numberSubscription)
        {
            return DbManager.GetInstance().FindPersonForNumberSubsription(numberSubscription);
        }

        /// <summary>
        /// Создать предварительную регистрацию тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="currentItem">Выбранная тренировка</param>
        public static void CreatePriorRegistration(Account account, ServicesInSubscription service, UpcomingTraining currentItem)
        {

            DbManager.GetInstance().CreatePriorRegistration(account, service, currentItem);
         
        }


        /// <summary>
        /// Звафиксировыать посещение тренировки
        /// </summary>
        /// <param name="account">Учетная запись</param>
        /// <param name="upcomingTraining">Предстоящая тренировка</param>
        public static void FixTheVisit(Account account, UpcomingTraining upcomingTraining)
        {
            if (account == null || upcomingTraining == null)
            {
                return;
            }

            DbManager.GetInstance().FixTheVisit(account, upcomingTraining);
        }

        /// <summary>
        /// Отказ о посещении тренировки
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <param name="upcTraining">предстоящая тренировка</param>
        public static void RefusalOfVisit(Account account, UpcomingTraining upcTraining)
        {
            if (account == null || upcTraining == null)
            {
                return;
            }

            DbManager.GetInstance().RefusalOfVisit(account, upcTraining);
        }
    }

    /// <summary>
    /// Класс выполняет операции - справочного характера
    /// </summary>
    static class Assistiant
    {
        /// <summary>
        /// Метод предоставляет спрачоные данны из бд
        /// </summary>
        /// <typeparam name="T">тип сущности</typeparam>
        /// <returns>коллекцию сущноситей конкретного типа</returns>
        public static ObservableCollection<T> GetReferenceData<T>() where T : class 
        {
            return DbManager.GetInstance().GetReferenceData<T>();
        }

        /// <summary>
        /// Сохранение данных
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SaveData<T>(T data) where T : class
        {
            DbManager.GetInstance().SaveData<T>(data);
        }

        public static void AddData<T1, T2>(T1 data1, T2 data2) where T1 : class where T2 : class
        {
            DbManager.GetInstance().AddData<T1, T2>(data1, data2);
        }

        /// <summary>
        /// Получить дотупные для пользователя расписание занятий по конкретной тренировке
        /// </summary>
        /// <param name="servicesInSubscription">доступные услуги</param>
        public static ObservableCollection<UpcomingTraining> GetAvailableTrainings(ServicesInSubscription servicesInSubscription)
        {
            return DbManager.GetInstance().GetAvailableTrainings(servicesInSubscription);
        }

        public static bool CheckTrainingOnAvailable(Account account, UpcomingTraining item)
        {
            if (item == null || account == null) return false;
            return DbManager.GetInstance().IsRecordAvailable(account, item);
        }

        public static ObservableCollection<PriceTrainingList> GetPriceTrainingList()
        {
            return DbManager.GetInstance().GetPriceTrainingList();
        }

        public static new ObservableCollection<Tarif> GetTarifs()
        {
            return DbManager.GetInstance().GetTarifs();
        }
    }
}
