using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace MediaTagger.Core
{
    public class FfmpegWrapper
    {
        private string _ffmpegPath;

        public FfmpegWrapper(string ffmpegPath)
        {
            _ffmpegPath = ffmpegPath;
        }

        public void CreateThumbnailImage(string videoPath, Duration thumbnailTime, string thumbnailImagePath)
        {
            var arguments = "-i \"" + videoPath + "\" -ss " + thumbnailTime.Value + " -f image2 -vframes 1 \"" + thumbnailImagePath + "\"";

            Ffmpeg(arguments);
        }

        public Duration GetDuration(string videoPath)
        {
            var arguments = "-i \"" + videoPath + "\"";

            string output = Ffmpeg(arguments);
            var match = Regex.Match(output, @"Duration\: (\d+\:\d+\:\d+\.\d+),");
            if (!match.Success || match.Groups[1] == null || String.IsNullOrEmpty(match.Groups[1].Value))
                return Duration.Zero;

            return Duration.FromTimeSpanString(match.Groups[1].Value);
        }

        private string Ffmpeg(string arguments)
        {
            var p = new Process();

            p.StartInfo.FileName = _ffmpegPath;
            p.StartInfo.Arguments = arguments;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardError = true;
            p.Start();

            string output = p.StandardError.ReadToEnd();
            p.WaitForExit();

            return output;
        }
    }
}
