using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Watermark_Empower
{
    public class TryDoubleBufferAgain : PictureBox
    {
        public TryDoubleBufferAgain()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Refresh();
            base.OnMouseMove(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Edit below to actually  draw a usefull rectangle
            e.Graphics.DrawRectangle(System.Drawing.Pens.Transparent, new System.Drawing.Rectangle(0, 0, Cursor.Position.X, Cursor.Position.Y));
        }
    }
}
