using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var library = new Library
            //(
            //    new[] 
            //    {
            //        new LibraryFolder(@"C:\Users\Stuart\AppData\Roaming\Mozilla\Firefox\Profiles\gkmv77kk.Stu\downloads")
            //    }, 
            //    new[]
            //    {
            //        new MediaFileType("Flash video", "flv", MediaType.Video),
            //        new MediaFileType("Windows media", "wmv", MediaType.Video),
            //        new MediaFileType("Mp4", "mp4", MediaType.Video),
            //        new MediaFileType("Avi", "avi", MediaType.Video),
            //        new MediaFileType("Jpeg", "jpg", MediaType.Image),
            //        new MediaFileType("Gif", "gif", MediaType.Image),
            //    }
            //);

            var library = new LibraryXmlReader().ReadFromFile("library.xml");

            foreach (var file in library.GetAllFiles(MediaType.Video, SortOrder.LastModified(OrderDirection.Descending)))
            {
                Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                    file.LastModified,
                    file.MediaFileType,
                    file.Size,
                    file.Name);
            }

            //new LibraryXmlWriter().WriteToFile(library, "library.xml");

            Console.ReadKey();
        }
    }
}
