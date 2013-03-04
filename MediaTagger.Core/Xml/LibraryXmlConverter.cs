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

        public LibraryXmlConverter(LibraryFolderXmlConverter folderConverter)
        {
            _folderConverter = folderConverter;
        }

        public Library ReadFromFile(string filePath)
        {
            var xml = XDocument.Load(filePath);

            var folders = _folderConverter.ReadChildren(xml, "Folder");

            return new Library(folders);
        }

        public void WriteToFile(Library library, string filePath)
        {
            var xml = new XDocument(
                new XElement("Library",
                    new XElement("Folders",
                        _folderConverter.WriteChildren(library.Folders, "Folder")
                    )
                )
            );

            xml.Save(filePath);
        }
    }
}
