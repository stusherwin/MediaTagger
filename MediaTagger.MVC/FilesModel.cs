using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FubuMVC.Core;

namespace MediaTagger.Server
{
    public class FilesOutputModel : JsonMessage
    {
        public string[] Files;
    }
}