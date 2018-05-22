using System.Threading;
using System.Windows.Input;

namespace Jarvis
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Variables

        private string _input = "";

        public string input
        {
            get
            {
                return jarvis.speechRecognition.input;
            }
            set
            {
                jarvis.speechRecognition.input = value;
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

        private JarvisClass jarvis;

        #endregion

        #region Commands

        public ICommand listenCommand { get; set; }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            jarvis = new JarvisClass();
            listenCommand = new RelayCommand(() =>
                jarvis.speechRecognition.listenForCommands = jarvis.speechRecognition.listenForCommands ? false : true);
            createUpdateInputThread();
        }

        #endregion

        private void createUpdateInputThread()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (!input.Equals(_input))
                    {
                        _input = input;
                        OnPropertyChanged("input");
                    }
                }
            }).Start();
        }

    }
}
