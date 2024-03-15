using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class BufferReader
    {
        private readonly MemoryStream stream;

        private readonly BinaryReader reader;

        public BufferReader(byte[] buffer)
        {
            stream = new MemoryStream(buffer);
            reader = new BinaryReader(stream);
        }

        ~BufferReader()
        {
            reader.Dispose();
            stream.Dispose();
        }

        public byte ReadByte()
        {
            return reader.ReadByte();
        }

        public short ReadShort()
        {
            return IPAddress.NetworkToHostOrder(reader.ReadInt16());
        }

        public string ReadString()
        {
            short length = ReadShort();
            byte[] bytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
