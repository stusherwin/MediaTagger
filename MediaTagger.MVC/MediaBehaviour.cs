using System.IO;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Http;
using FubuMVC.Core.Runtime;

namespace MediaTagger.Mvc
{
    public class MediaBehaviour : IActionBehavior
    {
        private const int BUFFER_SIZE = 0x40000;
        private readonly IFubuRequest _request;
        private readonly IHttpWriter _writer;

        public MediaBehaviour(IFubuRequest request, IHttpWriter writer)
        {
            _request = request;
            _writer = writer;
        }

        public void Invoke()
        {
            var model = _request.Get<MediaOutputModel>();

            _writer.WriteResponseCode(System.Net.HttpStatusCode.OK);

            _writer.WriteContentType(model.File.MediaFileType.ContentType);

            _writer.Write(s =>
            {
                using(var fileStream = File.OpenRead(model.File.Path))
                {
                    Copy(fileStream, s, BUFFER_SIZE);
                }
            });
        }

        public void InvokePartial()
        {
            Invoke();
        }

        private static void Copy(Stream source, Stream dest, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            var bytes = 0;

            while ((bytes = source.Read(buffer, 0, buffer.Length)) != 0)
                dest.Write(buffer, 0, bytes);
        }
    }
}