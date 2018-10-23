using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hena
{
	public class FtpClient
	{
		private string Host = null;
		private string Id = null;
		private string Password = null;
		private int BufferSize = 4096;

		/* Construct Object */
		public FtpClient(string host, string id, string password, int bufferSize = 4096)
		{
			Host = host; Id = id; Password = password; BufferSize = bufferSize;
		}

		private FtpWebRequest CreateRequest(string requestUriString, string method,
			bool useBinary = true, bool usePassive = true, bool useKeepAlive = true, int timeout = 5000)
		{
			FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(requestUriString);
			ftpRequest.Credentials = new NetworkCredential(Id, Password);
			ftpRequest.Timeout = timeout;
			ftpRequest.UseBinary = useBinary;
			ftpRequest.UsePassive = usePassive;
			ftpRequest.KeepAlive = useKeepAlive;

			ftpRequest.Method = method;
			return ftpRequest;
		}

		#region Async
		/* Download File */
		public async Task<bool> DownloadAsync(string remoteFile, string localFile)
		{
			try
			{
				var ftpRequest = CreateRequest(Host + "/" + remoteFile, WebRequestMethods.Ftp.DownloadFile, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				var ftpStream = ftpResponse.GetResponseStream();

				FileStream localFileStream = new FileStream(localFile, FileMode.Create);
				byte[] byteBuffer = new byte[BufferSize];
				int bytesRead = await ftpStream.ReadAsync(byteBuffer, 0, BufferSize);
				try
				{
					while (bytesRead > 0)
					{
						localFileStream.Write(byteBuffer, 0, bytesRead);
						bytesRead = await ftpStream.ReadAsync(byteBuffer, 0, BufferSize);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.ToString());
					NLog.LogManager.GetCurrentClassLogger().Error(ex);
					return false;
				}

				localFileStream.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				Console.WriteLine(ex.ToString());
				return false;
			}
			return true;
		}
		
		/* Upload File */
		public async Task<bool> UploadAsync(string remoteFile, byte[] contents)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{remoteFile}", WebRequestMethods.Ftp.UploadFile, true, true, true);
				ftpRequest.ContentLength = contents.Length;

				Stream ftpStream = await ftpRequest.GetRequestStreamAsync();
				{
					for (int offset = 0; offset < contents.Length; offset += BufferSize)
					{
						int len = Math.Min(BufferSize, contents.Length - offset);
						await ftpStream.WriteAsync(contents, offset, len);
					}
				}
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				{
					Console.WriteLine($"Upload File Complete, status {ftpResponse.StatusDescription}");
				}
				ftpResponse.Close();
				ftpStream.Close();
				ftpRequest = null;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				NLog.LogManager.GetCurrentClassLogger().Error(ex, $"remoteFile:{remoteFile}");
				return false;
			}

			return true;
		}

		/* Upload File */
		public async Task<bool> UploadAsync(string remoteFile, string localFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{remoteFile}", WebRequestMethods.Ftp.UploadFile, true, true, true);
				
				var ftpStream = await ftpRequest.GetRequestStreamAsync();
				FileStream localFileStream = new FileStream(localFile, FileMode.Create);
				byte[] byteBuffer = new byte[BufferSize];
				int bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
				try
				{
					while (bytesSent != 0)
					{
						await ftpStream.WriteAsync(byteBuffer, 0, bytesSent);
						bytesSent = await localFileStream.ReadAsync(byteBuffer, 0, BufferSize);
					}
				}
				catch (Exception ex)
				{
					NLog.LogManager.GetCurrentClassLogger().Error(ex, $"remoteFile:{remoteFile}, localFile:{localFile}");
					Console.WriteLine(ex.ToString());
					return false;
				}

				localFileStream.Close();
				ftpStream.Close();
				ftpRequest = null;
			}
			catch (Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex, $"remoteFile:{remoteFile}, localFile:{localFile}");
				Console.WriteLine(ex.ToString());
				return false;
			}
			return true;
		}

		/* Delete File */
		public async Task<bool> DeleteAsync(string deleteFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{deleteFile}", WebRequestMethods.Ftp.DeleteFile, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) {
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Delete Dir*/
		public async Task<bool> DeleteDirAsync(string deleteFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{deleteFile}", WebRequestMethods.Ftp.RemoveDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}
		/* Rename File */
		public async Task<bool> RenameAsync(string currentFileNameAndPath, string newFileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{currentFileNameAndPath}", WebRequestMethods.Ftp.Rename, true, true, true);
				ftpRequest.RenameTo = newFileName;

				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Create a New Directory on the FTP Server */
		public async Task<bool> CreateDirectoryAsync(string newDirectory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{newDirectory}", WebRequestMethods.Ftp.MakeDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) {
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Get the Date/Time a File was Created */
		public async Task<string> GetFileCreatedDateTimeAsync(string fileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{fileName}", WebRequestMethods.Ftp.GetDateTimestamp, true, true, true);
			
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				var ftpStream = ftpResponse.GetResponseStream();
				StreamReader ftpReader = new StreamReader(ftpStream);
				string fileInfo = null;
				try { fileInfo = ftpReader.ReadToEnd(); }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return "";
		}

		/* Get the Size of a File */
		public async Task<string> GetFileSizeAsync(string fileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{fileName}", WebRequestMethods.Ftp.GetFileSize, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string fileInfo = null;
				try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				
				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return "";
		}

		/* List Directory Contents File/Folder Name Only */
		public async Task<string[]> DirectoryListSimpleAsync(string directory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{directory}", WebRequestMethods.Ftp.ListDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)await ftpRequest.GetResponseAsync();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string directoryRaw = null;
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return new string[] { "" };
		}

		/* List Directory Contents in Detail (Name, Size, Created, etc.) */
		public string[] DirectoryListDetailedAsync(string directory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{directory}", WebRequestMethods.Ftp.ListDirectoryDetails, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();
				
				StreamReader ftpReader = new StreamReader(ftpStream);
				string directoryRaw = null;
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return new string[] { "" };
		}

		#endregion // Async


		#region Sync
		public bool Download(string remoteFile, string localFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{remoteFile}", WebRequestMethods.Ftp.DownloadFile, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();

				FileStream localFileStream = new FileStream(localFile, FileMode.Create);
				byte[] byteBuffer = new byte[BufferSize];
				int bytesRead = ftpStream.Read(byteBuffer, 0, BufferSize);
				try
				{
					while (bytesRead > 0)
					{
						localFileStream.Write(byteBuffer, 0, bytesRead);
						bytesRead = ftpStream.Read(byteBuffer, 0, BufferSize);
					}
				}
				catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }

				localFileStream.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Upload File */
		public bool Upload(string remoteFile, string localFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{remoteFile}", WebRequestMethods.Ftp.UploadFile, true, true, true);
				var ftpStream = ftpRequest.GetRequestStream();

				FileStream localFileStream = new FileStream(localFile, FileMode.Create);
				byte[] byteBuffer = new byte[BufferSize];
				int bytesSent = localFileStream.Read(byteBuffer, 0, BufferSize);
				try
				{
					while (bytesSent != 0)
					{
						ftpStream.Write(byteBuffer, 0, bytesSent);
						bytesSent = localFileStream.Read(byteBuffer, 0, BufferSize);
					}
				}
				catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }

				localFileStream.Close();
				ftpStream.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Delete File */
		public bool Delete(string deleteFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{deleteFile}", WebRequestMethods.Ftp.DeleteFile, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Delete Dir*/
		public bool DeleteDir(string deleteFile)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{deleteFile}", WebRequestMethods.Ftp.RemoveDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}
		/* Rename File */
		public bool Rename(string currentFileNameAndPath, string newFileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{currentFileNameAndPath}", WebRequestMethods.Ftp.Rename, true, true, true);
				ftpRequest.RenameTo = newFileName;
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Create a New Directory on the FTP Server */
		public bool CreateDirectory(string newDirectory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{newDirectory}", WebRequestMethods.Ftp.MakeDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();

				ftpResponse.Close();
				ftpRequest = null;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); return false; }
			return true;
		}

		/* Get the Date/Time a File was Created */
		public string GetFileCreatedDateTime(string fileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{fileName}", WebRequestMethods.Ftp.GetDateTimestamp, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string fileInfo = null;
				try { fileInfo = ftpReader.ReadToEnd(); }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return "";
		}

		/* Get the Size of a File */
		public string GetFileSize(string fileName)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{fileName}", WebRequestMethods.Ftp.GetFileSize, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string fileInfo = null;
				try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return "";
		}

		/* List Directory Contents File/Folder Name Only */
		public string[] DirectoryListSimple(string directory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{directory}", WebRequestMethods.Ftp.ListDirectory, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string directoryRaw = null;
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			
			return new string[] { "" };
		}

		/* List Directory Contents in Detail (Name, Size, Created, etc.) */
		public string[] DirectoryListDetailed(string directory)
		{
			try
			{
				var ftpRequest = CreateRequest($"{Host}/{directory}", WebRequestMethods.Ftp.ListDirectoryDetails, true, true, true);
				var ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				var ftpStream = ftpResponse.GetResponseStream();

				StreamReader ftpReader = new StreamReader(ftpStream);
				string directoryRaw = null;
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }

				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;

				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return new string[] { "" };
		}
		#endregion	// Sync
	}
}
