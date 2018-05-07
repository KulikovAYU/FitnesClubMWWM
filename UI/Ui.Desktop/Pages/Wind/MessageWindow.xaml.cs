using System.Windows;
using FitnessClubMWWM.Ui.Desktop.Constants;
using FitnessClubMWWM.Ui.Desktop.ViewModels;

namespace FitnessClubMWWM.Ui.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для CustomMessageBox.xaml
    /// </summary>
    public sealed partial class CustomMessageBox : Window
    {
        private CustomMessageBox()
        {
            InitializeComponent();
        }
        
        private static CustomMessageBox messageBox;

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, eMessageBoxIcons icon,
            MessageBoxResult defaultResult = MessageBoxResult.None)
        {
           messageBox = new CustomMessageBox();
           
            if (!(messageBox.DataContext is MessageWindowViewModel window))
                return MessageBoxResult.None;

            window.strMessage = messageBoxText;
            window.strHeader = caption;
            window.SetParametrs(button, icon);

            ///Можно было сделать через value converter, но времени мало
            switch (button)
            {
                
                case MessageBoxButton.OK:
                    window.LeftButtonCaption = "Ок";
                    messageBox.Ok.Visibility = Visibility.Visible;
                    messageBox.YesNo.Visibility = Visibility.Hidden;
                    messageBox.YesNoCancel.Visibility = Visibility.Hidden;
                    break;

                case MessageBoxButton.OKCancel:

                    window.LeftButtonCaption = "Да";
                    window.RightButtonCaption = "Нет";
                    messageBox.YesNo.Visibility = Visibility.Visible;
                    messageBox.Ok.Visibility = Visibility.Hidden;
                    messageBox.YesNoCancel.Visibility = Visibility.Hidden;
                    break;

                case MessageBoxButton.YesNo:
                    window.LeftButtonCaption = "Да";
                    window.RightButtonCaption = "Нет";
                    messageBox.YesNo.Visibility = Visibility.Visible;
                    messageBox.Ok.Visibility = Visibility.Hidden;
                    messageBox.YesNoCancel.Visibility = Visibility.Hidden;
                    break;

                case MessageBoxButton.YesNoCancel://Todo: метод 
                    window.LeftButtonCaption = "Да";
                    window.RightButtonCaption = "Нет";
                    window.CancelButtonCaption = "Отмена";
                    messageBox.YesNoCancel.Visibility = Visibility.Visible;
                    messageBox.Ok.Visibility = Visibility.Hidden;
                    messageBox.YesNo.Visibility = Visibility.Hidden;
                    break;
            }
        
            messageBox.ShowDialog();

            return messageBox.Result;
        }

        private MessageBoxResult Result = MessageBoxResult.None;
        public void  SetResult(MessageBoxResult Result)
        {
            if (Result != MessageBoxResult.None)
            {
                this.Result = Result;
            }
        }
    }
}
