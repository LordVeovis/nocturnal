using Meziantou.Framework.Win32;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kveer.Nocturnal
{
	public partial class Form1 : Form
	{
		[DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword,
	   int dwLogonType, int dwLogonProvider, out SafeAccessTokenHandle phToken);

		internal static SafeAccessTokenHandle _token;

		public Form1()
		{
			InitializeComponent();

			// fill autocomplete combobox with all computers from main domain
			cbxComputerName.Items.AddRange(LDAPHelper.GetAllComputersOnDomain());

			// initialize button text with username
			var currentUser = WindowsIdentity.GetCurrent();

			var isDomainAdmin = currentUser.Groups.OfType<SecurityIdentifier>().Any(c => c.IsWellKnown(WellKnownSidType.AccountDomainAdminsSid));

			if (!isDomainAdmin)
			{
				var creds = CredentialManager.PromptForCredentials(messageText: "Active Directory");

				const int LOGON32_PROVIDER_DEFAULT = 0;
				//This parameter causes LogonUser to create a primary token.   
				const int LOGON32_LOGON_INTERACTIVE = 2;

				// Call LogonUser to obtain a handle to an access token.   
				SafeAccessTokenHandle safeAccessTokenHandle;
				bool returnValue = LogonUser(creds.UserName, creds.Domain, creds.Password,
					LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT,
					out safeAccessTokenHandle);

				if (!returnValue)
                {
					throw new Exception("logon user error");
                }
				_token = safeAccessTokenHandle;

				//var p = new Process();
				//var pi = new ProcessStartInfo(Process.GetCurrentProcess().MainModule.FileName);
				//pi.Verb = "runas";
				//pi.UseShellExecute = true;
				//p.StartInfo = pi;
				//var res = p.Start();

				//Application.Exit();
			}
			button1.Text += currentUser.Name;
		}

		private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
		{
			var comp = (Computer)cbxComputerName.SelectedItem;
			var hostname = comp.CommonName;

			var bkeys = LDAPHelper.GetBitlockerKeys(hostname);

			_bitlockerKeys = bkeys;
			cbxBitlockerKey.Items.Clear();
			cbxBitlockerKey.Items.AddRange(_bitlockerKeys.Keys.Keys.ToArray());

			// bitlocker interface
			switch (_bitlockerKeys.Keys.Count)
			{
				case 0: cbxBitlockerKey.Enabled = false; break;
				case 1:
					cbxBitlockerKey.Enabled = true;
					cbxBitlockerKey.SelectedIndex = 0;
					break;
				default:
					cbxBitlockerKey.Enabled = true;
					tbxBitlockerKey.Text = "";
					break;

			}

			// rdp interface
			var computerNameExists = comp != null;
			button1.Enabled = computerNameExists;
			button2.Enabled = computerNameExists && comp.HasAdminPwd;
		}

		private ADBitlockerKeys _bitlockerKeys;

		private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
		{
			tbxBitlockerKey.Text = _bitlockerKeys.Keys[(string)cbxBitlockerKey.SelectedItem];
		}

		private void textBox1_Enter(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(tbxBitlockerKey.Text))
				Clipboard.SetText(tbxBitlockerKey.Text);
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			var hostname = ((Computer)cbxComputerName.SelectedItem).FQDN;
			await RDPHelper.ConnectAsMyself(hostname);
		}

		private async void button2_Click(object sender, EventArgs e)
		{
			var comp = (Computer)cbxComputerName.SelectedItem;
			var pwd = LDAPHelper.GetLocalAdminPassword(comp.CommonName);
			await RDPHelper.ConnectAsLocalAdmin(comp.FQDN, "admin-it", pwd);
		}
	}
}
