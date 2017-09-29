using System;
using ProtoBuf;

namespace HiLoSocket.Model
{
    /// <summary>
    /// Data Model for socket transmition.
    /// </summary>
    [Serializable]
    [ProtoContract]
    public class SocketCommandModel
    {
        /// <summary>
        /// Gets or sets the name of the command.
        /// </summary>
        /// <value>
        /// The name of the command.
        /// </value>
        [ProtoMember( 1 )]
        public string CommandName { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [ProtoMember( 2 )]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        [ProtoMember( 3 )]
        public string Results { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        [ProtoMember( 4 )]
        public DateTime Time { get; set; }
    }
}