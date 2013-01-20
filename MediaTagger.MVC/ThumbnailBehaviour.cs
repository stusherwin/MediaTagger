using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Http;

namespace MediaTagger.Mvc
{
    public class ThumbnailBehaviour : IActionBehavior
    {
        private readonly IFubuRequest _request;
        private readonly IHttpWriter _writer;

        public ThumbnailBehaviour(IFubuRequest request, IHttpWriter writer)
        {
            _request = request;
            _writer = writer;
        }

        public void Invoke()
        {
            var model = _request.Get<ThumbnailOutputModel>();

            _writer.WriteResponseCode(System.Net.HttpStatusCode.OK);

            _writer.WriteContentType(model.Thumbnail.Type.ContentType);
            _writer.Write(s => model.Thumbnail.Image.Save(s, model.Thumbnail.Type.Format));
        }

        public void InvokePartial()
        {
            Invoke();
        }
    }
}
