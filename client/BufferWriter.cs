using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    internal class BufferWriter
    {
        private readonly MemoryStream stream;

        private readonly BinaryWriter writer;

        private readonly BinaryReader reader;

        public BufferWriter()
        {
            stream = new MemoryStream();
            writer = new BinaryWriter(stream);
            reader = new BinaryReader(stream);
        }

        ~BufferWriter()
        {
            writer.Dispose();
            stream.Dispose();
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

        public void WriteBuffer(BufferWriter buffer)
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
            BufferWriter buffer = new BufferWriter();
            Flush();
            var data = ToArray();
            buffer.WriteShort((short)data.Length);
            buffer.WriteBytes(data);
            return buffer.ToArray();
        }
    }
}
