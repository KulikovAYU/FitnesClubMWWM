using System;
using System.Drawing;

namespace FitnesClubCL
{
    /// <summary>
    /// Интерфейс пользователя системы
    /// </summary>
    public interface ISystemUser
    {
        /// <summary>
        /// Имя и Отчество пользователя
        /// </summary>
        string UserFullName { get; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        string LoginName { get; }
        
        /// <summary>
        /// Дата рождения
        /// </summary>
        DateTime? DateOfBirdth { get; }

        /// <summary>
        /// Статус пользователя системы
        /// </summary>
        string Status { get; }

        /// <summary>
        /// Дата и время
        /// </summary>
        DateTime TimeInSystem { get; }

        /// <summary>
        /// Статус в отпуске или нет
        /// </summary>
        string VacationStatus { get; }
       
        /// <summary>
        /// Фотография пользователя
        /// </summary>
        Image UserPhoto { get; }
    } 
}
