using System;
using System.Linq;
using System.Text;
using System.IO;

namespace Collector.Utilities
{
    public class FileManager
    {

        private String dir;
        private string fname;
        public String FullFilePath;
        public FileStream fs;

        private bool NewFile = false;

        public FileManager()
        {

        }

        public string Directory
        {
            get => dir; set
            {
                dir = value;
                System.IO.Directory.CreateDirectory(dir);
            }
        }

        public string FileName
        {
            get => fname;

            set
            {
                fname = value;

                FullFilePath = Path.Combine(dir, value + ".csv");

                NewFile = !File.Exists(FullFilePath);

                if (fs != null)
                    fs.Dispose();

                fs = new FileStream(FullFilePath, FileMode.Append);
            }
        }

        public void AppendTextToFile(String Text)
        {
            try
            {
                byte[] b = Encoding.ASCII.GetBytes(Text);

                fs.Write(b, 0, b.Count());

                fs.Flush();
            }
            catch (Exception ex)
            {

            }
        }

        public void AppendTextToFileAsync(String Text)
        {
            try
            {
                byte[] b = Encoding.ASCII.GetBytes(Text);

                fs.WriteAsync(b, 0, b.Count());
            }
            catch (Exception ex)
            {

            }
        }

        public void WriteHeader(String Text)
        {
            if (NewFile)
            {
                try
                {
                    if (!Text.EndsWith(Environment.NewLine))
                        Text += Environment.NewLine;

                    byte[] b = Encoding.ASCII.GetBytes(Text);

                    fs.Write(b, 0, b.Count());

                    fs.Flush();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void Close()
        {
            try
            {
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            try
            {
                dir = null;
                fname = null;
                FullFilePath = null;
                fs.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void Reopen()
        {
            try
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                fs = new FileStream(FullFilePath, FileMode.Append);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
