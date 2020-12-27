using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualHostManager.Service;

namespace VirtualHostManager.Extensions
{
    internal static class VirualHostTemplateReadExtension
    {
        public static string VirualHostTemplateRead(this DataStorageService dataStorage, string fileName)
        {
            var result = dataStorage.Read<string>(fileName);
            if (string.IsNullOrEmpty(result))
            {
                result = @"<VirtualHost *:80>
  ServerName localhost
  ServerAlias localhost
  DocumentRoot ""${INSTALL_DIR}/www""
  < Directory ""${INSTALL_DIR}/www/"">
     Options + Indexes + Includes + FollowSymLinks + MultiViews
    AllowOverride All
    Require local
  </ Directory>
</VirtualHost>";
            }
            return result;
        }
    }
}
