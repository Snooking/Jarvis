using System.Speech.Recognition;

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

        private SpeechRecognitionEngine speechRecognitionEngine;
        private Choices choices;
        private GrammarBuilder grammarBuilder;
        private Grammar grammar;

        private SpeechRecognizer recognizer;

        public SpeechRecognition()
        {
            speechRecognitionEngine = new SpeechRecognitionEngine();
            choices = new Choices();
            addBasicChoices();
            prepareGrammar();
            speechRecognitionEngine.RequestRecognizerUpdate();
            speechRecognitionEngine.LoadGrammar(grammar);
            speechRecognitionEngine.SetInputToDefaultAudioDevice();
            speechRecognitionEngine.RecognizeAsync(RecognizeMode.Multiple);
            while (true)
            {
                speechRecognitionEngine.SpeechRecognized += sr_SpeechRcognized;
            }
            //prepareRecognizer();
        }

        private void addBasicChoices()
        {
            choices.Add("Jarvis?");
            choices.Add("Jarvis");
            choices.Add("Hello");
        }

        private void prepareGrammar()
        {
            grammarBuilder = new GrammarBuilder();
            grammarBuilder.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            grammarBuilder.Append(choices);
            grammarBuilder.AppendDictation();
            grammar = new Grammar(grammarBuilder);
        }

        private void prepareRecognizer()
        {
            recognizer = new SpeechRecognizer();
            recognizer.LoadGrammar(grammar);
            recognizer.Enabled = true;
            while (true)
            {
                recognizer.SpeechRecognized += new System.EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRcognized);
            }
        }

        public void sr_SpeechRcognized(object sender, SpeechRecognizedEventArgs e)
        {
            System.Console.WriteLine(e.Result.Text);
            if (e.Result.Text == "Jarvis")
            {
                System.Console.WriteLine("Hello sir");
            }
        }
    }
}
