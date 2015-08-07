using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RabbitMetaQueue.Infrastructure
{
    public class TextTable
    {
        private readonly List<KeyValuePair<string, string>> entries = new List<KeyValuePair<string, string>>();
        

        public void Add(string key, string value)
        {
            entries.Add(new KeyValuePair<string, string>(key, value));
        }


        public override string ToString()
        {
            return ToString(2);
        }


        public string ToString(int indent)
        {
            var keyLength = entries.Max(p => p.Key.Length) + 1;
            var prefix = new string(' ', indent);
            
            var result = new StringBuilder();

            foreach (var pair in entries)
            {
                result.Append(prefix)
                      .Append(pair.Key.PadRight(keyLength))
                      .AppendLine(pair.Value);
            }

            return result.ToString();
        }
    }
}
