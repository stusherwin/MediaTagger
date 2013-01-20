using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MediaTagger.Core.Xml
{
    public class MediaFileXmlConverter
    {
        TagXmlConverter _tagConverter;

        public MediaFileXmlConverter(TagXmlConverter tagConverter)
        {
            _tagConverter = tagConverter;
        }

        public MediaFile[] ReadChildren(XContainer parent, string childNodeName)
        {
            return parent.Descendants(childNodeName)
                .Select(t => Read(t))
                .ToArray();
        }

        public XElement[] WriteChildren(IEnumerable<MediaFile> items, string childNodeName)
        {
            return items
                .Select(i => Write(i, childNodeName))
                .ToArray();
        }

        private MediaFile Read(XElement element)
        {
            return new MediaFile(
                int.Parse(element.Attribute("Id").Value),
                element.Attribute("Name").Value,
                element.Attribute("Path").Value,
                DateTime.Parse(element.Attribute("LastModified").Value),
                new FileSize(long.Parse(element.Attribute("Size").Value)),
                Duration.FromTimeSpanString(element.Attribute("Duration").Value),
                MediaFileType.All.FirstOrDefault(t => t.Name == element.Attribute("Type").Value),
                _tagConverter.ReadChildren(element, "Tag")
            );
        }

        private XElement Write(MediaFile item, string nodeName)
        {
            return new XElement(nodeName,
                new XAttribute("Id", item.Id),
                new XAttribute("Name", item.Name),
                new XAttribute("Path", item.Path),
                new XAttribute("LastModified", item.LastModified),
                new XAttribute("Size", item.Size.Bytes),
                new XAttribute("Duration", item.Duration.ToString()),
                new XAttribute("Type", item.MediaFileType.Name),
                _tagConverter.WriteChildren(item.Tags, "Tag")
            );
        }
    }
}
