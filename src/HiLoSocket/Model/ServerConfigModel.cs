using System.ComponentModel.DataAnnotations;
using System.Net;
using HiLoSocket.CommandFormatter;
using HiLoSocket.Compressor;

namespace HiLoSocket.Model
{
    /// <summary>
    /// ConfigModel for Server constructor.
    /// </summary>
    public class ServerConfigModel
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
    }
}