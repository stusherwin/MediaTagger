using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediaTagger.Core.Xml
{
    public class TagXmlConverter
    {
        public Tag[] ReadChildren(XContainer parent, string childNodeName)
        {
            return parent.Descendants(childNodeName)
                .Select(t => Read(t))
                .ToArray();
        }

        public XElement[] WriteChildren(IEnumerable<Tag> items, string childNodeName)
        {
            return items
                .Select(i => Write(i, childNodeName))
                .ToArray();
        }

        private Tag Read(XElement element)
        {
            return new Tag(
                element.Attribute("Name").Value
            );
        }

        private XElement Write(Tag item, string nodeName)
        {
            return new XElement(nodeName,
                new XAttribute("Name", item.Name)
            );
        }
    }
}
