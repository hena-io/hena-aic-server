using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Hena
{
    public static class PasswordUtility
    {
        public static string HashPassword(string password)
        {
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public static bool VerifyPassword(string password, string hash)
		{
			try
			{
				return BCrypt.Net.BCrypt.Verify(password, hash);
			}
			catch(Exception ex)
			{
				NLog.LogManager.GetCurrentClassLogger().Error(ex);
				return false;
			}
		}

		private static string GenPassword(string email, string password)
		{
			string[] splitEmails = email.Trim().Split('@');
			string id = splitEmails.Length == 0 ? string.Empty : splitEmails[0];
			byte[] encrypt = AESUtility.AES_Encrypt(Encoding.UTF8.GetBytes(id + password + email.Length.ToString()), Encoding.UTF8.GetBytes(email + password.Length.ToString()));
			return Convert.ToBase64String(encrypt);
		}
    }
}
