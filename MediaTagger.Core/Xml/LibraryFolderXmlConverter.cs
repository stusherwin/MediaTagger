using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediaTagger.Core.Xml
{
    public class LibraryFolderXmlConverter
    {
        MediaFileXmlConverter _fileConverter;

        public LibraryFolderXmlConverter(MediaFileXmlConverter fileConverter)
        {
            _fileConverter = fileConverter;
        }

        public LibraryFolder[] ReadChildren(XContainer parent, string childNodeName)
        {
            return parent.Descendants(childNodeName)
                .Select(t => Read(t))
                .ToArray();
        }

        public XElement[] WriteChildren(IEnumerable<LibraryFolder> items, string childNodeName)
        {
            return items
                .Select(i => Write(i, childNodeName))
                .ToArray();
        }

        private LibraryFolder Read(XElement element)
        {
            var path = element.Attribute("Path").Value;
            var files = _fileConverter.ReadChildren(element, "File");

            return new LibraryFolder(path, files);
        }

        private XElement Write(LibraryFolder folder, string nodeName)
        {
            return new XElement(nodeName,
                new XAttribute("Path", folder.Path),
                _fileConverter.WriteChildren(folder.Files, "File")
            );
        }
    }
}
