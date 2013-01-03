using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Test
{
    public class LibraryXmlWriter
    {
        public void WriteToFile(Library library, string filePath)
        {
            var xml = new XDocument(
                new XElement("Library",
                    new XElement("FileTypes",
                        library.FileTypes.Select(t => new XElement("FileType",
                            new XAttribute("Extension", t.Extension),
                            new XAttribute("MediaType", t.MediaType),
                            new XAttribute("Name", t.Name)
                        ))
                    ),
                    new XElement("Folders", 
                        library.Folders.Select(f => new XElement("Folder", 
                            new XAttribute("Path", f.Path)
                        ))
                    ),
                    new XElement("Files", 
                        library.GetAllFiles().Select(f => new XElement("File",
                            new XAttribute("Name", f.Name),
                            new XAttribute("Path", f.Path)
                        ))
                    )
                )
            );

            xml.Save(filePath);
        }
    }
}
