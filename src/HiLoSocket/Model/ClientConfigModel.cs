using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;

namespace HiLoSocket.Model
{
    /// <summary>
    /// ConfigModel for Client constructor.
    /// </summary>
    public class ClientConfigModel
    {
        /// <summary>
        /// Gets or sets the type of the compress.
        /// </summary>
        /// <value>
        /// The type of the compress.
        /// </value>
        public CompressType? CompressType { get; set; }

        /// <summary>
        /// Gets or sets the type of the formatter.
        /// </summary>
        /// <value>
        /// The type of the formatter.
        /// </value>
        public FormatterType? FormatterType { get; set; }

        /// <summary>
        /// Gets or sets the local ip end point.
        /// </summary>
        /// <value>
        /// The local ip end point.
        /// </value>
        [Required( ErrorMessage = "本地 IP 沒有設定喔，不知道自己的 IP 嗎？" )]
        public IPEndPoint LocalIpEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the remote ip end point.
        /// </summary>
        /// <value>
        /// The remote ip end point.
        /// </value>
        [Required( ErrorMessage = "遠端 IP 沒有設定喔，不知道遠端的 IP 嗎？" )]
        public IPEndPoint RemoteIpEndPoint { get; set; }

        /// <summary>
        /// Gets or sets the time out time.
        /// </summary>
        /// <value>
        /// The time out time.
        /// </value>
        [Range( 2000, 120000, ErrorMessage = "Timeout 時間需介於 2000 ~ 120000 毫秒" )]
        public int TimeOutTime { get; set; }
    }
}