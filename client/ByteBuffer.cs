using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        public void WriteByte(byte value)
        {
            writer.Write(value);
        }

        public void WriteShort(short value)
        {
            writer.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void WriteInt(int value)
        {
            writer.Write(BitConverter.GetBytes(IPAddress.HostToNetworkOrder(value)));
        }

        public void WriteBytes(byte[] value, int index, int count)
        {
            writer.Write(value, index, count);
        }

        public void WriteBytes(byte[] value)
        {
            writer.Write(value, 0, value.Length);
        }

        public void WriteString(string value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            WriteShort((short)buffer.Length);
            WriteBytes(buffer);
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

        /// <summary>
        /// 加上整体长度
        /// </summary>
        /// <returns></returns>
        public byte[] Wrap()
        {
            ByteBuffer buffer = new ByteBuffer();
            Flush();
            var data = ToArray();
            buffer.WriteShort((short)data.Length);
            buffer.WriteBytes(data);
            return buffer.ToArray();
        }
    }
}
