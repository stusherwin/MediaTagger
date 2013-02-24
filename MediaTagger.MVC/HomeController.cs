using System.IO;
using System.Web.Hosting;

namespace MediaTagger.Mvc
{
    public class HomeController
    {
        public string Get_()
        {
            //TODO: Work out how to get web forms view engine working
            //return File.ReadAllText(HostingEnvironment.MapPath(@"/_content/html/video.htm"));
            return File.ReadAllText(HostingEnvironment.MapPath(@"/_content/html/home.htm"));
        }
    }
}