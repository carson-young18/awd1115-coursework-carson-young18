using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project9
{
    public class Corner : ITurnable
    {
        public string Turn()
        {
            return "You turned the corner.";
        }
    }
}
