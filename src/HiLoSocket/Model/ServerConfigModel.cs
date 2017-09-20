using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;

namespace HiLoSocket.Model
{
    public class ServerConfigModel
    {
        public CompressType? CompressType { get; set; }
        public FormatterType? FormatterType { get; set; }

        [Required( ErrorMessage = "本地 IP 沒有設定喔，不知道自己的 IP 嗎？" )]
        public IPEndPoint LocalIpEndPoint { get; set; }
    }
}