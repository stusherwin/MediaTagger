using System.Linq;
using MediaTagger.Core;
using MediaTagger.Core.FileSystem;
using MediaTagger.Core.Thumbnails;
using MediaTagger.Core.Xml;

namespace MediaTagger.Console
{
    class Program
    {
        private const string FFMPEG_PATH = @"C:\MediaTagger\ffmpeg.exe";
        private const string LIBRARY_FILE = @"C:\MediaTagger\Library.xml";
        private const string THUMBNAIL_LOCATION = @"C:\MediaTagger\Thumbnails";
        private const string THUMBNAIL_BASE_LOCATION = @"C:\MediaTagger\Thumbnails\Base";
        private const string THUMBNAIL_TEMP_LOCATION = @"C:\MediaTagger\Thumbnails\Temp";

        private static readonly ThumbnailGenerationOptions THUMBNAIL_OPTIONS = new ThumbnailGenerationOptions(
            new PointInTime(new Percentage(25)), 
            ImageType.Png, 
            new ImageSize(500, 375));

        private static readonly FfmpegWrapper _ffmpeg = new FfmpegWrapper(FFMPEG_PATH);
        private static readonly FileManager _fileManager = new FileManager();

        private static readonly LibraryFolderLoader _libraryFolderLoader = new LibraryFolderLoader(
            _fileManager,
            _ffmpeg,
            new MediaFileIdGenerator());

        private static readonly SizedThumbnailGenerator _sizedThumbnailGenerator = new SizedThumbnailGenerator(
            _fileManager,
            THUMBNAIL_LOCATION,
            new BaseThumbnailGenerator(
                _fileManager,
                THUMBNAIL_BASE_LOCATION,
                new FfmpegThumbnailGenerator(
                    _ffmpeg,
                    _fileManager,
                    THUMBNAIL_TEMP_LOCATION)));

        private static readonly LibraryXmlConverter _xmlConverter = new LibraryXmlConverter(
            new LibraryFolderXmlConverter(
                new MediaFileXmlConverter(
                    new TagXmlConverter())));

        static void Main(string[] args)
        {
            var library = _xmlConverter.ReadFromFile(LIBRARY_FILE);

            library.AddFolder(@"C:\MediaTagger\Files", _libraryFolderLoader);

            _xmlConverter.WriteToFile(library, LIBRARY_FILE);

            System.Console.WriteLine();
            System.Console.ReadKey();

            var generator = new BulkThumbnailGenerator(library, _sizedThumbnailGenerator);

            generator.ThumbnailGenerated += (_, e) =>
                System.Console.WriteLine("Generating thumbnail {1} of {2}: \"{0}\"", e.File.Name, e.Index, e.TotalFiles);

            generator.GenerateThumbnails(THUMBNAIL_OPTIONS);

            System.Console.WriteLine();
            System.Console.WriteLine("Done");
            System.Console.ReadKey();
        }
    }
}
