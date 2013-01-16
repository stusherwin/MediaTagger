using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MediaTagger.Core
{
    public class LibraryFolder
    {
        public string Path { get; private set; }

        public LibraryFolder(string path)
        {
            Path = path;
        }
    }
}
