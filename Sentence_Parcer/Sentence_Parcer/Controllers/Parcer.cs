using Sentence_Parcer.Entities;
using Sentence_Parcer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Sentence_Parcer.Controllers
{
    public class Parcer
    {
        private List<Sentence> _sentenceCollection;
        private Sentence _sentence;
        private List<Word> _words;
        private Word _word;
        private XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Sentence>));
        

        public Parcer()
        {
            _sentenceCollection = new List<Sentence>();
        }
        public void BindToModel(string input)
        {
            input = input.Trim();
            input = input.Replace(",", " ");
            string splitter = "!.?";
            string[] outdots = input.Split(splitter.ToCharArray());
            outdots = outdots.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            foreach (var item in outdots)
            {

                _words = new List<Word>();
                string[] outspaces = item.Split(' ').Select(a => a.Trim()).ToArray(); ;
                outspaces = outspaces.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                Array.Sort(outspaces, StringComparer.InvariantCulture);
                foreach (var word in outspaces)
                {
                    _word = new Word();
                    _word.Words = word;
                    _words.Add(_word);
                    _sentence = new Sentence();
                    _sentence.Words = _words;
                }
                _sentenceCollection.Add(_sentence);
            }
        }
        public void WriteToXmlDoc()
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = pathDesktop + "\\myXML.xml";
            using (StreamWriter stream = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(stream, _sentenceCollection);
                stream.Close();
            }
            // var xml = ToXML(_sentenceCollection);
        }

        public string ToXML()
        {
            using (StringWriter stringWriter = new StringWriter(new System.Text.StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Sentence>));
                xmlSerializer.Serialize(stringWriter, _sentenceCollection);
                return stringWriter.ToString();
            }
        }
        public void WriteToCSV()
        {
            string sentence;
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = pathDesktop + "\\mycsvfile.csv";
            string delimter = ",";
            using (var writer = new StreamWriter(filePath))
            {
                writer.WriteLine(GetHeaders());
                int y = 1;
                foreach (var item in _sentenceCollection)
                {
                    var sb = new StringBuilder();
                    foreach (var word in item.Words)
                    {
                        sb.Append(delimter);
                        sb.Append(word.Words);
                    }
                    string str = sb.ToString();
                    sentence = "Sentence " + y + str;
                    writer.WriteLine(string.Join(delimter, sentence));
                    y++;
                }
            }
        }

        public int ColumnCount()
        {
            int a = 0;
            int b;
            foreach (var item in _sentenceCollection)
            {
                b = item.Words.Count;
                if (b > a)
                {
                    a = b;
                }
            }
            return a;
        }

        public string GetHeaders()
        {
            var sb = new StringBuilder();
            int columns = ColumnCount();
            for (int i = 1; i <= columns; i++)
            {
                string word = ", " + "Word" + i;
                sb.Append(word);
            }
            string str = sb.ToString();
            return str;
        }

    }
}