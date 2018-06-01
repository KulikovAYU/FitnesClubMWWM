using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;
using FC_EMDB.EMDB.CF.DataAccess.Context;

namespace FC_EMDB.Utils
{


    /// <summary>
    /// Класс генерирует уникальный номер абонемента
    /// </summary>
    public static class AbonementGenerator
    {
        static AbonementGenerator()
        {
             m_rnd = new Random();
        }

        /// <summary>
        /// Функция генерирует номер абонемента
        /// </summary>
     
        /// <returns>Уникальный номер абонемента</returns>
        public static int CreateNumberSubscription()
        {
            DataBaseFcContext context = DbManager.GetDbContext();
            int number = 0;
            try
            {
                Generate(ref number, context);
            }
            catch (Exception e)
            {
                //Messenger.Default.Send<Exception>(e);
                Debug.Write("Исключение"+e.Message);
            }
            return number;
        }

        private static readonly Random m_rnd;
        private static int m_next;
        private const int MinValue = 999;
        private const int MaxValue = 10000;


        static void Generate(ref int inNumberSubscription, DataBaseFcContext context)
        {
            if (context == null)
                 return;

            if (context.Accounts.ToList().Count == 0)
            {
                m_next = m_rnd.Next(MinValue, MaxValue);
                inNumberSubscription = m_next;
            }
            else
            {
                //Получим номера абонементов всех клиеннтов
                var query = from numberSubscription in context.Accounts select numberSubscription.Abonement.NumberSubscription;
                //сгенерировали номер абонемента
                m_next = m_rnd.Next(MinValue, MaxValue);
                if (query.ToList().Contains(m_next))
                {
                    Generate(ref inNumberSubscription, context);
                }
                else
                {
                    inNumberSubscription = m_next;
                }
            }
        }
    }

    /// <summary>
    /// Класс предоставляет инструменты для работы с базой данных
    /// </summary>
    public static class SqlTools
    {
        static SqlTools()
        {

        }

        #region Группа методов для работы с изображениями

        /// <summary>
        /// Метод конвертирует изображение в массив битов для последующего сохранения его в базе данных
        /// </summary>
        /// <returns>byte[]</returns>
        public static byte[] ConvertImageToByteArray(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            return File.ReadAllBytes(Path.GetFullPath(fileName));
        }


        /// <summary>
        /// Метод конвертирует изображение из БД и устанавливает путь сохранения
        /// </summary>
        /// <typeparam name="Template">Тип данных клиента или пользователя системы</typeparam>
        /// <param name="data">Данные клиента или пользователя системы</param>
        public static void SavePhoto<Template>(ref Template data) where Template : class
        {
            Human personData = null;
            string PersonRole = null;
            //Если это пользователь системы
            if (data is Employee)
            {
                 personData = data as Employee;
                if (personData.HumanPhoto == null)
                    return;
                PersonRole = "Пользователь";
            }


            //Если это пользователь системы
            if (data is Account)
            {
                personData = data as Account;
                if (personData.HumanPhoto == null)
                    return;
                PersonRole  = "Клиент";
            }

            if(string.IsNullOrEmpty(PersonRole))
                return;


            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = personData.HumanFirstName + "_" + personData.HumanLastName + "_" + personData.HumanId;
                string savePathFolder = $@"{Environment.CurrentDirectory}\{"Temp"}\{PersonRole}";
                string localFilePath = $@"{savePathFolder}\{strFileName}.{"JPEG"}";
                DirectoryInfo directoryInfo = Directory.CreateDirectory(savePathFolder);
               
                //if (!File.Exists(localFilePath))
                //{
                    Stream myStream;
                    using (myStream = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        myStream.Write(personData.HumanPhoto, 0, personData.HumanPhoto.Length);
                        myStream.Dispose();
                    }
               // }
                return localFilePath;
            });
            outerNew.Wait();
            personData.StrPathPhoto = outerNew.Result;
            outerNew.Dispose();
        }

        /// <summary>
        /// Метод перезаписывает изображение в случае его изменения
        /// </summary>
        /// <typeparam name="Template"></typeparam>
        /// <param name="data">Данные для определения имени файла(оставил старые данные, чтобы нашел имя файла) </param>
        /// <param name="strPersonPhoto">Путь к фото пользователя</param>
        public static void UpdatePhoto<Template>(ref Template data, string strPersonPhoto) where Template : class
        {
           byte[] personPhoto = ConvertImageToByteArray(strPersonPhoto);

            if (personPhoto == null || data== null)
                return;
            // ReSharper disable once PossibleNullReferenceException
            (data as Account).HumanPhoto = personPhoto;
        }


        public static string SavePhoto<Template>(Template data) where Template : class
        {
            Account personData = null;
            string PersonRole = String.Empty;
            
            //Если это пользователь системы
            if (data is Account)
            {
                personData = data as Account;
                if (personData.HumanPhoto == null)
                    return string.Empty;
                PersonRole = "Клиент";
            }

            if (personData == null)
                return string.Empty;

            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = personData.HumanFirstName + "_" + personData.HumanLastName + "_" + personData.HumanId;
                string savePathFolder = $@"{Environment.CurrentDirectory}\{"Temp"}\{PersonRole}";
                string localFilePath = $@"{savePathFolder}\{strFileName}.{"JPEG"}";
                Directory.CreateDirectory(savePathFolder);

                    Stream myStream;
                    using (myStream = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        myStream.Write(personData.HumanPhoto, 0, personData.HumanPhoto.Length);
                        myStream.FlushAsync();
                    }
                return localFilePath;
            });
            outerNew.Wait();
            
            outerNew.Dispose();
            return outerNew.Result;
        }
    }
}
#endregion