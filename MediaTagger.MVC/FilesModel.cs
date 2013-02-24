using FubuMVC.Core;

namespace MediaTagger.Mvc
{
    public class FilesOutputModel : JsonMessage
    {
        public FileModel[] files;

        public class FileModel
        {
            public int id;
            public string title;
            public string description;
            public string contentType;
            public string mediaUrl;
            public string thumbnailUrl;
        }
    }
}