﻿namespace HiLoSocket.Compressor
{
    public interface ICompressor
    {
        byte[ ] Compress( byte[ ] bytes );

        byte[ ] Decompress( byte[ ] bytes );
    }
}