using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace tplink_smartplug
{
    public class TpLinkClient
    {
        /// <summary> 
        /// The amount of time in milliseconds the client will wait for a response from the device. 
        /// </summary> 
        public int ResponseTimeout { get; set; } = 100;

        private readonly TpLinkAutokeyCipher _cipher = new TpLinkAutokeyCipher();
        private readonly Encoding _encoding = Encoding.GetEncoding("ISO-8859-1");

        public async Task<string> IssueCommand(string command, string deviceIp, int devicePort)
        {
            IPAddress address;
            if (command == null) { throw new ArgumentNullException(nameof(command)); }
            if (deviceIp == null) { throw new ArgumentNullException(nameof(deviceIp)); }
            if (!IPAddress.TryParse(deviceIp, out address)) { throw new ArgumentException("Not a valid IP address"); }

            var encryptedCmd = _cipher.Encrypt(_encoding.GetBytes(command));
            var data = encryptedCmd;

            using (var tcpClient = new TcpClient(deviceIp, devicePort))
            using (var stream = tcpClient.GetStream())
            {
                tcpClient.Client.ReceiveTimeout = ResponseTimeout;

                await stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);

                var response = await ReceiveResponse(stream).ConfigureAwait(false);

                var decryptedBytes = _cipher.Decrypt(response);
                var decrypted = _encoding.GetString(decryptedBytes);
                return decrypted;
            }
        }

        private static async Task<byte[]> ReceiveResponse(NetworkStream stream)
        {
            using (var memStream = new MemoryStream())
            {
                var buffer = new byte[100];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length - 1).ConfigureAwait(false);
                while (bytesRead > 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                    bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length - 1).ConfigureAwait(false);
                }
                return memStream.ToArray();
            }
        }
    }
}