using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debug
{
   public class Example
    {
        public Example()
        {


        }

        public string PropertyDriver { get; set; }
        public string PropertyPassenger { get; set; }

    }

    public class ExampleCopy : ExampleSimply
    {
        public ExampleCopy(string test): base(test)
        {

        }

        public string PropertyMDrv { get; set; }
        public string PropertyFllr { get; set; }

    }

    public class ExampleSimply
    {

        public ExampleSimply(string test)
        {

        }

        public string PropertyDriver { get; set; }
        public string PropertyPassenger { get; set; }

    }
}
