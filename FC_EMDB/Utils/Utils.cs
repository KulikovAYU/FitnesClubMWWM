using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            return File.ReadAllBytes(Path.GetFullPath(fileName));
        }

        ///// <summary>
        ///// Метод конвертирует изображение из БД
        ///// </summary>
        ///// <param name="entitity">сущность</param>
        ///// <param name="nIdHuman">id шник</param>
        ///// <returns></returns>
        //public static Image ConvertToImageFromByteArray(eEntities entitity, int nIdHuman)
        //{
        //    DataBaseFcContext context = DbManager.GetDbContext();

        //    switch (entitity)
        //    {
        //        case eEntities.eNone:
        //            break;
        //        case eEntities.eEmployee:
        //            //var queryGetEmployees = from arrByte in context.Employees where arrByte.EmployeeId == myIntVariable select arrByte;
        //            var queryGetHumanE = from arrByte in context.Accounts where arrByte.ClientId == nIdHuman select arrByte; //запрос на получение коллекции работников
        //            var queryGetPhotoE = from arrByte in queryGetHumanE select arrByte.ClientPhoto; // запрос на получение фотографии пользователя

        //            MemoryStream memoryStreamE = new MemoryStream();
        //            memoryStreamE.Write(queryGetPhotoE.First(), 0, queryGetPhotoE.First().Length);
        //            Image currentImageE = Image.FromStream(memoryStreamE);
        //            return currentImageE;

        //        case eEntities.eClient:

        //            var queryGetHumanC = from arrByte in context.Accounts where arrByte.ClientId == nIdHuman select arrByte; //запрос на получение коллекции клиентов
        //            var queryGetPhotoC = from arrByte in queryGetHumanC select arrByte.ClientPhoto; // запрос на получение фотографии пользователя
        //            //var queryGetName = from name in queryGetHuman let fullName = name.NumberSubscription + "_" + name.ClientFirstName + "_" + name.ClientLastName
        //            //    select fullName;

        //            MemoryStream memoryStream = new MemoryStream();

        //            memoryStream.Write(queryGetPhotoC.First(), 0, queryGetPhotoC.First().Length);
        //            Image currentImage = Image.FromStream(memoryStream);
        //            //Учатсток кода для сохранения
        //            //DirectoryInfo directoryInfo = Directory.CreateDirectory(Environment.CurrentDirectory + @"\Temp");
        //            //currentImage.Save(directoryInfo.ToString()+ @"\"+queryGetName.First()+".BMP", System.Drawing.Imaging.ImageFormat.Bmp);
        //            //memoryStream.Dispose();
        //            //currentImage.Dispose();
        //            return currentImage;

        //        default:
        //            throw new ArgumentOutOfRangeException(nameof(entitity), entitity, null);
        //    }
        //    return null;
        //}

        #endregion


    }
}
