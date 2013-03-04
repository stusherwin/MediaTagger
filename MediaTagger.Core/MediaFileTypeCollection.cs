using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaTagger.Core
{
    public class MediaFileTypeCollection
    {
        private readonly MediaFileType[] _fileTypes;

        public MediaFileTypeCollection(MediaFileType[] fileTypes)
        {
            _fileTypes = fileTypes;
        }

        public MediaFileTypeCollection(params MediaFileType[][] fileTypes)
        {
            _fileTypes = fileTypes.SelectMany(ft => ft).ToArray();
        }

        public MediaFileType FindByExtension(string extension)
        {
            if (!extension.StartsWith("."))
                extension = "." + extension;

            var found = _fileTypes.FirstOrDefault(mt => mt.Extension == extension);

            if (found == null)
                throw new ArgumentException(String.Format("Media file type with extension \"{0}\" not found", extension));

            return found;
        }

        public MediaFileType FindByName(string name)
        {
            var found = _fileTypes.FirstOrDefault(mt => mt.Name == name);

            if (found == null)
                throw new ArgumentException(String.Format("Media file type with name \"{0}\" not found", name));

            return found;
        }

        public IEnumerable<MediaFileType> OfType(MediaType mediaType)
        {
            return _fileTypes.Where(mt => mt.MediaType == mediaType);
        } 
    }
}