using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediaTagger.Core.Xml
{
    public class LibraryFolderXmlConverter
    {
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
            return new LibraryFolder(element.Attribute("Path").Value);
        }

        private XElement Write(LibraryFolder folder, string nodeName)
        {
            return new XElement(nodeName,
                new XAttribute("Path", folder.Path)
            );
        }
    }
}
