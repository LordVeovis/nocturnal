using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;
using System.Security.Principal;

namespace Kveer.Nocturnal
{
    internal class Computer
    {
        public string CommonName { get; set; }

        public string FQDN { get; set; }

        public bool HasAdminPwd { get; set; }

        public override string ToString()
        {
            return CommonName;
        }
    }

    internal static class LDAPHelper
    {
        /// <summary>
        /// Gets the LDAP path for the default domain.
        /// </summary>
        /// <returns></returns>
        static string GetCurrentDomainPath()
        {
            using (var de = new DirectoryEntry("LDAP://RootDSE"))
                return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

        /// <summary>
        /// Fetches all computers in the current domain.
        /// </summary>
        /// <returns></returns>
        public static Computer[] GetAllComputersOnDomain()
        {
            var rootDSE = GetCurrentDomainPath();
            using (var directorySearcher = new DirectorySearcher(rootDSE))
            {
                directorySearcher.Filter = $"(&(ObjectCategory=computer)(operatingSystem=Windows*))";
                directorySearcher.PropertiesToLoad.Add("cn");
                directorySearcher.PropertiesToLoad.Add("dNSHostName");
                directorySearcher.PropertiesToLoad.Add("ms-Mcs-AdmPwdExpirationTime");

                using (var res = directorySearcher.FindAll())
                {
                    var matchingComputers = res
                        .OfType<SearchResult>()
                        .Where(o => o.Properties["dNSHostName"].Count > 0)
                        //.Select(o => o.Properties["dNSHostName"][0])
                        //.Cast<string>()
                        .Select(o => new Computer
                        {
                            CommonName = (string)o.Properties["cn"][0],
                            FQDN = (string)o.Properties["dNSHostName"][0],
                            HasAdminPwd = o.Properties["ms-Mcs-AdmPwdExpirationTime"].Count != 0
                        })
                        .OrderBy(o => o.CommonName)
                        .ToArray();

                    return matchingComputers;
                }
            }
        }

        public static string GetLocalAdminPassword(string cn)
        {
            var res = WindowsIdentity.RunImpersonated(
            Form1._token,
            () => {
                var rootDSE = GetCurrentDomainPath();
                using (var directorySearcher = new DirectorySearcher(rootDSE))
                {
                    directorySearcher.Filter = $"(&(ObjectCategory=computer)(cn={cn}))";
                    directorySearcher.PropertiesToLoad.Add("cn");
                    directorySearcher.PropertiesToLoad.Add("ms-Mcs-AdmPwd");

                    var res = directorySearcher.FindOne();

                    return (string)res.Properties["ms-Mcs-AdmPwd"][0];
                }
            });

            return res;
        }

        public static ADBitlockerKeys GetBitlockerKeys(string cn)
        {
            var rootDSE = GetCurrentDomainPath();
            SearchResult ldapComputerName;
            var bkeys = new ADBitlockerKeys(cn);

            WindowsIdentity.RunImpersonated(
                Form1._token,
                () =>
                {
                    using (var dsearcher = new DirectorySearcher(rootDSE))
                    {
                        dsearcher.Filter = "(&(ObjectCategory=computer)(cn=" + cn + "))";
                        ldapComputerName = dsearcher.FindOne();
                    }

                    SearchResultCollection btKeys;
                    var rpath = ldapComputerName.Path;
                    using (var dsearcher = new DirectorySearcher(rpath)
                    {
                        SearchRoot = ldapComputerName.GetDirectoryEntry(), //without this line we get every entry in AD.
                        Filter = "(objectClass=msFVE-RecoveryInformation)"
                    })
                    {
                        dsearcher.PropertiesToLoad.Add("msfve-recoveryguid");
                        dsearcher.PropertiesToLoad.Add("msfve-recoverypassword");

                        btKeys = dsearcher.FindAll();
                    }

                    foreach (SearchResult item in btKeys)
                    {
                        if (item.Properties.Contains("msfve-recoveryguid") && item.Properties.Contains("msfve-recoverypassword"))
                        {
                            var pid = (byte[])item.Properties["msfve-recoveryguid"][0];
                            var rky = item.Properties["msfve-recoverypassword"][0].ToString();

                            bkeys.AddKey(pid, rky);
                        }
                    }
                });

            return bkeys;
        }
    }

    public class ADBitlockerKeys
    {
        public ADBitlockerKeys(string hostname)
        {
            SystemName = hostname;
            Keys = new Dictionary<string, string>();
        }

        public void AddKey(byte[] id, string key)
        {
            Keys.Add(ConvertID(id), key);
        }

        private static string ConvertID(byte[] id) =>
              id[3].ToString("X02") + id[2].ToString("X02")
            + id[1].ToString("X02") + id[0].ToString("X02") + "-"
            + id[5].ToString("X02") + id[4].ToString("X02") + "-"
            + id[7].ToString("X02") + id[6].ToString("X02") + "-"
            + id[8].ToString("X02") + id[9].ToString("X02") + "-"
            + id[10].ToString("X02") + id[11].ToString("X02")
            + id[12].ToString("X02") + id[13].ToString("X02")
            + id[14].ToString("X02") + id[15].ToString("X02");

        public string SystemName { get; }

        public IDictionary<string, string> Keys { get; private set; }
    }
}
