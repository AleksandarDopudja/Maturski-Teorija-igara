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
    public partial class Form2 : Form
    {
        public Form2(int mod)
        {
            InitializeComponent();
            modIgre = mod;
        }

        Graphics g;
        int n, modIgre, komp;
        bool limitirano = true;
        tabla tb, sejv;
     
        private void Form2_MouseClick(object sender, MouseEventArgs e)
        {
            int j = e.X / tb.sirinaPolja;
            int i = e.Y / tb.sirinaPolja;
            if (e.X <= tb.Width && tb.prazno(i, j))
            {
                tb.odigrajPotez(i, j);
                if (krajIgre()) return;

                switch (modIgre)
                {
                    case 0:
                        break;
                    case 1:
                        botIgra();
                        krajIgre();
                        break;
                    case 2:
                        pametanBot();
                        krajIgre();
                        break;
                    default:
                        break;
                }
                tb.IspisStanja(listBox1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (modIgre)
            {
                case 1:
                    botIgra();
                    break;
                case 2:
                    pametanBot();
                    break;
                default:
                    break;
            }
            krajIgre();
            tb.IspisStanja(listBox1);
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            tb.IscrtavanjeTable();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            PostavkaForme();
            g = this.CreateGraphics();
            ResetTable();
        }

        public void ResetTable()
        {
            n = (int)numericUpDown1.Value;
            tb = new tabla(n, this.Height, this.Height, g);
            Refresh();
            tb.IspisStanja(listBox1);
            
        }

        public bool krajIgre()
        {
            int pobedio = tb.proveraTablice();
            if (pobedio == 0)
            {
                if (tb.BrPopunjenih == tb.N * tb.N)
                {
                    MessageBox.Show("Nereseno!");
                    ResetTable();
                    return true;
                }
                return false;
            }

            MessageBox.Show("Pobedio igrac " + ((pobedio == 1) ? "x" : "o") + "!");
            ResetTable();
            return true;
        }

        private void PostavkaForme()
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            numericUpDown1.Location = new Point(this.Width - numericUpDown1.Width - 5, 5);
            numericUpDown1.Minimum = 3;
            numericUpDown1.Value = 3;
            label1.Location = new Point(this.Width - numericUpDown1.Width - 5, 30);
            listBox1.Location = new Point(this.Width - numericUpDown1.Width - 5, 30 + 30);
            button4.Location = new Point(this.Width - button4.Width - 5, 30);
            Izlaz.Location = new Point(this.Width - Izlaz.Width - 5, 30);
            comboBox1.Location = new Point(this.Width - comboBox1.Width - 5, 30 + 160);
            button1.Location = new Point(this.Width - button4.Width - 5, 30 + button4.Height + 30);
            button2.Location = new Point(this.Width - comboBox1.Width - 5, 30 + 160 + 200);
            button3.Location = new Point(this.Width - comboBox1.Width - 5, 30 + 160 + 300);
            //button4.Location = new Point(this.Width - comboBox1.Width - 5, 30 + 160 + 300 + 100);
            listBox1.Visible = false;
            numericUpDown1.Visible = false;
            comboBox1.Visible = false;
            //button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            Izlaz.Visible = false;
            label1.Text = "";
        }

        public void botIgra() //randomBot
        {
            Random r = new Random();
            int brojPraznih = tb.N * tb.N - tb.BrPopunjenih;
            int randomPolje = Math.Abs(r.Next(0, brojPraznih));

            int i, j = 0, nadjenoI = -1, nadjenoJ = -1;
            bool nasaoNesto = false;
            for (i = 0; i < n && ((randomPolje > 0) || !nasaoNesto); i++)
                for (j = 0; j < n && ((randomPolje > 0) || !nasaoNesto); j++)
                {
                    if (tb.polje(i, j) == 0)
                    {
                        nasaoNesto = true;

                        randomPolje--;
                        if (randomPolje <= 0)
                        {
                            nadjenoI = i;
                            nadjenoJ = j;
                            break;
                        }
                    }
                }
            i = nadjenoI;
            j = nadjenoJ;

            if (!tb.odigrajPotez(i, j))
                MessageBox.Show("Greska!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form f = new Form1();
            f.Show();
            this.Close();
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.ForeColor = Color.Magenta;
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Magenta;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            button1.ForeColor = Color.Lime;
            button4.ForeColor = Color.Lime;
        }

        private void Izlaz_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void pametanBot()
        {
            komp = tb.IgracNaPotezu;
            potez p = minimax(tb, (n > 4 && limitirano) ? 3 : -1);
            tb.odigrajPotez(p.i, p.j);

        }

        potez minimax(tabla t, int dubina = -1)
        {
            int pom = t.proveraTablice();
            if (pom != 0) return new potez(-1, -1, (pom == komp) ? 10 : -10);
            if (t.puno() || dubina == 0) return new potez(-1, -1, 0);

            int trenutno = t.IgracNaPotezu;
            potez maksimum = new potez(-1, -1, -1000);
            potez minimum = new potez(-1, -1, 1000);

            for (int i = 0; i < t.N; i++)
                for (int j = 0; j < t.N; j++)
                    if (t.polje(i, j) == 0)
                    {
                        tabla kop = (tabla)t.Clone();
                        kop.G = null;
                        kop.odigrajPotez(i, j);
                        potez resenje = minimax(kop, (dubina == -1) ? -1 : dubina - 1);
                        if (trenutno == komp && resenje.vrednost > maksimum.vrednost)
                        {
                            maksimum = resenje;
                            maksimum.i = i;
                            maksimum.j = j;
                            if (maksimum.vrednost == 10) return maksimum;
                        }

                        if (trenutno != komp && resenje.vrednost < minimum.vrednost)
                        {
                            minimum = resenje;
                            minimum.i = i;
                            minimum.j = j;
                            if (minimum.vrednost == -10) return minimum;
                        }


                    }
            if (trenutno == komp)
                return maksimum;
            else
                return minimum;
        }

    }
}
