using System;
using System.IO;

namespace Log_Lite.LogWriter
{
    public class FileLogWriter : ILogWriter
    {
        private string fileName;
        private string directoryPath;
        private string filePath;


        public FileLogWriter() : this("Log.txt", AppDomain.CurrentDomain.BaseDirectory + "/") { }

        public FileLogWriter(string fileName, string directoryPath)
        {
            this.fileName = fileName;
            this.directoryPath = directoryPath;
            filePath = directoryPath + fileName;
        }


        public void Write(string log)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(log);
                }
                // TODO: check if backup is needed
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
