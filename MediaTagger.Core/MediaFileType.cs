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

        public static MediaFileType[] All = new[] 
        {
            new MediaFileType("Flash video", "flv", "video/x-flv", MediaType.Video),
            new MediaFileType("Windows media", "wmv", "video/x-ms-wmv", MediaType.Video),
            new MediaFileType("Mp4", "mp4", "video/mp4", MediaType.Video),
            new MediaFileType("Avi", "avi", "video/avi", MediaType.Video),
            new MediaFileType("Jpeg", "jpg", "image/jpeg", MediaType.Image),
            new MediaFileType("Gif", "gif", "image/gif", MediaType.Image),
        };
    }
}
