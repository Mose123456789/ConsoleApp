using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interfaces
{

    public class FileService
    {
        private readonly string _filePath = @"c:\Projects\contacts.json";

        // Den sparar innehållet inom filen och sedan kopplar den till sitt filepath så den kan hämtas upp igen.
        public bool SaveConentToFile(string content)
        {
            try
            {
                using (var sw = new StreamWriter(_filePath))
                {
                    sw.WriteLine(content);
                }

                return true;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }

        // och den här hämtar upp ineehåll som har sparats i filen
        public string GetContentFromFile()
        {
            try
            {
                if (File.Exists(_filePath))
                {
                    using var sr = new StreamReader(_filePath);
                    return sr.ReadToEnd();
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }
    }
}