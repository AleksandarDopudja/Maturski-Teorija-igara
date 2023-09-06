using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace zavRad
{
    class tabla : ICloneable
    {
        int n, width, height, brPopunjenih, igracNaPotezu;
        int[,] stanje;
        Graphics g;

        public tabla(int n, int width, int height, Graphics g) : this(n, width, height, g, new int[n, n], 1, 0)
        {
        }
        public tabla(int n, int width, int height, Graphics g, int[,] st, int igracNaPotezu, int brPopunjenih)
        {
            this.n = n;
            this.width = width;
            this.height = height;
            this.brPopunjenih = brPopunjenih;
            this.igracNaPotezu = igracNaPotezu;
            this.g = g;
            stanje = (int[,])st.Clone();
        }

        public object Clone()
        {
            return new tabla(n, width, height, g, stanje, igracNaPotezu, brPopunjenih);
        }

        public int N
        {
            get { return n; }
            set { n = value; }
        }
        public Graphics G
        {
            get { return g; }
            set { g = value; }
        }
        public int BrPopunjenih
        {
            get { return brPopunjenih; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public int sirinaPolja
        {
            get { return width / n; }
        }
        public int IgracNaPotezu
        {
            get { return igracNaPotezu; }
        }

        public int[,] StanjeTable
        {
            get { return stanje; }
            set { stanje = (int[,])value.Clone(); }
        }

        public void IscrtavanjeTable()
        {
            if (g == null)
                return;
            Pen p = new Pen(Color.Lime, 5);
            for (int i = 1; i < n; i++)
            {
                g.DrawLine(p, new Point(sirinaPolja * i, 0), new Point(sirinaPolja * i, height));
                g.DrawLine(p, new Point(0, sirinaPolja * i), new Point(width, sirinaPolja * i));
            }
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    crtajPolje(i, j, stanje[i, j]);
        }

        public void IspisStanja(ListBox l)
        {
            if (g == null)
                return;
            l.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                string s = "";
                for (int j = 0; j < n; j++)
                    s += stanje[i, j] + " ";
                l.Items.Add(s);
            }
        }

        public bool prazno(int i, int j)
        {
            return (stanje[i, j] == 0);
        }

        public void crtajPotez(int i, int j)
        {
            if (g == null)
                return;
            crtajPolje(i, j, igracNaPotezu);

        }
        private void crtajPolje(int i, int j, int igracNaPotezu)
        {
            if (g == null)
                return;
            Pen p = new Pen(Color.Lime, 3);
            SolidBrush b = new SolidBrush(Color.White);
            if (igracNaPotezu == 0)
                return;
            if (igracNaPotezu == 1)
            {
                g.DrawLine(p, new Point(j * sirinaPolja + sirinaPolja / 5, i * sirinaPolja + sirinaPolja / 5), new Point(j * sirinaPolja + sirinaPolja / 5 + sirinaPolja - sirinaPolja / 5 * 2, i * sirinaPolja + sirinaPolja / 5 + sirinaPolja - sirinaPolja / 5 * 2));
                g.DrawLine(p, new Point(j * sirinaPolja + sirinaPolja / 5 + sirinaPolja - sirinaPolja / 5 * 2, i * sirinaPolja + sirinaPolja / 5), new Point(j * sirinaPolja + sirinaPolja / 5, i * sirinaPolja + sirinaPolja / 5 + sirinaPolja - sirinaPolja / 5 * 2));
            }
            else { g.DrawEllipse(p, j * sirinaPolja + sirinaPolja / 5, i * sirinaPolja + sirinaPolja / 5, sirinaPolja - sirinaPolja / 5 * 2, sirinaPolja - sirinaPolja / 5 * 2); }
        }

        private bool provera(int i, int j, int smer, int brojac, int znak)
        {
            if (brojac == 0)
                return true;
            if (i < 0 || j < 0 || i >= n || j >= n || znak != stanje[i, j])
                return false;
            j += (smer < 2) ? 1 : 0;
            i += (smer > 0) ? 1 : 0;
            j -= (smer == 3) ? 1 : 0;
            return provera(i, j, smer, brojac - 1, znak);
        }

        public int proveraTablice()
        {
            bool rez = false;
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    if (stanje[i, j] != 0)
                    {
                        for (int x = 0; (!rez) && (x < 4); x++)
                            rez = rez || provera(i, j, x, 3, stanje[i, j]);

                        if (rez)
                            return stanje[i, j];
                    }
                }
            return 0;

        }

        public bool odigrajPotez(int i, int j)
        {
            if (!prazno(i, j))
                return false;

            stanje[i, j] = igracNaPotezu;
            brPopunjenih++;
            crtajPotez(i, j);
            igracNaPotezu = (igracNaPotezu % 2) + 1;
            return true;
        }

        public int polje(int i, int j)
        {
            return stanje[i, j];
        }

        public bool nereseno()
        {
            return (proveraTablice() == 0 && puno());
        }

        public bool puno()
        {
            return (brPopunjenih == n * n);
        }
    }
}
