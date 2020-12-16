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

            var fileStream = new FileStream(Application.UserAppDataPath + fileName, FileMode.Truncate, FileAccess.ReadWrite);

            var serializeData = JsonConvert.SerializeObject(data);
            byte[] info = new UTF8Encoding(true).GetBytes(serializeData);
            fileStream.Write(info, 0, info.Length);

            fileStream.Close();
        }
        public void Save(string fileName, string data)
        {

            var fileStream = new FileStream(Application.UserAppDataPath + fileName, FileMode.Truncate, FileAccess.ReadWrite);

            byte[] info = new UTF8Encoding(true).GetBytes(data);
            fileStream.Write(info, 0, info.Length);

            fileStream.Close();
        }
        public T Read<T>(string fileName)
        {
            if(!File.Exists(Application.UserAppDataPath + fileName))
            {
                File.Create(Application.UserAppDataPath + fileName).Close();
            }
            using (StreamReader r = new StreamReader(Application.UserAppDataPath + fileName))
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
