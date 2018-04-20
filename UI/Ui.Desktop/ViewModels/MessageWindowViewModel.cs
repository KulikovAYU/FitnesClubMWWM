using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace FitnessClubMWWM.Ui.Desktop.ViewModels
{
    public class MessageWindowViewModel : ViewModelBase
    {
    
    #region Свойства

        /// <summary>
        /// Шапка
        /// </summary>
        public string strHeader { get; set; } = "Заголовок окна";

        /// <summary>
        /// Сообщение
        /// </summary>
        public string strMessage { get; set; }

        //public RelayCommand<Window> CloseCommand => new RelayCommand<Window>(this.CloseWindow);

        //private void CloseWindow(Window window)
        //{
        //    window?.Close();
        //}

        public RelayCommand<Window> CloseCommand => new RelayCommand<Window>((Window window) => { window?.Close(); });

        #endregion

    }
}
