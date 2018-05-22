using System.Speech.Recognition;
using System.Threading;

namespace Jarvis
{
    public class SpeechRecognition
    {
        private bool _listenForCommands;
        public bool listenForCommands
        {
            get
            {
                return _listenForCommands;
            }
            set
            {
                _listenForCommands = value;
            }
        }

        public string input { get; set; } = "";

        private SpeechRecognitionEngine speechRecognitionEngine;
        private Choices choices;
        private GrammarBuilder grammarBuilder;
        private Grammar grammar;

        public SpeechRecognition()
        {
            speechRecognitionEngine = new SpeechRecognitionEngine();
            choices = new Choices();
            addBasicChoices();
            prepareGrammar();
            prepareRecognitionEngine();
            createThreadForListening();
        }

        private void addBasicChoices()
        {
            choices.Add("Jarvis");
            choices.Add("Hello");
        }

        private void prepareGrammar()
        {
            grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            grammarBuilder.Append(choices);
            //grammarBuilder.AppendDictation();
            grammar = new Grammar(grammarBuilder);
        }

        private void prepareRecognitionEngine()
        {
            speechRecognitionEngine.RequestRecognizerUpdate();
            speechRecognitionEngine.LoadGrammar(grammar);
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void createThreadForListening()
        {
            new Thread(() =>
            {
                speechRecognitionEngine.SpeechRecognized += sr_SpeechRcognized;
            }).Start();
        }

        public void sr_SpeechRcognized(object sender, SpeechRecognizedEventArgs e)
        {
            input += e.Result.Text;
            if (e.Result.Text == "Jarvis")
            {
                listenForCommands = true;
            }
            if (e.Result.Text == "Thank you")
            {
                listenForCommands = false;
            }
        }
    }
}
