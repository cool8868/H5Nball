using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace Games.NBall.Common
{
    public class CompressHelper
    {
        public static byte[] Compress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream gzs = new DeflateStream(ms, CompressionMode.Compress, false))
                {
                    gzs.Write(bytes, 0, bytes.Length);
                }
                ms.Close();
                return ms.ToArray();
            }
        }

        public static byte[] Decompress(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes, false))
            {
                using (DeflateStream gzs = new DeflateStream(ms, CompressionMode.Decompress, false))
                {
                    using (MemoryStream dest = new MemoryStream())
                    {
                        byte[] tmp = new byte[bytes.Length];
                        int read;
                        while ((read = gzs.Read(tmp, 0, tmp.Length)) != 0)
                        {
                            dest.Write(tmp, 0, read);
                        }
                        dest.Close();
                        return dest.ToArray();
                    }
                }
            }
        }
    }
}
