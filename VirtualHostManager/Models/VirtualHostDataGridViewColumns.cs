using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualHostManager.Models
{
    class VirtualHostDataGridViewColumns
    {
        public bool Url { set; get; }
        public bool Directory { set; get; }
        public bool Description { set; get; }
        public bool Status { set; get; }
        public bool Author { set; get; }
        public bool ErrorLogs { set; get; }
        public bool CreateAt { set; get; }
        public bool UpdateAt { set; get; }
    }
}
