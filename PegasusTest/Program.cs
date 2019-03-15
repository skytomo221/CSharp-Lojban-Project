using System;
using System.Collections.Generic;
using System.Linq;

namespace PegasusTest
{
    public class Node
    {
        public string Label { get; set; }
        public object Child { get; set; }

        public Node(string label, IList<Node> child) { Label = label; Child = child; }
        public Node(string label, Node child) { Label = label; Child = child.Child; }
        public Node(string label, string child) { Label = label; Child = child; }
        public override string ToString()
        {
            if (Child is string)
            {
                var child = Child as string;
                return "{\n\t\"" + Label + "\"\n\t\"" + child + "\"\n}";
            }
            else if (Child is IList<Node>)
            {
                var child = Child as IList<Node>;
                return "{\n\t\"" + Label + "\"\n\t" + string.Join("\n", child.Select(x => x.ToString())).Replace("\n", "\n\t") + "\n}";
            }
            return base.ToString();
        }
        public static IList<Node> Union(params object[] node)
        {
            var list = new List<Node>();
            foreach (var item in node)
            {
                if (item is Node)
                {
                    list.Add(item as Node);
                }
                else if (item is IList<Node>)
                {
                    list = list.Union(item as IList<Node>).ToList();
                }
            }
            return list;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //IList<Node> a = new List<Node>() { new Node("a", "1") };
            //Node b = new Node("b", "2");
            //Node c = new Node("c", Node.Union(a, b));
            //Console.WriteLine(c.ToString());

            var parser = new LojbanParser();
            var result = parser.Parse(",a");
            Console.WriteLine(result.ToString());
        }
    }
}
