using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Bespoke.Infrastructure.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static T Clone<T>(this T source)
        {
            using (var ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                binaryFormatter.Serialize(ms, source);
                ms.Seek(0, SeekOrigin.Begin);

                return (T) binaryFormatter.Deserialize(ms);
            }
        }

        public static byte[] GetBytes(this string input)
        {
            var bytes = new byte[input.Length*sizeof (char)];
            Buffer.BlockCopy(input.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string GetString(this byte[] bytes)
        {
            var chars = new char[bytes.Length/sizeof (char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}