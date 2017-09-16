using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.CommandFormatter;

namespace HiLoSocket.Model
{
    public class ServerModel
    {
        public FormatterType? FormatterType { get; set; }

        [Required( ErrorMessage = "本地 IP 沒有設定喔，不知道自己的 IP 嗎？" )]
        public IPEndPoint LocalIpEndPoint { get; set; }
    }
}