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
            var result = from valsi in jbovlaste
                         where valsi.Notes == null
                         select valsi;
            foreach (Jbovlaste.Valsi valsi in result)
            {
                System.Console.WriteLine(valsi.Word);
            }
            System.Console.WriteLine(jbovlaste["cidjrbentou"].Notes);
            jbovlaste["cidjrbentou"] = jbovlaste["cidja"];
            foreach (var item in jbovlaste["cidro"].Notes)
            {
                System.Console.WriteLine(item);
            }
            System.Console.WriteLine(jbovlaste["cidjrbentou"].Notes);
        }
    }
}
