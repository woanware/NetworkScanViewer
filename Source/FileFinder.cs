using System;
using System.IO;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class FileFinder
    {
        private delegate void StartDelegate(string path);

        public delegate void UpdateEventHandler(string path);
        public event EventHandler CompleteEvent;
        public event UpdateEventHandler UpdateEvent;

        /// <summary>
        /// 
        /// </summary>
        public void Start(string path)
        {
            StartDelegate startDelegate = new StartDelegate(DoStart);
            startDelegate.BeginInvoke(path, null, null);   
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        private void DoStart(string path)
        {
            DirSearch(path);

            this.OnComplete();
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
            if (UpdateEvent != null)
            {
                UpdateEvent(path);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void OnComplete()
        {
            if (CompleteEvent != null)
            {
                CompleteEvent(this, new EventArgs());
            }
        }
    }
}
