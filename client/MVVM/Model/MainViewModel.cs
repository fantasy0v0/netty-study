using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace client.MVVM.Model
{
    internal class MainViewModel : ObservableObject
    {
		private string host;

		public string Host
		{
			get { return host; }
			set { SetProperty(ref host, value); }
		}

		private int port;

		public int Port
		{
			get { return port; }
			set { SetProperty(ref port, value); }
		}

		private string deviceId;

		public string DeviceId
        {
			get { return deviceId; }
			set { SetProperty(ref deviceId, value); }
        }

        public RelayCommand Connect { get; set; }

        public MainViewModel()
		{
			Host = "127.0.0.1";
			Port = 8888;
			Connect = new RelayCommand(param =>
			{
				TcpClient client = new TcpClient(Host, Port);
				var stream = client.GetStream();
				ByteBuffer buffer = new ByteBuffer();
                // 消息类型 0 请求
                buffer.WriteByte(0);
                // 消息id
                buffer.WriteShort(1);
                // 业务类型
                buffer.WriteShort(100);
				string data = "你好吗?";
				buffer.WriteString(data);
				var bytes = buffer.Wrap();
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
                stream.Close();
				client.Close();
            }, param =>
			{
				return true;
			});
		}

	}
}
