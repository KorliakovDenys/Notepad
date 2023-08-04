using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Notepad
{
    internal class FileManager 
    { 
        public bool SaveTextToFile(string filePath, string text)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, text);
                    return true;
                }
                
            }
            catch
            {

            }
            return false;
        }

        public bool SaveTextToFileAs(string filePath, string fileExtension, string text)
        {
            try
            {
                const string txt = ".txt";
                if (filePath.EndsWith(".txt"))
                {
                    filePath = filePath.Substring(0, filePath.Length - txt.Length);
                }

                filePath = filePath + fileExtension;
                File.WriteAllText(filePath, text);
                return true;
            }
            catch 
            {

            }
            return false;
        }
        public bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);   
        }

       
    }
}
