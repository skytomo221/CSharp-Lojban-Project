using Newtonsoft.Json;

namespace Lojban
{
    public class LojbanParser
    {
        public string Json { get => JsonConvert.SerializeObject(Result, Formatting.Indented); }
        public string Text { get; set; }
        public object Result { get; private set; }
        public LojbanParser() { }
        public LojbanParser(string text) { Text = text; }
        public object Parse()
        {
            var parser = new Camxes();
            return Result = parser.Parse(Text);
        }
        public object Parse(string text)
        {
            var parser = new Camxes();
            return Result = parser.Parse(Text = text);
        }
    }
}
