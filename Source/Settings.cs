using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System;

namespace woanware
{
    /// <summary>
    /// 
    /// </summary>
    public class Settings
    {
        public bool AlwaysOnTop { get; set; }
        public bool ColourSevereItems { get; set; }
        public bool RemoveNewLinesOnExport { get; set; }
        public Point FormLocation { get; set; }
        public Size FormSize { get; set; }
        public FormWindowState FormState { get; set; }
        private const string FILENAME = "Settings.xml";
        public int NumResultsPerPage = 5000;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Load()
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");

                if (File.Exists(System.IO.Path.Combine(path, FILENAME)) == false)
                {
                    return string.Empty;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                if (File.Exists(System.IO.Path.Combine(path, FILENAME)) == false)
                {
                    return "Cannot locate configuration file: " + System.IO.Path.Combine(path, FILENAME);
                }

                FileInfo info = new FileInfo(System.IO.Path.Combine(path, FILENAME));
                using (FileStream stream = info.OpenRead())
                {
                    Settings settings = (Settings)serializer.Deserialize(stream);

                    FormLocation = settings.FormLocation;
                    FormSize = settings.FormSize;
                    FormState = settings.FormState;
                    FormState = settings.FormState;
                    ColourSevereItems = settings.ColourSevereItems;
                    RemoveNewLinesOnExport = settings.RemoveNewLinesOnExport;
                    AlwaysOnTop = settings.AlwaysOnTop;
                    NumResultsPerPage = settings.NumResultsPerPage;

                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string Save()
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                using (StreamWriter writer = new StreamWriter(System.IO.Path.Combine(path, FILENAME), false))
                {
                    serializer.Serialize((TextWriter)writer, this);
                    return string.Empty;
                }
            }
            catch (FileNotFoundException fileNotFoundEx)
            {
                return fileNotFoundEx.Message;
            }
            catch (UnauthorizedAccessException unauthAccessEx)
            {
                return unauthAccessEx.Message;
            }
            catch (IOException ioEx)
            {
                return ioEx.Message;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool FileExists
        {
            get
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"woanware\" + Application.ProductName + @"\");
                return File.Exists(System.IO.Path.Combine(path, FILENAME));
            }
        }
    }
}
