using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace client.MVVM.Model
{
    internal class MainViewModel : ObservableObject, IDisposable
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

		private bool isConnected;

		public bool IsConnected
		{
			get { return isConnected; }
			set { SetProperty(ref isConnected, value); }
		}

		public RelayCommand ConnectCommand { get; set; }

		public RelayCommand CloseCommand { get; set; }

		public RelayCommand ReportCommand { get; set; }

		private Client client;

        public MainViewModel()
		{
			Host = "127.0.0.1";
			Port = 8888;

			ConnectCommand = new RelayCommand(param =>
			{
                if (null == Host || 0 == Host.Length)
                {
                    MessageBox.Show("请填写主机地址");
                    return;
                }
				if (Port < 0 || Port > 65535)
				{
                    MessageBox.Show("请填写正确的端口号");
                    return;
                }
                if (null == DeviceId || 0 == DeviceId.Length)
				{
					MessageBox.Show("请填写设备编号");
					return;
				}
                client = new Client(Host, Port);
				IsConnected = true;
            }, param =>
			{
				return !IsConnected;
			});
			CloseCommand = new RelayCommand(param =>
			{
				client?.Close();
				IsConnected = false;
			}, param =>
			{
				return IsConnected;
			});
			ReportCommand = new RelayCommand(param =>
			{
				if (null == client)
				{
					return;
				}
                BufferWriter buffer = new BufferWriter();
                // 消息类型 0 请求
                buffer.WriteByte(0);
                // 消息id
                buffer.WriteShort(1);
                // 业务类型
                buffer.WriteShort(100);
                string data = "你好吗?";
                buffer.WriteString(data);
                var bytes = buffer.Wrap();
                client.Submit(bytes);
            }, param =>
			{
				return IsConnected;
			});
		}

        public void Dispose()
        {
            client?.Close();
        }
    }
}
