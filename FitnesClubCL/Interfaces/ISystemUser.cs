using System;
using System.Drawing;
using FitnesClubCL.Classes;

namespace FitnesClubCL
{
    /// <summary>
    /// Интерфейс пользователя системы
    /// </summary>
    public interface ISystemUser
    {
        /// <summary>
        /// Данные пользователя, которые он отправил
        /// </summary>
        AutorizationUserData AutorizationUserData { get; }

        /// <summary>
        /// Данные пользователя, которые выернула модель
        /// </summary>
        UserData? WorkingUserData { get; }
    }

    /// <summary>
    /// Интерфейс регистрационных данных клиента
    /// </summary>
    public interface IClientRegisterData
    {
        NewClientData ClientData { get; set; }
    }



}
