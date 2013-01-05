using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTagger.Core
{
    public class Tag
    {
        public string Name { get; private set; }

        public Tag(string name)
        {
            Name = name;
        }
    }
}
