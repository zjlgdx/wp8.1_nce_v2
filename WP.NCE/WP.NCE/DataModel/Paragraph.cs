namespace WP.NCE.DataModel
{
    public class Paragraph
    {
        public Paragraph(string time, string sentence, string cnSentence)
        {
            Time = time;
            Sentence = sentence;
            CnSentence = cnSentence;
        }
        public string Time { get; private set; }
        public string Sentence { get; private set; }
        public string CnSentence { get; private set; }

        public override string ToString()
        {
            return this.Sentence;
        }
    }
}