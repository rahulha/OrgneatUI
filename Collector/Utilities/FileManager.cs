using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace Collector.Utilities
{
    public class FileManager
    {

        private String dir;
        private string fname;
        public String FullFilePath;
        //public FileStream fs;

        private StreamWriter sw;

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

                if (this.sw != null)
                    this.sw.Dispose();

                sw = new StreamWriter(new FileStream(FullFilePath, FileMode.Append), Encoding.UTF8);
            }
        }

        public void AppendTextToFile(String Text)
        {
            try
            {
                //byte[] b = Encoding.ASCII.GetBytes(Text);

                //sw.Write( b, 0, b.Count());
                sw.Write(Text);
                sw.Flush();
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

                //fs.WriteAsync(b, 0, b.Count());
                sw.WriteAsync(Text);
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

                    //byte[] b = Encoding.ASCII.GetBytes(Text);

                    //fs.Write(b, 0, b.Count());

                    sw.Write(Text);
                    sw.Flush();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public async void CloseAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    //sw.Flush();

                    sw.Close();
                }
                catch (Exception ex)
                {
                    Thread.Sleep(1000);
                }
            });


        }

        public void Dispose()
        {
            try
            {
                dir = null;
                fname = null;
                FullFilePath = null;
                sw.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void Reopen()
        {
            try
            {
                if (sw != null)
                {
                    sw.Close();
                    sw.Dispose();
                }

                //fs = new FileStream(FullFilePath, FileMode.Append);
                sw = new StreamWriter(new FileStream(FullFilePath, FileMode.Append), Encoding.UTF8);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
