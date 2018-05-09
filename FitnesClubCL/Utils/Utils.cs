using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using FitnesClubCL.CF_EMDB;
using GalaSoft.MvvmLight.Messaging;

namespace FitnesClubCL.Utils
{
    class Utils
    {
    }


  

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
        public static int CreateNumberSubscription(DataBaseFcContext context)
        {
            int number = 0;
            try
            {
                Generate(ref number, context);
                //бросим исключение в случае нулевого значения номера абонемента
                if (number == 0)
                    throw new Exception("Клиенту фитнес-клуба не был присвоен номер абонемента"); ;
            }
            catch (Exception e)
            {
                Messenger.Default.Send<Exception>(e);
                Debug.Write("Исключение"+e.Message);
            }
            return number;
        }

        private static readonly Random m_rnd;
        private static int m_next;
        private const int MinValue = 999;
        private const int MaxValue = 100000;


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
    /// Класс предоставляет загрузку и выгрузку изображений из БД
    /// </summary>
    public static class ImageSQLController
    {
        static ImageSQLController()
        {

        }

        /// <summary>
        /// Конвертируем изображение в массив битов
        /// </summary>
        /// <returns></returns>
        public static byte[] ConvertToByteArray()
        {
            string fileName = "/FitnessClubMWWM.Ui.Desktop;component/Images/AutorizationPictureImages/fitness-club2.jpg";                  //Путь к файлу
            Image image = Image.FromFile(fileName);
            MemoryStream memoryStream = new MemoryStream();                                                                       //Поток в который запишем изображение
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            return memoryStream.ToArray();
        }
    }
}
