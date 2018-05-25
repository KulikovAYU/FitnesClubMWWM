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
        /// <param name="context">контекст БД</param>
        /// <returns>Уникальный номер абонемента</returns>
        public static int CreateNumberSubscription()
        {
            DataBaseFcContext context = DbManager.GetDbContext();
            int number = 0;
            try
            {
                Generate(ref number, context);
                ////бросим исключение в случае нулевого значения номера абонемента
                //if (number == 0)
                //    throw new Exception("Клиенту фитнес-клуба не был присвоен номер абонемента"); ;
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
                var query = from numberSubscription in context.Accounts select numberSubscription.NumberSubscription;
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
        /// Метод конвертирует изображение в массив битов для последующего сохранения его в базе данных
        /// </summary>
        public static byte[] ConvertImageToByteArray<Template>( Template data) where Template : class
        {
            if (data is NewClientData)
            {
                var clientData = data as NewClientData;
                if (string.IsNullOrEmpty(clientData.PathPersonPhoto))
                    return null;

                return File.ReadAllBytes(Path.GetFullPath(clientData.PathPersonPhoto));
            }
            return null;
        }

        /// <summary>
        /// Метод конвертирует изображение из БД и устанавливает путь сохранения
        /// </summary>
        /// <typeparam name="Template">Тип данных клиента или пользователя системы</typeparam>
        /// <param name="data">Данные клиента или пользователя системы</param>
        public static void SavePhoto<Template>(ref Template data) where Template : class
        {
            PersonData personData = null;
            //Если это пользователь системы
            if (data is UserData)
            {
                 personData = data as UserData;
                if (personData.PersonPhoto == null)
                    return;
            }

            //Если это клиент
            if (data is NewClientData)
            {
                 personData = data as NewClientData;
                if (personData.PersonPhoto == null)
                    return;
            }

            if (personData == null)
                return;

            var outerNew = Task<string>.Factory.StartNew(() =>
            {
                string strFileName = personData.PersonFirstName + "_" + personData.PersonLastName + "_" + personData.PersonId;
                string savePathFolder = $@"{Environment.CurrentDirectory}\{"Temp"}\{personData.PersonRole}";
                string localFilePath = $@"{savePathFolder}\{strFileName}.{"JPEG"}";

                if (!File.Exists(localFilePath))
                {
                    Stream myStream;
                    using (myStream = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        myStream.Write(personData.PersonPhoto, 0, personData.PersonPhoto.Length);
                        myStream.FlushAsync();
                    }
                }
                return localFilePath;
            });
            outerNew.Wait();
            personData.PathPersonPhoto = outerNew.Result;
            outerNew.Dispose();
        }

        /// <summary>
        /// Метод перезаписывает изображение в случае его изменения
        /// </summary>
        /// <typeparam name="Template"></typeparam>
        /// <param name="data">Данные для определения имени файла(оставил старые данные, чтобы нашел имя файла) </param>
        /// <param name="PersonPhoto">Массив байтов фото</param>
        public static void UpdatePhoto<Template>(ref Template data, byte[] PersonPhoto) where Template : class
        {
           

            if (data is Account)
            {

                var clientData = data as Account;

                bool isEquals = true;

                if (PersonPhoto != null && clientData.ClientPhoto != null)
                {
                    isEquals = (BitConverter.ToInt32(PersonPhoto, 0) ^ BitConverter.ToInt32(clientData.ClientPhoto, 0)) != 0;
                }

                if (PersonPhoto == null && clientData.ClientPhoto!=null)
                {
                    PersonPhoto = clientData.ClientPhoto;
                }

                if (clientData.ClientPhoto == null && PersonPhoto!=null)
                {
                    clientData.ClientPhoto = PersonPhoto;
                    isEquals = false;
                }

                var outerNew = Task.Factory.StartNew(() =>
                {
                    string strFileName = clientData.ClientFirstName + "_" + clientData.ClientLastName + "_" + clientData.ClientId;
                    string savePathFolder = $@"{Environment.CurrentDirectory}\{"Temp"}\Клиент";
                    DirectoryInfo directoryInfo = Directory.CreateDirectory(savePathFolder);
                    string localFilePath = $@"{savePathFolder}\{strFileName}.{"JPEG"}";
                  
                  
                    if (File.Exists(localFilePath) && !isEquals)
                    {
                        Stream myStream;
                        using (myStream = File.Open(localFilePath, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                          myStream.Write(PersonPhoto, 0, PersonPhoto.Length);
                            myStream.FlushAsync();
                        }
                    }
                });
                outerNew.Wait();
            }
        }

        /// <summary>
        /// Метод преобразует данные из val2 в val1
        /// </summary>
        /// <typeparam name="Temp1">Шаблон val1</typeparam>
        /// <typeparam name="Temp2">Шаблон val2</typeparam>
        /// <param name="val1">Выходное значение</param>
        /// <param name="val2">Преобразуемое значение</param>
        public static void Convert<Temp1, Temp2>(ref Temp1 val1, Temp2 val2) where Temp1 : class where Temp2 : class
        {
            if (val1 == null || val2 == null)
                return;

            if (val1 is Account && val2 is NewClientData)
            {
                var account = (val1 as Account);
                var newClientData = (val2 as NewClientData);
              
                SqlTools.UpdatePhoto(ref account, SqlTools.ConvertImageToByteArray(newClientData));
                
                account.ClientFirstName = newClientData.PersonFirstName;
                account.ClientLastName = newClientData.PersonLastName;
                account.ClientFamilyName = newClientData.PersonFamilyName;
                account.ClientAdress = newClientData.PersonAdress;
                account.ClientDateOfBirdth = newClientData.PersonDateOfBirdth;
                account.ClientGender = newClientData.PersonGender;
                //account.ClientId = newClientData.PersonId;
                account.ClientMail = newClientData.PersonMail;
                account.ClientPasportDataSeries = newClientData.ClientPasportDataSeries;
                account.ClientPasportDataNumber = newClientData.ClientPasportDataNumber;
                account.ClientPasportDataIssuedBy = newClientData.ClientPasportDataIssuedBy;
                account.ClientPhoneNumber = newClientData.PersonPhoneNumber;
              
               account.ClientPhoto = SqlTools.ConvertImageToByteArray(newClientData); //запишем фотографию
                
             
            }
        }
    }
}
#endregion