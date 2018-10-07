using System.Speech.Synthesis;

namespace Jarvis
{
    public class Talker
    {
        private SpeechSynthesizer synthesizer;
        private PromptBuilder builder;

        public string output { get; set; } = "";

        public Talker()
        {
            synthesizer = new SpeechSynthesizer();
            synthesizer.SelectVoiceByHints(VoiceGender.Male);
            builder = new PromptBuilder();
            saySentence("Hello Sir");
        }

        private void saySentence(string sentence)
        {
            builder.ClearContent();
            builder.AppendText(sentence);
            synthesizer.Speak(builder);
            output += sentence;
        }
    }
}
