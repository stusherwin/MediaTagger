namespace MediaTagger.Core
{
    public class VideoType : MediaFileType
    {
        public VideoType(string name, string extension, string contentType)
            : base(name, extension, contentType, MediaType.Video)
        {
        }

        public static VideoType Flv = new VideoType("Flash video", ".flv", "video/x-flv");
        public static VideoType Wmv = new VideoType("Windows media", ".wmv", "video/x-ms-wmv");
        public static VideoType Mp4 = new VideoType("Mp4", ".mp4", "video/mp4");
        public static VideoType Avi = new VideoType("Avi", ".avi", "video/avi");

        public static new VideoType[] All = new[] { Flv, Wmv, Mp4, Avi };
    }
}