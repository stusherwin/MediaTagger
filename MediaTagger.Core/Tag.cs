namespace MediaTagger.Core
{
    public class Tag
    {
        public string Name { get; private set; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
