﻿namespace FitnessClubMWWM.Logic.Ui
{

    /// <summary>
    /// Страница приложения
    /// </summary>
    public enum ApplicationPage
    {
        /// <summary>
        /// Начальная страница регистрации
        /// </summary>
        Login = 0,

        /// <summary>
        /// Ошибка авторизации
        /// </summary>
         AutorizationError = 1,

        /// <summary>
        /// Главная страница
        /// </summary>
        MainPage = 2,

        /// <summary>
        /// Главная страница
        /// </summary>
        ConfrimExit = 3,

        /// <summary>
        /// Панель администратора
        /// </summary>
        AdminPanel = 4,

        /// <summary>
        /// Подпанель в панели администратора, отвечающая за создание нового пользователя
        /// </summary>
        AddNewUserPage = 5,

        /// <summary>
        /// Панель в панели администратора, отвечающая за предоставление прав пользователя
        /// </summary>
        AddUserPrivilegyPage = 6,
        
        /// <summary>
        /// Панель рабочий кабинет
        /// </summary>
        WorkingCabinet = 7,

        /// <summary>
        /// Панель регистрации нового посетителя фитнес-клуба
        /// </summary>
        RegisterNewClient = 8,

        /// <summary>
        /// Панель списка клиентов
        /// </summary>
        ClientPage = 9,

        /// <summary>
        /// Панель списка тренировок
        /// </summary>
        ClassSchedule = 10,


    }
}
