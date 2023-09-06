using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zavRad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        private void Form1_Click(object sender, EventArgs e)
        {            
            
        }

        Graphics gr;
        int modIgre = 2;
        private void Form1_Load(object sender, EventArgs e)
        {
            PostavkaForme();
            gr = this.CreateGraphics();
            timer1.Enabled = true;
        }

        private void PostavkaForme()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            button1.Width = this.Width / 5;
            button1.Height = this.Height / 10;
            button2.Width = this.Width / 5;
            button2.Height = this.Height / 10;
            button3.Width = this.Width / 5;
            button3.Height = this.Height / 10;
            button1.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 3);
            button2.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 3 + button1.Height + 30);
            button3.Location = new Point(this.Width / 2 - button1.Width / 2, this.Height / 3 + 2*button1.Height + 2*30);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (t)
            {
                modIgre = 0;
                Restart();
            }
            else
            {
                Form f = new Form2(modIgre);
                f.Show();
                this.Hide();
            }
        }

        bool g = false;
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!g) button1.ForeColor = Boja();
            g = true;
        }

        private Color Boja()
        {
            Color b = Color.Black;
            Random r = new Random();
            int br = r.Next(1, 4);
            if (br == 1) b = Color.Red;
            else if (br == 2) b = Color.Yellow;
            else if (br == 3) b = Color.Magenta;
            else b = Color.Aqua;

            return b;
        }

        private void Restart()
        {
            button1.Text = "Igraj";
            button2.Text = "Mod igre";
            button3.Text = "Izlaz";
            t = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Lime;
            button2.ForeColor = Color.Lime;
            button3.ForeColor = Color.Lime;
            if (g) g = false;
            if (g1) g1 = false;
            if (g2) g2 = false;
        }

        bool g1 = false;
        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!g1) button2.ForeColor = Boja();
            g1 = true;
        }

        bool g2 = false;
        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            if (!g2) button3.ForeColor = Boja();
            g2 = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(!t) Application.Exit();
            modIgre = 2;
            Restart();           
        }

        bool t = false;
        private void button2_Click(object sender, EventArgs e)
        {
            if (t)
            {
                modIgre = 1;
                Restart();
            }
            else
            {
                button1.Text = "PvP";
                button2.Text = "Glupi bot";
                button3.Text = "Pametni bot";
                t = true;
            }
        }

        bool k = true;
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Pen p = new Pen(Boja(), 3);
            Random r = new Random();
            int rr = r.Next(20, 80);
            if (!k) { gr.DrawEllipse(p, e.X - rr / 2, e.Y - rr / 2, rr, rr); k = true; }
            else
            {
                gr.DrawLine(p, e.X - rr, e.Y - rr, e.X + rr, e.Y + rr);
                gr.DrawLine(p, e.X - rr, e.Y + rr, e.X + rr, e.Y - rr);
                k = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
        }
    }
}
