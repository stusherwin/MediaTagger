using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaTagger.Server
{
    public class FilesController
    {
        public FilesOutputModel Get_files()
        {
            return new FilesOutputModel
            {
                Files = new[] { "file1.jpg", "file2.jpg" }
            };
        }
    }
}