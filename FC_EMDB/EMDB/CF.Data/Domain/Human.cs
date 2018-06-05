using System;
using GalaSoft.MvvmLight;

namespace FC_EMDB.EMDB.CF.Data.Domain
{
   public class Human : ViewModelBase
    {
        /// <summary>
        /// Id клиента (ключ PK)
        /// </summary>
        public int HumanId { get; set; }

        /// <summary>
        /// Имя клиента
        /// </summary>
        public string HumanFirstName { get; set; }

        /// <summary>
        /// Отчетство клиента
        /// </summary>
        public string HumanLastName { get; set; }

        /// <summary>
        /// Фамилия клиента
        /// </summary>
        public string HumanFamilyName { get; set; }

        /// <summary>
        /// Дата рождения клиента
        /// </summary>
        public DateTime? HumanDateOfBirdth { get; set; }

        /// <summary>
        /// Пол клиента
        /// </summary>
        public string HumanGender { get; set; }

        /// <summary>
        /// Адрес клиента
        /// </summary>
        public string HumanAdress { get; set; }

        /// <summary>
        /// Номер телефона клиента
        /// </summary>
        public string HumanPhoneNumber { get; set; }

        /// <summary>
        /// е-мэйл клиента
        /// </summary>
        public string HumanMail { get; set; }

        /// <summary>
        /// Фото клиента
        /// </summary>
        public byte[] HumanPhoto { get; set; }

        /// <summary>
        /// Путь к фотографии (в сохранении БД не участвует)
        /// </summary>
        public string StrPathPhoto { get; set; }

        /// <summary>
        /// Паспортные данные клиента
        /// </summary>
        /// Серия
        public string HumanPasportDataSeries { get; set; }
        /// <summary>
        /// Номер
        /// </summary>
        public string HumanPasportDataNumber { get; set; }
        /// <summary>
        /// Кем выдан
        /// </summary>
        public string HumanPasportDataIssuedBy { get; set; }
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? HumanPasportDatеOfIssue { get; set; }

        /// <summary>
        /// Полное имя (в БД не участвует)
        /// </summary>
        public string HumanFullName => $"{HumanFamilyName} {HumanFirstName} {HumanLastName}";
    }
}
