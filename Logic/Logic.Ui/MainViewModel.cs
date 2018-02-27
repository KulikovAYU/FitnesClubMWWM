using GalaSoft.MvvmLight;

namespace FitnessClubMWWM.Logic.Ui
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                WindowTitle = "MvvmSample (Design)";
                // Code runs in Blend --> create design time data.
            }
            else
            {
                WindowTitle = "MvvmSample";
                // Code runs "for real"
            }
        }

        public string WindowTitle { get; private set; }
    }
}