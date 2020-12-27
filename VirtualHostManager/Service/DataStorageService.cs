using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualHostManager.Service
{
    class DataStorageService
    {
        private string _basePath = Path.Combine(Application.UserAppDataPath);
        public DataStorageService()
        {
        }

        public void Save(string fileName, object data)
        {
            var file = Path.Combine(Application.UserAppDataPath, fileName);
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            var fileStream = new FileStream(file, FileMode.Truncate, FileAccess.ReadWrite);

            var serializeData = JsonConvert.SerializeObject(data);
            byte[] info = new UTF8Encoding(true).GetBytes(serializeData);
            fileStream.Write(info, 0, info.Length);

            fileStream.Close();
        }
        public void Save(string fileName, string data)
        {
            var file = Path.Combine(Application.UserAppDataPath, fileName);

            var fileStream = new FileStream(file, FileMode.Truncate, FileAccess.ReadWrite);

            byte[] info = new UTF8Encoding(true).GetBytes(data);
            fileStream.Write(info, 0, info.Length);

            fileStream.Close();
        }
        public T Read<T>(string fileName)
        {
            var file = Path.Combine(Application.UserAppDataPath, fileName);
            if (!File.Exists(file))
            {
                File.Create(file).Close();
            }
            using (StreamReader r = new StreamReader(file))
            {
                string rs = r.ReadToEnd();
                if(typeof(T) == typeof(String))
                {
                    return (T)(object)rs;
                }
                return JsonConvert.DeserializeObject<T>(rs);
            }
        }
    }
}
