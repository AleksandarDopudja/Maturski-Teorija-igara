using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zavRad
{
    class potez
    {
        public int i;
        public int j;
        public int vrednost;

        public potez(int i, int j, int vrednost)
        {
            this.i = i;
            this.j = j;
            this.vrednost = vrednost;
        }

        public override string ToString()
        {
            return vrednost.ToString() + "(" + i.ToString() + "," + j.ToString() + ")";
        }
    }
}
