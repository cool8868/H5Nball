using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Games.NBall.Common
{
    public static class ByteWriter
    {
        public static void WriteTo(BinaryWriter writer, int value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<int> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, ushort value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, short value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<short> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, long value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<long> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, double value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<double> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, bool value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<bool> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, byte value)
        {
            writer.Write(value);
        }

        public static void WriteTo(BinaryWriter writer, List<byte> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var buffer = Encoding.UTF8.GetBytes(value);
                writer.Write(buffer.Length);
                writer.Write(buffer);
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, List<string> values)
        {
            if (values != null)
            {
                writer.Write(values.Count);
                foreach (var value in values)
                {
                    WriteTo(writer, value);
                }
            }
            else
            {
                writer.Write(0);
            }
        }

        public static void WriteTo(BinaryWriter writer, Guid value)
        {
            WriteTo(writer,value.ToString());
        }
    }
}