using MediaTagger.Core;

namespace MediaTagger.Mvc
{
    public class MediaOutputModel
    {
        public MediaFile File;

        public MediaOutputModel(MediaFile file)
        {
            File = file;
        }
    }
}