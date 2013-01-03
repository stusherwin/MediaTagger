using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class FileSize
    {
        private long _bytes;

        public FileSizeUnit Unit { get { return FileSizeUnit.FromBytes(_bytes); } }
        public double Value { get { return Unit.GetValue(_bytes); } }

        public FileSize(long bytes)
        {
            _bytes = bytes;
        }

        public override string ToString()
        {
            return Value.ToString("0.#") + Unit;
        }
    }
}
