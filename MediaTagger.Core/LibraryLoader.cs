using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MediaTagger.Core
{
    public class LibraryLoader
    {
        private FfmpegWrapper _ffmpeg;

        public LibraryLoader(FfmpegWrapper ffmpeg)
        {
            _ffmpeg = ffmpeg;
        }

        public Library Load(LibraryFolder[] folders, MediaType mediaType)
        {
            var fileTypes = MediaFileType.All.Where(ft => ft.MediaType == mediaType);

            var files = fileTypes
                .SelectMany(ft =>
                    folders.SelectMany(lf => new DirectoryInfo(lf.Path).GetFiles("*." + ft.Extension, SearchOption.AllDirectories).Select(f => new
                    {
                        Type = ft,
                        FileInfo = f
                    })))
                .Select((f, i) => CreateMediaFile(f.FileInfo, f.Type, i))
                .ToArray();

            return new Library(folders, files);
        }

        private MediaFile CreateMediaFile(FileInfo fileInfo, MediaFileType mediaFileType, int id)
        {
            return new MediaFile(
                id,
                System.IO.Path.GetFileNameWithoutExtension(fileInfo.Name),
                fileInfo.FullName,
                fileInfo.LastWriteTime,
                new FileSize(fileInfo.Length),
                GetDuration(fileInfo.FullName),
                mediaFileType,
                new List<Tag>()
            );
        }

        public TimeSpan GetDuration(string filePath)
        {
            return _ffmpeg.GetDuration(filePath);
        }
    }
}
