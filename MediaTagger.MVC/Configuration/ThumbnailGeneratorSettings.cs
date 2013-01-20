namespace MediaTagger.Mvc.Configuration
{
    public class ThumbnailGeneratorSettings
    {
        public string FfmpegPath { get; set; }
        public string TempFileLocation { get; set; }
        public string ThumbnailLocation { get; set; }
        public double DefaultThumbnailTimePercentage { get; set; }
    }
}