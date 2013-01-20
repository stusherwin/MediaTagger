namespace MediaTagger.Core
{
    public interface IThumbnailGenerator
    {
        ThumbnailImage Generate(MediaFile videoFile, Duration thumbnailDuration);
    }
}
