using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualHostManager.UserControls
{
    class DataGridViewDoubleBuffered : DataGridView
    {
        public DataGridViewDoubleBuffered()
        {
            DoubleBuffered = true;
        }
    }
}
