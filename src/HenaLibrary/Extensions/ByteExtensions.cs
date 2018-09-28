using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Hena
{
	public static class ByteExtensions
	{
		public static int CheckSum(this byte[] bytes, int startIndex = 0)
		{
			int t = 0;
			for (int i = bytes.Length - 1; i >= startIndex; --i) { t += bytes[i]; }
			return t;
		}

		#region Compression
		public static byte[] GZipCompress(this byte[] buffer, CompressionLevel compressionLevel = CompressionLevel.Optimal)
		{
			MemoryStream stream = new MemoryStream(buffer);
			byte[] compressed = stream.GZipCompress(compressionLevel);
			stream.Close();
			return compressed;
		}

		public static byte[] GZipDecompress(this byte[] buffer)
		{
			MemoryStream stream = new MemoryStream(buffer);
			var decompressed = stream.GZipDecompress();
			stream.Close();
			return decompressed;
		}
		#endregion // Compression

		#region ToBytes
		public static byte[] ToBytes(this bool value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this char value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this short value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this int value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this long value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this ushort value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this uint value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this ulong value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this float value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this double value)
		{
			return BitConverter.GetBytes(value);
		}

		public static byte[] ToBytes(this string value)
		{
			return Encoding.Default.GetBytes(value);
		}
		#endregion // ToBytes

		#region To Variables
		public static bool ToBoolean(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToBoolean(value, startIndex);
		}

		public static char ToChar(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToChar(value, startIndex);
		}

		public static double ToDouble(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToDouble(value, startIndex);
		}

		public static short ToInt16(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToInt16(value, startIndex);
		}

		public static int ToInt32(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToInt32(value, startIndex);
		}

		public static long ToInt64(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToInt64(value, startIndex);
		}

		public static float ToSingle(this byte[] value, int startIndex = 0)
		{
			return BitConverter.ToSingle(value, startIndex);
		}

		public static string ToString(this byte[] value)
		{
			return BitConverter.ToString(value);
		}

		public static string ToString(this byte[] value, int startIndex)
		{
			return BitConverter.ToString(value, startIndex);
		}

		public static string ToString(this byte[] value, int startIndex, int length)
		{
			return BitConverter.ToString(value, startIndex, length);
		}

		public static ushort ToUInt16(this byte[] value, int startIndex)
		{
			return BitConverter.ToUInt16(value, startIndex);
		}

		public static uint ToUInt32(this byte[] value, int startIndex)
		{
			return BitConverter.ToUInt32(value, startIndex);
		}

		public static ulong ToUInt64(this byte[] value, int startIndex)
		{
			return BitConverter.ToUInt64(value, startIndex);
		}
		#endregion // To Variables

		#region Encoding
		// 
		public static byte[] GetBytes(Encoding dstEncoding, string value)
		{
			return Encoding.Convert(Encoding.Default, dstEncoding, Encoding.Default.GetBytes(value));
		}

		public static byte[] GetASCIIBytes(string value) { return GetBytes(Encoding.ASCII, value); }
		public static byte[] GetBigEndianUnicodeBytes(string value) { return GetBytes(Encoding.BigEndianUnicode, value); }
		public static byte[] GetUnicodeBytes(string value) { return GetBytes(Encoding.Unicode, value); }
		public static byte[] GetUTF32Bytes(string value) { return GetBytes(Encoding.UTF32, value); }
		public static byte[] GetUTF7Bytes(string value) { return GetBytes(Encoding.UTF7, value); }
		public static byte[] GetUTF8Bytes(string value) { return GetBytes(Encoding.UTF8, value); }

		//
		public static string ToString(Encoding dstEncoding, byte[] value)
		{
			return dstEncoding.GetString(value);
		}

		public static string ToString(Encoding dstEncoding, byte[] value, int startIndex)
		{
			return dstEncoding.GetString(value, startIndex, value.Length - startIndex);
		}

		public static string ToString(Encoding dstEncoding, byte[] value, int startIndex, int length)
		{
			return dstEncoding.GetString(value, startIndex, length);
		}

		public static string ToASCIIString(byte[] value) { return ToString(Encoding.ASCII, value); }
		public static string ToASCIIString(byte[] value, int startIndex) { return ToString(Encoding.ASCII, value, startIndex); }
		public static string ToASCIIString(byte[] value, int startIndex, int length) { return ToString(Encoding.ASCII, value, startIndex, length); }

		public static string ToBigEndianUnicodeString(byte[] value) { return ToString(Encoding.BigEndianUnicode, value); }
		public static string ToBigEndianUnicodeString(byte[] value, int startIndex) { return ToString(Encoding.BigEndianUnicode, value, startIndex); }
		public static string ToBigEndianUnicodeString(byte[] value, int startIndex, int length) { return ToString(Encoding.BigEndianUnicode, value, startIndex, length); }

		public static string ToUnicodeString(byte[] value) { return ToString(Encoding.Unicode, value); }
		public static string ToUnicodeString(byte[] value, int startIndex) { return ToString(Encoding.Unicode, value, startIndex); }
		public static string ToUnicodeString(byte[] value, int startIndex, int length) { return ToString(Encoding.Unicode, value, startIndex, length); }

		public static string ToUTF32String(byte[] value) { return ToString(Encoding.UTF32, value); }
		public static string ToUTF32String(byte[] value, int startIndex) { return ToString(Encoding.UTF32, value, startIndex); }
		public static string ToUTF32String(byte[] value, int startIndex, int length) { return ToString(Encoding.UTF32, value, startIndex, length); }

		public static string ToUTF7String(byte[] value) { return ToString(Encoding.UTF7, value); }
		public static string ToUTF7String(byte[] value, int startIndex) { return ToString(Encoding.UTF7, value, startIndex); }
		public static string ToUTF7String(byte[] value, int startIndex, int length) { return ToString(Encoding.UTF7, value, startIndex, length); }

		public static string ToUTF8String(byte[] value) { return ToString(Encoding.UTF8, value); }
		public static string ToUTF8String(byte[] value, int startIndex) { return ToString(Encoding.UTF8, value, startIndex); }
		public static string ToUTF8String(byte[] value, int startIndex, int length) { return ToString(Encoding.UTF8, value, startIndex, length); }
		#endregion
	}
}
