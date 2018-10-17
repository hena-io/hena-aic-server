using System;
using System.Collections.Generic;

namespace Hena
{
	public static class Base58
	{
		private static readonly char[] Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz".ToCharArray();
		private static readonly int Base58Length = Alphabet.Length;
		private static readonly int[] INDEXES = new int[128];
		private const int Base256Length = 256;

		static Base58()
		{
			for (int i = 0; i < INDEXES.Length; i++)
			{
				INDEXES[i] = -1;
			}

			for (int i = 0; i < Alphabet.Length; i++)
			{
				INDEXES[Alphabet[i]] = i;
			}
		}

		public static string Encode(bool input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(short input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(int input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(long input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(ushort input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(uint input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(ulong input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(float input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(double input) { return Encode(BitConverter.GetBytes(input)); }
		public static string Encode(string input) { return Encode(System.Text.Encoding.UTF8.GetBytes(input)); }

		public static bool DecodeToBool(string input) { return BitConverter.ToBoolean(Decode(input), 0); }
		public static short DecodeToInt16(string input) { return BitConverter.ToInt16(Decode(input), 0); }
		public static int DecodeToInt32(string input) { return BitConverter.ToInt32(Decode(input), 0); }
		public static long DecodeToInt64(string input) { return BitConverter.ToInt64(Decode(input), 0); }
		public static ushort DecodeToUInt16(string input) { return BitConverter.ToUInt16(Decode(input), 0); }
		public static uint DecodeToUInt32(string input) { return BitConverter.ToUInt32(Decode(input), 0); }
		public static ulong DecodeToUInt64(string input) { return BitConverter.ToUInt64(Decode(input), 0); }
		public static float DecodeToFloat(string input) { return BitConverter.ToSingle(Decode(input), 0); }
		public static double DecodeToDouble(string input) { return BitConverter.ToDouble(Decode(input), 0); }
		public static string DecodeToString(string input) { return System.Text.Encoding.UTF8.GetString(Decode(input)); }

		/// <summary>
		/// Encodes an arbitrary buffer into a base58 encoded string.
		/// </summary>
		public static string Encode(byte[] input)
		{
			var buffer = input;

			// The actual encoding.
			char[] temp = new char[buffer.Length * 2];
			int j = temp.Length;

			int zeroCount = LeadingZerosCount(buffer);
			int startAt = zeroCount;
			while (startAt < buffer.Length)
			{
				byte mod = divmod58(buffer, startAt);
				if (buffer[startAt] == 0)
				{
					++startAt;
				}
				temp[--j] = Alphabet[mod];
			}

			// Strip extra '1' if there are some after decoding.
			while (j < temp.Length && temp[j] == Alphabet[0])
			{
				++j;
			}

			// Add as many leading '1' as there were leading zeros.
			while (--zeroCount >= 0)
			{
				temp[--j] = Alphabet[0];
			}

			return new string(temp, j, temp.Length - j);
		}

		/// <summary>
		/// Decodes a base58 encoded string into its original buffer.
		/// </summary>
		public static byte[] Decode(string input)
		{
			if (input.Length == 0)
			{
				return new byte[0];
			}

			byte[] input58 = new byte[input.Length];

			// Transform the String to a base58 byte sequence
			for (int i = 0; i < input.Length; ++i)
			{
				char c = input[i];

				int digit58 = -1;
				if (c >= 0 && c < 128)
				{
					digit58 = INDEXES[c];
				}

				if (digit58 < 0)
				{
					throw new Exception("Illegal character " + c + " at " + i);
				}

				input58[i] = (byte)digit58;
			}

			// Count leading zeroes
			int zeroCount = 0;
			while (zeroCount < input58.Length && input58[zeroCount] == 0)
			{
				++zeroCount;
			}

			// The encoding
			byte[] temp = new byte[input.Length];
			int j = temp.Length;

			int startAt = zeroCount;
			while (startAt < input58.Length)
			{
				byte mod = divmod256(input58, startAt);
				if (input58[startAt] == 0)
				{
					++startAt;
				}

				temp[--j] = mod;
			}

			// Do no add extra leading zeroes, move j to first non null byte.
			while (j < temp.Length && temp[j] == 0)
			{
				++j;
			}

			var result = new byte[temp.Length - (j - zeroCount)];
			Array.Copy(temp, j - zeroCount, result, 0, result.Length);
			return result;
		}
		
		private static int LeadingZerosCount(IReadOnlyList<byte> buffer)
		{
			int leadingZeros = 0;
			for (leadingZeros = 0; leadingZeros < buffer.Count && buffer[leadingZeros] == 0; leadingZeros++) ;
			return leadingZeros;
		}

		/// <summary>
		/// number -> number / 58, returns number % 58
		/// </summary>
		private static byte divmod58(byte[] number, int startAt)
		{
			int remainder = 0;
			for (int i = startAt; i < number.Length; i++)
			{
				int digit256 = (int)number[i] & 0xFF;
				int temp = remainder * Base256Length + digit256;

				number[i] = (byte)(temp / Base58Length);

				remainder = temp % Base58Length;
			}

			return (byte)remainder;
		}

		/// <summary>
		/// number -> number / 256, returns number % 256
		/// </summary>
		private static byte divmod256(byte[] number58, int startAt)
		{
			int remainder = 0;
			for (int i = startAt; i < number58.Length; i++)
			{
				int digit58 = (int)number58[i] & 0xFF;
				int temp = remainder * Base58Length + digit58;

				number58[i] = (byte)(temp / Base256Length);

				remainder = temp % Base256Length;
			}

			return (byte)remainder;
		}

		private static byte[] copyOfRange(byte[] buffer, int start, int end)
		{
			var result = new byte[end - start];
			Array.Copy(buffer, start, result, 0, end - start);
			return result;
		}
	}
}
