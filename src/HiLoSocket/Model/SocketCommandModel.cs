using System;

namespace HiLoSocket.Model
{
    [Serializable]
    public class SocketCommandModel
    {
        public string CommandName { get; set; }
        public Guid Id { get; set; }
        public object Results { get; set; }
        public DateTime Time { get; set; }
    }
}