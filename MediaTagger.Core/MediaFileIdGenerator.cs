namespace MediaTagger.Core
{
    public class MediaFileIdGenerator
    {
        private int _currentId = 1;

        public void Initialise(int startingId)
        {
            _currentId = startingId;
        }

        public int GetNextId()
        {
            return _currentId++;
        }
    }
}