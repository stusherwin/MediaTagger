using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using FubuMVC.Core.Behaviors;
using System.Drawing.Imaging;

namespace MediaTagger.Server
{
    public class ImageController
    {
        public ImageOutputModel get_Image_FileName(ImageInputModel model)
        {
            var image = Image.FromFile(@"C:\Users\Stuart\Documents\" + model.FileName);

            return new ImageOutputModel(image, ImageFormat.Png, "image/png");
        }
    }
}