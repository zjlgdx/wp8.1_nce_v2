namespace WP.NCE.DataModel
{
    public class LineContent
    {
        public LineContent(string content)
        {
            Content = content;
        }
        public string Content { get; private set; }

        public override string ToString()
        {
            return this.Content;
        }
    }
}