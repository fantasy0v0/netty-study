using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class StreamHelper
    {
        public static byte[] GetBytes(Stream stream, int count)
        {
            byte[] data = new byte[count];
            stream.Read(data, 0, count);
            return data;
        }

        public static BufferReader GetBufferReader(Stream stream, int count)
        {
            return new BufferReader(GetBytes(stream, count));
        }
    }
}
