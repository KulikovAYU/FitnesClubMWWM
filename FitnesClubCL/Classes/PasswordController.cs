using System;
using System.Collections.ObjectModel;
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
        public static void GetSystemUserData([CanBeNull]AutorizationUserData datAutorizationUserData, out UserData userData)
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
        public static void IsExistRecord<T>(ref T recordData) where T : class
        {
            if (recordData == null)
                return;

            DbManager.GetInstance().GetRecord(ref recordData);
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
    }
}
