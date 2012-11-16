using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Test
{
    public class LibraryXmlReader
    {
        public Library ReadFromFile(string filePath)
        {
            var xml = XDocument.Load(filePath);

            var folders = xml.Descendants("Folder")
                .Select(f => new LibraryFolder(f.Attribute("Path").Value))
                .ToArray();

            var fileTypes = xml.Descendants("FileType")
                .Select(t =>
                    new MediaFileType(
                        t.Attribute("Name").Value,
                        t.Attribute("Extension").Value,
                        (MediaType)Enum.Parse(typeof(MediaType), t.Attribute("MediaType").Value)))
                .ToArray();

            return new Library(folders, fileTypes);
        }
    }
}
