using System;
using System.IO;
using System.Threading.Tasks;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class FileFinder
    {
        public event woanware.Events.DefaultEvent CompleteEvent;
        public event woanware.Events.MessageEvent UpdateEvent;

        /// <summary>
        /// 
        /// </summary>
        public void Start(string path)
        {
            Task task = Task.Factory.StartNew(() =>
            {
                try
                {
                    DirSearch(path);
                }
                catch (Exception){}
                finally
                {
                    this.OnComplete();
                }

            }, TaskCreationOptions.LongRunning);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        private void DirSearch(string directory)
        {
            try
            {
                // Process the list of files found in the directory. 
                string[] fileEntries = System.IO.Directory.GetFiles(directory);
                foreach (string fileName in fileEntries)
                {
                    FileInfo fileInfo = new FileInfo(fileName);

                    this.OnUpdate(fileInfo.FullName);
                }

                // Recurse into subdirectories of this directory.
                string[] subdirEntries = System.IO.Directory.GetDirectories(directory);
                foreach (string subdir in subdirEntries)
                {
                    DirSearch(subdir);
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void OnUpdate(string path)
        {
            var handler = UpdateEvent;
            if (handler != null)
            {
                handler(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            var handler = CompleteEvent;
            if (handler != null)
            {
                handler();
            }
        }
    }
}
