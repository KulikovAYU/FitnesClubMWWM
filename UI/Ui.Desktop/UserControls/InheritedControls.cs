using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

//В ЭТОМ КЛАССЕ БУДУТ ОПРЕДЕЛЯТЬСЯ ПОЛЬЗОВАТЕЛЬСКИЕ ЭЛЕМЕНТЫ УПРАВЛЕНИЯ

namespace FitnessClubMWWM.Ui.Desktop.UserControls
{
    class InheritedControls
    {
    }

    class CustomTextBox : TextBox
    {

        //public static RoutedEvent ButtonClearEvent;

        //static CustomTextBox()
        //{
        //    ButtonClearEvent = EventManager.RegisterRoutedEvent("ButtonClearClick", RoutingStrategy.Bubble, typeof(RoutedEventHandler),
        //        typeof(CustomTextBox));
        //}

        //public event RoutedEventHandler ButtonClearClick
        //{
        //    add => AddHandler(ButtonClearEvent,value);
        //    remove => RemoveHandler(ButtonClearEvent,value);
        //}

        //protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        //{
        //    base.OnMouseLeftButtonUp(e);
        //    RoutedEventArgs args = new RoutedEventArgs(CustomTextBox.ButtonClearEvent,this);
        //    RaiseEvent(args);
        //}

        //void Change()
        //{
        //    Text = null;
        //}

    }

    /// <summary>
    /// Класс кнопки, отвечающей за очистку текстового поля Textbox-a
    /// </summary>
    class ClearButton : Button
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
    class ShowPasswordButton : Button
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

    /// <summary>
    /// Класс определяет кнопку на стартовой странице
    /// </summary>
    class StartButton : Button
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

        #region Свойство, определяющее векторную картинку
        [Bindable(true)]
        [Category("Векторная картинка")]
        public Geometry GeomPicture
        {
            get => base.GetValue(GeomPictureProperty) as Geometry;
            set => base.SetValue(GeomPictureProperty, value);
        }

        public static readonly DependencyProperty GeomPictureProperty =
            DependencyProperty.Register("GeomPicture", typeof(Geometry), typeof(StartButton), new PropertyMetadata(Geometry.Empty));
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
            DependencyProperty.Register("MyProperty", typeof(Brush), typeof(StartButton), (PropertyMetadata)new FrameworkPropertyMetadata(Border.BorderBrushProperty.DefaultMetadata.DefaultValue, FrameworkPropertyMetadataOptions.None));
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
            DependencyProperty.Register("GeomPictureWidth", typeof(double), typeof(StartButton), new PropertyMetadata(0.0));
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
            DependencyProperty.Register("GeomPictureHeight", typeof(double), typeof(StartButton), new PropertyMetadata(0.0));
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
            DependencyProperty.Register("GeomPictureMargin", typeof(Thickness), typeof(StartButton), (PropertyMetadata)new FrameworkPropertyMetadata((object)new Thickness(), FrameworkPropertyMetadataOptions.AffectsMeasure));
        #endregion
    }
    
}