using System;

namespace MediaTagger.Core.Thumbnails
{
    public class ThumbnailGeneratedEventArgs : EventArgs
    {
        public MediaFile File { get; private set; }
        public int Index { get; private set; }
        public int TotalFiles { get; private set; }

        public ThumbnailGeneratedEventArgs(MediaFile file, int index, int totalFiles)
        {
            File = file;
            Index = index;
            TotalFiles = totalFiles;
        }
    }
}