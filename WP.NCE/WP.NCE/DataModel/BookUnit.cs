namespace WP.NCE.DataModel
{
    public class BookUnit
    {
        public BookUnit(string key, string unitIndex, string name)
        {
            Key = key;
            UnitIndex = unitIndex;
            Name = name;

            ImagePath = "Assets/nce_book.png";
        }

        public string Key { get; private set; }
        public string UnitIndex { get; private set; }
        public string Name { get; private set; }

        public string ImagePath { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}