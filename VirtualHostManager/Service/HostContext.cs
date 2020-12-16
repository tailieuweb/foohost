using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VirtualHostManager.Models;

namespace VirtualHostManager
{
    class HostContext
    {
        //private string filePath = @"C:\wamp64\bin\apache\apache2.4.39\conf\extra\httpd-vhosts.conf";
        // private string filePath = @"K:\wamp64\bin\apache\apache2.4.35\conf\httpd.conf";
        private string _filePath;
        public List<Hosts> data { get; set; }
        public HostContext()
        {
            _filePath = @"C:\Windows\System32\drivers\etc\hosts";
        }
        
        public void SaveChanges()
        {
            var lines = new List<string>();
            data.ForEach(x =>
            {
                var line = string.Format("{0} {1} {2}", x.IpAddress, x.DomainName, x.Comment == "" ? "" : "#" + x.Comment);
                if (!x.Status)
                {
                    line = "#" + line;
                }
                lines.Add(line);
            });
            File.WriteAllLines(_filePath, lines);
        }

        public void Read()
        {
            data = new List<Hosts>();
            try
            {
                var lines = File.ReadAllLines(_filePath);
                for (var i = 0; i < lines.Length; i += 1)
                {
                    Regex regex = new Regex(@"#?(\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3})\s*(.*?)\s*(#.*)?$", RegexOptions.Singleline);

                    if (regex.IsMatch(lines[i]))
                    {
                        var line = lines[i].Trim();
                        var host = new Hosts()
                        {
                            Status = !line.StartsWith("#"),
                            IpAddress = Regex.Match(line, @"\d{0,3}\.\d{0,3}\.\d{0,3}\.\d{0,3}").Value,
                            DomainName = Regex.Match(line, @"\s(.*?)(\s|$)").Value.Trim(),
                            Comment = Regex.Match(line, @"\s#(.*?)$").Value.Trim().TrimStart('#')
                        };
                        data.Add(host);
                    }
                    // Process line
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
