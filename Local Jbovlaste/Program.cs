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
            //var result = from valsi in jbovlaste
            //             where valsi.Notes == null
            //             select valsi;
            //foreach (Valsi valsi in result)
            //{
            //    System.Console.WriteLine(valsi.Word);
            //}
            System.Console.WriteLine(jbovlaste["cidjrbentou"].Notes);
            jbovlaste["cidjrbentou"] = jbovlaste["cidja"];
            System.Console.WriteLine(jbovlaste["cidja"].Notes);
            System.Console.WriteLine(jbovlaste["cidjrbentou"].Notes);
        }
    }
}
