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

        private string _output = "";

        public string output
        {
            get
            {
                return jarvis.talker.output;
            }
            set
            {
                jarvis.talker.output = value;
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
                jarvis.speechRecognition.listenForCommands = 
                jarvis.speechRecognition.listenForCommands ? false : true);
            createUpdateInputOutputThread();
        }

        #endregion

        private void createUpdateInputOutputThread()
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
                    if(!output.Equals(_output))
                    {
                        _output = output;
                        OnPropertyChanged("output");
                    }
                }
            }).Start();
        }

    }
}
