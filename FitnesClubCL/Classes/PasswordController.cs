using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using FC_EMDB;

namespace FitnesClubCL.Classes
{
    /// <summary>
    /// класс для управления паролями пользователей
    /// </summary>
 static class PasswordController
    {
        /// <summary>
        /// Метод запрашивает данные из БД и возвращает роль пользователя в случае успеха
        /// </summary>
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordHash">Пароль</param>
        /// <param name="strRole">Роль пользователя в системе</param>
       public static void CheckUserNameAndPassword(string strUserName, string strPasswordHash, out string strRole)
        {
            if (strPasswordHash == null || strUserName == null)
            {
                strRole = null;
                return;
            }
           GeneratePasswordHash(ref strPasswordHash);
          
           DbManager.GetInstance().GetUserRole(strUserName, strPasswordHash, out strRole);
       }

        /// <summary>
        /// Метод генерирует хэш пароля
        /// </summary>
        /// <param name="password">строка(хэш) пароля</param>
        private static void GeneratePasswordHash(ref string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password);
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
        /// <param name="strUserName">Имя пользователя</param>
        /// <param name="strPasswordHash">Паротль</param>
        /// <param name="userData">Список данных пользователя (пока без фото)</param>
        public static void GetSystemUserData(string strUserName, string strPasswordHash, Dictionary<string, object> userData)
        {
            GeneratePasswordHash(ref strPasswordHash);
            DbManager.GetInstance().GetSystemUserData(strUserName, strPasswordHash, userData);
        }

    }
}
