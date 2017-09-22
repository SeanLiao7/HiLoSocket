using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;

namespace HiLoSocket.Model
{
    public class ClientConfigModel
    {
        public CompressType? CompressType { get; set; }
        public FormatterType? FormatterType { get; set; }

        [Required( ErrorMessage = "本地 IP 沒有設定喔，不知道自己的 IP 嗎？" )]
        public IPEndPoint LocalIpEndPoint { get; set; }

        [Required( ErrorMessage = "遠端 IP 沒有設定喔，不知道遠端的 IP 嗎？" )]
        public IPEndPoint RemoteIpEndPoint { get; set; }

        [Range( 2000, 120000, ErrorMessage = "Timeout 時間需介於 2000 ~ 120000 毫秒" )]
        public int TimeOutTime { get; set; }
    }
}