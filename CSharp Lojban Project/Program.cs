using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Local_Jbovlaste
{
    class Program
    {
        static void Main(string[] args)
        {
            var jbovlaste = new Jbovlaste.Dictionary("banjupunu.xml");
            var result = (from valsi in jbovlaste
                         orderby valsi.Selmaho
                          select valsi.Selmaho).Distinct();
            foreach (var selmaho in result)
            {
                System.Console.WriteLine(selmaho);
            }
        }
    }
}
