using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHostManager.Models
{
    public class VirtualHost
    {
        public string Url { set; get; }
        public string Directory { set; get; }
        public string Description { set; get; }
        public bool Status { set; get; }
        public string Context { set; get; }
        public string Author { set; get; }
        public string ErrorLogs { set; get; }
        public string CreateAt { set; get; }
        public string UpdateAt { set; get; }


    }
    public enum VirtualHostDetailType
    {
        Create,
        View,
        Edit,
    }
}
