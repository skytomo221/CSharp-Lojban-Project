using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

namespace Local_Jbovlaste
{
    public struct User
    {
        string UserName { get; set; }
        string RealName { get; set; }
    }

    public class Valsi
    {
        bool UnOfficial { get; set; }
        User User { get; set; }
        string Definition { get; set; }
        int DefinitionId { get; set; }
        string Notes { get; set; }
    }

    public class Gismu : Valsi
    {
        List<string> Rafsi { get; set; }
    }

    public class Cmavo : Valsi
    {
        string Selmaho { get; set; }
    }

    public class JbovlasteDictionary : SortedDictionary<string, Valsi>
    {
        public JbovlasteDictionary() { }
        public JbovlasteDictionary(string filename)
        {
            Load(filename);
        }

        public void Load(string filename)
        {
            var doc = new XmlDocument();
            doc.Load(filename);
            var dictionary = doc.SelectNodes("dictionary/direction/valsi");
            foreach (XmlElement node in dictionary)
            {
                var type = node.Attributes["type"].InnerText;
                if (type == "gismu") ;
                else if (Regex.IsMatch(type, "(cmavo)|(cmavo-compound)|(experimental cmavo)|(bu-letteral)")) ;
                else if (Regex.IsMatch(type, "(fu'ivla)|(lujvo)")) ;
                if (node.Attributes["word"].InnerText)
                        Console.WriteLine(node.Attributes["word"].InnerText);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var doc = new XmlDocument();
            doc.Load("banjupunu.xml");
            var dictionary = doc.SelectNodes("dictionary/direction/valsi");
            foreach (XmlElement node in dictionary)
            {
                Console.WriteLine(node.Attributes["word"].InnerText);
            }
        }
    }
}
