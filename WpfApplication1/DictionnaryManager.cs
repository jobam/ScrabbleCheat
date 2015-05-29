using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace WpfApplication1
{
    internal class DictionnaryManager
    {
        public int MaxLetters { get; set; }
        public String DestPath { get; set; }

        public Boolean DictionnaryToFiles(String path)
        {
            try
            {
                var lines = File.ReadAllLines(path, Encoding.UTF8);
                var wordList = lines.ToList();

                for (var i = 3; i <= MaxLetters; i++)
                {
                    var outPath = @"C:\Program Files (x86)\ScrabbleCheat\dictionnary\fichier" + i;
                    File.WriteAllLines(outPath,
                        wordList.FindAll(x => x.Length == i).ToArray<String>(), Encoding.UTF8);
                }

                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }

        public List<String> GetWordsList(int nb)
        {
            try
            {
                var path = @".\dictionnary\fichier" + nb;
                var lines = File.ReadAllLines(path, Encoding.UTF8);
                var wordList = lines.ToList();
                return wordList;
            }
            catch (Exception)
            {
                return new List<String>();
            }
        }
    }
}