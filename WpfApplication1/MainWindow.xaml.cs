using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace WpfApplication1
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dictionnaryManager = new DictionnaryManager
            {
                MaxLetters = 12
            };
            var letters = wordBox.Text;
            var findedWords = new List<String>();
            var outList = new List<String>();
            try
            {
                for (var i = 0; i < letters.Length - 2; i++)
                {
                    var listWords = dictionnaryManager.GetWordsList(letters.Length - i);
                    findedWords = (listWords.Where(mot => !mot.Except(letters).Any())).ToList();
                    if (findedWords.Count != 0)
                    {
                        foreach (var word in findedWords)
                        {
                            if (FormatValid(word, letters))
                                outList.Add(word);
                        }
                    }
                    if (outList.Count != 0)
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception looking for words");
            }
            resultBox.ItemsSource = outList;
        }

        // a faire : verifier le nombre d'occurence de la lettre et checker le nb d'occurence dans le mot.
        private bool FormatValid(string format, string allowableLetters)
        {
            foreach (var c in format.ToCharArray())
            {
                if (!checkOccurences(c, allowableLetters, format))
                    return false;
            }

            return true;
        }

        private bool checkOccurences(char c, string source, string target)
        {
            var occurencesSource = 0;
            var occurencesTarget = 0;

            foreach (var caract in source.ToCharArray())
            {
                if (caract == c)
                    occurencesSource++;
            }
            foreach (var caract in target.ToCharArray())
            {
                if (caract == c)
                    occurencesTarget++;
                if (occurencesTarget > occurencesSource)
                    return false;
            }
            if (occurencesTarget > occurencesSource)
                return false;
            return true;
        }

        //====================================================

        private void Button_Dictionnaire(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            var dlg = new OpenFileDialog();

            // Set filter for file extension and default file extension
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text documents (.txt)|*.txt";
            // Display OpenFileDialog by calling ShowDialog method
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox
            if (result == true)
            {
                // Open document
                var filename = dlg.FileName;
                DicionnaireTextBox.Text = filename;
                var generator = new DictionnaryManager
                {
                    MaxLetters = 12
                };
                generator.DictionnaryToFiles(filename);
            }
        }
    }
}