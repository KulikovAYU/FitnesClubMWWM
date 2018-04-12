using System.ComponentModel;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

//В ЭТОМ КЛАССЕ БУДУТ ОПРЕДЕЛЯТЬСЯ ПОЛЬЗОВАТЕЛЬСКИЕ ЭЛЕМЕНТЫ УПРАВЛЕНИЯ

namespace FitnessClubMWWM.Ui.Desktop.UserControls
{
    class InheritedControls
    {
    }


    public class PasswordHelper
    {
        public string PasswordString { get; set; }
    }

    /// <summary>
    /// Класс кнопки, отвечающей за очистку текстового поля Textbox-a
    /// </summary>
    public class ClearButton : Button
    {
        [Bindable(true)]
        [Category("Очистить поле")]
        public bool IsClear
        {
            get { return (bool)GetValue(IsClearProperty); }
            set { SetValue(IsClearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClearProperty =
            DependencyProperty.Register("IsClear", typeof(bool), typeof(ClearButton), new PropertyMetadata(false));
    }

    /// <summary>
    /// Класс кнопки, отвечающей за показ пароля
    /// </summary>
    public class ShowPasswordButton : Button
    {
        [Bindable(true)]
        [Category("Показать пароль")]
        public bool IsShowingPassword
        {
            get { return (bool)GetValue(IsClearProperty); }
            set { SetValue(IsClearProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsClear.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsClearProperty =
            DependencyProperty.Register("IsShowingPassword", typeof(bool), typeof(ShowPasswordButton), new PropertyMetadata(false));
    }


    class CustomPasswordBox : TextBox
    {

        #region Пароль для passwordbox
        [Bindable(true)]
        [Category("Показать пароль")]
        public string Password
        {
            get => (string)GetValue(PasswordProperty);
            set => SetValue(PasswordProperty, value);
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox), new PropertyMetadata(null));

        #endregion


     
    }

    public static class PasswordBoxAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // when the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event

            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPassword);
        }

        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BoundPassword);
        }

        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }



    /// <summary>
    /// Класс определяет кнопку с векторной картинкой
    /// </summary>
    class ButtonWithPicture : Button
    {
        #region Свойство, определяющее векторную картинку
        [Bindable(true)]
        [Category("Векторная картинка")]
        public Geometry GeomPicture
        {
            get => base.GetValue(GeomPictureProperty) as Geometry;
            set => base.SetValue(GeomPictureProperty, value);
        }

        public static readonly DependencyProperty GeomPictureProperty =
            DependencyProperty.Register("GeomPicture", typeof(Geometry), typeof(Button), new PropertyMetadata(Geometry.Empty));
        #endregion

        #region Свойство, определяющее цвет векторной картинки
        [Bindable(true)]
        [Category("Векторная картинка")]
        public Brush GeomPictureFill
        {
            get => GetValue(GeomPictureFillProperty) as Brush;
            set => SetValue(GeomPictureFillProperty, value);
        }

        public static readonly DependencyProperty GeomPictureFillProperty =
            DependencyProperty.Register("MyProperty", typeof(Brush), typeof(Button), (PropertyMetadata)new FrameworkPropertyMetadata(Border.BorderBrushProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));
        #endregion

        #region Свойство, определяющее ширину векторной картинки
        [Bindable(true)]
        [Category("Векторная картинка")]
        public double GeomPictureWidth
        {
            get => (double)GetValue(GeomPictureWidthProperty);
            set => SetValue(GeomPictureWidthProperty, value);
        }
        public static readonly DependencyProperty GeomPictureWidthProperty =
            DependencyProperty.Register("GeomPictureWidth", typeof(double), typeof(Button), new PropertyMetadata(0.0));
        #endregion

        #region Свойство, определяющее высоту векторной картинки
        [Bindable(true)]
        [Category("Векторная картинка")]
        public double GeomPictureHeight
        {
            get => (double)GetValue(GeomPictureHeightProperty);
            set => SetValue(GeomPictureHeightProperty, value);
        }
        public static readonly DependencyProperty GeomPictureHeightProperty =
            DependencyProperty.Register("GeomPictureHeight", typeof(double), typeof(Button), new PropertyMetadata(0.0));
        #endregion

        #region Сойство, определяющее отступы от внутренних границ контейнера векторной картинки
        [Bindable(true)]
        [Category("Векторная картинка")]
        public Thickness GeomPictureMargin
        {
            get => (Thickness)GetValue(GeomPictureMarginProperty);
            set => SetValue(GeomPictureMarginProperty, value);
        }

        public static readonly DependencyProperty GeomPictureMarginProperty =
            DependencyProperty.Register("GeomPictureMargin", typeof(Thickness), typeof(Button), (PropertyMetadata)new FrameworkPropertyMetadata((object)new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion
    }

    /// <summary>
    /// Класс определяет кнопку на стартовой странице
    /// </summary>
    class StartButton : ButtonWithPicture
    {
        #region Свойство коментарий к функции кнопки
        [Bindable(true)]
        [Category("Строка коментария")]
        public string ButtonComment
        {
            get => (string)GetValue(ButtonCommentProperty);
            set => SetValue(ButtonCommentProperty, value);
        }

        public static readonly DependencyProperty ButtonCommentProperty =
            DependencyProperty.Register("ButtonComment", typeof(string), typeof(StartButton), new PropertyMetadata(string.Empty));
        #endregion

        #region Свойство, определяющее цвет шрифта коментария к функции кнопки
        [Bindable(true)]
        [Category("Строка коментария")]
        public Brush ButtonCommentBrush
        {
            get => (Brush)GetValue(ButtonCommentBrushProperty);
            set => SetValue(ButtonCommentBrushProperty, value);
        }

        public static readonly DependencyProperty ButtonCommentBrushProperty =
            DependencyProperty.Register("ButtonCommentBrush", typeof(Brush), typeof(StartButton), new PropertyMetadata());
        #endregion

        #region Свойство, определяющее тип шрифта коментария
        [Bindable(true)]
        [Category("Строка коментария")]
        public FontFamily ButtonCommentFontFamily
        {
            get => (FontFamily)GetValue(ButtonCommentFontFamilyProperty);
            set => SetValue(ButtonCommentFontFamilyProperty, value);
        }

        public static readonly DependencyProperty ButtonCommentFontFamilyProperty =
            DependencyProperty.Register("ButtonCommentFontFamily", typeof(FontFamily), typeof(StartButton), (PropertyMetadata)new FrameworkPropertyMetadata((object)SystemFonts.MessageFontFamily, FrameworkPropertyMetadataOptions.Inherits));
        #endregion

        #region Свойство, определяющее заполнение текста
        [Bindable(true)]
        [Category("Строка коментария")]
        public TextWrapping ButtonCommentWrapping
        {
            get => (TextWrapping)GetValue(MyPropertyProperty);
            set => SetValue(MyPropertyProperty, value);
        }

        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("ButtonCommentWrapping", typeof(TextWrapping), typeof(StartButton), new PropertyMetadata(TextWrapping.NoWrap));
        #endregion

        #region Свойство, определяющее высоту шрифта
        [Bindable(true)]
        [Category("Строка коментария")]
        public double ButtonCommentFontSize
        {
            get => (double)GetValue(ButtonCommentFontSizeProperty);
            set => SetValue(ButtonCommentFontSizeProperty, value);
        }

        public static readonly DependencyProperty ButtonCommentFontSizeProperty =
            DependencyProperty.Register("ButtonCommentFontSize", typeof(double), typeof(StartButton), new PropertyMetadata(0.0));
        #endregion
    }

    /// <summary>
    /// Класс определяет кнопку о пользователе
    /// </summary>
    class AboutUserButton : ButtonWithPicture
    {

    }
}