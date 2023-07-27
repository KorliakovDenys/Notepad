using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace Notepad
{
    internal class SaveToFile
    {
        public bool SaveTextToFile(string filePath, string text)
        {
            try
            {
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                return false;
            }

        }

        public bool SaveTextToFileAs(string filePath, string fileExtension, string text)
        {
            try
            {
                if (filePath.EndsWith(".txt"))
                {
                    filePath = filePath = filePath.Substring(0, filePath.Length - 4);
                }

                filePath = filePath + fileExtension;
                File.WriteAllText(filePath, text);
                return true;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка при сохранении файла: " + ex.Message);
                return false;
            }
        }
        public bool IsFileExist(string filePath)
        {
            return File.Exists(filePath);   
        }

       
    }
}
