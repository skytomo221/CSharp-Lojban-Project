using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace Lojban.Jbovlaste
{
    public struct User : IEnumerable<string>
    {
        public string UserName { get; set; }
        public string RealName { get; set; }

        public IEnumerator<string> GetEnumerator()
        {
            yield return UserName;
            yield return RealName;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return UserName;
            yield return RealName;
        }

        public void Add(object obj)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Valsi
    {
        public bool UnOfficial { get; set; }
        public string Word { get; set; }
        public string Type { get; set; }
        public string Selmaho { get; set; }
        public List<string> Rafsi { get; set; }
        public User User { get; set; }
        public string Definition { get; set; }
        public int DefinitionId { get; set; }
        public string Notes { get; set; }
        public List<string> GlossWord { get; set; }
    }

    public class Jbovlaste : List<Valsi>
    {
        public Jbovlaste() { }
        public Jbovlaste(string filename)
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
                var valsi = new Valsi();
                valsi.UnOfficial = node.GetAttribute("unofficial") == bool.TrueString;
                valsi.Word = node.GetAttribute("word");
                valsi.Type = node.GetAttribute("type");
                if (node.SelectSingleNode("selmaho") != null)
                {
                    valsi.Selmaho = node.SelectSingleNode("selmaho").InnerText;
                }
                valsi.Rafsi = new List<string>();
                foreach (XmlElement rafsi in node.SelectNodes("rafsi"))
                {
                    valsi.Rafsi.Add(rafsi.InnerText);
                }
                var user = (XmlElement)node.SelectSingleNode("user");
                valsi.User = new User
                {
                    UserName = user.SelectSingleNode("username").InnerText,
                    RealName = user.SelectSingleNode("realname").InnerText,
                };
                valsi.Definition = node.SelectSingleNode("definition").InnerText;
                valsi.DefinitionId = int.Parse(node.SelectSingleNode("definitionid").InnerText);
                if (node.SelectSingleNode("notes") != null)
                {
                    valsi.Notes = node.SelectSingleNode("notes").InnerText;
                }
                valsi.GlossWord = new List<string>();
                foreach (XmlElement glossword in node.SelectNodes("glossword"))
                {
                    valsi.GlossWord.Add(glossword.InnerText);
                }
                Add(valsi);
            }
        }

        public Valsi this[string word]
        {
            set
            {
                var result = Find(valsi => valsi.Word == word);
                result = value;
            }
            get
            {
                return Find(valsi => valsi.Word == word);
            }
        }
    }
}
