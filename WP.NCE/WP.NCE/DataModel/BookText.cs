namespace WP.NCE.DataModel
{
    public class BookText
    {
        public BookText(string key, string index, string name, string video)
        {
            Key = key;
            Index = index;
            Name = name;
            Video = video;
        }

        public string Key { get; private set; }
        public string Index { get; private set; }
        public string Name { get; private set; }
        public string Video { get; private set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}