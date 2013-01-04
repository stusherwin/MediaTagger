using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MediaTagger.Server
{
    public class ImageOutputModel
    {
        public Image Image;
        public ImageFormat Format;
        public string ContentType;

        public ImageOutputModel(Image image, ImageFormat format, string contentType)
        {
            Image = image;
            Format = format;
            ContentType = contentType;
        }
    }
}