using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace client
{
    class Task
    {
        public byte[] data;

        public TaskCompletionSource<object> promise;
    }

    internal class Client
    {
        private readonly TcpClient _client;

        private volatile bool exited = false;

        private readonly ConcurrentStack<Task> stack = new ConcurrentStack<Task>();

        public Client(string host, int port)
        {
            _client = new TcpClient(host, port);
            var t = new Thread(Run)
            {
                IsBackground = true
            };
            t.Start(this);
        }

        ~Client()
        {
            Close();
        }

        private static void Run(object param)
        {
            Client client = (Client)param;
            var stream = client._client.GetStream();
            // -1 还未读取长度信息
            // 0 已经读取长度信息, 等待接受足够的长度
            short status = -1;
            // 消息整体长度
            short msgLength = -1;
            while (!client.exited)
            {
                // 处理写队列
                if (stream.CanWrite && !client.stack.IsEmpty)
                {
                    if (client.stack.TryPop(out Task task))
                    {
                        byte[] data = task.data;
                        stream.Write(data, 0, data.Length);
                        task.promise.SetResult(null);
                    }
                }
                // 处理读队列
                if (stream.CanRead && stream.DataAvailable)
                {
                    // 读取消息头
                    if (-1 == status && client._client.Available >= 2)
                    {
                        var reader = StreamHelper.GetBufferReader(stream, 2);
                        status = 0;
                        msgLength = reader.ReadShort();
                    }
                    else if (0 == status && client._client.Available >= msgLength)
                    {
                        // 读取后续的消息体
                        var reader = StreamHelper.GetBufferReader(stream, 2);
                        // TODO 通知
                        status = -1;
                        msgLength = -1;
                    }
                }
                Thread.Sleep(1);
            }
            client._client.Close();
        }

        public Task<object> Submit(byte[] data)
        {
            var promise = new TaskCompletionSource<object>();
            stack.Push(new Task
            {
                data = data,
                promise = promise
            });
            return promise.Task;
        }

        public void Close()
        {
            exited = true;
            if (null != _client && _client.Connected)
            {
                _client.Close();
            }
        }
    }

}
