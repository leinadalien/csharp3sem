using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Classes
{
    public class EmpoyeeComparer: IComparer<Classes.Employee>
    {
        public EmpoyeeComparer()
        {
        }
        public int Compare(Employee x, Employee y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
