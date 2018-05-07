using System.Windows;
using System.Windows.Media;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.Pages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    /// <summary>
    /// Класс служит контекстом данных для кастомного messagebox-а
    /// </summary>
    public class MessageWindowViewModel : ViewModelBase
    {

       public void SetParametrs(MessageBoxButton button, eMessageBoxIcons icon)
        {
            SetIcon(icon);
        }

        private void SetIcon(eMessageBoxIcons icon)
        {
            switch (icon)
            {
                case eMessageBoxIcons.eNone:
                    break;

                case eMessageBoxIcons.eAsk: 
                    PicGeometry = Geometry.Parse(
                        "F1 M 145.891,115.634C 135.873,124.325 126.443,133.672 117.663,143.615L 85.6833,115.357C 95.7277,103.979 106.518,93.2833 117.983,83.343L 145.891,115.634 Z M 449.666,17.7478C 628.269,77.7566 724.462,271.344 664.517,450.138C 604.572,628.931 411.191,725.225 232.588,665.216C 53.9846,605.208 -42.2066,411.621 17.7368,232.825L 58.2446,246.399C 48.0511,276.871 42.8383,308.791 42.8077,340.925C 42.6531,505.792 176.038,639.571 340.73,639.724C 505.423,639.878 639.056,506.351 639.21,341.483C 639.363,176.616 505.979,42.84 341.287,42.6853C 283.127,42.6026 226.219,59.6034 177.615,91.5813L 154.163,55.9177C 209.722,19.3364 274.788,-0.106728 341.287,2.28882e-005C 378.135,0.0173569 414.734,6.01064 449.666,17.7478 Z M 93.3158,175.159C 85.9378,186.169 79.3207,197.672 73.5096,209.585L 35.1339,190.739C 41.8376,177.101 49.4515,163.931 57.9248,151.319L 93.3158,175.159 Z M 362.607,425.529L 362.607,512.223L 319.968,512.223L 319.968,384.167L 341.287,384.167C 411.935,384.167 469.207,326.834 469.207,256.112C 469.207,185.389 411.935,128.056 341.287,128.056C 270.64,128.056 213.368,185.389 213.368,256.112L 170.728,256.112C 170.736,170.073 234.695,97.4812 319.968,86.7258C 413.426,74.938 498.733,141.227 510.509,234.785C 522.284,328.343 456.066,413.742 362.607,425.529 Z M 319.968,554.908L 362.607,554.908L 362.607,597.594L 319.968,597.594L 319.968,554.908 Z ");

                    break;

                case eMessageBoxIcons.eWarning:
                    PicGeometry = Geometry.Parse(
                        "F1 M 775.158,729.691C 789.34,751.362 773.883,781.798 748.227,780.363L 747.909,780.363L 32.1042,780.363C 15.6911,781.32 -9.32702,762.038 2.94302,733.356L 361.005,17.8845C 367.697,1.31233 401.48,-12.8694 419.168,17.8845L 775.158,729.691 Z M 84.69,715.509L 695.323,715.509L 390.007,105.207L 84.69,715.509 Z M 357.499,306.145C 357.499,288.138 372,273.638 390.007,273.638C 408.013,273.638 422.514,288.138 422.514,306.145L 422.514,506.763C 422.514,524.77 408.013,539.27 390.007,539.27C 372,539.27 357.499,524.77 357.499,506.763L 357.499,306.145 Z M 390.007,588.509C 408.048,588.509 422.673,603.134 422.673,621.175C 422.673,639.216 408.048,653.841 390.007,653.841C 371.965,653.841 357.34,639.216 357.34,621.175C 357.34,603.134 371.965,588.509 390.007,588.509 Z ");
                    break;
            }
        }
    #region Свойства

        /// <summary>
        /// Шапка
        /// </summary>
        public string strHeader { get; set; } = "Заголовок окна";

        /// <summary>
        /// Сообщение
        /// </summary>
        public string strMessage { get; set; } = "Текст сообщения";

        /// <summary>
        /// Левая кнопка
        /// </summary>
        public string LeftButtonCaption { get; set; } = "Да";

        /// <summary>
        /// Правая кнопка
        /// </summary>
        public string RightButtonCaption { get; set; } = "Нет";
        /// <summary>
        /// Кнопка отмены
        /// </summary>
        public string CancelButtonCaption { get; set; } = "Отмена";
        /// <summary>
        /// Картинка в окне
        /// </summary>
        public Geometry PicGeometry { get; set; }
        //public RelayCommand<Window> CloseCommand => new RelayCommand<Window>(this.CloseWindow);

        //private void CloseWindow(Window window)
        //{
        //    window?.Close();
        //}

        #endregion

    #region Команды от элемента управления (Кастомного  мессаджбокса)
        /// <summary>
        /// Закрытие (нажатие на крестик)
        /// </summary>
        public RelayCommand<Window> CloseCommand => new RelayCommand<Window>((window) => { window?.Close(); });

        public RelayCommand<Window> OkPressCommand => new RelayCommand<Window>((window) =>
        {
            (window as CustomMessageBox)?.SetResult(MessageBoxResult.OK);
            CloseCommand.Execute(window);
        });

        public RelayCommand<Window> NoPressCommand => new RelayCommand<Window>((window) =>
        {
            (window as CustomMessageBox)?.SetResult(MessageBoxResult.No);
            CloseCommand.Execute(window);
        });

        public RelayCommand<Window> CancelPressCommand => new RelayCommand<Window>((window) =>
        {
            (window as CustomMessageBox)?.SetResult(MessageBoxResult.Cancel);
            CloseCommand.Execute(window);
        });

        public RelayCommand<Window> YesPressCommand => new RelayCommand<Window>(window =>
        {
            (window as CustomMessageBox)?.SetResult(MessageBoxResult.Yes);
            CloseCommand.Execute(window);
        });
        #endregion
    }
}
