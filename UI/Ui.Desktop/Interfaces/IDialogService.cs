namespace FitnessClubMWWM.Ui.Desktop.Interfaces
{
    /// <summary>
    /// Данный интерфейс определяет функционал для работы с диалоговыми окнами
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Создать диалоговое окно
        /// </summary>
         IDialogService CreateDialogService { get; }

        /// <summary>
        /// Показ сообщения
        /// </summary>
        /// <param name="message">текст сообщения</param>
        void ShowMessage(string message);

        /// <summary>
        /// Путь к выбранному файлу
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Открытие файла
        /// </summary>
        /// <param name="strDefFileExtention"> Расширение файла</param>
        /// <returns></returns>
        bool OpenFileDialog(string strDefFileExtention = "", string strDefFileFilters = "");

        /// <summary>
        /// Сохранение файла
        /// </summary>
        /// <returns></returns>
        bool SaveFileDialog();

    }
}
