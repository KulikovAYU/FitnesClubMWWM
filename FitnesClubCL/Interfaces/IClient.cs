using System;

namespace FitnesClubCL
{
   public interface IClient
    {
        /// <summary>
        /// Имя клиента
        /// </summary>
         string ClientFirstName { get; }
        /// <summary>
        /// Фамилия клиента
        /// </summary>
         string ClientFamilyName { get; }
        /// <summary>
        /// Отчетство клиента
        /// </summary>
         string ClientLastName { get; }
        /// <summary>
        /// Дата рождения клиента
        /// </summary>
         DateTime? ClientDateOfBirdth { get;  }
        /// <summary>
        /// Номер телефона клиента+
        /// </summary>
         string ClientPhoneNumber { get; }
        /// <summary>
        /// Паспортные данные клиента
        /// </summary>
        /// Серия
         string ClientPasportDataSeries { get; }
        /// <summary>
        /// Номер
        /// </summary>
         string ClientPasportDataNumber { get; }
        /// <summary>
        /// Кем выдан
        /// </summary>
         string ClientPasportDataIssuedBy { get; }
        /// <summary>
        /// Дата выдачи
        /// </summary>
         DateTime? ClientPasportDatеOfIssue { get; }

        /// <summary>
        /// Номер абонемента
        /// </summary>
         int NumberSubscription { get;}
        ///// <summary>
        ///// Когда зарегистрирован абонемент
        ///// </summary>
        // DateTime? AccountregistrationDate { get; set; } = DateTime.Now;
        /// <summary>
        ///// Id 
        ///// </summary>
        // int ClientId { get; private set; }
        ///// <summary>
        /// Пол клиента
        /// </summary>
         string ClientGender { get; }
        /// <summary>
        /// Адрес клиента 
        /// </summary>
         string ClientAdress { get;  }
        /// <summary>
        /// е-мэйл клиента
        /// </summary>
         string ClientMail { get; }
        /// <summary>
        /// Фото клиента
        /// </summary>
         byte[] ClientPhoto { get; }

      
    }
}
