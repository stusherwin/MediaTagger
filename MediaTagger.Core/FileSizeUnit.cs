using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class FileSizeUnit
    {
        public string Symbol { get; private set; }
        public long NoOfBytes { get; private set; }

        public FileSizeUnit(string symbol, long noOfBytes)
        {
            Symbol = symbol;
            NoOfBytes = noOfBytes;
        }

        public double GetValue(long bytes)
        {
            return Math.Round((double)bytes / (double)NoOfBytes, 2);
        }

        public override string ToString()
        {
            return Symbol;
        }

        public static FileSizeUnit Bytes = new FileSizeUnit("B", 1);
        public static FileSizeUnit Kilobytes = new FileSizeUnit("KB", 1024);
        public static FileSizeUnit Megabytes = new FileSizeUnit("MB", 1024 * 1024);
        public static FileSizeUnit Gigabytes = new FileSizeUnit("GB", 1024 * 1024 * 1024);

        public static FileSizeUnit[] All = new[] { Bytes, Kilobytes, Megabytes, Gigabytes };

        public static FileSizeUnit FromBytes(long bytes)
        {
            if (bytes <= 0)
                return Megabytes;

            return All
                .OrderByDescending(u => u.NoOfBytes)
                .First(u => bytes >= u.NoOfBytes);
        }
    }
}
