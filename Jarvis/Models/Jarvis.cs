namespace Jarvis
{
    class JarvisClass
    {
        public Talker talker;
        public SpeechRecognition speechRecognition;

        public JarvisClass()
        {
            talker = new Talker();
            speechRecognition = new SpeechRecognition();
        }
    }
}
