namespace WP.NCE.DataModel
{
    public class Vocabulary
    {
        public Vocabulary(string word, string pronounce, string comment)
        {
            Word = word;
            Pronounce = pronounce;
            Comment = comment;
        }
        public string Word { get; private set; }
        public string Pronounce { get; private set; }
        public string Comment { get; private set; }

        public override string ToString()
        {
            return this.Word;
        }
    }
}