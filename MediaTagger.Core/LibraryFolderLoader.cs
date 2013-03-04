using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using MediaTagger.Core.FileSystem;
using MediaTagger.Core.Thumbnails;

namespace MediaTagger.Core
{
    public class LibraryFolderLoader
    {
        private readonly FfmpegWrapper _ffmpeg;
        private readonly FileManager _fileManager;
        private readonly MediaFileIdGenerator _idGenerator;

        public LibraryFolderLoader(FileManager fileManager, FfmpegWrapper ffmpeg, MediaFileIdGenerator idGenerator)
        {
            _ffmpeg = ffmpeg;
            _fileManager = fileManager;
            _idGenerator = idGenerator;
        }

        public LibraryFolder Load(string folderPath)
        {
            var files = LoadFiles(folderPath, MediaFileType.All.OfType(MediaType.Video));

            return new LibraryFolder(folderPath, files);
        }

        private IEnumerable<MediaFile> LoadFiles(string folderPath, IEnumerable<MediaFileType> fileTypes)
        {
            var extensions = fileTypes.Select(ft => ft.Extension);

            return _fileManager
                .GetAllFileData(folderPath, extensions)
                .Select(f => LoadMediaFile(f));
        }

        private MediaFile LoadMediaFile(FileData fileData)
        {
            return new MediaFile(
                _idGenerator.GetNextId(),
                fileData.Name,
                fileData.Path,
                fileData.LastModified,
                fileData.Size,
                GetDuration(fileData.Path),
                MediaFileType.All.FindByExtension(fileData.Extension),
                new List<Tag>());
        }

        public Duration GetDuration(string filePath)
        {
            return _ffmpeg.GetDuration(filePath);
        }
    }
}