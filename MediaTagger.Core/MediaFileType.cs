namespace MediaTagger.Core
{
    public class MediaFileType
    {
        public string Name { get; private set; }
        public string Extension { get; private set; }
        public MediaType MediaType { get; private set; }

        public MediaFileType(string name, string extension, MediaType mediaType)
        {
            Name = name;
            Extension = extension;
            MediaType = mediaType;
        }

        public override string ToString()
        {
            return Name;
        }

        public static MediaFileType[] All = new[] 
        {
            new MediaFileType("Flash video", "flv", MediaType.Video),
            new MediaFileType("Windows media", "wmv", MediaType.Video),
            new MediaFileType("Mp4", "mp4", MediaType.Video),
            new MediaFileType("Avi", "avi", MediaType.Video),
            new MediaFileType("Jpeg", "jpg", MediaType.Image),
            new MediaFileType("Gif", "gif", MediaType.Image),
        };
    }
}
