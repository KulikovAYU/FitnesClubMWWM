using FC_EMDB.Classes;
using FC_EMDB.EMDB.CF.Data.Domain;

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
        Employee WorkingUserData { get; }
    }
}
