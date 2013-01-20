namespace MediaTagger.Core
{
    public class FileSize
    {
        public long Bytes { get; set; }
        public FileSizeUnit Unit { get { return FileSizeUnit.FromBytes(Bytes); } }
        public double Value { get { return Unit.GetValue(Bytes); } }

        public FileSize(long bytes)
        {
            Bytes = bytes;
        }

        public override string ToString()
        {
            return Value.ToString("0.#") + Unit;
        }
    }
}
