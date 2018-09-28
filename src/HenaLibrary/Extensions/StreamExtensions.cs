using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public static class StreamExtensions
	{
		public static MemoryStream ToMemoryStream(this Stream stream)
		{
			MemoryStream newStream = new MemoryStream();
			stream.CopyTo(newStream);
			return newStream;
		}
		
		public static byte[] GZipCompress(this Stream stream, CompressionLevel compressionLevel = CompressionLevel.Optimal)
		{
			byte[] compressed = null;
			MemoryStream outStream = new MemoryStream();
			try
			{
				using (GZipStream compressStream = new GZipStream(outStream, compressionLevel))
				{
					stream.CopyTo(compressStream);
				}
			}
			catch (Exception) { }
			finally
			{
				compressed = outStream.ToArray();
				outStream.Close();
			}
			return compressed;
		}

		public static byte[] GZipDecompress(this Stream stream)
		{
            DateTime beginTime = DateTime.UtcNow;
			byte[] decompressed = null;
			MemoryStream outStream = new MemoryStream();
			try
			{
				using (GZipStream decompressStream = new GZipStream(stream, CompressionMode.Decompress))
				{
					decompressStream.CopyTo(outStream);
                    decompressStream.Close();
                }
			}
			catch (Exception) { }
			finally
			{
				decompressed = outStream.ToArray();
				outStream.Close();
			}
            DateTime endTime = DateTime.UtcNow;
            TimeSpan offset = endTime - beginTime;
            return decompressed;
		}
	}
}
