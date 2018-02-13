using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ram.SAPI
{
    class DrawIcon
    {
        Form1 form;
        Size size;
        Color cpucolor;
        Color memcolor;
        Color foncolor;
        Icon cursor;
        Bitmap cursorBitmap;
        Rectangle rect;
        int tip;
        int fontsize;
        Font font;


        public DrawIcon(Form1 f)
        {
            form = f;
            tip = 0;
            fontsize = 12;
            size = new Size(32, 32);
            font = new Font("Consola", fontsize);
            foncolor = Color.White;
            cpucolor = Color.FromArgb(244, 107, 10);
            memcolor = Color.FromArgb(19, 138, 185);     
            rect = new Rectangle(0, 0, size.Width, size.Height);
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);


        public void setCpuColor(Color cpu)
        {
            cpucolor = cpu;
        }

        public void setMemColor(Color mem)
        {
            memcolor = mem;
        }
        public void setColor(Color cpu,Color mem)
        {
            cpucolor = cpu;
            memcolor = mem;
        }

        public void setFonColor(Color fon)
        {
            foncolor = fon;
        }

        public void setTip(int t)
        {
            tip = t;
        }

        public int getTip()
        {
            return tip;
        }

        public void upFontSize()
        {
            fontsize++;
            font = new Font(font.FontFamily,fontsize);
        }

        public void downFontSize()
        {
            fontsize--;
            font = new Font(font.FontFamily, fontsize);
        }

        public void setFont(Font f)
        {
            font = f;
        }

        public void SetTaskIconDynamic(string number,string mem)
        { 
            //graphics.DrawImage(this.notifyIcon1.Icon.ToBitmap(), rect);
            cursorBitmap = new Bitmap(size.Width, size.Height);
            Graphics graphics = Graphics.FromImage(cursorBitmap);
            graphics.Clear(Color.FromArgb(0, 0, 0 ,0));
            graphics.ResetClip();

            float cpuh = rect.Height*float.Parse(number) / 100;
            int cpudy = Convert.ToInt32(cpuh);


            float memh = rect.Height * float.Parse(mem) / 100;
            int memdy = Convert.ToInt32(memh);

            graphics.FillRectangle(new SolidBrush(cpucolor), new Rectangle(rect.Width / 2, rect.Height - cpudy, rect.Width / 2, cpudy));
            graphics.FillRectangle(new SolidBrush(memcolor), new Rectangle(0, rect.Height - memdy, rect.Width / 2, memdy));

            if (tip == 0)
            {
                graphics.DrawString(number, font, new SolidBrush(foncolor), new Rectangle(0, 0, rect.Width, rect.Height), new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                });
            }
            else if (tip == 1)
            {
                graphics.DrawString(mem, font, new SolidBrush(foncolor), new Rectangle(0, 0, rect.Width, rect.Height), new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                });
            }
            else
            {

            }
            //生成Icon
            
            cursor = Icon.FromHandle(cursorBitmap.GetHicon());
            graphics.Dispose();
            cursorBitmap.Dispose();
            
            //更新任务栏图标样式
            form.notifyIcon1.Icon = cursor;
            DestroyIcon(cursor.Handle);
        }
    }
}
