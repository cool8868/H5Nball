using System;
using System.Collections.Generic;
using System.Text;

namespace Games.NBall.Common
{
    public static class ByteReader
    {
        public static bool ReadBoolean(byte[] data, ref int offset)
        {
            var value = BitConverter.ToBoolean(data, offset);
            offset += 1;
            return value;
        }
        
        public static List<bool> ReadBooleanList(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<bool>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadBoolean(data, ref offset));
                }
            }
            return values;
        }

        public static byte ReadByte(byte[] data, ref int offset)
        {
            var value = data[offset];
            offset += 1;
            return value;
        }

        public static List<byte> ReadByteList(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<byte>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadByte(data, ref offset));
                }
            }
            return values;
        }

        public static short ReadShort(byte[] data, ref int offset)
        {
            var value = BitConverter.ToInt16(data, offset);
            offset += 2;
            return value;
        }

        public static ushort ReadUShort(byte[] data, ref int offset)
        {
            var value = BitConverter.ToUInt16(data, offset);
            offset += 2;
            return value;
        }

        public static List<short> ReadShortList(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<short>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadShort(data, ref offset));
                }
            }
            return values;
        }

        public static int ReadInt32(byte[] data, ref int offset)
        {
            var value = BitConverter.ToInt32(data, offset);
            offset += 4;
            return value;
        }

        public static List<int> ReadInt32List(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<int>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadInt32(data, ref offset));
                }
            }
            return values;
        }

        public static long ReadInt64(byte[] data, ref int offset)
        {
            var value = BitConverter.ToInt64(data, offset);
            offset += 8;
            return value;
        }

        public static List<long> ReadInt64List(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<long>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadInt64(data, ref offset));
                }
            }
            return values;
        }

        public static double ReadDouble(byte[] data, ref int offset)
        {
            var value = BitConverter.ToDouble(data, offset);
            offset += 8;
            return value;
        }

        public static List<double> ReadDoubleList(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<double>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadDouble(data, ref offset));
                }
            }
            return values;
        }

        public static string ReadString(byte[] data, ref int offset)
        {
            var stringlength = BitConverter.ToInt32(data, offset);
            offset += 4;
            if (stringlength > 0)
            {
                var value = Encoding.UTF8.GetString(data, offset, stringlength);
                offset += stringlength;
                return value;
            }
            return string.Empty;
        }

        public static List<string> ReadStringList(byte[] data, ref int offset)
        {
            var length = BitConverter.ToInt32(data, offset);
            offset += 4;
            var values = new List<string>(length);
            if (length > 0)
            {
                for (var i = 0; i < length; i++)
                {
                    values.Add(ReadString(data, ref offset));
                }
            }
            return values;
        }
    }
}