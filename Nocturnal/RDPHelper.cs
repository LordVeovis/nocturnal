using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Kveer.Nocturnal
{
	internal class RDPHelper
	{
		internal static async Task InternalConnect(string rdpFile)
		{
			var proc = Process.Start(Environment.ExpandEnvironmentVariables(@"%windir%\System32\mstsc.exe"), $"\"{rdpFile}\" /remoteGuard");

			await Task.Delay(5000);
			if (File.Exists(rdpFile))
				File.Delete(rdpFile);
		}

		public static async Task ConnectAsMyself(string fqdn)
		{
			var tmpFile = Path.GetTempFileName();

			using (var f = File.CreateText(tmpFile))
			{
				await f.WriteLineAsync($"full address:s:{fqdn}");
				await f.WriteLineAsync($"username:s:{Environment.UserName}");
				await f.WriteLineAsync($"domain:s:{Environment.UserDomainName}");
				await f.WriteLineAsync($"authentication level:i:2");
			}

			await InternalConnect(tmpFile);
		}

		public static async Task ConnectAsLocalAdmin(string fqdn, string username, string password)
		{
			var tmpFile = Path.GetTempFileName();

			var encPwd = ProtectedData.Protect(GetBytes(password), null, DataProtectionScope.CurrentUser);
			var hexEncPwd = GetHex(encPwd);

			//var dp = new Crypt32Wrapper(Crypt32Wrapper.Store.USE_MACHINE_STORE);
			//byte[] e = dp.Encrypt(GetBytes(password), null, "psw");
			//var pwd = GetHex(e);

			using (var f = File.CreateText(tmpFile))
			{
				await f.WriteLineAsync($"full address:s:{fqdn}");
				await f.WriteLineAsync($"username:s:{Environment.UserName}");
				await f.WriteLineAsync($"domain:s:{Environment.UserDomainName}");
				await f.WriteLineAsync($"authentication level:i:2");
				await f.WriteLineAsync($"password 51:b:{hexEncPwd}");
			}

			await InternalConnect(tmpFile);
		}

		static byte[] GetBytes(string text)
		{
			return UnicodeEncoding.Unicode.GetBytes(text);
		}

		private static string GetHex(byte[] byt_text)
		{
			string ret = string.Empty;

			for (int i = 0; i < byt_text.Length; i++)
			{
				ret += Convert.ToString(byt_text[i], 16).PadLeft(2, '0').ToUpper();
			}

			return ret;
		}
	}
}
