using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class ByteBuffer
    {
        private readonly MemoryStream stream;

        private readonly BinaryWriter writer;

        public ByteBuffer()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
        }

        ~ByteBuffer()
        {
            writer.Dispose();
        }

        public byte[] ToArray()
        {
            return stream.ToArray();
        }

        public void Write(byte value)
        {
            writer.Write(value);
        }

        public void Write(short value)
        {
            writer.Write(value);
        }

        public void Write(int value)
        {
            writer.Write(value);
        }

        public void Write(string value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            writer.Write(buffer);
        }

        public void Write(ByteBuffer buffer)
        {
            buffer.Flush();
            var bytes = buffer.ToArray();
            writer.Write(bytes);
        }

        public void Flush()
        {
            writer.Flush();
        }
    }
}
