using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace ViScheduler
{
    public interface IPhraseLoader
    {
        string GetPhrase();
        Dictionary<string,string> GetAllPhrases();
        void DeletePhrase(string id);
        void AddPhrase(string phrase);
    }

    public class PhraseLoader : IPhraseLoader
    {
        private readonly List<XElement> _elements;
        private readonly string _path ;
        public PhraseLoader(string path)
        {
            _path = path;
            if (!File.Exists(path))
            {
                CreateFile(path);
            }
            var doc = XDocument.Load(path);
            var root = doc.Root;
            if (root == null) return;
            _elements = root.Elements().ToList();
        }

        private static void CreateFile(string path)
        {
            var doc = new XDocument(new XElement("phrases"));
            doc.Save(path);
        }

        public string GetPhrase()
        {
            var cnt = _elements.Count();
            if (cnt == 0) return "";
            var rand = new Random();
            var ind = rand.Next(cnt);
            var elem = _elements[ind];
            return elem.Value;
        }

        public Dictionary<string, string> GetAllPhrases()
        {
            return _elements.OrderBy(r => r.Value)
                .ToDictionary(element => element.Attribute("id").ToString(), element => element.Value);
        }

        public void DeletePhrase(string id)
        {
            _elements.RemoveAll(r=>r.Attribute("id").ToString().Equals(id));
            SaveXml();
        }

        public void AddPhrase(string phrase)
        {
            var id = Guid.NewGuid();
            var element = new XElement("phrase", new XAttribute("id", id.ToString("D"))) {Value = phrase};
            _elements.Add(element);
            SaveXml();
        }

        private void SaveXml()
        {
            var doc = new XDocument(new XElement("phrases", _elements));
            doc.Save(_path);
        }
    }
}
