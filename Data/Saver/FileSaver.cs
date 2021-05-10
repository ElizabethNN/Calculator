using System.Collections.Generic;
using System.IO;

namespace Calculator.Saver
{
    class FileSaver : ISaver
    {
        string path;
        public FileSaver(string path)
        {
            this.path = path;
        }

        public void saveData(Dictionary<string, string> data)
        {
            var lines = new List<string>();
            foreach (KeyValuePair<string, string> i in data)
            {
                lines.Add(i.Key + " " + i.Value);
            }
            File.WriteAllLines(path, lines.ToArray());
        }

        public Dictionary<string, string> loadData()
        {
            var result = new Dictionary<string, string>();
            string[] lines = File.ReadAllLines(path);
            foreach (string i in lines)
            {
                string[] temp = i.Split(' ');
                result.Add(temp[0], temp[1]);
            }
            return result;
        }
    }
}
