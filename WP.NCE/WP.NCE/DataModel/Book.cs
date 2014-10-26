namespace WP.NCE.DataModel
{
    public class Book
    {
        public Book(string key, string bookIndex, string enName, string cnName, string shortName, string imgUrl)
        {
            Key = key;
            BookIndex = bookIndex;
            EnName = enName;
            CnName = cnName;
            ShortName = shortName;
            ImgUrl = "Assets/nce_book.png";
        }

        public string Key { get; private set; }
        public string BookIndex { get; private set; }
        public string EnName { get; private set; }
        public string CnName { get; private set; }
        public string ShortName { get; private set; }

        public string ImgUrl { get; private set; }

        //ImgUrl=/minisite/appweb/images/nce_book.png

        public override string ToString()
        {
            return this.EnName;
        }
    }
}