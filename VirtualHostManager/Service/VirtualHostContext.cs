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
    class VirtualHostContext
    {
        //private string filePath = @"C:\wamp64\bin\apache\apache2.4.39\conf\extra\httpd-vhosts.conf";
        // private string filePath = @"K:\wamp64\bin\apache\apache2.4.35\conf\httpd.conf";
        private string _filePath;
        public List<VirtualHost> data { get; set; }
        public VirtualHostContext(string filePath)
        {
            _filePath = filePath;
        }

        public void setPath(string path)
        {
            _filePath = path;
        }
        
        public void SaveChanges()
        {
            StringBuilder context = new StringBuilder();
            data.ForEach(x =>
            {
                var Prefix = x.Status ? "" : "#";
                var subContext = new StringBuilder();
                subContext.Append("# Virtural Host Manager" + System.Environment.NewLine);
                subContext.Append("# Author:" + x.Author + System.Environment.NewLine);
                subContext.Append("# Url:" + x.Url + System.Environment.NewLine);
                subContext.Append("# Directory:" + x.Directory + System.Environment.NewLine);
                subContext.Append("# CreateAt:" + x.CreateAt + System.Environment.NewLine);
                subContext.Append("# UpdateAt:" + x.UpdateAt + System.Environment.NewLine);
                subContext.Append("# Description:" + x.Description + System.Environment.NewLine);
                subContext.Append("###" + System.Environment.NewLine);
                subContext.Append(string.Join(System.Environment.NewLine, x.Context.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None).Select(xx => Prefix + xx)));

                var result = subContext.ToString();
                result += System.Environment.NewLine;
                result += System.Environment.NewLine;
                result += System.Environment.NewLine;
                context.Append(result);
            });
            File.WriteAllText(_filePath, context.ToString());
        }

        public void Read()
        {
            try
            {
                string[] stringSeparators = new string[] { "\r\n" };
                Regex regex = new Regex(@"(# Virtural Host Manager(.*?)###)?((\n|\r\n)#*\s*<\s*VirtualHost[^>]*>(.*?)<\s*\/\s*VirtualHost\s*>)", RegexOptions.Singleline);
                var rawData = regex.Matches(File.ReadAllText(_filePath))
                                    .Cast<Match>()
                                    .Select(m => m.Value)
                                    .ToList();
                data = rawData.Select(x =>
                {
                    var context = Regex.Match(x, @"\n{1}#*[^\S\r\n]*<\s*VirtualHost[^>]*>(.*?)<\s*\/\s*VirtualHost\s*>", RegexOptions.Singleline).Value.Trim('\n');
                    var status = !context.StartsWith("#");
                    if (!status)
                    {
                        var a = context.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None).Select(xx => xx.Trim('#'));
                        context = string.Join("\r\n", context.Split(new[] { System.Environment.NewLine }, StringSplitOptions.None)
                                         .Select(xx => xx.Replace("#", ""))
                                         .ToList());
                    }
                    var userDeclareData = Regex.Match(x, @"# Virtural Host Manager(.*?)###", RegexOptions.Singleline).Value.Trim('\n');
                    return new VirtualHost()
                    {
                        Url = Regex.Match(x, @"ServerName(.*?)\n").Value.Replace("ServerName", "").Replace("\r\n", ""),
                        CreateAt = Regex.Match(userDeclareData, @"CreateAt:(.*?)\n").Value.Replace("CreateAt:", "").Replace("\r\n", ""),
                        Description = Regex.Match(userDeclareData, @"Description:(.*?)\n").Value.Replace("Description:", "").Replace("\r\n", ""),
                        Directory = Regex.Match(x, @"<Directory(.*?)>\s*\n").Value.Replace("<Directory", "").Replace(">", "").Replace("\r\n", "").Trim().Trim('"'),
                        Context = context,
                        Author = Regex.Match(userDeclareData, @"Author:(.*?)\n").Value.Replace("Author:", "").Replace("\r\n", ""),
                        UpdateAt = Regex.Match(userDeclareData, @"UpdateAt:(.*?)\n").Value.Replace("UpdateAt:", "").Replace("\r\n", ""),
                        ErrorLogs = Regex.Match(x, @"ErrorLog(.*?)\n").Value.Replace("ErrorLog", "").Replace("\r\n", ""),
                        Status = status
                    };
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
