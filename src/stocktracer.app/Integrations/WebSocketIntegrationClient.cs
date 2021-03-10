using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace stocktracer.app.Integrations
{
    public class WebSocketIntegrationClient
    {
        public delegate void ReceiveMessageDelegate(byte[] message);
        public delegate void ChangeStateForConnectedDelegate();

        public event ChangeStateForConnectedDelegate OnConnected;
        public event ReceiveMessageDelegate OnReceiveMessage;
        private readonly ClientWebSocket websocket;
        private readonly Uri websocketUrl;
        private readonly CancellationToken currentCancellationToken;
        private Task receiveTask;

        public WebSocketIntegrationClient(ClientWebSocket websocket, Uri websocketUrl, CancellationToken cancellationToken)
        {
            this.websocket = websocket;
            this.websocketUrl = websocketUrl;
            currentCancellationToken = cancellationToken;
        }

        /// <summary>
        /// Connect to the websocket and begin yielding messages
        /// received from the connection.
        /// </summary>
        public async Task ConnectAsync()
        {
            await websocket.ConnectAsync(websocketUrl, currentCancellationToken);

            if (websocket.State == WebSocketState.Open)
            {
                this.receiveTask = ReceiveMessageBackgroundAsync();
                this.InvokeOnConnected();
                return;
            }

            throw new InvalidOperationException("The websocket cannot be opened for unknown reason.");
        }

        async private Task ReceiveMessageBackgroundAsync()
        {
            while (!currentCancellationToken.IsCancellationRequested)
            {
                WebSocketReceiveResult result;
                var buffer = new ArraySegment<byte>(new byte[2048]);
                using (var ms = new MemoryStream())
                {
                    do
                    {
                        
                        result = await websocket.ReceiveAsync(buffer, currentCancellationToken);
                        ms.Write(buffer.Array, buffer.Offset, result.Count);
                    } while (!result.EndOfMessage);

                    ms.Seek(0, SeekOrigin.Begin);

                    InvokeOnReceiveMessage(ms.ToArray());
                }

                if (result.MessageType == WebSocketMessageType.Close)
                    break;
            }
        }


        private void InvokeOnConnected()
        {
            OnConnected?.Invoke();
        }

        private void InvokeOnReceiveMessage(byte[] message)
        {
            OnReceiveMessage?.Invoke(message);
        }

        /// <summary>
        /// Send a message on the websocket.
        /// This method assumes you've already connected via ConnectAsync
        /// </summary>
        public Task SendStringAsync(string data, CancellationToken cancellation)
        {
            var encoded = Encoding.UTF8.GetBytes(data);
            var buffer = new ArraySegment<byte>(encoded, 0, encoded.Length);
            return websocket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, cancellation);
        }

        public void Dispose()
        {
            this.websocket.Dispose();
        }
    }
}