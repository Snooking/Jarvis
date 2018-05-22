using System.Windows.Input;

namespace Jarvis
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Variables

        private string _input;

        public string input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                OnPropertyChanged("input");
            }
        }

        private string _output;

        public string output
        {
            get
            {
                return _output;
            }
            set
            {
                _output = value;
                OnPropertyChanged("output");
            }
        }

        #endregion

        #region Commands

        public ICommand listenCommand { get; set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            JarvisClass jarvis = new JarvisClass();
            listenCommand = new RelayCommand(() => 
                jarvis.speechRecognition.listenForCommands = jarvis.speechRecognition.listenForCommands ? true : false);
        }

        #endregion
    }
}
