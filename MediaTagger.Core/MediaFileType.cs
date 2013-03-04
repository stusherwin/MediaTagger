using System.Drawing.Imaging;

namespace MediaTagger.Core
{
    public class MediaFileType
    {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string ContentType { get; private set; }
        public MediaType MediaType { get; private set; }

        public MediaFileType(string name, string extension, string contentType, MediaType mediaType)
        {
            Name = name;
            Extension = extension;
            MediaType = mediaType;
            ContentType = contentType;
        }

        public override string ToString()
        {
            return Name;
        }

        public static MediaFileTypeCollection All = new MediaFileTypeCollection(VideoType.All, ImageType.All);
    }
}
