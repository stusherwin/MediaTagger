namespace MediaTagger.Core.Thumbnails
{
    public interface IThumbnailGenerator
    {
        ThumbnailImage Generate(MediaFile videoFile, ThumbnailGenerationOptions options);
    }
}
