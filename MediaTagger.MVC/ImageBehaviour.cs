using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Http;
using System.Drawing;
using System.Drawing.Imaging;

namespace MediaTagger.Server
{
    public class ImageBehaviour : IActionBehavior
    {
        private readonly IFubuRequest _request;
        private readonly IHttpWriter _writer;

        public ImageBehaviour(IFubuRequest request, IHttpWriter writer)
        {
            _request = request;
            _writer = writer;
        }

        public void Invoke()
        {
            var model = _request.Get<ImageOutputModel>();

            _writer.WriteResponseCode(System.Net.HttpStatusCode.OK);

            _writer.WriteContentType(model.ContentType);
            _writer.Write(s => model.Image.Save(s, model.Format));
        }

        public void InvokePartial()
        {
            Invoke();
        }
    }
}
