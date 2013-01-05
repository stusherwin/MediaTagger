using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediaTagger.Core.Xml
{
    public class LibraryXmlConverter
    {
        LibraryFolderXmlConverter _folderConverter;
        MediaFileXmlConverter _fileConverter;

        public LibraryXmlConverter(LibraryFolderXmlConverter folderConverter, MediaFileXmlConverter fileConverter)
        {
            _folderConverter = folderConverter;
            _fileConverter = fileConverter;
        }

        public Library ReadFromFile(string filePath)
        {
            var xml = XDocument.Load(filePath);

            var folders = _folderConverter.ReadChildren(xml, "Folder");
            var files = _fileConverter.ReadChildren(xml, "File");

            return new Library(folders, files);
        }

        public void WriteToFile(Library library, string filePath)
        {
            var xml = new XDocument(
                new XElement("Library",
                    new XElement("Folders",
                        _folderConverter.WriteChildren(library.Folders, "Folder")
                    ),
                    new XElement("Files",
                        _fileConverter.WriteChildren(library.GetAllFiles(), "File")
                    )
                )
            );

            xml.Save(filePath);
        }
    }
}
