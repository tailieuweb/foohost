using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualHostManager.Forms
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        }

        protected Panel blurPanel()
        {
            // take a screenshot of the form and darken it:
            Bitmap bmp = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            using (Graphics G = Graphics.FromImage(bmp))
            {
                G.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                G.CopyFromScreen(this.PointToScreen(new Point(0, 0)), new Point(0, 0), this.ClientRectangle.Size);
                double percent = 0.60;
                Color darken = Color.FromArgb((int)(255 * percent), Color.Black);
                using (Brush brsh = new SolidBrush(darken))
                {
                    G.FillRectangle(brsh, this.ClientRectangle);
                }
            }
            // put the darkened screenshot into a Panel and bring it to the front:
            Panel p = new Panel();
            p.Location = new Point(0, 0);
            p.Size = this.ClientRectangle.Size;
            p.BackgroundImage = bmp;
            this.Controls.Add(p);
            p.BringToFront();

            return p;
        }
    }
}
